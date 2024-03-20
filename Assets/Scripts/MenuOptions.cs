using UnityEngine;

public class MenuOptions : MonoBehaviour
{
    public GameObject PageGeneral;
    public GameObject PageSounds;
    public GameObject PageControls;

    public void Start() => PageGeneralClick();    
    public void PageGeneralClick()
    {
        PageSounds.SetActive(false);
        PageGeneral.SetActive(true); 
        PageControls.SetActive(false); 
    }

    public void PageSoundsClick()
    {
        PageSounds.SetActive(true);
        PageGeneral.SetActive(false);    
        PageControls.SetActive(false);
    }

    public void PageControlsClick()
    {
        PageSounds.SetActive(false);
        PageGeneral.SetActive(false);    
        PageControls.SetActive(true);
    }

    void Update()
    {
        if (MenuClicks.resetOptions) PageGeneralClick();
    }
}
