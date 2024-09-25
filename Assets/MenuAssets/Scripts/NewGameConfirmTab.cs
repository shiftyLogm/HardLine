using UnityEngine;
using TMPro;

public class NewGameConfirmTab : MonoBehaviour
{
    public GameObject tabConfirm;
    public TextMeshProUGUI className;
    public TextMeshProUGUI nameSave;
    public UnityEngine.UI.Image imageClass; 
    public UnityEngine.UI.Image blackScreen;
    public static NewGameData newgameInfo;
    void Start()
    {
        newgameInfo = FindObjectOfType<NewGameData>();
        tabConfirm.SetActive(false);
        blackScreen.enabled = false;
    }

    public void ShowTab()
    {
        tabConfirm.SetActive(true);
        newgameInfo.GetDataNewGame();
        className.text = "CLASS: " + newgameInfo.classNames[newgameInfo.classIdx].ToUpper();
        nameSave.text = "NAME: " + newgameInfo.NameSaveGame;
        blackScreen.enabled = true;
    }

    public void NoOption() 
    {   
        blackScreen.enabled = false;
        tabConfirm.SetActive(false);
    }
}
