using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AdjustBloom : MonoBehaviour
{
    [SerializeField] private Volume volumePostProcessing;
    private Bloom bloomValue;
    public static float valueIntensity;
    void Start()
    {   
        volumePostProcessing.GetComponent<Volume>();
        volumePostProcessing.profile.TryGet(out bloomValue);
    }
    void Update() => bloomValue.intensity.value = valueIntensity;
}
