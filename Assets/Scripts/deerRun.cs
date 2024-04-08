using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class deerRun : MonoBehaviour
{
    private Animator deerAnimation;

    public Transform target;
    public NavMeshAgent agent;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<AudioSource>().isPlaying)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
        if (deerAnimation == null)
        {
            deerAnimation = gameObject.GetComponent<Animator>();
        }
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        transform.LookAt(Camera.main.transform);

        agent.SetDestination(target.position);
    }
}
