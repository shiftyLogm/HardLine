using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMenuHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float transitionSpeed;
    public TextMeshProUGUI textComponent;

    public GameObject buttonComponent;

    private Vector3 normalVector;
    private Vector3 hoverVector;
    private Vector3 TargetVector;
    private Color TargetColor;

    private Color normalColor = Color.white;
    private Color hoverColor = Color.yellow;
    void Start()
    {
        transitionSpeed = 5f * Time.deltaTime;
        TargetColor = normalColor;
        normalVector = buttonComponent.transform.localScale;
        TargetVector = normalVector;
        hoverVector = new Vector3(2.35f, 2.35f, 1.175f);
    }

    void Update()
    {
        textComponent.color = Color.Lerp(textComponent.color, TargetColor, transitionSpeed);
        buttonComponent.transform.localScale = Vector3.Lerp(buttonComponent.transform.localScale, TargetVector, transitionSpeed);
    }
    public void OnPointerEnter(PointerEventData eventData) 
    {
        TargetColor = hoverColor;
        TargetVector = hoverVector;
    }
    public void OnPointerExit(PointerEventData eventData) 
    {
        TargetColor = normalColor;
        TargetVector = normalVector;
    }
}