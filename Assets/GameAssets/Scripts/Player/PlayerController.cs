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

    State state;


    // Variaveis
    public Animator animator;

    private PlayerControls playerControls;
    
    private Rigidbody2D rb;

    // Criando uma variavel para saber a direçao para onde o jogador quer ir
    Vector2 mov;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // States Setup
        idleState.Setup(animator, rb);
        runState.Setup(animator, rb);

        // State inicial
        state = idleState;
        state.Direction("rigth");

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
        if(rb.velocity.x != 0 || rb.velocity.y != 0) DirectionFacing();
        

        SelectState();
        state.Do();
    }

    void DirectionFacing()
    {
        // Verifica a velocidade no eixo x caso a do eixo y seja 0
        if(rb.velocity.y == 0)
        {
            if(rb.velocity.x > 0) state.Direction("rigth");
            else if(rb.velocity.x < 0) state.Direction("left");
        }
        // Verifica a velocidade do eixo y caso a do eixo x seja 0
        else if(rb.velocity.x == 0)
        {
            if(rb.velocity.y > 0) state.Direction("up");
            else if(rb.velocity.y < 0) state.Direction("down");
        }
    }

    #region State

    void SelectState()
    {
        State oldState = state;

        if(rb.velocity.x == 0 && rb.velocity.y == 0) 
        {
            state = idleState;
        }
        else 
        {
            state = runState;
        }

        // Caso o oldState seja diferente state atual ou caso o state atual foi concluido, troca de estado ou continua no mesmo
        if(state != oldState || state.isComplete)
        {
            oldState.Exit();
            state.Initialize();
            state.Enter();
        }
        
    }

    #endregion
    
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
        }
    }

    #endregion;
 
}
