using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WriteStatusEffect : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI statusText;
    public float typingSpeed = 0.04f;
    
    void Start() => statusText = GetComponent<TextMeshProUGUI>();
    public IEnumerator DisplayLine(string Name, string Status)
    {
        nameText.text = "";
        statusText.text = "";

        foreach(char letter in Name.ToCharArray())
        {
            nameText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        foreach(char letter in Status.ToCharArray())
        {
            statusText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
