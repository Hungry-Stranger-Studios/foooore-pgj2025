using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// Note that the scene switching logic was taken from Comp-3 Interactive on YouTube
// https://www.youtube.com/watch?v=kgbUFQDKOBM
public class SceneController : MonoBehaviour
{
    public Image fadeEffect;

    void Awake()
    {
        Debug.Log("SM MADE");
        //Called when script instance is first loaded
        fadeEffect.gameObject.SetActive(true);
        fadeEffect = GetComponentInChildren<Image>();
        fadeEffect.rectTransform.sizeDelta = new Vector2(Screen.width + 20, Screen.height + 20);
        fadeEffect.gameObject.SetActive(false);
        GameManager.Instance.SetSceneController(this);
    }

    public void LoadScene(int index, float duration = 1, float waitTime = 0)
    {
        GameManager.Instance.GetSceneController().StartCoroutine(GameManager.Instance.GetSceneController().FadeScene(index, duration, waitTime));
    }

    public void ReloadScene(float duration = 0, float waitTime = 0)
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex, duration, waitTime);
    }

    private IEnumerator FadeScene(int sceneIndex, float duration, float waitTime)
    {
        fadeEffect.gameObject.SetActive(true);

        for(float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            fadeEffect.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, t));
            yield return null;
        }

        SceneManager.LoadScene(sceneIndex);

        yield return new WaitForSeconds(waitTime);

        for (float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            fadeEffect.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, t));
            yield return null;
        }

        fadeEffect.gameObject.SetActive(false);
    }
}
