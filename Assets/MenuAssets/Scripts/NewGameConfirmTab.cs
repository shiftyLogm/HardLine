using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class NewGameConfirmTab : MonoBehaviour
{
    public GameObject tabConfirm;
    public TextMeshProUGUI className;
    public TextMeshProUGUI nameSave;
    public UnityEngine.UI.Image imageClass; 
    public GameObject blackScreen;
    public static NewGameData newgameInfo;
    public GameObject yesObject;
    public GameObject noObject;

    void Start()
    {
        newgameInfo = FindObjectOfType<NewGameData>();
    }

    public void ShowTab()
    {
        tabConfirm.SetActive(true);
        newgameInfo.GetDataNewGame();
        className.text = "CLASS: " + newgameInfo.classNames[newgameInfo.classIdx].ToUpper();
        nameSave.text = "NAME: " + newgameInfo.NameSaveGame;
        blackScreen.SetActive(true);
        HoverTabsClassNG.navigateTabsNewGame = false;
    }

    public void NoOption() 
    {
        blackScreen.SetActive(false);
        tabConfirm.SetActive(false);
        HoverTabsClassNG.navigateTabsNewGame = true;
    }

    void Update()
    {
        if (this.isActiveAndEnabled && Input.GetKeyDown(KeyCode.Escape))
        {
            NoOption();
        }

    }
}
