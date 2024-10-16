using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Infos : MonoBehaviour
{
    public static bool fullScreenValue;
    [SerializeField] Toggle _fullScreenObj;

    public static float brightnessValue;
    [SerializeField] Slider _brightnessObj;

    public static float master;
    [SerializeField] Slider masterSlide;

    public static float music;
    [SerializeField] Slider musicSlide;

    public static float effect;
    [SerializeField] Slider effectSlide;
    void Start()
    {
        _fullScreenObj.isOn = fullScreenValue;
        _brightnessObj.value = brightnessValue;
        masterSlide.value = master;
        musicSlide.value = music;
        effectSlide.value = effect;
    }
}
