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
            {"NameClass1", "C1"},
            {"NameClass2", "C2"},
            {"NameClass3", "C3"}
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
