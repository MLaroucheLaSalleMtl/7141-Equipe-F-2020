using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class Enemy : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    [SerializeField] bool hasDetected = false;
    [SerializeField] private Transform target;
    [SerializeField] private float agentSpeed = 10f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (hasDetected)
        {
            // the bot chases the player
            agent.destination = target.position;
            agent.speed = 15f;
          
        }
        else
        {
            agent.speed = agentSpeed;
            agent.destination = points[destPoint].position;
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
                
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger declanché");
        // the bot radar detects the player
        if (other.gameObject.tag == "player")
        {

            Debug.Log("Player detected");
            // the bot chases the player
            hasDetected = true;
        }
    }


}