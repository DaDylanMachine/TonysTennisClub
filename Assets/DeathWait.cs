using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathWait : MonoBehaviour
{
    public bool start = false;
    public bool nextScene = false;
    public GameObject transitionObj;
    public float fadeTime = 3;
    public float fadeTime2 = 5;
    public float fadeTime3 = 8;

    private void Awake()
    {
        StartCoroutine(transition());

        if (nextScene)
        {
            start = false;
            SceneManager.LoadScene("End");
        }
    }

    IEnumerator transition()
    {
        yield return new WaitForSecondsRealtime(5);

        float elapsedTime = 0.0f;
        Color temp = transitionObj.GetComponent<Image>().color;
        while (elapsedTime < fadeTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            temp.a = -Mathf.Clamp01(elapsedTime / fadeTime);
            transitionObj.GetComponent<Image>().color = temp;
        }

        nextScene = true;
        yield return new WaitForSecondsRealtime(3);
    }
}

