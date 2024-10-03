using UnityEngine;
using UnityEngine.UI;

public class FullScreenCheck : MonoBehaviour
{
    [SerializeField] private Toggle fullScreen;

    public void changeToggle()
    {
        fullScreen.isOn = !fullScreen.isOn;
    }

}
