using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public static class Helper
{
    public static int DictKeyToIndex<Tkey, TValue>(Dictionary<Tkey, TValue> dict, string value)
    {
        int index = 0;
        foreach(var idx in dict)
        {
            if(idx.Key.ToString() == value)
            {
                return index;
            }
            index++;
        }

        return 0;
    }
}
