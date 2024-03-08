using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;

public class teste : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    [SerializeField] private GameObject ArrowImage;

    private Vector3 initialScale;
    float transformScale = 1.195f;
    float speed = 0.5f;
    void Start(){
        initialScale = gameObject.transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, initialScale * transformScale, speed).setEase(LeanTweenType.easeSpring);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, initialScale, speed).setEase(LeanTweenType.easeSpring);
    }

}
