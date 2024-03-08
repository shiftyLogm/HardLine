using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
public class MenuClicks : MonoBehaviour
{
    private bool SetAnimateOptions = false;
    private bool EscKeyAnimateOptions = false;
    public RectTransform MainMenuRect;

    [SerializeField] public Material Blur;
    [SerializeField] public RectTransform OptionsMenu;
    [SerializeField] public GameObject ArrowTurnBack;
    void Start()
    {
        MainMenuRect = GetComponent<RectTransform>();
        Blur.SetFloat("_Size", 0f);
        MainMenuRect.anchoredPosition = new Vector2(0, -330.6f);
        OptionsMenu.anchoredPosition = new Vector2(0, 1054);
        ArrowTurnBack.transform.localScale = new Vector3(2, 1, 2);
    }

    public void NewGameButtonClick()
    {
        Debug.Log("New");
    }
    public void OptionsButtonClick()
    {
        SetAnimateOptions = true;
    }
    public void ExitButtonClick() 
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    public void ArrowButtonClick()
    {
        SetAnimateOptions = false;
    }

    void Update(){
        if (SetAnimateOptions) 
        {
            FunctionsMenu.AnimateVectorLerp(MainMenuRect, new (0f, -700f), 8);

            float initialSize = Blur.GetFloat("_Size");
            float finalSize = 4f; 
            int _speedSize = 4;
            initialSize = Mathf.Lerp(initialSize, finalSize, Time.deltaTime * _speedSize);
            Blur.SetFloat("_Size", initialSize);

            FunctionsMenu.AnimateVectorLerp(OptionsMenu, new (0, 0), 8);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetAnimateOptions = false;
        }

        if (SetAnimateOptions == false)
        {
            float initialSize = Mathf.Lerp(Blur.GetFloat("_Size"), 0f, Time.deltaTime * 4);
            Blur.SetFloat("_Size", initialSize);
            FunctionsMenu.AnimateVectorLerp(MainMenuRect, new (0, -330.6f), 8);
            FunctionsMenu.AnimateVectorLerp(OptionsMenu,new (0, 1054), 8);
            ArrowTurnBack.transform.localScale = new Vector3(2, 1, 2);
        }
    }
}
