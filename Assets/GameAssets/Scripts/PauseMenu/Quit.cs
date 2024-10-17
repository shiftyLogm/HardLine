using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Quit : MonoBehaviour
{

    private float transitionSpeed;
    public TextMeshProUGUI textComponent;

    public GameObject buttonComponent;

    private Vector3 normalVector;
    public Vector3 hoverVector;
    public Vector3 TargetVector;
    public Color TargetColor;

    private Color normalColor = Color.white;
    public Color hoverColor;
    public void quitGameOnClick() => Application.Quit();

    void Start()
    {
        transitionSpeed = 5f * Time.deltaTime;
        TargetColor = normalColor;
        normalVector = buttonComponent.transform.localScale;
        TargetVector = normalVector;
        hoverVector = new Vector3(1.5f, 1.5f, 1.175f);
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
        EventSystem.current.SetSelectedGameObject(null);
        MenuClicks.setNavigate = true;
        ExitGameScreenNavigate.setNavigateExitScreen = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TargetColor = normalColor;
        TargetVector = normalVector;
    }

    public void OnSelect()
    {
        TargetColor = hoverColor;
        TargetVector = hoverVector;
    }

    public void OnDeselect()
    {
        TargetColor = normalColor;
        TargetVector = normalVector;
    }

}