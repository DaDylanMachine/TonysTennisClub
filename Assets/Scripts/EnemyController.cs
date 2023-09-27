using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;
    private NavMeshHit hit;
    private bool blocked = false;

    // Start is called before the first frame update
    void Start()
    {
        //Applies the Transform component of the target and the NavMeshAgent component of the agent to their respective variables.
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Variable that determines the distance between the player/target and the enemy/agent.
        float distance = Vector3.Distance(target.position, transform.position);

        //If the player/target is within the look radius of the enemy/agent, make the enemy walk towards them.
        if (distance <= lookRadius)
        {
            blocked = NavMesh.Raycast(transform.position, target.position, out hit, NavMesh.AllAreas);

            if(!blocked)
            {
                agent.SetDestination(target.position);
            }
        }
    }

    //Creates a gizmo around the enemy that visualizes the radius of what the enemy can see.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

    }
}
