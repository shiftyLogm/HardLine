using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class TransformHover : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    private GameObject targetObject;
    public Vector3 scaleHover;
    private Vector3 initialScale;
    public Vector3 targetScale;
    private float _speed;
    void Start() 
    {
        targetObject = gameObject;
        initialScale = targetObject.transform.localScale;
        scaleHover = initialScale;
        _speed = 5f * Time.deltaTime;
    }
    public void OnPointerEnter(PointerEventData eventData) => scaleHover = targetScale;

    public void OnPointerExit(PointerEventData eventData) => scaleHover = initialScale;

    void Update() => targetObject.transform.localScale = Vector3.Lerp(targetObject.transform.localScale, scaleHover, _speed);
}
