using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EntityStats entityStats;
    private Rigidbody2D rb;

    // Campo de visao variaveis
    public float raioVisao;
    public LayerMask layerMask;
    Transform target;
    public Transform player;

    //NavMeshAgent agent;

    //static float agentDrift = 0.0001f; // minimal
    

    // Start is called before the first frame update
    void Start()
    {
        entityStats = GetComponent<EntityStats>();
        rb = GetComponent<Rigidbody2D>();
        //agent = GetComponent<NavMeshAgent>();

        //agent.updateRotation = false;
        //agent.updateUpAxis = false;

        target = null;
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
        Gizmos.DrawRay(transform.position, player.position - transform.position);
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
                    return;
                }
            }
        }
        target = null;
    }

    void Move()
    { 
        FindPlayer();

        if(target != null) 
        {
            Vector2 direction = target.position - transform.position; // Direçao que o objeto tem que ir
            direction = direction.normalized; // Normalizando essa direçao
            rb.velocity = direction * entityStats.moveSpeed * Time.fixedDeltaTime;
            return;
        }
        rb.velocity = new Vector2(0,0);
    }


    // Funçao apenas para resolver um erro do NavMeshAgent
    /*void SetDestination(GameObject target)
    {   
		if(Mathf.Abs(transform.position.x - target.transform.position.x) < agentDrift)
        {
            var driftPos = target.transform.position + new Vector3(agentDrift, 0f, 0f);
            agent.SetDestination(driftPos);
        }
    }*/

    
}
