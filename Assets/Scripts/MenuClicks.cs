using Nova;
using NovaSamples.Effects;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
public class MenuClicks : MonoBehaviour
{
    public static bool SetMenuOptions;
    public static bool resetOptions = false;
    public RectTransform MainMenuRect;
    public RectTransform OptionsMenu;
    public GameObject ArrowTurnBack;
    public GameObject MenuOptions;
    public Image BonfireReal;
    public RectTransform bonfirePos;
    public RectTransform BonfireFalse;
    public GameObject blockblur_obj;
    public RectTransform blockblur_rect;
    public RectTransform PanelNewGame;
    public RectTransform ArrowTurnBackNewGame;
    public GameObject ArrowTurnBackNGObj;

    void Start()
    {
        MainMenuRect = GetComponent<RectTransform>();
        BonfireFalse.anchoredPosition = new Vector2(2891, -367.6f);
        MainMenuRect.anchoredPosition = new Vector2(0, -330.6f);
        OptionsMenu.anchoredPosition = new Vector2(0, 1054);
        ArrowTurnBack.transform.localScale = new Vector3(2, 1, 2);
        PanelNewGame.anchoredPosition = new Vector2(-2005, -.46f);
        bonfirePos.anchoredPosition = new Vector2(570, -367.7f);
        blockblur_rect.anchoredPosition = new Vector2(504, -356);
        blockblur_obj.SetActive(false);
        ArrowTurnBackNewGame.anchoredPosition = new Vector2(687, 667);
        ArrowTurnBackNewGame.rotation = Quaternion.Euler(182, 0, 156.74f);
    }

    public void NewGameButtonClick()
    {
        BonfireFalse.transform.position = new Vector2(2891, -1121);
        LeanTween.move(MainMenuRect, new Vector2(1953, -330.6f), 1f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(bonfirePos, new Vector2(2523, -367.7f), 1f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(blockblur_rect, new Vector2(2457, -356), 1f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(PanelNewGame, new Vector2(-4, -.46f), 1f).setEase(LeanTweenType.easeInOutCubic);
        Invoke("downArrow", .75f);
    }

    public void OptionsButtonClick()
    {
        LeanTween.alpha(BonfireReal.rectTransform, 0f, .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(MainMenuRect, new (0f, -700f), .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(OptionsMenu, new(0, 0), .5f).setEase(LeanTweenType.easeInOutQuad); 
        LeanTween.value(gameObject, updateBlur, 0f, 30f, .5f).setEase(LeanTweenType.easeInOutQuad);
        Invoke("activateBlur", .25f);
        blockblur_obj.SetActive(true);
        SetMenuOptions = false;
    }

    public void ExitButtonClick() 
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    void desactivateBlur() => blockblur_obj.SetActive(false);
    void activateBlur() => blockblur_obj.SetActive(true); 
    void downArrow() 
    {
        LeanTween.move(ArrowTurnBackNewGame, new(829, 418), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.rotateZ(ArrowTurnBackNGObj, -40f, 1).setEase(LeanTweenType.easeInOutQuad);
    } 
    void upArrow() 
    {
         ArrowTurnBackNewGame.anchoredPosition = new Vector2(687, 667);
         ArrowTurnBackNewGame.rotation = Quaternion.Euler(182, 0, 156.74f);
    }
    
    public void ArrowButtonClick() 
    {
        LeanTween.alpha(BonfireReal.rectTransform, 1f, .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(MainMenuRect, new (0, -330.6f), .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(OptionsMenu, new(0, 1054), .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.value(gameObject, updateBlur, 30f, 0f, .5f).setEase(LeanTweenType.easeInOutQuad);
        Invoke("desactivateBlur", .25f);
        SetMenuOptions = true;
    }

    public void ArrowButtonClickNewGame()
    {
        BonfireFalse.anchoredPosition = new Vector2(2891, -367.6f);
        LeanTween.move(MainMenuRect, new Vector2(0, -330.6f), 1f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(bonfirePos, new Vector2(570, -367.7f), 1f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(blockblur_rect, new Vector2(504, -356), 1f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(PanelNewGame, new Vector2(-2005, -.46f), 1f).setEase(LeanTweenType.easeInOutCubic);
        Invoke("upArrow", 1);
    }

    void updateBlur(float val) => BlurEffect.blurRadius = val;
    void Update()
    {  
        AdjustBloom.valueIntensity = OptionsMenu.anchoredPosition.y <= 200 ? 0 : 1;
        resetOptions = OptionsMenu.anchoredPosition.y > 1000 ? true : false;
        
        if (Input.GetKeyDown(KeyCode.Escape) && SetMenuOptions == false) ArrowButtonClick();
    }
}
