using NovaSamples.Effects;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
public class MenuClicks : MonoBehaviour
{
    private bool SetAnimateOptions = false;
    public static bool SetMenuOptions;
    public RectTransform MainMenuRect;

    public RectTransform OptionsMenu;
    public GameObject ArrowTurnBack;
    public GameObject MenuOptions;
    public Image BonfireReal;
    public GameObject UIBlur;
    private float speedColor;
    private Color initialColor;
    private bool SetBlurAnimation;
    
    void Start()
    {
        MainMenuRect = GetComponent<RectTransform>();
        MainMenuRect.anchoredPosition = new Vector2(0, -330.6f);
        OptionsMenu.anchoredPosition = new Vector2(0, 1054);
        ArrowTurnBack.transform.localScale = new Vector3(2, 1, 2);
        speedColor = 10f;
        initialColor = BonfireReal.color;
    }

    public void NewGameButtonClick()
    {
        Debug.Log("New");
    }
    public void OptionsButtonClick() => SetAnimateOptions = true;
    public void ExitButtonClick() 
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    public void ArrowButtonClick() => SetAnimateOptions = false;

    void updateBlur(float val)
    {
        BlurEffect.blurRadius = val;
    }

    void updateBlurValue()
    {
        if (SetBlurAnimation)
        {
            LeanTween.value(gameObject, updateBlur, 0, 30f, 1f).setEase(LeanTweenType.easeSpring);
        } 
        else
        {
            LeanTween.value(gameObject, updateBlur, 0, 30f, 1f).setEase(LeanTweenType.easeSpring);
        }
    }

    void Update()
    {
        if (SetAnimateOptions) 
        {   
            SetMenuOptions = false;
            BonfireReal.color = Color.Lerp(BonfireReal.color, new(0, 0, 0 ,0), speedColor * Time.deltaTime);
            FunctionsMenu.AnimateVectorLerp(MainMenuRect, new (0f, -700f), 8);
            FunctionsMenu.AnimateVectorLerp(OptionsMenu, new (0, 0), 8); 
            updateBlurValue();
        }   

        Debug.Log(BlurEffect.blurRadius);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetAnimateOptions = false;
        } 

        if (SetAnimateOptions == false)
        {
            SetBlurAnimation = false;
            SetMenuOptions = true;
            BonfireReal.color = Color.Lerp(BonfireReal.color, initialColor, speedColor * Time.deltaTime);
            FunctionsMenu.AnimateVectorLerp(MainMenuRect, new (0, -330.6f), 8);
            FunctionsMenu.AnimateVectorLerp(OptionsMenu, new (0, 1054), 8);
        }

    }
}
