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

        // Setup dos States
        idleState.Setup(animator, enemyMovement.rb);
        runState.Setup(animator, enemyMovement.rb);
        attackState.Setup(animator, enemyMovement.rb, entityStats);
        meleeAttack.Setup(animator, enemyMovement.rb, entityStats);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);

        if(enemyMovement.rb.velocity != new Vector2(0,0)) DirectionFacing();

        SelectState();
        // Se nao for o attackState, state ira rolar normal
        if(state != attackState) 
        {
            // Parando a rotina caso ela esteja em execuçao
            if(coroutineRunning)
            {
                coroutineRunning = false;
                StopCoroutine(AttackDelayFunc());
            }
            state.Do();
            return;
        }
        
        // Se for o attackState, state tera um delay
        
        StartCoroutine(AttackDelayFunc());
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
        idleState.direction = state.direction;
        attackState.direction = state.direction;
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
            if(state == null) state = oldState;

            if(state == idleState) isTrueOrFalseAction = false;
        }

        // Movimento
        if(!isTrueOrFalseAction)
        {
            if(enemyMovement.rb.velocity.x == 0 && enemyMovement.rb.velocity.y == 0)
            {
                state = idleState;
                return;
            }

            state = runState;
        }

        // Caso o oldState seja diferente state atual troca de estado
        if(state != oldState)
        {
            oldState.Exit();
            state.Initialize();
            state.Enter();
        }
        if(state.isComplete)
        {
            isAttacking = false;
            isTrueOrFalseAction = false;
        }
    }

    IEnumerator AttackDelayFunc()
    {
        if(coroutineRunning) yield break;
        coroutineRunning = true;

        while(state == attackState)
        {
            yield return new WaitForSeconds(1);
            state.Do(); 
            SelectAttackTypeAndAttack();
        }   
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
