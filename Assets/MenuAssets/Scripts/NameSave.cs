using TMPro;
using UnityEngine;

public class NameSave : MonoBehaviour
{
    public TMP_InputField saveName;
    public TextMeshProUGUI Normaltext;
    public TextMeshProUGUI Placeholder;
    private string Placeholdertext;

    void Start()
    {
        saveName = GetComponent<TMP_InputField>();
        Placeholdertext = Placeholder.text;
    }

    public void selectInputField() => Placeholder.text = "";
    public void deselectInputField()
    {
        if (Normaltext.text.Length <= 1) Placeholder.text = Placeholdertext;
    }

    void Update()
    {
        if (MoveNewGameTabs.clearText) Normaltext.SetText("");
    }
}
