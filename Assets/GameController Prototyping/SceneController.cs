using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// Note that the scene switching logic was taken from Comp-3 Interactive on YouTube
// https://www.youtube.com/watch?v=kgbUFQDKOBM
public class SceneController : MonoBehaviour
{
    //Should be a singleton
    public static SceneController instance { get; private set; }
    public Image fadeEffect;

    void Awake()
    {
        //singleton enforcing
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            fadeEffect.gameObject.SetActive(true);
            fadeEffect = GetComponentInChildren<Image>();
            fadeEffect.rectTransform.sizeDelta = new Vector2(Screen.width + 20, Screen.height + 20);
            fadeEffect.gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void LoadScene(int index, float duration = 1, float waitTime = 0)
    {
        instance.StartCoroutine(instance.FadeScene(index, duration, waitTime));
    }

    public static void ReloadScene(float duration = 0, float waitTime = 0)
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
