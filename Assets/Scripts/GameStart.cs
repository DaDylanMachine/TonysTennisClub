using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameStart : MonoBehaviour
{
    public bool start = false;
    public bool nextScene = false;
    public GameObject transitionObj;
    public GameObject hireLetterObj;
    public GameObject button1;
    public GameObject text1;
    public GameObject button2;
    public GameObject text2;
    public float fadeTime = 3;
    public float fadeTime2 = 5;
    public float fadeTime3 = 8;

    public void PlayGame()
    {
        start = true;
        button1.GetComponent<Button>().interactable = false;
        button2.GetComponent<Button>().interactable = false;


    }

    public void QuitGame()
    {
        Debug.Log("Quitting Application");
        Application.Quit();
    }

    private void Update()
    {
        if (start)
        {

            StartCoroutine(transition());

            if (nextScene)
            {
                start = false;
                Debug.Log("Starting Next Scene");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    IEnumerator transition()
    {
        float elapsedTime = 0.0f;
        Color temp = transitionObj.GetComponent<Image>().color;
        Color temp2 = button1.GetComponent<Image>().color; 
        while (elapsedTime < fadeTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            temp.a = Mathf.Clamp01(elapsedTime / fadeTime);
            transitionObj.GetComponent<Image>().color = temp;
            temp2.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            button1.GetComponent<Image>().color = temp2;
            button2.GetComponent<Image>().color = temp2;
            text1.GetComponent<TextMeshProUGUI>().color = temp2;
            text2.GetComponent<TextMeshProUGUI>().color = temp2;
        }
        yield return new WaitForSecondsRealtime(3);

        StartCoroutine(transition2());

    }

    IEnumerator transition2()
    {
        float elapsedTime = 0.0f;
        Color temp = hireLetterObj.GetComponent<Image>().color;
        while (elapsedTime < fadeTime2)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            temp.a = Mathf.Clamp01(elapsedTime / fadeTime2);
            hireLetterObj.GetComponent<Image>().color = temp;
        }

        yield return new WaitForSecondsRealtime(10);

        nextScene = true;

    }

}
