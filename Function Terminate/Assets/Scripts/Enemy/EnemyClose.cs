using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyClose : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    [SerializeField] bool hasDetected = false;
    [SerializeField] private Transform target;
    [SerializeField] private float agentSpeed = 10f;


    // enemy hp
    [SerializeField] private int hp = 150;
    // enemy damage 1/4 player's max health ?
    [SerializeField] private int damage = 100;
    private PlayerHealth player;

    public int Hp { get => hp; set => hp = value; }
    public int Damage { get => damage; set => damage = value; }

    // enemy take damage function
    public void TakeDamage(int damage)
    {
        this.Hp -= damage;
    }

    // check if the enemy is dead
    public bool IsDead()
    {
        return (this.hp <= 0);
    }


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
        // the bot radar detects the player
        if (other.gameObject.tag == "player")
        {

            Debug.Log("Player detected");
            // the bot chases the player
            hasDetected = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }


    public void Attack()
    {

    }


}