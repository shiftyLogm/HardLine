using UnityEngine;
using UnityEngine.EventSystems;


public class ExitGameScreenNavigate : MonoBehaviour
{
    public static bool setNavigateExitScreen;
    public GameObject YesObject;
    public GameObject NoObject;

    void Update()
    {
        if ( (Input.GetKeyDown(KeyCode.LeftArrow) && setNavigateExitScreen) || (Input.GetKeyDown(KeyCode.RightArrow) && setNavigateExitScreen) )
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(
                Input.GetKeyDown(KeyCode.LeftArrow) ? YesObject : NoObject
            );
            setNavigateExitScreen = false;
        }
    }
}
