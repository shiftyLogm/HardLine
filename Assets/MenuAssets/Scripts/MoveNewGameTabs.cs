using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEditor;
using UnityEngine.UIElements;

public class MoveNewGameTabs : MonoBehaviour
{
    private RectTransform _tabPosition;
    private float _speed;
    public RectTransform NameSaveInput;
    private Vector3 _initialPosition;
    private Vector3 _finalPosition;
    private Vector3 _initialScaleTab; 
    private Vector3 _TargetScaleTab;
    private Vector3 _targetNewSaveInputPos;
    private Vector3 _initialNewSaveInputPos;
    private GameObject[] tabNG;
    public static bool _setMoveNG;
    public List<EventTrigger> eventsTrigger = new List<EventTrigger>();
    public List<TransformHover> eventsHover = new List<TransformHover>();
    private NameSave objNameSave;

    void Start()
    {
        _tabPosition = GetComponent<RectTransform>();
        _initialPosition = _tabPosition.anchoredPosition;
        _finalPosition = new Vector3(-1837, 234);
        _initialNewSaveInputPos = new Vector3(0, -678);
        _targetNewSaveInputPos = new Vector3(0, -283);
        NameSaveInput.anchoredPosition = _initialNewSaveInputPos; 
        _speed = .75f;
        setListEventTrigger(eventsTrigger, true);
        tabNG = GameObject.FindGameObjectsWithTag("TabNG");
        _initialScaleTab = tabNG[0].transform.localScale;
        _TargetScaleTab = tabNG[0].GetComponent<TransformHover>().targetScale;
        objNameSave = GameObject.FindObjectOfType<NameSave>();
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
        LeanTween.move(NameSaveInput, _targetNewSaveInputPos, 1).setEase(LeanTweenType.easeInOutCubic);
        _setMoveNG = true;
        MenuClicks.SetMenuNemGame = false;
        setListEventTrigger(eventsTrigger, false);
        setListEventHover(eventsHover, _initialScaleTab);
        objNameSave.setFocusInputField();
    }
    public void turnTabs() 
    {
        LeanTween.move(_tabPosition, _initialPosition, _speed).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(NameSaveInput, _initialNewSaveInputPos, .5f).setEase(LeanTweenType.easeInOutCubic);
        _setMoveNG = false;
        MenuClicks.SetMenuNemGame = true;
        setListEventTrigger(eventsTrigger, true);
        foreach(var Tab in tabNG) Tab.GetComponent<TransformHover>().targetScale = _TargetScaleTab; 
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