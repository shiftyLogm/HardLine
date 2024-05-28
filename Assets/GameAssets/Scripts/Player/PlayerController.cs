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
    public RangeAttack rangeAttack;
    public ArcherAttack archerAttack;
    public MageAttack mageAttack;

    // Variaveis
    private Animator _animator;
    private Rigidbody2D _rb;
    private EntityStats _entityStats;
    private bool _isAttacking = false;
    private bool _isDashing = false;
    private bool _isTrueOrFalseAction = false;
    private bool _canChangeDirection;

    // Fogueira
    public GameObject[] fogueiras;
    

    // Criando uma variavel para saber a direçao para onde o jogador quer ir
    Vector2 mov;
    Vector2 oldMov;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _entityStats = GetComponent<EntityStats>();

        // States Setup
        idleState.Setup(_animator, _rb);
        runState.Setup(_animator, _rb);
        attackState.Setup(_animator, _rb, _entityStats);
        dashState.Setup(_animator, _rb, _entityStats);

        // Attack Types Setup
        meleeAttack.Setup(_animator, _rb, _entityStats);
        rangeAttack.Setup(_animator, _rb, _entityStats);

        // State inicial
        state = idleState;
        state.direction = "right";
        attackState.direction = "right";

        // Fogueira
        fogueiras = GameObject.FindGameObjectsWithTag("Fogueira");
    }

    void FixedUpdate()
    {
        mov = UserInput.Instance.MoveInput;
        if(!_isTrueOrFalseAction) oldMov = mov;

        // -=-=-=-=-=-=-=


        

        // Fazendo o jogador andar
        if(!_isTrueOrFalseAction) _rb.velocity = mov * _entityStats.moveSpeed * Time.fixedDeltaTime;
    }

    void Update()
    {
        #region Input Actions

        // Input Actions
        if(UserInput.Instance.AttackInput && !_isAttacking)
        {
            _isTrueOrFalseAction = true;
            _isAttacking = true;
            SelectAttackTypeAndAttack();
        }

        if(UserInput.Instance.DashInput)
        {
            _isTrueOrFalseAction = true;
            _isDashing = true;
        }

        if(UserInput.Instance.InteractionInput)
        {
            foreach(GameObject fogueira in fogueiras)
            {
                if(fogueira.GetComponent<Fogueira>().canUseFireplace) 
                {
                    fogueira.GetComponent<Fogueira>().Interact();
                }
            }
        }

        #endregion

        if(state == dashState)
        {
            dashState.mov = mov; // Setando qual direçao devo ir ao dar dash
        }
        
        // Seleciona a animaçao da direçao correspondente com a velocidade
        if (_rb.velocity != new Vector2(0, 0)) DirectionFacing();

        SelectState();
        state.Do();
    }

    #region Direction

    private void DirectionFacing()
    {
        Dictionary<string, bool> dictActions = new()
        {
            {"up", _rb.velocity.y > 0},
            {"down", _rb.velocity.y < 0},
            {"right", _rb.velocity.x > 0},
            {"left", _rb.velocity.x < 0}
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
            {attackState, _isAttacking && (state == idleState || state == runState)},
            {dashState, _isDashing && (state == runState)}
        };


        // _isTrueOrFalseActions
        if(_isTrueOrFalseAction)
        {
            state = Helper.FindKeyState(dict, true);
            if(state == null) state = oldState;

            if(state == idleState) _isTrueOrFalseAction = false;
        }

        // Movimento
        if(!_isTrueOrFalseAction)
        {
            if (_rb.velocity.x == 0 && _rb.velocity.y == 0)
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
            _isAttacking = false;
            _isDashing = false;
            _isTrueOrFalseAction = false;
        }
    }

    #endregion

    #region Attack Types
    
    private void SelectAttackTypeAndAttack()
    {
        Dictionary<string, AttackState> attackTypeDict = new()
        {
            {"Warrior", meleeAttack},
            {"Archer", archerAttack},
            {"Mage", mageAttack}
        };

        
        Debug.Log($"classe {attackTypeDict[PlayerClassesController.Instance.idxClass]}");
        
        attackTypeDict[PlayerClassesController.Instance.idxClass].attackPoint = attackState.SelectAttackPoint();
        attackTypeDict[PlayerClassesController.Instance.idxClass].Attack();

    }

    #endregion
}