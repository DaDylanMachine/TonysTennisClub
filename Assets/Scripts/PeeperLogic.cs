using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PeeperLogic : MonoBehaviour
{
    public float minAggressionTimer = 10.0f;
    public float maxAggressionTimer = 15.0f;
    private float viewTime = 0f;
    private float viewSpeed = .9f;
    public float minViewTime = 3.0f;
    private bool angered = false;
    private bool startAnger = false;
    private bool stopAnger = false;

    //float damping = 6.0f;

    private Animator peeperAnimation;

    public Transform target;
    public NavMeshAgent agent;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (peeperAnimation == null)
        {
            peeperAnimation = gameObject.GetComponent<Animator>();
        }
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        /*var rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);*/

        transform.LookAt(Camera.main.transform);
        

        if (!startAnger)
        {
            startAnger = true;
            gameObject.transform.Find("Heartbeat").GetComponent<AudioSource>().Play();
            StartCoroutine(peeperAggression());          
        }

        if (angered && peeperAnimation.GetCurrentAnimatorStateInfo(0).IsName("Stalker_run work"))
        {
            agent.SetDestination(target.position);
            if (!gameObject.transform.Find("PeeperFootsteps").GetComponent<AudioSource>().isPlaying)
            {
                gameObject.transform.Find("PeeperFootsteps").GetComponent<AudioSource>().Play();
            }
        }

        Vector3 peeperPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (peeperPosition.x >= 0f && peeperPosition.x <= 1f && peeperPosition.y >= 0f && peeperPosition.y <= 1f && peeperPosition.z >= 0)
        {
            viewTime += viewSpeed * Time.deltaTime;
        }
        else
        {
            viewTime = 0f;
        }

        if (viewTime >= minViewTime && angered == false && stopAnger == false)
        {
            stopAnger = true;
            StartCoroutine(peeperDespawn());
        }

        
    }

    private IEnumerator peeperAggression()
    {
        float selectedAggressionTimer = Random.Range(minAggressionTimer, maxAggressionTimer);
        Debug.Log("Peeper Angry in " + selectedAggressionTimer + " seconds.");
        yield return new WaitForSecondsRealtime(selectedAggressionTimer);

        if (!stopAnger)
        {
            angered = true;
            peeperAnimation.SetBool("Angered", true);
            gameObject.transform.Find("Heartbeat").GetComponent<AudioSource>().mute = true;
            gameObject.GetComponent<AudioSource>().Play();
            Debug.Log("Peeper Angered.");
            
        }
        else
        {
            gameObject.transform.Find("Heartbeat").GetComponent<AudioSource>().Stop();
            Debug.Log("Peeper Chilling.");
        }

    }

    private IEnumerator peeperDespawn()
    {

        Debug.Log("Peeper Spotted, despawn.");
        yield return new WaitForSecondsRealtime(.1f);
        gameObject.transform.Find("tballright").gameObject.SetActive(true);
        gameObject.transform.Find("tballleft").gameObject.SetActive(true);
        gameObject.transform.Find("tballright").parent = null;
        gameObject.transform.Find("tballleft").parent = null;
        Destroy(gameObject);
        gameObject.transform.Find("Heartbeat").GetComponent<AudioSource>().Stop();
        GameObject.Find("GameManager").GetComponent<EnemySpawns>().peeperSpawned = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.position);
    }
}
