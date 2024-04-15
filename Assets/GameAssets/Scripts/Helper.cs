using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
}
