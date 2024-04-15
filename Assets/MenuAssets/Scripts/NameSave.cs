using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameSave : MonoBehaviour
{
    public TMP_InputField saveName;

    void Start()
    {
        saveName = GetComponent<TMP_InputField>();
    }

    void setFocusInputField() 
    {
        saveName.Select();
        saveName.ActivateInputField();
    }

    void Update()
    {
        if (MoveNewGameTabs._setMoveNG) Invoke("setFocusInputField", 1.1f);
    }
    
}
