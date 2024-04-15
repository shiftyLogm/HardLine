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
    

    // Start is called before the first frame update
    void Start()
    {
        entityStats = GetComponent<EntityStats>();
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = 0;

        target = null;
        
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
                    agent.SetDestination(new Vector3(target.position.x, target.position.y + aumentoLinhaRay, 0f)); // Esse novo vector fara com que a linha do SetDestination va para o centro do target
                    return;
                }
            }
        }
        target = null;
    }

    void Move()
    { 
        FindPlayer();

        if(agent.hasPath) 
        {
            Vector2 direction = agent.destination - transform.position; // Direçao que o objeto tem que ir
            direction = direction.normalized; // Normalizando essa direçao
            rb.velocity = direction * entityStats.moveSpeed * Time.fixedDeltaTime;
            return;
        }
        rb.velocity = new Vector2(0,0);
    }    
}
