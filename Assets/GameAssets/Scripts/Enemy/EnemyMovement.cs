using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    private EntityStats entityStats;
    private Rigidbody2D rb;

    // Campo de visao variaveis
    public float raioVisao;
    public LayerMask layerMask;
    Transform target;
    

    // Start is called before the first frame update
    void Start()
    {
        entityStats = GetComponent<EntityStats>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        Move();
    }

    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, raioVisao);
    }

    void FindPlayer()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, raioVisao, layerMask);
        if(collider != null) 
        {
            Vector2 pos = transform.position;
            Vector2 targPos = collider.transform.position;
            Vector2 direction = targPos - pos;
            direction = direction.normalized;

            RaycastHit2D hit = Physics2D.Raycast(pos, direction);
            if(hit.transform != null) 
            {
                if(hit.transform.CompareTag("Player")) target = collider.transform;
                else target = null;
            }
        }
        else target = null;
    }

    void Move()
    {
        FindPlayer();

        if(target != null) 
        {
            Vector2 direction = target.position - transform.position;
            direction = direction.normalized;
            rb.velocity = direction * entityStats.moveSpeed * Time.fixedDeltaTime;
            
        }
    }

}
