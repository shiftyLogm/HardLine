using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AdjustBloom : MonoBehaviour
{
    [SerializeField] private Volume volumePostProcessing;
    private Bloom bloomValue;
    void Start()
    {   
        volumePostProcessing.GetComponent<Volume>();
        volumePostProcessing.profile.TryGet(out bloomValue);
    }
 
    [ContextMenu("test")]

    void Update()
    {
        bloomValue.intensity.value = 1; 
    }
}
