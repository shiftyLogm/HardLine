using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuClicks : MonoBehaviour
{
    private bool SetAnimateOptions = false;
    private Vector2 newPosition;
    private Vector2 initialPosition;

    private RectTransform MainMenuRect;

    [SerializeField] private Material Blur;
    void Start(){
        MainMenuRect = GetComponent<RectTransform>();
        Blur.SetFloat("_Size", 0f);
    }

    public void NewGameButtonClick(){
        Debug.Log("New");
    }
    public void OptionsButtonClick(){
        SetAnimateOptions = true;
    }
    public void ExitButtonClick() {
        Application.Quit();
        Debug.Log("Exit");
    }

    void Update(){

        if (SetAnimateOptions) {

            initialPosition = MainMenuRect.anchoredPosition;
            newPosition = new Vector2(0, -700);
            float _speed = Time.deltaTime * 8;
            MainMenuRect.anchoredPosition = Vector2.Lerp(initialPosition, newPosition, _speed);

            float blursize_transiction = Mathf.Lerp(0f, 2f, Time.deltaTime);
            Blur.SetFloat("_Size", blursize_transiction);
        }
    }
}
