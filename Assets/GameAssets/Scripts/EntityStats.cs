using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [SerializeField]
    private int _level = 1;
    public int maxHp;
    public float hp;
    public int maxXp;
    public float xp;
    public float moveSpeed;
    public float attackRange;
    public float attackDamage;
    public float dashForce;
    public float projectileForce;
    public float attackCooldown;


    // Start is called before the first frame update
    void Start()
    {
        SetHp();
    }

    // Update is called once per frame
    void Update()
    {
        BlinkDamage();

    }

    public void TakeDamage(float _damage)
    {
        hp -= _damage;

        HUD.Instance.ShowDamageOnScreen(_damage, this.gameObject.transform.position);

        // Mudar cor ao tomar dano
        GetComponentInChildren<SpriteRenderer>().color = (this.tag == "Player") ? Color.red : Color.magenta;

        if(hp <= 0)
        {
            if(this.gameObject.tag == "Enemy")
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>().LevelUp(xp);
            }
            Death();
        }
    }

    void Death()
    {
        Destroy(this.gameObject);
    }

    public void SetHp()
    {
        hp = maxHp;
    }

    private void BlinkDamage()
    {
        if(GetComponentInChildren<SpriteRenderer>().color == Color.white && this.tag == "Player") return;
        if(GetComponentInChildren<SpriteRenderer>().color == Color.red && this.tag == "Enemy") return;

        GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(GetComponentInChildren<SpriteRenderer>().color, (this.tag == "Player") ? Color.white : Color.red, 5 * Time.deltaTime);
    }

    private void LevelUp(float xpToUp)
    {
        xp += xpToUp;

        while(xp >= maxXp)
        {
            _level += 1;
            xp -= maxXp;
            if(xp < 0) xp = 0;
            maxXp += 50;
        }
    }


}
