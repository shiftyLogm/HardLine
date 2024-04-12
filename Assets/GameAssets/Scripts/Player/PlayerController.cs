using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // States
    public PlayerRunState runState;
    public PlayerIdleState idleState;
    public PlayerAttackState attackState;

    State state;


    // Variaveis
    public Animator animator;
    private PlayerControls playerControls;
    private Rigidbody2D rb;
    bool isAttacking = false;

    // Criando uma variavel para saber a direçao para onde o jogador quer ir
    Vector2 mov;
    Vector2 oldMov;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // States Setup
        idleState.Setup(animator, rb);
        runState.Setup(animator, rb);
        attackState.Setup(animator, rb);

        // State inicial
        state = idleState;
        attackState.direction = "rigth";

        // Referenciando o script PlayerControls a variavel criada
        playerControls = new PlayerControls();
    }

    void FixedUpdate()
    {
        // Fazendo o jogador andar
        rb.velocity = mov * GetComponent<EntityStats>().moveSpeed * Time.fixedDeltaTime;
    }

    void Update()
    {
        // Seleciona a animaçao da direçao correspondente com a velocidade
        if (rb.velocity != new Vector2(0, 0)) DirectionFacing();


        SelectState();
        state.Do();

        if(isAttacking) mov = new Vector2(0,0);
        else mov = oldMov;
    }

    void DirectionFacing()
    {

        // Verifica as velocidades no eixo y
        if (rb.velocity.y > 0) state.direction = "up";
        if (rb.velocity.y < 0) state.direction = "down";

        // Verifica as velocidades no eixo x
        if (rb.velocity.x > 0) state.direction = "rigth";
        if (rb.velocity.x < 0) state.direction = "left";

        idleState.direction = state.direction;
        attackState.direction = state.direction;

    }

    #region State

    void SelectState()
    {
        State oldState = state;
        
        // Movimento
        if(isAttacking)
        {
            state = attackState;
        }
        else
        {
            if (rb.velocity.x == 0 && rb.velocity.y == 0)
            {
                state = idleState;
            }
            else
            {
                state = runState;
            }
        }

        // Caso o oldState seja diferente state atual ou caso o state atual foi concluido, troca de estado ou continua no mesmo
        if (state != oldState)
        {
            oldState.Exit();
            state.Initialize();
            state.Enter();
        }
        if(state.isComplete)
        {
            isAttacking = false;
        }


    }

    #endregion

    #region Input System

    public void OnMove(InputAction.CallbackContext context)
    {
        // Colocando o valor Vector2 a variavel de direçao criada
        mov = context.ReadValue<Vector2>();
        oldMov = mov;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("atacou");
            isAttacking = true;
        }
    }

    #endregion;

}
