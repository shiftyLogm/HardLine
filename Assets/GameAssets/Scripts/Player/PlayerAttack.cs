using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Array dos attackPoints
    public GameObject[] attackPoints;
    //Index do attack point
    public int attackPoint = 0;

    EntityStats entityStats;

    // Layer dos inimigos;
    public LayerMask enemyLayers;

    // Variavel apenas para teste do tamanho da hitbox
    public float _attackRange;

    // Start is called before the first frame update
    void Start()
    {
        entityStats = GetComponent<EntityStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        // Pega o collider de todo objeto que estiver no enemyLayers
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoints[attackPoint].transform.position, entityStats.attackRange, enemyLayers); // ponto inicial, raio e o layer

        // Dando dano em cada objeto que foi pego
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EntityStats>().TakeDamage(entityStats.attackDamage);
        }
    }

    // Funçao apenas para desenha a hitbox
    void OnDrawGizmosSelected()
    {
        if(attackPoints[attackPoint] == null || attackPoint > attackPoints.Length)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoints[attackPoint].transform.position, _attackRange);
    }
}
