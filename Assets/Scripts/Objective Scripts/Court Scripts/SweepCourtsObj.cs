using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepCourtsObj : MonoBehaviour
{
    // GameObject and Component variables to edit them within the script.
    public Material unbrushedMaterial;
    public Material brushedMaterial;
    public float timer = 0f;
    public float timeRate = .01f;
    public float finalTime = 15f;

    private void Start()
    {
        this.GetComponent<MeshRenderer>().material = unbrushedMaterial;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Cart")
        {
            StartCoroutine(brushCourt());
        }
        
    }

    private IEnumerator brushCourt()
    {
        while (timer < finalTime)
        {
            Debug.Log("Lerping Between Brushed and Unbrushed");
            timer += timeRate * Time.deltaTime;
            float lerp = Mathf.PingPong(timer, finalTime) / finalTime;
            this.GetComponent<MeshRenderer>().material.Lerp(unbrushedMaterial, brushedMaterial, lerp);
            yield return null;
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
