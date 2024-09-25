using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNewGame : MonoBehaviour
{
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

}
