using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartNewGame : MonoBehaviour
{
    [SerializeField] private Toggle _fullScreenObj;
    [SerializeField] private Slider _brightnessObj;
    public void OnClick()
    {
        StartCoroutine(ChangeScene());
    }
    
    private IEnumerator ChangeScene()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);

        yield return new WaitForSeconds(1);
        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        PlayerClassesController.Instance.idxClass = NewGameConfirmTab.newgameInfo.classNames[NewGameConfirmTab.newgameInfo.classIdx];
        
        yield return null;
        SceneManager.UnloadSceneAsync("Menu");
    }

    void Update()
    {
        Infos.brightnessValue = _brightnessObj.value;
        Infos.fullScreenValue = _fullScreenObj.isOn;
    }

}
