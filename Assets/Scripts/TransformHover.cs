using UnityEngine;
using UnityEngine.EventSystems;

public class TransformHover : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    public GameObject targetObject;

    private Vector3 initialScale;
    public float transformScale = 1.195f;
    float speed = 0.5f;
    void Start() => initialScale = targetObject.transform.localScale;

    public void OnPointerEnter(PointerEventData eventData) => LeanTween.scale(targetObject, initialScale * transformScale, speed).setEase(LeanTweenType.easeSpring);

    public void OnPointerExit(PointerEventData eventData) => LeanTween.scale(targetObject, initialScale, speed).setEase(LeanTweenType.easeSpring);

}
