using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawns : MonoBehaviour
{
    //Set Enemies here
    public GameObject peeperEnemy;
    public GameObject deerEnemy;
    public float peeperTimer = 0.0f;
    public float peeperMinTime = 60.0f;
    public float peeperMaxTime = 120.0f;
    public Camera playerCamera;
    private float peeperSelectedTime;
    public bool peeperSpawned = false;
    public bool deerSpawned = false;
    public GameObject deerSpawnPoint;

    //Peeper Spawn Logic:
    //Create random timer interval with a time between x and y seconds
    //When the timer is reached, select a random spawn location for the peeper enemy that has a raycast to the player, NOT in the player sight range (determined by ray)
    //Optionally play sound when enemy spawns to alert player (believe this is crucial as otherwise may seem too random)
    //Give a random wait time between a and b seconds for the player to look at the peeper
    //If enemy is looked at in time (based on the sight range), despawn enemy and restart spawn sequence (optionally spawn tennis balls for added effect)
    //If enemy is not looked at time, determine fastest possible route to player and chase until death
    public void peeperLogic()
    {
        Debug.Log("Peeper Spawned");
    }

    //Deer Spawn Logic:
    //Determine area in the map that should be used as play area
    //If player steps out of the play area, spawn the deer enemy randomly within raycast range of player (optionally outside treeline, ignore collisions)
    //Deer will determine fastest possible route to player and chase until death
    public void deerLogic()
    {
        if (!deerSpawned)
        {
            deerSpawned = true;
            Vector3 deerSpawnSpot = deerSpawnPoint.transform.position;
            if (Instantiate(deerEnemy, deerSpawnSpot, Quaternion.identity))
            {
                Debug.Log("Deer Spawned");
            }

        }
    }

    private void Start()
    {
        StartCoroutine(peeperSpawn());
    }

    private IEnumerator peeperSpawn()
    {
        if (!peeperSpawned)
        {
            peeperSelectedTime = UnityEngine.Random.Range(peeperMinTime, peeperMaxTime);
            Debug.Log("Selected Peeper Time: " + peeperSelectedTime);
            yield return new WaitForSecondsRealtime(peeperSelectedTime);

            Debug.Log("Start Peeper Spawn");
            peeperSpawned = true;

            //This shit is gonna require some explaining...

            //First determine a random x, y, and z outside of the RELATIVE VIEWPORT
            float spawnX = UnityEngine.Random.Range(-0.2f, 0.2f);
            float spawnY = UnityEngine.Random.Range(-0.2f, 0.2f);
            spawnX += Mathf.Sign(spawnX);
            spawnY += Mathf.Sign(spawnY);
            float spawnZ = UnityEngine.Random.Range(-20.0f,20.0f);
            while (spawnZ >= -1 && spawnZ <= 1)
                spawnZ = UnityEngine.Random.Range(-20.0f, 20.0f);

            //Set the Peeper Spawn point with the relative viewport
            Vector3 peeperSpawnSpot = playerCamera.ViewportToWorldPoint(new Vector3(spawnX, spawnY, spawnZ));
            //Set Y to a bit above the ground
            peeperSpawnSpot.y = 11;
            //Determine if there is an object in the current selected spawnpoint
            float radius = 1f;
            while (Physics.CheckSphere(peeperSpawnSpot, radius))
            {
                spawnX = UnityEngine.Random.Range(-0.2f, 0.2f);
                spawnY = UnityEngine.Random.Range(-0.2f, 0.2f);
                spawnX += Mathf.Sign(spawnX);
                spawnY += Mathf.Sign(spawnY);
                spawnZ = UnityEngine.Random.Range(-20.0f, 20.0f);
                while (spawnZ >= -5 && spawnZ <= 5)
                    spawnZ = UnityEngine.Random.Range(-20.0f, 20.0f);

                //Set the Peeper Spawn point with the relative viewport
                peeperSpawnSpot = playerCamera.ViewportToWorldPoint(new Vector3(spawnX, spawnY, spawnZ));
                //Set Y to a bit above the ground
                peeperSpawnSpot.y = 11;
            }

            Debug.Log("Peeper Spawn Point: " + peeperSpawnSpot);
            //Finally, instantiate
            if (Instantiate(peeperEnemy, peeperSpawnSpot, Quaternion.identity))
                Debug.Log("Peeper Spawned");

        }

        while (peeperSpawned)
        {
            yield return null;
        }
        Debug.Log("Peeper Despawned, starting new timer");
        StartCoroutine(peeperSpawn());
    }

}
