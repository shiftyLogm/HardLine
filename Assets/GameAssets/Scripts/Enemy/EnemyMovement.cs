using System;
using System.Collections;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private EntityStats entityStats;
    public Rigidbody2D rb;

    // EnemyController
    EnemyController enemyController;

    // Campo de visao variaveis
    public float raioVisao;
    public LayerMask layerMask;

    GameObject target;
    Transform player;

    NavMeshAgent agent;

    // Aumento altura da linha ray
    public float aumentoLinhaRay;

    // Lugar que o inimigo vai voltar quando nao encontrar player
    GameObject[] enemySpawns;
    private int indexEnemySpawns = 0;
    private bool patrol = true;
    private bool coroutineRunning = false;

    // Variaveis
    private bool hasPath = false;
    private bool closeEnough = false;
    public bool insideCollider = false;
    

    // Start is called before the first frame update
    void Start()
    {
        entityStats = GetComponent<EntityStats>();
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        enemyController = GetComponent<EnemyController>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = null;
        
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // EnemySpawns
        enemySpawns = GameObject.FindGameObjectsWithTag("EnemySpawns");
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        Move();
    }

    void Update()
    {
        FindPlayer();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, raioVisao);
        //if(player != null) Gizmos.DrawRay(transform.position, new Vector2(player.transform.position.x, player.transform.position.y + aumentoLinhaRay) - new Vector2(transform.position.x, transform.position.y));
    }

    void FindPlayer()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, raioVisao, layerMask);

        if(collider != null) 
        {
            Vector2 pos = transform.position; // Posiçao do objeto
            Vector2 targPos = collider.transform.position; // Posicao do collider
            Vector2 direction = targPos - pos; // Direçao que é para ir
            direction = direction.normalized; // Normalizando a direçao -1 0 1

            RaycastHit2D hit = Physics2D.Raycast(pos, direction); // Criando uma linha da posiçao do objeto ate a direçao criada e guardando tudo que essa linha colide em uma variavel
            if(hit.transform != null) 
            {    
                if(hit.transform.tag == "Player") 
                {
                    target = collider.gameObject;
                    SetTarget(target);
                    patrol = false;
                    StopCoroutine(PatrolFunc());
                    coroutineRunning = false;
                    return;
                }
            }
        }
        target = null; 
    }

    IEnumerator PatrolFunc()
    {
        coroutineRunning = true;
        while(patrol)
        {
            // Dando um tempo ate ele ir para o proximo spawn
            yield return new WaitForSeconds(3);
            target = enemySpawns[indexEnemySpawns];
            SetTarget(target);

            // Rotina ira parar ate que ele chegue ao spawn
            yield return new WaitUntil(() => !hasPath);
        

            // Aumentando index do spawn
            indexEnemySpawns++;
            if(indexEnemySpawns >= enemySpawns.Length) indexEnemySpawns = 0;

        }
        coroutineRunning = false;
    }

    void Move()
    { 
        if(hasPath && !closeEnough) 
        {   
            // Transformando em vector2
            Vector2 agentDestination = new Vector2(agent.destination.x, agent.destination.y);
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);

            Vector2 direction = agentDestination - pos; // Direçao que o objeto tem que ir

            float remainingDistance = Mathf.Ceil((direction.x - direction.y) * 100) / 100; // Distancia que falta para ir
            if(Mathf.Abs(remainingDistance) <= 0.02f) 
            {
                hasPath = false;
                return;
            }

            direction = direction.normalized; // Normalizando essa direçao

            if(!enemyController.isTrueOrFalseAction) rb.velocity = direction * entityStats.moveSpeed * Time.fixedDeltaTime;
            return;
        }

        Debug.Log("No path");
        rb.velocity = new Vector2(0,0);
        patrol = !hasPath;
        if(!coroutineRunning) StartCoroutine(PatrolFunc());
        
    }    

    private void SetTarget(GameObject target)
    {
        agent.SetDestination(new Vector3(target.transform.position.x, target.transform.position.y + aumentoLinhaRay, 0)); // Esse novo vector fara com que a linha do SetDestination va para o centro do target
        hasPath = true;
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject.tag);
        if(target != null && target.tag == "Player")
        {
            if(collider.gameObject.tag == "Player")
            {
                insideCollider = true;
                closeEnough = true;
                enemyController.isAttacking = true;
                enemyController.isTrueOrFalseAction = true;
                rb.velocity = new Vector2(0,0);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(target != null && target.tag == "Player")
        {
            if(collider.gameObject.tag == "Player")
            {
                insideCollider = false;
                closeEnough = false;
            }
        }
    }
}
