using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameData : MonoBehaviour
{
    public Dictionary<string, string> classNames;
    public string classIdx;
    public string NameSaveGame;
    public NameSave objNameSave;
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
    }
}
