using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // States
    public IdleState idleState;
    public RunState runState;

    State state;

    // Variaveis
    private Animator animator;
    private bool isTrueOrFalseAction = false;

    // EnemyMovement
    EnemyMovement enemyMovement;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        enemyMovement = gameObject.GetComponent<EnemyMovement>();
        
        // State inicial
        state = idleState;

        // Setup dos States
        idleState.Setup(animator, enemyMovement.rb);
        runState.Setup(animator, enemyMovement.rb);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyMovement.rb.velocity != new Vector2(0,0)) DirectionFacing();

        SelectState();
        state.Do();
    }

    #region DirectionFacing

    private void DirectionFacing()
    {
        Dictionary<string, bool> dict = new()
        {
            {"up", enemyMovement.rb.velocity.y > 0},
            {"down", enemyMovement.rb.velocity.y < 0},
            {"right", enemyMovement.rb.velocity.x > 0},
            {"left", enemyMovement.rb.velocity.y < 0}
        };

        var key = Helper.FindKey(dict, true);
        state.direction = key;
        idleState.direction = state.direction;
    }

    #endregion


    #region States
    private void SelectState()
    {
        State oldState = state;

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
    }

    #endregion
}
