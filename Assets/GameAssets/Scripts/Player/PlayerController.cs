using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerControls playerControls;
    
    private Rigidbody2D rb;

    // Criando uma variavel para saber a direçao para onde o jogador quer ir
    Vector2 mov;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Referenciando o script PlayerControls a variavel criada
        playerControls = new PlayerControls();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Colocando o valor Vector2 a variavel de direçao criada
        mov = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {   
            GetComponent<PlayerAttack>().Attack();
        }
    }

    void FixedUpdate()
    {
        // Fazendo o jogador andar
        rb.velocity = mov * GetComponent<EntityStats>().moveSpeed * Time.fixedDeltaTime;
    }
}
