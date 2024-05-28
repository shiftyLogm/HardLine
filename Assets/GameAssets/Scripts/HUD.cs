using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class HUD : MonoBehaviour
{
    public static HUD Instance {get; private set;}


    EntityStats playerStats;

    public GameObject damagePopUp; // Canvas do dano para mostrar na tela

    public Slider hpBar;

    // Fogueira interaction
    public GameObject interaction;
    public bool distFogPlayer;


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
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHpBar();
    }

    // Funçao que mostra o dano na tela
    public void ShowDamageOnScreen(float _damage, Vector2 fatherPos)
    {
        GameObject newPopUp = Instantiate(damagePopUp, fatherPos, Quaternion.identity);
        newPopUp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0,2), 2), ForceMode2D.Impulse);
        newPopUp.GetComponentInChildren<TextMeshProUGUI>().text = _damage.ToString();
        Destroy(newPopUp, 0.3f);
    }

    // Funçao para mudar o slider de hp quando tomar dano
    void ChangeHpBar()
    {
        hpBar.maxValue = playerStats.maxHp;
        hpBar.value = playerStats.hp;
    }

    
}
