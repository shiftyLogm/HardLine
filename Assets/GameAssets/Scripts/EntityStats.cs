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
    public float defence;
    public int vitality;
    public float dexterity;
    public float intelligence;
    public float dashForce;
    public float projectileForce;
    public float attackCooldown;
    public bool invencible = false;

    //Particles
    public GameObject bloodParticle;
    public GameObject deathParticle;

    public int levelsUped;


    // Start is called before the first frame update
    void Start()
    {
        SetStatus();
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

    #region Seters Stats
    public void SetStatus()
    {
        SetDef();
        hp = maxHp;

        SetDex();

        // Colocando na tela de level up os stats
        HUD.Instance.SetLevelUpScreenStats();
    }

    public void SetDex()
    {
        moveSpeed += dexterity/100;
        if(level == 1) return;

        dexterity += 2; 
    }

    public void SetDef()
    {
        maxHp += vitality + 2;
        if(level == 1) return;
        defence += 2; 
        vitality += 2;
    }
    #endregion

    private void BlinkDamage()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(GetComponentInChildren<SpriteRenderer>().color, Color.white, 5 * Time.deltaTime);
    }

    private void LevelUp(float xpToUp)
    {
        xp += xpToUp;

        while(xp >= maxXp)
        {
            levelsUped += 1;
            level += 1;
            xp -= maxXp;
            if(xp < 0) xp = 0;
            maxXp += 50;
        }
    }



}
