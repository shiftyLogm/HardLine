using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;

public class NewGameConfirmTab : MonoBehaviour
{
    public GameObject tabConfirm;
    public TextMeshProUGUI className;
    public TextMeshProUGUI nameSave;
    public UnityEngine.UI.Image imageClass; 
    public GameObject blackScreen;
    public static NewGameData newgameInfo;
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
    }

    public void NoOption() 
    {
        blackScreen.SetActive(false);
        tabConfirm.SetActive(false);
    }

    void Update()
    {
        if (this.isActiveAndEnabled && Input.GetKeyDown(KeyCode.Escape))
        {
            NoOption();
        }
    }
}
