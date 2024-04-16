using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverTabsClassNG : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
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
    void Start()
    {
        initialColor = Color.white;
        initialScale = tabClass.transform.localScale;
        _transitionSpeedColor = 2f * Time.deltaTime;
        _transitionSpeedScale = 5 * Time.deltaTime;
        colorHover = initialColor;
        scaleHover = initialScale;
        targetScale = new Vector3(1.1f, 1.1f, 1.1f);
        initialArmourColor = new Color(20f / 225f, 20f / 225f, 20f / 225f, 255f / 225f);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        colorHover = targetColor;
        scaleHover = targetScale;
        idxStringValue = tabClass.name;
        idxInt = Array.IndexOf(ClassesString, idxStringValue);
        armourImage = tabClass.transform.Find($"ClassArmour{idxInt + 1}");
        armourImage.GetComponent<Image>().color = targetColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        colorHover = initialColor;
        scaleHover = initialScale;
        armourImage.GetComponent<Image>().color = initialArmourColor;
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
