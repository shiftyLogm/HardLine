using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class OptionsNavigate : MonoBehaviour
{
    public static bool setNavigateOptions;
    public GameObject generalConfigs;
    public GameObject soundConfigs;

    public void OnHover() 
    {
        EventSystem.current.SetSelectedGameObject(null);
        setNavigateOptions = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && setNavigateOptions || Input.GetKeyDown(KeyCode.DownArrow) && setNavigateOptions)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(
                Input.GetKeyDown(KeyCode.UpArrow) ? generalConfigs : soundConfigs
            );
            setNavigateOptions = false;
        }   
    }
}
