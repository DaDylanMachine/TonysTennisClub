using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerLogic : MonoBehaviour
{
    public GameObject GameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        GameManager.GetComponent<EnemySpawns>().deerLogic();
    }
}
