using NovaSamples.Effects;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class MenuClicks : MonoBehaviour
{
    private bool SetAnimateOptionsOpen = false;
    private bool SetAnimateOptionsClose = false;
    private bool SetBlurAnimation = false;
    public static bool SetMenuOptions;
    public RectTransform MainMenuRect;

    public RectTransform OptionsMenu;
    public GameObject ArrowTurnBack;
    public GameObject MenuOptions;
    public Image BonfireReal;
    public GameObject UIBlur;
    private float speedColor;
    private Color initialColor;
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
        LeanTween.move(MainMenuRect, new (0f, -700f), .75f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(OptionsMenu, new (0, 0), .75f).setEase(LeanTweenType.easeInOutQuad); 
        LeanTween.value(gameObject, updateBlur, 0, 30f, .5f).setEase(LeanTweenType.easeInOutQuad);
    }
    public void ExitButtonClick() 
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    public void ArrowButtonClick() 
    {
        LeanTween.move(MainMenuRect, new (0, -330.6f), .75f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(OptionsMenu, new (0, 1054), .75f).setEase(LeanTweenType.easeInOutQuad);
    }

    void updateBlur(float val)
    {
        BlurEffect.blurRadius = val;
    }

    void Update()
    {  
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetAnimateOptionsClose = true;
        } 

        if (SetAnimateOptionsClose)
        {
            SetMenuOptions = true;
            FunctionsMenu.AnimateVectorLerp(MainMenuRect, new (0, -330.6f), 8);
            FunctionsMenu.AnimateVectorLerp(OptionsMenu, new (0, 1054), 8);
            SetAnimateOptionsClose = false;
        }

    }
}
