using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.UI;
using Microsoft.Unity.VisualStudio.Editor;
public class NewGameConfirmTab : MonoBehaviour
{
    public GameObject tabConfirm;
    public TextMeshProUGUI className;
    public TextMeshProUGUI nameSave;
    public Image imageClass; 
    public UnityEngine.UI.Image blackScreen;
    private NewGameData newgameInfo;

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
        className.text = newgameInfo.classNames[newgameInfo.classIdx].ToUpper();
        nameSave.text = newgameInfo.NameSaveGame;
        blackScreen.enabled = true;
    }

    public void NoOption() 
    {   
        blackScreen.enabled = false;
        tabConfirm.SetActive(false);
    }

}
