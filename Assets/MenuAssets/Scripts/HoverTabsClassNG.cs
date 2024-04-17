using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
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
        Classes = GameObject.FindGameObjectsWithTag("Classes");
        changeClassBTN.SetActive(false);
    }
    public void OnPointerEnter()
    {
        colorHover = targetColor;
        scaleHover = targetScale;
        idxStringValue = tabClass.name;
        idxInt = Array.IndexOf(ClassesString, idxStringValue);
        armourImage = tabClass.transform.Find($"ClassArmour{idxInt + 1}");
        armourImage.GetComponent<Image>().color = targetColor;
        NameClassObj = GameObject.Find($"NameClass{idxInt + 1}");
        StatusClassObj = GameObject.Find($"StatusClass{idxInt + 1}");
        writeEffect.nameText = NameClassObj.GetComponent<TextMeshProUGUI>();
        writeEffect.statusText = StatusClassObj.GetComponent<TextMeshProUGUI>();
        StartCoroutine(writeEffect.DisplayLine(NameClass, StatusClass));
    }

    public void OnPointerExit()
    {
        colorHover = initialColor;
        scaleHover = initialScale;
        armourImage.GetComponent<Image>().color = initialArmourColor;
        StopAllCoroutines();
        NameClassObj.GetComponent<TextMeshProUGUI>().text = "";
        StatusClassObj.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void OnPointerClick(bool value)
    {
        foreach(var classObj in Classes) classObj.GetComponent<EventTrigger>().enabled = value;
        changeClassBTN.SetActive(!value);
        try { if (value) OnPointerExit(); }
        catch (NullReferenceException) {} 
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
    void Update() 
    {
        tabClass.GetComponent<Image>().color = Color.Lerp(tabClass.GetComponent<Image>().color, colorHover, _transitionSpeedColor);
        tabClass.transform.localScale = Vector3.Lerp(tabClass.transform.localScale, scaleHover, _transitionSpeedScale);
    }
}
