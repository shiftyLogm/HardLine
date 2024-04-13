using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set;}
    protected float startTime;
    public float time => Time.time - startTime;
    
    protected Dictionary<string, string> directions = new Dictionary<string, string>
    {
        {"up", "Up"},
        {"down", "Down"},
        {"left", "Left"},
        {"right", "Right"}
    };

    // Blackboard variables
    protected Animator animator;
    protected Rigidbody2D rb;
    protected EntityStats entityStats;
    public Vector2 mov;
    public string direction;

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }

    public void Setup(Animator _animator, Rigidbody2D _rb, [Optional]EntityStats _entityStats) 
    {
        animator = _animator;
        rb = _rb;
        entityStats = _entityStats;
    }

    public void Initialize()
    {
        isComplete = false;
        startTime = Time.time;
    }
}
