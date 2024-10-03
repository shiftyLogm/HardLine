using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class Helper
{
    public static string FindKey(Dictionary<string, bool> dict, bool value)
    {
        var idx = dict.FirstOrDefault(x => x.Value == value);
        return idx.Key;
    }

    public static State FindKeyState(Dictionary<State, bool> dict, bool value)
    {
        var idx = dict.FirstOrDefault(x => x.Value == value);
        return idx.Key;
    }

    public static T RandomIndex<T>(this T[] array)
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }

}
