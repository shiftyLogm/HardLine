using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NovaSamples.UIControls;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveNewGameTabs : MonoBehaviour
{
    private RectTransform _tabPosition;
    private Vector3 _initialPosition;
    private Vector3 _finalPosition;
    private float _speed;
    public static bool _setMoveNG;
    public List<EventTrigger> eventsTrigger = new List<EventTrigger>();
    public List<TransformHover> eventsHover = new List<TransformHover>();
    void Start()
    {
        _tabPosition = GetComponent<RectTransform>();
        _initialPosition = _tabPosition.anchoredPosition;
        _finalPosition = new Vector3(-1837, 234);
        _speed = .75f;
        setListEventTrigger(eventsTrigger, true);
        setListEventHover(eventsHover, true);
    }
    private void setListEventTrigger(List<EventTrigger> events, bool value)
    {
        foreach(var idx in events) idx.enabled = value;
    }

    private void setListEventHover(List<TransformHover> events, bool value)
    {
        foreach(var idx in events) idx.enabled = value;
    }
    public void moveTabs()
    {
        LeanTween.move(_tabPosition, _finalPosition, _speed).setEase(LeanTweenType.easeInOutCubic);
        _setMoveNG = true;
        MenuClicks.SetMenuNemGame = false;
        // setListEventTrigger(eventsTrigger, false);
        // setListEventHover(eventsHover, false);
    }
    public void turnTabs() 
    {
        LeanTween.move(_tabPosition, _initialPosition, _speed).setEase(LeanTweenType.easeInOutCubic);
        _setMoveNG = false;
        MenuClicks.SetMenuNemGame = true;
        // setListEventTrigger(eventsTrigger, true);
        // setListEventHover(eventsHover, true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (_setMoveNG) turnTabs();
            else MenuClicks.SetMenuNemGame = true;
        }
    }
}