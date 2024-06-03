using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewGameNavigation : MonoBehaviour
{
    public static bool navigateNewGame;
    public GameObject ng1;
    public GameObject ng2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && navigateNewGame || Input.GetKeyDown(KeyCode.DownArrow) && navigateNewGame)
        {
            EventSystem.current.SetSelectedGameObject(
                Input.GetKeyDown(KeyCode.UpArrow) ? ng1 :
                Input.GetKeyDown(KeyCode.DownArrow) ? ng2 : null);
            navigateNewGame = false;
        }
    }
}
