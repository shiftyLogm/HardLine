using UnityEngine;

public class MenuOptions : MonoBehaviour
{
    public GameObject PageGeneral;
    public GameObject PageSounds;
    public GameObject PageControls;

    void Start()
    {  
        PageGeneral.SetActive(true);
        PageSounds.SetActive(false);
        PageControls.SetActive(false);
    }
    
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

    void Update(){
        if (MenuClicks.SetMenuOptions)
        {
            Start();
        }
    }
}
