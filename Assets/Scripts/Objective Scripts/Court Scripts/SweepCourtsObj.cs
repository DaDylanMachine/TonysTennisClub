using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepCourtsObj : MonoBehaviour
{
    // GameObject and Component variables to edit them within the script.
    public GameObject unbrushedCourt;
    public GameObject gameManager;
    public float timer = 0f;
    public float timeRate = .005f;
    public float finalTime = 15f;
    private Color tempColor;
    public bool courtUpdated = false;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Cart")
        {
            //if (collision.gameObject.GetComponent<>)
            StartCoroutine(brushCourt());
        }
        
    }

    private IEnumerator brushCourt()
    {
        while (timer < finalTime)
        {
            tempColor = unbrushedCourt.GetComponent<MeshRenderer>().material.color;
            //Debug.Log("Unbrushing");
            timer += timeRate * Time.deltaTime;
            tempColor.a = 1.0f - Mathf.Clamp01(timer / finalTime);
            unbrushedCourt.GetComponent<MeshRenderer>().material.color = tempColor;
            yield return null;
        }

    }

    private void Update()
    {
        if (!courtUpdated)
        {
            if (unbrushedCourt.GetComponent<MeshRenderer>().material.color.a == 0f)
            {
                gameManager.GetComponent<BrushCourtsObj>().brushedCourts += 1;
                unbrushedCourt.SetActive(false);
                courtUpdated = true;
            }

        }
    }

    //// Function that changes the Court Texture to Brushed after the cart is on the court for about 10 seconds.
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Cart")
    //    {
    //        timer += timeRate * Time.deltaTime;
    //        if (timer >= finalTime)
    //        {
    //            Debug.Log("poggers");
    //            courtTexture.material = newMaterial;
    //        }
    //
    //    }
    //}
    //
    //// Resets the timer for brushing courts.
    //private void OnCollisionExit(Collision collision)
    //{
    //    timer = 0f;
    //}
}
