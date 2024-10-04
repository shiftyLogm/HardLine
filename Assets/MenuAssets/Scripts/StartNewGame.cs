using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartNewGame : MonoBehaviour
{
    [SerializeField] private Toggle _fullScreenObj;
    [SerializeField] private Slider _brightnessObj;
    [SerializeField] private GameObject _blackScreenTransition;
    public void OnClick()
    {
        StartCoroutine(ChangeScene());
    }
    
    private IEnumerator ChangeScene()
    {
        _blackScreenTransition.SetActive(true);
        StartCoroutine(fadeOutGame());

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        _blackScreenTransition.SetActive(false);
        
        yield return new WaitForSeconds(1.5f);
        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        PlayerClassesController.Instance.idxClass = NewGameConfirmTab.newgameInfo.classNames[NewGameConfirmTab.newgameInfo.classIdx];
        
        yield return null;
        SceneManager.UnloadSceneAsync("Menu");
    }

    private IEnumerator fadeOutGame()
    {
        float elapsedTime = 0.0f;
        float _speed = 2f;

        while (elapsedTime < _speed)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsedTime / _speed);
            _blackScreenTransition.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    void Update()
    {
        Infos.brightnessValue = _brightnessObj.value;
        Infos.fullScreenValue = _fullScreenObj.isOn;
    }

}
