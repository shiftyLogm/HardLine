using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Rendering;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.Linq;


public class MenuClicks : MonoBehaviour
{
    public Volume globalVolume;
    public static bool SetMenuNemGame;
    public static bool SetMenuOptions;
    public static bool resetOptions = false;
    public RectTransform MainMenuRect;
    public RectTransform OptionsMenu;
    public GameObject ArrowTurnBack;
    public GameObject MenuOptions;
    public RectTransform PanelNewGame;
    public RectTransform PanelMainMenu;
    public RectTransform GameLogo;
    private Vector3 InitialVectorButtonMenu;
    private Vector3 TargetVectorButtonMenu;
    private GameObject[] buttonsMenu;
    public List<Button> buttonComponents = new List<Button>();
    public List<ButtonMenuHover> eventsHover = new List<ButtonMenuHover>();
    private bool waitForNewGameScreen = false;
    private bool waitForOptionsScreen = false;
    private bool ExitGameEsc;
    public GameObject ExitGameScreen;
    public TextMeshProUGUI YesText;
    public TextMeshProUGUI NoText;
    public TextMeshProUGUI TextExitGame;
    public Color fadeOutColor = new Color(255, 255, 255, 0);
    public Image BlackScreen;
    public GameObject newgameButton;
    public GameObject optiongameButton;
    public static bool setNavigate = true;
    private bool turnExitScreenWithEsc = false;
    private Dictionary<Action, bool> dictEscOptions;
    private HoverTabsClassNG hoverTabsObj;

    void Start()
    {
        MainMenuRect = GetComponent<RectTransform>();
        MainMenuRect.anchoredPosition = new Vector2(0, -330.6f);
        OptionsMenu.anchoredPosition = new Vector2(0, 1054);
        ArrowTurnBack.transform.localScale = new Vector3(2, 1, 2);
        PanelNewGame.anchoredPosition = new Vector2(-1920, -.46f);
        PanelMainMenu.anchoredPosition = new Vector2(0, 0);
        buttonsMenu = GameObject.FindGameObjectsWithTag("ButtonMenu");
        InitialVectorButtonMenu = buttonsMenu[0].transform.localScale;
        TargetVectorButtonMenu = new Vector3(2.35f, 2.35f, 1.175f);
        GameLogo.anchoredPosition = new Vector2(0, 211);
        ExitGameScreen.SetActive(false);
        ExitGameEsc = true;
        BlackScreen.enabled = false;
        hoverTabsObj = FindObjectOfType<HoverTabsClassNG>();
        hoverTabsObj.enabled = false;
    }
    
    private void DisableAndEnableOnClick(List<Button> list, bool value)
    {
        foreach(var button in list) button.interactable = value;
    }

    private void DisableHoverButton(List<ButtonMenuHover> list, Color color, Vector3 vector)
    {
        foreach(var idx in list)
        {
            var a = idx.GetComponent<ButtonMenuHover>();
            a.hoverColor = color;
            a.hoverVector = vector;
        }
    }

    private void turnButtonsNormal()
    {
        foreach (var button in buttonsMenu)
        {
            var hoverComp = button.GetComponent<ButtonMenuHover>();
            hoverComp.hoverColor = Color.yellow;
            hoverComp.hoverVector = TargetVectorButtonMenu;
        }
    }

