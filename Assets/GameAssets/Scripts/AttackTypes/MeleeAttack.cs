using UnityEngine;

public class MeleeAttack : AttackState
{
    public override void Attack()
    {
        HUD.Instance.weaponAttack.volume = 0.3f;
        HUD.Instance.weaponAttack.PlayOneShot(weaponSFX);
        // Pega o collider de todo objeto que estiver no enemyLayers
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoints[attackPoint].transform.position, entityStats.attackRange, targetLayers); // ponto inicial, raio e o layer

        // Dando dano em cada objeto que foi pego
        foreach(Collider2D target in hitTargets)
        {
            // Knockback
            target.gameObject.GetComponent<KnockbackFeedback>().sender = GetComponentInParent<EntityStats>().gameObject;
            target.gameObject.GetComponent<KnockbackFeedback>().PlayFeedback();
            

            if(target.GetComponent<EntityStats>() == null) Debug.Log("sem entityStats do target");

            target.GetComponent<EntityStats>().TakeDamage(entityStats.attackDamage);
        }
    }
}
