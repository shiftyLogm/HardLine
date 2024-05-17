using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameData : MonoBehaviour
{
    private Dictionary<string, string> classNames;
    private string classIdx;
    private string NameSaveGame;
    private NameSave objNameSave;
    void Start()
    {
        objNameSave = FindObjectOfType<NameSave>();
        classNames = new()
        {
            {"NameClass1", "Warrior"},
            {"NameClass2", "Archer"},
            {"NameClass3", "Mage"}
        };
    }
    public void GetDataNewGame()
    {
        NameSaveGame = objNameSave.Normaltext.text.ToUpper();
        classIdx = HoverTabsClassNG.ClassNewGameData;
        Debug.Log(classNames[classIdx]);
        Debug.Log(NameSaveGame);
    }
}
