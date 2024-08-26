using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverTabsClassNG : MonoBehaviour
{
    public GameObject tabClass;
    public Color targetColor;
    public RectTransform tabPos;
    public float[] flexSpeedEnter;
    public float[] flexSpeedExit;
    private Color colorHover;
    private Color initialColor;
    private float _transitionSpeedColor;
    public GameObject[] Classes;
    public string[] ClassesString = {"Class1", "Class2", "Class3"};
    private Transform armourImage;
    private Vector3 initialScale;
    private Vector3 targetScale;
    private Vector3 scaleHover;
    private float _transitionSpeedScale;
    private int idxInt;
    private string idxStringValue;
    private Color initialArmourColor;
    private WriteStatusEffect writeEffect;
    public string NameClass;
    [TextArea] public string StatusClass;
    private GameObject NameClassObj;
    private GameObject StatusClassObj;
    public GameObject changeClassBTN;
    public GameObject StartGameBTN;
    private NameSave inputField;
    public static string ClassNewGameData;
    public Color emissionColor;
    private MoveNewGameTabs objNewGameTabs;
    public static bool navigateTabsNewGame;
    private bool disableSelect;
    public static bool escNewGame;
    private NewGameConfirmTab newGameInfo;
    void Start()
    {
        initialColor = new Color(166f/ 255, 166f/ 255, 166f/ 255, 1);
        initialScale = tabClass.transform.localScale;
        _transitionSpeedColor = 2f * Time.deltaTime;
        _transitionSpeedScale = 5 * Time.deltaTime;
        colorHover = initialColor;
        scaleHover = initialScale;
        targetScale = new Vector3(1.1f, 1.1f, 1.1f);
        initialArmourColor = new Color(20f / 225f, 20f / 225f, 20f / 225f, 1);
        writeEffect = FindObjectOfType<WriteStatusEffect>();
        changeClassBTN.SetActive(false);
        inputField = FindObjectOfType<NameSave>();
        objNewGameTabs = FindObjectOfType<MoveNewGameTabs>();
        newGameInfo = FindObjectOfType<NewGameConfirmTab>();
    }

    public void OnPointerEnter()
    {   
        Find();
        colorHover = targetColor;
        scaleHover = targetScale;
        writeEffect.nameText = NameClassObj.GetComponent<TextMeshProUGUI>();
        writeEffect.statusText = StatusClassObj.GetComponent<TextMeshProUGUI>();
        StartCoroutine(writeEffect.DisplayLine(NameClass, StatusClass));
        EventSystem.current.SetSelectedGameObject(null);
        ClassNewGameData = NameClassObj.name;
        navigateTabsNewGame = false;
        disableSelect = true;
    }

    public void OnSelect()
    {
        if (!disableSelect)
        {
            Find();
            colorHover = targetColor;
            scaleHover = targetScale;
            writeEffect.nameText = NameClassObj.GetComponent<TextMeshProUGUI>();
            writeEffect.statusText = StatusClassObj.GetComponent<TextMeshProUGUI>();
            StartCoroutine(writeEffect.DisplayLine(NameClass, StatusClass));
            ClassNewGameData = NameClassObj.name;
        }
    }

    public void OnDeselect()
    {
        colorHover = initialColor;
        scaleHover = initialScale;
        armourImage.GetComponent<Image>().color = initialArmourColor;
        StopAllCoroutines();
        NameClassObj.GetComponent<TextMeshProUGUI>().text = "";
        StatusClassObj.GetComponent<TextMeshProUGUI>().text = "";
    }

    private void Find()
    {
        idxStringValue = tabClass.name;
        idxInt = Array.IndexOf(ClassesString, idxStringValue);
        armourImage = tabClass.transform.Find($"ClassArmour{idxInt + 1}");
        armourImage.GetComponent<Image>().color = targetColor;
        NameClassObj = GameObject.Find($"NameClass{idxInt + 1}");
        StatusClassObj = GameObject.Find($"StatusClass{idxInt + 1}");
    }

    public void OnPointerExit()
    {
        colorHover = initialColor;
        scaleHover = initialScale;
        armourImage.GetComponent<Image>().color = initialArmourColor;
        StopAllCoroutines();
        NameClassObj.GetComponent<TextMeshProUGUI>().text = "";
        StatusClassObj.GetComponent<TextMeshProUGUI>().text = "";
        navigateTabsNewGame = true;
        disableSelect = false;
    }

    public void OnPointerClick(bool value)
    {
        if (!value) objNewGameTabs.DisableUpdate();
        foreach(var classObj in Classes) classObj.GetComponent<EventTrigger>().enabled = value;
        changeClassBTN.SetActive(!value);
        MoveNewGameTabs._setMoveNG = value;
        inputField.saveName.Select();

        try 
        {
            if (value) 
            {
                OnPointerExit(); 
                changeClassBTN.GetComponent<TransformHover>().transform.localScale = new Vector3(1f, 1f, 1f);
                changeClassBTN.GetComponent<TransformHover>().scaleHover = new Vector3(1f, 1f, 1f);
                objNewGameTabs.EnableUpdate();
            }
        }

        catch (NullReferenceException) {}
    }

    public void turnTabsNormal(bool value)
    {
        foreach (var idx in Classes) idx.GetComponent<EventTrigger>().enabled = value;
    }

    public void classAnimation(int rectvalueY, float[] arrayspeed)
    {
        for (int i = 0; i < Classes.Length; i++)
        {           
            var rect = Classes[i].GetComponent<RectTransform>();
            float rectvalueX = rect.anchoredPosition.x;
            LeanTween.move(Classes[i].GetComponent<RectTransform>(), new(rectvalueX, rectvalueY), arrayspeed[i]).setEase(LeanTweenType.easeInOutCubic);
        }
    }
    void desactiveEventTrigger() 
    {
        if (MoveNewGameTabs.desactiveEventTrigger) turnTabsNormal(false);
    }

    void Update() 
    {
        tabClass.GetComponent<Image>().color = Color.Lerp(tabClass.GetComponent<Image>().color, colorHover, _transitionSpeedColor);
        tabClass.transform.localScale = Vector3.Lerp(tabClass.transform.localScale, scaleHover, _transitionSpeedScale);
        StartGameBTN.SetActive(changeClassBTN.activeSelf && inputField.Normaltext.text.Length > 1 ? true : false);

        if (Input.GetKeyDown(KeyCode.Escape) && escNewGame) 
        {
            OnPointerClick(true);
            Invoke("desactiveEventTrigger", 0.01f);
            navigateTabsNewGame = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && navigateTabsNewGame || Input.GetKeyDown(KeyCode.RightArrow) && navigateTabsNewGame)
        {
            EventSystem.current.SetSelectedGameObject(
                Input.GetKeyDown(KeyCode.LeftArrow) ? Classes[0] :
                Input.GetKeyDown(KeyCode.RightArrow) ? Classes[1] : null);
            navigateTabsNewGame = false;
        }

        if (StartGameBTN.activeSelf && Input.GetKeyDown(KeyCode.Return)) newGameInfo.ShowTab();
        
    }
}
