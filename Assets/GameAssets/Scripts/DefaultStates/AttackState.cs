using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    #region variaveis dos AttackTypes

    public GameObject[] attackPoints;
    //Index do attack point
    public int attackPoint = 0;

    // Layer dos inimigos;
    public LayerMask targetLayers;

    // Variavel apenas para teste do tamanho da hitbox
    public float _attackRange;

    #endregion

    public override void Enter()
    {

        rb.velocity = new Vector2(0,0);

        string animationToPlay = directions[direction];
        animator.Play($"ATTACK{animationToPlay}");
    }

    public override void Do()
    {
        rb.velocity = new Vector2(0,0);

        string animationToPlay = directions[direction];
        animator.Play($"ATTACK{animationToPlay}");
        if(animator.GetCurrentAnimatorStateInfo(0).IsName($"ATTACK{animationToPlay}") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) isComplete = true;
        Debug.Log(isComplete);
    }

    public override void Exit()
    {

    }


    void OnDrawGizmosSelected()
    {
        if(attackPoints == null) return;

        if(attackPoints[attackPoint] == null || attackPoint > attackPoints.Length)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoints[attackPoint].transform.position, _attackRange);
    }

    #region Funçoes dos attack types

    public virtual void Attack() { }

    public int SelectAttackPoint()
    {
        Dictionary<string, int> dict = new()
        {
            {"right", 0},
            {"left", 1},
            {"up", 2},
            {"down", 3}
        };
        return dict[direction];
    }

    #endregion
}
