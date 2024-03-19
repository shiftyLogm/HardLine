using NovaSamples.Effects;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
public class MenuClicks : MonoBehaviour
{
    public static bool SetMenuOptions;
    public RectTransform MainMenuRect;

    public RectTransform OptionsMenu;
    public GameObject ArrowTurnBack;
    public GameObject MenuOptions;
    public Image BonfireReal;
    public GameObject UIBlur;
    public Camera MainCamera;

    void Start()
    {
        MainMenuRect = GetComponent<RectTransform>();
        MainMenuRect.anchoredPosition = new Vector2(0, -330.6f);
        OptionsMenu.anchoredPosition = new Vector2(0, 1054);
        ArrowTurnBack.transform.localScale = new Vector3(2, 1, 2);
    }

    public void NewGameButtonClick()
    {
        Debug.Log("New");
    }
    public void OptionsButtonClick()
    {
        LeanTween.alpha(BonfireReal.rectTransform, 0f, .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(MainMenuRect, new (0f, -700f), .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(OptionsMenu, new (0, 0), .5f).setEase(LeanTweenType.easeInOutQuad); 
        LeanTween.value(gameObject, updateBlur, 0f, 30f, .5f).setEase(LeanTweenType.easeInOutQuad);
        SetMenuOptions = false;
    }
    public void ExitButtonClick() 
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    public void ArrowButtonClick() 
    {
        
        LeanTween.alpha(BonfireReal.rectTransform, 1f, .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(MainMenuRect, new (0, -330.6f), .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(OptionsMenu, new (0, 1054), .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.value(gameObject, updateBlur, 30f, 0f, .5f).setEase(LeanTweenType.easeInOutQuad);
        SetMenuOptions = true;
    }

    void updateBlur(float val)
    {
        BlurEffect.blurRadius = val;
    }

    void Update()
    {  
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ArrowButtonClick();
        } 
    }
}