    void EnableWaitForNewGameScreen() => waitForNewGameScreen = true;
    public void NewGameButtonClick()
    {
        LeanTween.move(PanelNewGame, new Vector2(-4, -.46f), 1f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(PanelMainMenu, new Vector2(1920, 0), 1f).setEase(LeanTweenType.easeInOutCubic);
        SetMenuNemGame = true;
        DisableAndEnableOnClick(buttonComponents, false);
        DisableHoverButton(eventsHover, Color.white, InitialVectorButtonMenu);
        Invoke("EnableWaitForNewGameScreen", 1f);
        ExitGameEsc = false;
        setNavigate = true;
        NewGameNavigation.navigateNewGame = true;
        hoverTabsObj.enabled = true;
    }

    void EnableWaitForOptionsScreen() => waitForOptionsScreen = true;

    public void OptionsButtonClick()
    {
        LeanTween.move(GameLogo, new Vector2(0, -700), .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(MainMenuRect, new (0f, -1241f), .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(OptionsMenu, new(0, 0), .6f).setEase(LeanTweenType.easeInOutQuad); 
        SetMenuOptions = true;
        DisableAndEnableOnClick(buttonComponents, false);
        DisableHoverButton(eventsHover, Color.white, InitialVectorButtonMenu);
        Invoke("EnableWaitForOptionsScreen", .5f);
        ExitGameEsc = false;
        setNavigate = true;
        OptionsNavigate.setNavigateOptions = true;
        HoverTabsClassNG.navigateTabsNewGame = false;
    }

    void EnableWaitForExitScreen() => ExitGameEsc = true;
    void EnableturnExitScreenWithEsc() => turnExitScreenWithEsc = true;

    public void ExitButtonClick() 
    {
        ExitGameEsc = false;
        ExitGameScreen.SetActive(true);
        setValuesExit(true);
        NoText.GetComponentInParent<ButtonMenuHover>().TargetColor = Color.white;
        YesText.GetComponentInParent<ButtonMenuHover>().TargetColor = Color.white;
        ExitGameScreen.GetComponent<Image>().color = Color.white;
        YesText.color = Color.white;
        NoText.color = Color.white;
        TextExitGame.color = Color.white;
        ExitGameScreen.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        DisableAndEnableOnClick(buttonComponents, false);
        DisableHoverButton(eventsHover, Color.white, InitialVectorButtonMenu);
        BlackScreen.color = new Color(0, 0, 0, a: 0.98f);
        BlackScreen.enabled = true;
        setNavigate = true;
        Invoke("EnableturnExitScreenWithEsc", 0.2f);
        ExitGameScreenNavigate.setNavigateExitScreen = true;
    }

    public void YesOption() => Application.Quit();
    
    private IEnumerator fadeOutExitGame()
    {
        float elapsedTime = 0.0f;
        float _speed = 0.3f;

        while (elapsedTime < _speed)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / _speed);
            ExitGameScreen.GetComponent<Image>().color = new Color(255, 255, 255, alpha);
            YesText.color = new Color(255, 255, 255, alpha);
            NoText.color = new Color(255, 255, 255, alpha);
            TextExitGame.color = new Color(255, 255, 255, alpha);
            BlackScreen.color = new Color(0, 0, 0, alpha - 0.02f);
            yield return null;
        }
    }

    private IEnumerator TurnHoverNoOption()
    { 
        var NoTextHover = NoText.GetComponentInParent<ButtonMenuHover>();
        var YesTextHover = YesText.GetComponentInParent<ButtonMenuHover>();
        NoTextHover.TargetColor = new Color(84, 185, 0);
        NoTextHover.TargetVector = new Vector2(2, 2);
        YesTextHover.TargetColor = new Color(195, 4, 0);
        YesTextHover.TargetVector = new Vector2(2, 2);
        yield return null;
    }

    private void setValuesExit(bool value)
    {
        YesText.GetComponentInParent<ButtonMenuHover>().enabled = value;
        YesText.GetComponentInParent<Button>().enabled = value;
        NoText.GetComponentInParent<ButtonMenuHover>().enabled = value;
        NoText.GetComponentInParent<Button>().enabled = value;
    }  

    private IEnumerator NoOptionCoroutine()
    {
        yield return StartCoroutine(TurnHoverNoOption());
        yield return StartCoroutine(fadeOutExitGame());
        setValuesExit(false);
        ExitGameScreen.SetActive(false);
        ExitGameEsc = true;
        BlackScreen.enabled = false;
    }
    public void NoOption()
    {          
        DisableAndEnableOnClick(buttonComponents, true);
        turnButtonsNormal();
        StartCoroutine(NoOptionCoroutine());
        turnExitScreenWithEsc = false;     
        ExitGameScreenNavigate.setNavigateExitScreen = false;
    }

    public void ArrowButtonClick() 
    {
        LeanTween.move(GameLogo, new Vector2(0, 211), .5f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(MainMenuRect, new (0, -330.6f), .5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(OptionsMenu, new(0, 1054), .5f).setEase(LeanTweenType.easeInOutQuad);
        SetMenuOptions = false;
        waitForOptionsScreen = false;
        DisableAndEnableOnClick(buttonComponents, true);
        turnButtonsNormal();
        Invoke("EnableWaitForExitScreen", .5f);
        EventSystem.current.SetSelectedGameObject(null);
        setNavigate = true;
        OptionsNavigate.setNavigateOptions = false;
        hoverTabsObj.enabled = false;
    }

    public void ArrowButtonClickNewGame()
    {
        LeanTween.move(PanelMainMenu, new Vector2(0, 0), 1f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(PanelNewGame, new Vector2(-1920, -.46f), 1f).setEase(LeanTweenType.easeInOutCubic);
        SetMenuNemGame = false;
        waitForNewGameScreen = false;
        EventSystem.current.SetSelectedGameObject(null);
        setNavigate = true;
        DisableAndEnableOnClick(buttonComponents, true);
        turnButtonsNormal();
        Invoke("EnableWaitForExitScreen", 1);
        NewGameNavigation.navigateNewGame = false;
    }

    private Action findKeyToEsc(Dictionary<Action, bool> dict, bool value)
    {
        var idx = dict.FirstOrDefault(x => x.Value == value);
        return idx.Key;
    }

    private void EscValue()
    {
        try
        {
            dictEscOptions = new()
            {
                {() => ArrowButtonClick(), SetMenuOptions && waitForOptionsScreen && KeyRebindingWait.setRebindingKeys},
                {() => ArrowButtonClickNewGame(), SetMenuNemGame && waitForNewGameScreen},
                {() => ExitButtonClick(), ExitGameEsc},
                {() => NoOption(), turnExitScreenWithEsc}
            };

            findKeyToEsc(dictEscOptions, true).Invoke();
        }

        catch(NullReferenceException) {}

    }

    void Update()
    {  
        resetOptions = OptionsMenu.anchoredPosition.y > 1000 ? true : false;

        if (Input.GetKey(KeyCode.Escape)) EscValue();

        if (Input.GetKeyDown(KeyCode.UpArrow) && setNavigate || Input.GetKeyDown(KeyCode.DownArrow) && setNavigate)
        {
            EventSystem.current.SetSelectedGameObject(
                Input.GetKeyDown(KeyCode.UpArrow) ? newgameButton :
                Input.GetKeyDown(KeyCode.DownArrow) ? optiongameButton : null);
            setNavigate = false;
        }
    }
};
