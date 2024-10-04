using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject _pauseMenu;

    private RectTransform _pauseMenuPos;
    private bool _isActive;
    private bool _coroutineIsRunning;

    private void Start()
    {
        // Atribuindo o Rect para manuseio da posicao
        _pauseMenuPos = _pauseMenu.GetComponent<RectTransform>();
        _isActive = false;
    }

    public void moveMenu(int pos)
    {
        if (!_coroutineIsRunning) StartCoroutine(timeToOpenOrClose(pos));
    }

    private IEnumerator timeToOpenOrClose(int pos)
    {
        _coroutineIsRunning = true;
        LeanTween.move(_pauseMenuPos, new(0, pos), .6f).setEase(LeanTweenType.easeInOutQuad); 
        
        yield return new WaitForSeconds(0.6f);
        
        _isActive = pos == 0 ? true : false;
        _coroutineIsRunning = false;

        yield return null;
    }


    private void Update()
    {
        if (!_isActive && Input.GetKeyDown(KeyCode.Escape)) 
        {
            moveMenu(0);
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            moveMenu(1000);
        }
    }
}
