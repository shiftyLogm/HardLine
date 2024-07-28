using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [Header("Stats")]
    public int level = 1;
    public int maxHp;
    public float hp;
    public int maxXp;
    public float xp;
    public float moveSpeed;
    public float attackRange;
    public float attackDamage;
    public float faith;
    public float defense;
    public float vitality;
    public float dexterity;
    public float inteligence;
    public float luck;
    public float dashForce;
    public float projectileForce;
    public float attackCooldown;
    public bool invencible = false;

    //Particles
    public GameObject bloodParticle;
    public GameObject deathParticle;


    // Start is called before the first frame update
    void Start()
    {
        SetHp();
        moveSpeed += dexterity/100;
    }

    // Update is called once per frame
    void Update()
    {
        BlinkDamage();
    }

    public void TakeDamage(float _damage)
    {
        if(invencible == false)
        {
            hp -= _damage;

            HUD.Instance.ShowDamageOnScreen(_damage, this.gameObject.transform.position);

            //Particle instance
            if(bloodParticle)
            {
                Instantiate(bloodParticle, this.gameObject.transform.position, Quaternion.identity);
            } 

            // Mudar cor ao tomar dano
            GetComponentInChildren<SpriteRenderer>().color = Color.red;

            if(hp <= 0)
            {
                if(this.gameObject.tag == "Enemy")
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>().LevelUp(xp);
                }
                Death();
            }
        }
    }

    void Death()
    {
        //Particle instance
        if(deathParticle)
        {
            Instantiate(deathParticle, this.gameObject.transform.position, Quaternion.Euler(new Vector3(-90,0,0)));
        }

        Destroy(this.gameObject);
    }

    public void SetHp()
    {
        hp = maxHp;
    }

    private void BlinkDamage()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(GetComponentInChildren<SpriteRenderer>().color, Color.white, 5 * Time.deltaTime);
    }

    private void LevelUp(float xpToUp)
    {
        xp += xpToUp;

        while(xp >= maxXp)
        {
            level += 1;
            xp -= maxXp;
            if(xp < 0) xp = 0;
            maxXp += 50;
        }
    }


}
