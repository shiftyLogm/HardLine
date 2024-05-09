using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    // States
    public RunState runState;
    public IdleState idleState;
    public AttackState attackState;
    public PlayerDashState dashState;

    State state;

    // Attack Types
    public MeleeAttack meleeAttack;

    // Variaveis
    private Animator animator;
    private PlayerControls playerControls;
    private Rigidbody2D rb;
    private EntityStats entityStats;
    private bool isAttacking = false;
    private bool isDashing = false;
    private bool isTrueOrFalseAction = false;
    private bool canChangeDirection;
    PlayerClassesController playerClassesController;

    // Fogueira
    public Fogueira fogueira;
    

    // Criando uma variavel para saber a direçao para onde o jogador quer ir
    Vector2 mov;
    Vector2 oldMov;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        entityStats = GetComponent<EntityStats>();
        playerClassesController = GetComponent<PlayerClassesController>();

        // States Setup
        idleState.Setup(animator, rb);
        runState.Setup(animator, rb);
        attackState.Setup(animator, rb, entityStats);
        dashState.Setup(animator, rb, entityStats);

        // Attack Types Setup
        meleeAttack.Setup(animator, rb, entityStats);

        // State inicial
        state = idleState;
        state.direction = "right";
        attackState.direction = "right";

        // Referenciando o script PlayerControls a variavel criada
        playerControls = new PlayerControls();   
    }

    void FixedUpdate()
    {
        if(state == dashState)
        {
            dashState.mov = oldMov; // Setando qual direçao devo ir ao dar dash
            return;
        }

        // Fazendo o jogador andar
        if(!isTrueOrFalseAction) rb.velocity = mov * entityStats.moveSpeed * Time.fixedDeltaTime;
    }

    void Update()
    {
        // Seleciona a animaçao da direçao correspondente com a velocidade
        if (rb.velocity != new Vector2(0, 0) && canChangeDirection) DirectionFacing();


        SelectState();
        state.Do();

        if(!isTrueOrFalseAction) oldMov = mov;
    }

    #region Direction

    private void DirectionFacing()
    {
        Dictionary<string, bool> dictActions = new()
        {
            {"up", rb.velocity.y > 0},
            {"down", rb.velocity.y < 0},
            {"right", rb.velocity.x > 0},
            {"left", rb.velocity.x < 0}
        };

        var key = Helper.FindKey(dictActions, true);
        state.direction = key;
        idleState.direction = state.direction;
        attackState.direction = state.direction;
    }   

    #endregion

    #region State

    private void SelectState()
    {
        State oldState = state;
        
        Dictionary<State, bool> dict = new()
        {
            {attackState, isAttacking && (state == idleState || state == runState)},
            {dashState, isDashing && (state == runState)}
        };


        // isTrueOrFalseActions
        if(isTrueOrFalseAction)
        {
            state = Helper.FindKeyState(dict, true);
            if(state == null) state = oldState;

            if(state == idleState) isTrueOrFalseAction = false;
        }

        // Movimento
        if(!isTrueOrFalseAction)
        {
            if (rb.velocity.x == 0 && rb.velocity.y == 0)
            {
                state = idleState;
            }

            else state = runState;
        }
        
        // Caso o oldState seja diferente state atual troca de estado
        if (state != oldState)
        {
            oldState.Exit();
            state.Initialize();
            state.Enter();
        }
        if(state.isComplete)
        {
            isAttacking = false;
            isDashing = false;
            isTrueOrFalseAction = false;
        }


    }

    #endregion

    #region Input System

    public void OnMove(InputAction.CallbackContext context)
    {
        // Colocando o valor Vector2 a variavel de direçao criada
        if(context.performed || context.started) 
        {
            mov = context.ReadValue<Vector2>();
            canChangeDirection = true;
        }
        else if (context.canceled)
        {
            mov = new Vector2(0,0);
            canChangeDirection = false;  
        } 

        
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && isAttacking == false)
        {
            isTrueOrFalseAction = true;
            isAttacking = true;
            SelectAttackTypeAndAttack();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isTrueOrFalseAction = true;
            isDashing = true;
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(fogueira.canUseFireplace) 
            {
                fogueira.Spawn();
                fogueira.RestoreHP();
            }
        }
    }

    #endregion;

    #region Attack Types
    
    private void SelectAttackTypeAndAttack()
    {
        Dictionary<string, AttackState> attackTypeDict = new()
        {
            {"Warrior", meleeAttack}
        };

        attackTypeDict[playerClassesController.idxClass].attackPoint = attackState.SelectAttackPoint();
        attackTypeDict[playerClassesController.idxClass].Attack();

    }

    #endregion
}