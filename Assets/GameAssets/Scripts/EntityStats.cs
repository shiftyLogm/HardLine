using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public int maxHp;
    float hp;
    public float moveSpeed;
    public float attackRange;
    public float attackDamage;
    public float dashForce;


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
        
        // Mudar cor ao tomar dano
        GetComponentInChildren<SpriteRenderer>().color = (this.tag == "Player") ? Color.red : Color.magenta;

        if(hp <= 0)
        {
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
        if(GetComponentInChildren<SpriteRenderer>().color == Color.white && this.tag == "Enemy") return;

        GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(GetComponentInChildren<SpriteRenderer>().color, (this.tag == "Player") ? Color.white : Color.red, 5 * Time.deltaTime);
    }
}
