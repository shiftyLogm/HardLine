using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{    
    public Animator animator;

    private PlayerControls playerControls;
    
    private Rigidbody2D rb;

    // Criando uma variavel para saber a direçao para onde o jogador quer ir
    Vector2 mov;

    // MeleeBaseState
    MeleeBaseState meleeBaseState;

    // StateMachine
    private StateMachine stateMachine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Referenciando o script PlayerControls a variavel criada
        playerControls = new PlayerControls();

        // StateMachine
        stateMachine = GetComponent<StateMachine>();

        // MeleeStateMachine
        meleeBaseState = new MeleeBaseState();
    }

    void FixedUpdate()
    {
        // Fazendo o jogador andar
        rb.velocity = mov * GetComponent<EntityStats>().moveSpeed * Time.fixedDeltaTime;
    }

    void Update()
    {

    }

    
    #region Input System

    public void OnMove(InputAction.CallbackContext context)
    {
        // Colocando o valor Vector2 a variavel de direçao criada
        mov = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {   
            Debug.Log("atacou");

            meleeBaseState.OnUpdate();

            if(stateMachine.CurrentState.GetType() == typeof(IdleCombatState))
            {
                stateMachine.SetNextState(new GroundEntryState());
            }
        }
    }

    #endregion;
 
}
