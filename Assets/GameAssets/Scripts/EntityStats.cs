using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public int maxHp;
    int hp;
    public float moveSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}