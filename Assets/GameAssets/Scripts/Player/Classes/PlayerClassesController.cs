using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClassesController : MonoBehaviour
{
    // Class states
    State state;
    public WarriorClass warriorClass;
    public MageClass mageClass;
    public RangedClass rangedClass;

    // EntityStats
    EntityStats entityStats;

    // Index de classes
    public string idxClass = "";

    // Start is called before the first frame update
    void Start()
    {
        entityStats = GetComponent<EntityStats>();

        SelectClass();

        warriorClass.Setup(_entityStats: entityStats);
        rangedClass.Setup(_entityStats: entityStats);
        mageClass.Setup(_entityStats: entityStats);

        state.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectClass()
    {
        Dictionary<string, State> classes = new()
        {
            {"Warrior", warriorClass},
            {"Ranged", rangedClass},
            {"Mage", mageClass}
        };

        state = classes[idxClass];
    }
}
