using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string nextSceneName;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get {
            if(!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }        
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {

    }

    public void LoadNextScene(string nextSceneName){
        this.nextSceneName = nextSceneName;
        StartCoroutine(LoadMyAsyncScene());
    }

    IEnumerator LoadMyAsyncScene()
    {    
        // AsyncOperation�� ���� Scene Load ������ �� �� �ִ�.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);

        // Scene�� �ҷ����� ���� �Ϸ�Ǹ�, AsyncOperation�� isDone ���°� �ȴ�.
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }

    public void ReturnTitleScene() {
        SceneManager.LoadScene("TitleScene");        
    }

    public void LoadNextLevelScene() {
        if (nextSceneName == "GamePhaseStage1") {
            LoadNextScene("GamePhaseStage2");
        }
        else if (nextSceneName == "GamePhaseStage2") {
            LoadNextScene("GamePhaseStage3");
        }
        else if (nextSceneName == "GamePhaseStage3") {
            LoadNextScene("GamePhaseStage4");
        }
        else if (nextSceneName == "GamePhaseStage4") {
            LoadNextScene("GamePhaseStage5");
        }        
    }
}
