using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private EntityStats entityStats;
    public Rigidbody2D rb;

    // Campo de visao variaveis
    public float raioVisao;
    public LayerMask layerMask;

    Transform target;
    Transform player;

    NavMeshAgent agent;

    // Aumento altura da linha ray
    public float aumentoLinhaRay;

    // Lugar que o inimigo vai voltar quando nao encontrar player
    GameObject[] enemySpawns;
    private int indexEnemySpawns = 0;
    private bool patrol = true;
    private bool coroutineRunning = false;
    

    // Start is called before the first frame update
    void Start()
    {
        entityStats = GetComponent<EntityStats>();
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();

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
                if(hit.transform.CompareTag("Player")) 
                {
                    target = collider.transform;
                    agent.SetDestination(new Vector2(target.position.x, target.position.y + aumentoLinhaRay)); // Esse novo vector fara com que a linha do SetDestination va para o centro do target
                    patrol = false;
                    return;
                }
            }
        }
        target = null; 
    }

    IEnumerator PatrolFunc()
    {
        if(coroutineRunning) yield break;
        coroutineRunning = true;
        patrol = true;

        Debug.Log("Dentro da rotina");
        while(patrol)
        {
            
            Debug.Log("indo ate spawn");
            agent.SetDestination(new Vector2(enemySpawns[indexEnemySpawns].transform.position.x, enemySpawns[indexEnemySpawns].transform.position.y));
            
            // Rotina ira parar ate que ele chegue ao spawn
            yield return new WaitUntil(() => new Vector2(transform.position.x, transform.position.y) == new Vector2(enemySpawns[indexEnemySpawns].transform.position.x, enemySpawns[indexEnemySpawns].transform.position.y));
            
            // Aumentando index do spawn
            indexEnemySpawns++;
            if(indexEnemySpawns >= enemySpawns.Length) indexEnemySpawns = 0;
            
            // Dando um tempo ate ele ir para o proximo spawn
            yield return new WaitForSeconds(3);

        }
        coroutineRunning = false;
        Debug.Log("tenho caminho");
    }

    void Move()
    { 
        if(agent.hasPath) 
        {   

            // Transformando em vector2
            Vector2 agentDestination = new Vector2(agent.destination.x, agent.destination.y);
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);

            Vector2 direction = agentDestination - pos; // Direçao que o objeto tem que ir

            direction = direction.normalized; // Normalizando essa direçao
            rb.velocity = direction * entityStats.moveSpeed * Time.fixedDeltaTime;
            return;
        }

        Debug.Log("No path");
        rb.velocity = new Vector2(0,0);
        //StartCoroutine(PatrolFunc());
        
    }    
}
