using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SetPostProcessingValues : MonoBehaviour
{
    public UnityEngine.UI.Slider sliderBrightness;
    public Volume volumePostProcessing;
    private ColorAdjustments brightness;
    
    void Start()
    {
        volumePostProcessing.profile.TryGet<ColorAdjustments>(out brightness);
        sliderBrightness.minValue = -1;
        sliderBrightness.maxValue = 1;
        brightness.postExposure.value = 0;
    }

    public void ChangeBrightness() => brightness.postExposure.value = sliderBrightness.value;
}
