using System;
using UnityEngine;

public class MoveNewGameTabs : MonoBehaviour
{
    private RectTransform _tabPosition;
    private Vector3 _initialPosition;
    private Vector3 _finalPosition;
    private float _speed;
    private bool _setMoveNG;
    void Start()
    {
        _tabPosition = GetComponent<RectTransform>();
        _initialPosition = _tabPosition.anchoredPosition;
        _finalPosition = new Vector3(-1837, 234);
        _speed = .75f;
    }

    public void moveTabs()
    {
        LeanTween.move(_tabPosition, _finalPosition, _speed).setEase(LeanTweenType.easeInOutCubic);
        _setMoveNG = true;
        MenuClicks.SetMenuNemGame = false;
    }
    private void turnTabs() 
    {
        LeanTween.move(_tabPosition, _initialPosition, _speed).setEase(LeanTweenType.easeInOutCubic);
        _setMoveNG = false;
    }
    void Update()
    {
        Debug.Log(_setMoveNG);
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (_setMoveNG) turnTabs();
            else MenuClicks.SetMenuNemGame = true;
        }
    }
}