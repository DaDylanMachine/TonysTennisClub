using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEnd : MonoBehaviour
{
    public bool start = false;
    public bool nextScene = false;
    public GameObject transitionObj;
    public PlayerManager managePlayer;
    public float fadeTime = 3;
    public float fadeTime2 = 5;
    public float fadeTime3 = 8;

    public void TransitionEnd()
    {
        StartCoroutine(transition());

        if (nextScene)
        {
            start = false;
            
            if (managePlayer.isDead)
                SceneManager.LoadScene("Death");
            else
                SceneManager.LoadScene("End");
        }
    }

    IEnumerator transition()
    {
        float elapsedTime = 0.0f;
        Color temp = transitionObj.GetComponent<Image>().color;
        while (elapsedTime < fadeTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            temp.a = Mathf.Clamp01(elapsedTime / fadeTime);
            transitionObj.GetComponent<Image>().color = temp;
        }

        nextScene = true;
        yield return new WaitForSecondsRealtime(3);


    }
}
