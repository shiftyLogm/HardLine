using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Threading;

public class MoveNewGameTabs : MonoBehaviour
{
    private RectTransform _tabPosition;
    private float _speedExit;
    private float _speedEnter;
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
    private HoverTabsClassNG objClassNG;
    public GameObject[] classChangeobj;
    public static bool clearText = false;
    public bool waitForTurnTabs = false;
    void Start()
    {
        _tabPosition = GetComponent<RectTransform>();
        _initialPosition = _tabPosition.anchoredPosition;
        _finalPosition = new Vector3(-1837, 234);
        _initialNewSaveInputPos = new Vector3(0, -678);
        _targetNewSaveInputPos = new Vector3(0, -342);
        NameSaveInput.anchoredPosition = _initialNewSaveInputPos; 
        _speedEnter = .65f;
        _speedExit = .9f;
        setListEventTrigger(eventsTrigger, true);
        tabNG = GameObject.FindGameObjectsWithTag("TabNG");
        _initialScaleTab = tabNG[0].transform.localScale;
        _TargetScaleTab = tabNG[0].GetComponent<TransformHover>().targetScale;
        objClassNG = FindObjectOfType<HoverTabsClassNG>();
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

    void enablewaitForTurnTabs() => waitForTurnTabs = true;
    public void moveTabs()
    {
        LeanTween.move(_tabPosition, _finalPosition, _speedEnter).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(NameSaveInput, _targetNewSaveInputPos, 1).setEase(LeanTweenType.easeInOutCubic);
        _setMoveNG = true;
        MenuClicks.SetMenuNemGame = false;
        setListEventTrigger(eventsTrigger, false);
        setListEventHover(eventsHover, _initialScaleTab);
        objClassNG.classAnimation(-100, objClassNG.flexSpeedEnter);
        objClassNG.turnTabsNormal(true);
        clearText = false;
        Invoke("enablewaitForTurnTabs", 1);
    }
    public void turnTabs() 
    {
        objClassNG.turnTabsNormal(false);
        LeanTween.move(_tabPosition, _initialPosition, _speedExit).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.move(NameSaveInput, _initialNewSaveInputPos, .5f).setEase(LeanTweenType.easeInOutCubic);
        _setMoveNG = false;
        MenuClicks.SetMenuNemGame = true;
        setListEventTrigger(eventsTrigger, true);
        foreach(var Tab in tabNG) Tab.GetComponent<TransformHover>().targetScale = _TargetScaleTab;
        objClassNG.classAnimation(620, objClassNG.flexSpeedExit);
        objClassNG.StartGameBTN.SetActive(false);
        objClassNG.changeClassBTN.SetActive(false);
        clearText = true;
        waitForTurnTabs = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && waitForTurnTabs) if (_setMoveNG) turnTabs();
    }

    public void DisableUpdate() => this.enabled = false;
    public void EnableUpdate() => this.enabled = true;
}