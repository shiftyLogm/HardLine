using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Infos : MonoBehaviour
{
    public static bool fullScreenValue;
    [SerializeField] Toggle _fullScreenObj;

    public static float brightnessValue;
    [SerializeField] Slider _brightnessObj;
    void Start()
    {
        _fullScreenObj.isOn = fullScreenValue;
        _brightnessObj.value = brightnessValue;
    }
}
