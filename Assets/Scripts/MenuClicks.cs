using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClicks : MonoBehaviour
{
    [SerializeField] public GameObject MainMenu;
    private float _Speed = 1.5f;
    private Vector2 m_NewPosition;
    bool SetAnimateOptions = false;

    public void NewGameButtonClick(){
        Debug.Log("New");
    }
    public void OptionsButtonClick(){
        Debug.Log("Options");
        SetAnimateOptions = true;
    }
    public void ExitButtonClick() {
        Application.Quit();
        Debug.Log("Exit");
    }

    void Update(){
        if (SetAnimateOptions) {
            m_NewPosition = new Vector2(0.0f, -300.0f);
            MainMenu.transform.Translate(m_NewPosition * Time.deltaTime * _Speed);
        }
    }
}
