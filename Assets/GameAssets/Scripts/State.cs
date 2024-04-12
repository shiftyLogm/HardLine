using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set;}
    protected float startTime;
    public float time => Time.time - startTime;
    
    protected Dictionary<string, string> transitions = new Dictionary<string, string>
    {
        {"up", "Up"},
        {"down", "Down"},
        {"left", "Left"},
        {"rigth", "Rigth"}
    };

    // Blackboard variables
    protected Animator animator;
    protected Rigidbody2D rb;
    protected string direction;

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }

    public void Setup(Animator _animator, Rigidbody2D _rb) 
    {
        animator = _animator;
        rb = _rb;
    }

    public void Direction(string _direction)
    {
        direction = _direction;
    }

    public void Initialize()
    {
        isComplete = false;
        startTime = Time.time;
    }
}
