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
    }

    IEnumerator transition()
    {
        yield return new WaitForSecondsRealtime(10);
        float elapsedTime = 0.0f;
        Color temp = transitionObj.GetComponent<Image>().color;
        while (elapsedTime < fadeTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            temp.a = Mathf.Clamp01(elapsedTime / fadeTime);
            transitionObj.GetComponent<Image>().color = temp;
        }
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene("End");
    }
}

