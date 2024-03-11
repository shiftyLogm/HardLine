using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FunctionsMenu : MonoBehaviour
{
    public static void AnimateVectorLerp(RectTransform objectTransform, Vector2 newPosition, float speed)
    {
        Vector2 initialPosition = objectTransform.anchoredPosition;
        objectTransform.anchoredPosition = Vector2.Lerp(initialPosition, newPosition, speed * Time.deltaTime);        
    }
}
