using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepCourtsObj : MonoBehaviour
{
    // GameObject and Component variables to edit them within the script.
    public Material newMaterial;
    public MeshRenderer courtTexture;
    public float timer = 0f;
    public float timeRate = 1.3f;
    public float finalTime = 15f;

    // Function that changes the Court Texture to Brushed after the cart is on the court for about 10 seconds.
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Cart")
        {
            timer += timeRate * Time.deltaTime;
            if (timer >= finalTime)
            {
                Debug.Log("poggers");
                courtTexture.material = newMaterial;
            }

        }
    }

    // Resets the timer for brushing courts.
    private void OnCollisionExit(Collision collision)
    {
        timer = 0f;
    }
}
