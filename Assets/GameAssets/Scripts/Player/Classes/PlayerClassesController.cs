using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerClassesController : MonoBehaviour
{
    public static PlayerClassesController Instance { get; set; }

    // Class states
    // State state;
    // public WarriorClass warriorClass;
    // public MageClass mageClass;
    // public RangedClass rangedClass;

    // EntityStats
    // EntityStats entityStats;

    // Index de classes
    public string idxClass = "";
    public void setIdxClass(string v)
    {
        this.idxClass = v;
    }
    // Prefabs das classes

    [SerializeField]
    private GameObject _warriorClass;
    
    [SerializeField]
    private GameObject _mageClass;
    
    [SerializeField]
    private GameObject _archerClass;

    public GameObject player;

    // SFX
    public AudioClip swordHitSound;

    void Awake()
    {
        if(Instance == null) Instance = this;

        // entityStats = GetComponent<EntityStats>();

        SelectClass();

        // warriorClass.Setup(_entityStats: entityStats);
        // rangedClass.Setup(_entityStats: entityStats);
        // mageClass.Setup(_entityStats: entityStats);

        // state.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectClass()
    {
        Dictionary<string, GameObject> classes = new()
        {
            {"Warrior", _warriorClass},
            {"Archer", _archerClass},
            {"Mage", _mageClass}
        };

        // Instanciando o player com sua classe
        player = Instantiate(classes[idxClass], new Vector3(-3.14f, 1.612f, 0), Quaternion.identity);
    }

    public AudioClip SelectHitSound()
    {
        Dictionary<string, AudioClip> classes = new()
        {
            {"Warrior", swordHitSound},
            {"Archer", swordHitSound},
            {"Mage", swordHitSound}
        };

        return classes[idxClass];
    }
}
