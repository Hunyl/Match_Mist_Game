using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public string firstSceneName;
    Canvas canvas;
    GameObject introBG;
    GameObject gameOverBG;
    public float FadeTime = 2f;
    private void Awake() {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        introBG = GameObject.Find("IntroBG");
        gameOverBG = GameObject.Find("GameOverBG");

        // 첫번째 씬이 아니면 바로 introBG
        if (GameManager.Instance.nextSceneName != firstSceneName) {
            ShowIntroBG();
        }
    }

    public void ShowIntroBG() {
        introBG.GetComponent<AudioSource>().Play();
        StartCoroutine(DestroyIntroBG());        
    }
    
    IEnumerator FadeOutIntroBG()
    {
        while(introBG.GetComponent<Image>().color.a > 0)
        {
            Color color = introBG.GetComponent<Image>().color;
            color.a -= Time.deltaTime;
            introBG.GetComponent<Image>().color = color;
            yield return null;
        }
    }

    IEnumerator DestroyIntroBG()
    {
        yield return new WaitForSeconds(4f);

        StartCoroutine("FadeOutIntroBG");

        Destroy(introBG, 2f);
    }

    public void PressHomeButton() {
        GameManager.Instance.ReturnTitleScene();
    }

    public void PressRestartButton() {
        GameManager.Instance.RestartScene();
    }

    public void PressNextLevelButton() {
        GameManager.Instance.LoadNextLevelScene();
    }
}
