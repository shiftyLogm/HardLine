using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AttackState
{
    public override void Attack()
    {
        // Pega o collider de todo objeto que estiver no enemyLayers
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoints[attackPoint].transform.position, entityStats.attackRange, targetLayers); // ponto inicial, raio e o layer

        // Dando dano em cada objeto que foi pego
        foreach(Collider2D target in hitTargets)
        {
            target.GetComponent<EntityStats>().TakeDamage(entityStats.attackDamage);
        }
    }
}