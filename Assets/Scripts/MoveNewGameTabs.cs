using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class MoveNewGameTabs : MonoBehaviour
{
    private RectTransform _tabPosition;
    private Vector3 _initialPosition;
    private Vector3 _finalPosition;
    private float _speed;
    private Vector3 initialScaleTab; 
    private Vector3 TargetScaleTab;
    private GameObject[] tabNG;
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
        tabNG = GameObject.FindGameObjectsWithTag("TabNG");
        initialScaleTab = tabNG[0].transform.localScale;
        TargetScaleTab = tabNG[0].GetComponent<TransformHover>().targetScale;
    }
    private void setListEventTrigger(List<EventTrigger> events, bool value)
    {
        foreach(var idx in events) idx.enabled = value;
    }

    private void setListEventHover(List<TransformHover> events, Vector3 scale)
    {
        foreach(var idx in events)
        {
            var Scales = idx.GetComponent<TransformHover>();
            Scales.scaleHover = scale;
            Scales.targetScale = scale;
        }
    }
    public void moveTabs()
    {
        LeanTween.move(_tabPosition, _finalPosition, _speed).setEase(LeanTweenType.easeInOutCubic);
        _setMoveNG = true;
        MenuClicks.SetMenuNemGame = false;
        setListEventTrigger(eventsTrigger, false);
        setListEventHover(eventsHover, initialScaleTab);
    }
    public void turnTabs() 
    {
        LeanTween.move(_tabPosition, _initialPosition, _speed).setEase(LeanTweenType.easeInOutCubic);
        _setMoveNG = false;
        MenuClicks.SetMenuNemGame = true;
        setListEventTrigger(eventsTrigger, true);
        foreach(var Tab in tabNG) Tab.GetComponent<TransformHover>().targetScale = TargetScaleTab; 
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