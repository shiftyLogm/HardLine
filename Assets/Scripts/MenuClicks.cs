using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClicks : MonoBehaviour
{
    public void NewGameButtonClick(){
        Debug.Log("New");
    }
    public void OptionsButtonClick(){
        Debug.Log("Options");
    }
    public void ExitButtonClick() {
        Application.Quit();
        Debug.Log("Exit");
    }

}
