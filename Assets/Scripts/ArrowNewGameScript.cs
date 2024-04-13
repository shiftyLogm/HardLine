using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;
using System.Linq;
public class ArrowNewGameScript : MonoBehaviour, IPointerClickHandler
{   
    private Action lambdaExec; 
    public void OnPointerClick(PointerEventData eventData) => TurnOption();
    private MenuClicks turnMenu;
    private MoveNewGameTabs turnNewGame;

    void Start()
    {
        turnMenu = FindObjectOfType<MenuClicks>();
        turnNewGame = FindObjectOfType<MoveNewGameTabs>();
    }
     
    private void TurnOption()
    {
        Dictionary<Action, bool> dictArrowOptions = new()
        {
            {() => turnMenu.ArrowButtonClickNewGame(), MenuClicks.SetMenuNemGame},
            {() => turnNewGame.turnTabs(), MoveNewGameTabs._setMoveNG}
        };

        lambdaExec = findKeyArrow(dictArrowOptions, true);
        lambdaExec.Invoke();
    }

    private Action findKeyArrow(Dictionary<Action, bool> dict, bool value)
    {
        var idx = dict.FirstOrDefault(x => x.Value == value);
        return idx.Key;
    }

}
