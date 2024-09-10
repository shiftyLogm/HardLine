using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // States
    public IdleState idleState;
    public RunState runState;
    public AttackState attackState;

    State state;

    // Attack Types
    public MeleeAttack meleeAttack;

    // Variaveis
    private Animator animator;
    public bool isTrueOrFalseAction = false;
    public bool isAttacking = false;
    private bool coroutineRunning = false;

    // EnemyMovement
    EnemyMovement enemyMovement;

    // EntityStats
    EntityStats entityStats;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyMovement = gameObject.GetComponent<EnemyMovement>();
        entityStats = GetComponent<EntityStats>();
        
        // State inicial
        state = idleState;
        state.direction = "right";
        attackState.direction = "right";

        // Setup dos States
        idleState.Setup(animator, enemyMovement.rb);
        runState.Setup(animator, enemyMovement.rb);
        attackState.Setup(animator, enemyMovement.rb, entityStats);
        meleeAttack.Setup(animator, enemyMovement.rb, entityStats);
    }

    // Update is called once per frame
    void Update()
    {
        DirectionFacing();

        SelectState();
        state.Do();

        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    #region DirectionFacing

    private void DirectionFacing()
    {
        Dictionary<string, bool> dict = new()
        {
            {"right", enemyMovement.rb.velocity.x > 0 && (Mathf.Abs(enemyMovement.rb.velocity.y) < enemyMovement.rb.velocity.x)},
            {"left", enemyMovement.rb.velocity.x < 0 && (Mathf.Abs(enemyMovement.rb.velocity.y) < Mathf.Abs(enemyMovement.rb.velocity.x))},
            {"up", enemyMovement.rb.velocity.y > 0 && (enemyMovement.rb.velocity.x < enemyMovement.rb.velocity.y)},
            {"down", enemyMovement.rb.velocity.y < 0 && (enemyMovement.rb.velocity.x < Mathf.Abs(enemyMovement.rb.velocity.y))}
        };
 
        var key = Helper.FindKey(dict, true);
        state.direction = key;
        idleState.direction = key;
        attackState.direction = key;
    }

    #endregion

    #region States
    private void SelectState()
    {
        State oldState = state;

        Dictionary<State, bool> dict = new()
        {
            {attackState, isAttacking && (state == idleState || state == runState)}
        };

        // isTrueOrFalseActions
        if(isTrueOrFalseAction)
        {
            state = Helper.FindKeyState(dict, true);
            
            if(state == idleState) isTrueOrFalseAction = false;
            if(state == null) state = oldState;
        }

        // Movimento
        if(!isTrueOrFalseAction)
        {
            if(enemyMovement.rb.velocity.x == 0 && enemyMovement.rb.velocity.y == 0)
            {
                state = idleState;
            }

            else state = runState;
        }

        

        // Caso o oldState seja diferente state atual troca de estado
        if(state != oldState)
        {
            oldState.Exit();
            state.Initialize();
            state.Enter();
            return;
        }

        // Se o player estiver dentro do range de ataque, e o state estiver completo, ele ira para animaçao de idle antes de atacar novamente
        if(enemyMovement.insideCollider && state.isComplete)
        {
            state = idleState;
            isTrueOrFalseAction = false;
            return;
        }

        // Esse IF so vai iniciar a rotina, se for a primeira vez que o player entrou no range do inimigo (se o player sair, isso reseta)
        if(enemyMovement.insideCollider && !coroutineRunning)
        {
            isAttacking = true;
            isTrueOrFalseAction = true;
            StartCoroutine(AttackDelayFunc());
            return;
        }

        if(state.isComplete)
        {
            isAttacking = false;
            isTrueOrFalseAction = false;
        }
    }

    IEnumerator AttackDelayFunc()
    {
        coroutineRunning = true;
        isAttacking = true;

        while(state == attackState)
        {
            SelectAttackTypeAndAttack();
            isAttacking = false;

            yield return new WaitForSeconds(2);
            StartCoroutine(AttackDelayFunc());
        } 
        coroutineRunning = false;
    }

    #endregion

    #region Attack Types

    private void SelectAttackTypeAndAttack()
    {
        meleeAttack.attackPoint = attackState.SelectAttackPoint();
        meleeAttack.Attack();
    }

    #endregion
}
