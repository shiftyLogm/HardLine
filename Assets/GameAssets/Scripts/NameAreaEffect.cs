using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameAreaEffect : MonoBehaviour
{
    private TextMeshProUGUI areaName;
    void Start() 
    {
        areaName = GetComponent<TextMeshProUGUI>();
        areaName.text = "";
        StartCoroutine(AreaNameEffect("Forgotten Lair"));
    }
    
    public IEnumerator AreaNameEffect(string text)
    {
        float typingSpeed = 0.1f;

        foreach(char letter in text.ToCharArray())
        {
            areaName.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(3);
        typingSpeed -= 0.04f;

        for (int i = areaName.text.Length - 1; i >= 0; i--)
        {
            areaName.text = areaName.text.Remove(i, 1);
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
