using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverTabsClassNG : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tabClass;
    public Color targetColor;
    private Color initialColor;
    private Color colorProp;
    private float _transitionSpeed;

    void Start()
    {
        initialColor = tabClass.GetComponent<Image>().color;
        colorProp = tabClass.GetComponent<Image>().color;
        _transitionSpeed = 5 * Time.deltaTime;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        colorProp = Color.Lerp(colorProp, targetColor, _transitionSpeed);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        colorProp = Color.Lerp(colorProp, initialColor, _transitionSpeed);
    }
}
