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
        
    }

    public void TakeDamage(float _damage)
    {
        hp -= _damage;

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
}
