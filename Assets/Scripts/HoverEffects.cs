using TMPro;
using UnityEngine;

public class GlowEffect : MonoBehaviour
{
    public float normalFontSize = 20f;
    public float hoverFontSize = 22f;
    public float transitionSpeed = 5f;
    public Color normalColor = Color.white;
    public Color hoverColor = Color.yellow;

    private TextMeshProUGUI textComponent;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // Verifica se o mouse está sobre o texto
        if (RectTransformUtility.RectangleContainsScreenPoint(textComponent.rectTransform, Input.mousePosition))
        {
            textComponent.fontSize = Mathf.Lerp(textComponent.fontSize, hoverFontSize, Time.deltaTime * transitionSpeed);
            textComponent.color = Color.Lerp(textComponent.color, hoverColor, Time.deltaTime * transitionSpeed);
        }
        else
        {
            textComponent.fontSize = Mathf.Lerp(textComponent.fontSize, normalFontSize, Time.deltaTime * transitionSpeed);
            textComponent.color = Color.Lerp(textComponent.color, normalColor, Time.deltaTime * transitionSpeed);
        }
    }
}
