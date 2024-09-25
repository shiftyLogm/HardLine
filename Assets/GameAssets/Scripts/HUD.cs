using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using System.Collections.Generic;
using System;
using System.Collections;

public class HUD : MonoBehaviour
{
    public static HUD Instance {get; private set;}


    EntityStats playerStats;
    public bool isCoroutineRunning;

    public GameObject damagePopUp; // Canvas do dano para mostrar na tela
    public GameObject levelUpPopUp; // Canvas do texto de level up

    public Slider hpBar;
    public Slider xpBar;

    // Fogueira interaction
    public GameObject interaction;
    public bool distFogPlayer;

    // LevelUp Screen
    [Header("LevelUp Screen")]
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI maxHp;
    [SerializeField] private TextMeshProUGUI faith;
    [SerializeField] private TextMeshProUGUI intelligence;
    [SerializeField] private TextMeshProUGUI vit;
    [SerializeField] private TextMeshProUGUI def;
    [SerializeField] private TextMeshProUGUI dex;
    [SerializeField] private TextMeshProUGUI movSpeed;
    public GameObject levelUpScreen;

    // Cor de levelUp
    public Color32 levelUpColor;



    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    } 

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>();

        // Cor de levelUp
        levelUpColor = new Color32(253, 215, 92, 255);

    }

    // Update is called once per frame
    void Update()
    {
        ChangeHpBar();
        ChangeXpBar();
    }

    // Funçao que mostra o dano na tela
    public void ShowDamageOnScreen(float _damage, Vector2 fatherPos)
    {
        GameObject newPopUp = Instantiate(damagePopUp, fatherPos, Quaternion.identity);
        newPopUp.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(0,2), 2), ForceMode2D.Impulse);
        newPopUp.GetComponentInChildren<TextMeshProUGUI>().text = _damage.ToString();
        Destroy(newPopUp, 0.3f);
    }

    // Funçao que mostra o texto que o jogador upou de nivel
    public void ShowLevelUpTextScreen()
    {
        Instantiate(levelUpPopUp, new Vector3(-814, 334, 0), Quaternion.identity);   
    }

    // Funçao para mudar o slider de hp quando tomar dano
    public void ChangeHpBar()
    {
        hpBar.maxValue = playerStats.maxHp;
        hpBar.value = playerStats.hp;
    }
    
    // Função para mudar o slider de xp quando eliminar um enimigo
    public void ChangeXpBar()
    {
        xpBar.maxValue = playerStats.maxXp;
        xpBar.value = playerStats.xp;
    }

    public void LevelUp(string stat){
        Dictionary<string, Action> levelUpDict = new()
        {
            {"str", () => playerStats.attackDamage += 2},
            {"faith", () => {playerStats.faith += 2; playerStats.intelligence += 2;}},
            {"def", () => playerStats.SetDef()},
            {"dex", () => playerStats.SetDex()},
        };
        playerStats.levelsUped -= 1;
        levelUpDict[stat]();
        
    }

    #region LevelUp Screen
    
    // Stats que aparecerao na tela de levelUP
    public void SetLevelUpScreenStats()
    {
        level.text = $"Level: {playerStats.level}";
        maxHp.text = $"Max Health: {playerStats.maxHp}";
        faith.text = $"Faith: {playerStats.faith}";
        intelligence.text = $"Intelligence: {playerStats.intelligence}";
        vit.text = $"Viatlity: {playerStats.vitality}";
        def.text = $"Defence: {playerStats.defence}";
        dex.text = $"Dexterity: {playerStats.dexterity}";
        movSpeed.text = $"Move Speed: {playerStats.moveSpeed}";
    }

    #endregion
}
