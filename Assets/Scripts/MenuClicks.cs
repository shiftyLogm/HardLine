using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuClicks : MonoBehaviour
{
    public void ExitClick(){
        Debug.Log("Teste");
        Application.Quit();
    }
}
