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
    [SerializeField] private float stoppingDistance = 5f;
    [SerializeField] private float retreatDistance = 2f;
    [SerializeField] private float DetectionDistance = 10f;
    [SerializeField] private float fireRate;
    [SerializeField] private float startFireRate;
    [SerializeField] private GameObject projectile;
    // enemy hp
    [SerializeField] private int hp = 150;
    // enemy damage 1/4 player's max health ?
    [SerializeField] private int damage = 100;


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
        
        // the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
        GotoNextPoint();
        //fireRate = startFireRate;
        fireRate = 1f;
        startFireRate = Time.time;
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

        if (Vector3.Distance(transform.position, target.position) <= DetectionDistance)
            hasDetected = true;
 

        if (hasDetected)
        {
            
           

            if (Vector3.Distance(transform.position, target.position) > stoppingDistance)
            {
                agent.destination = target.position;
                agent.speed = 15f;
            }
            else if(Vector3.Distance(transform.position, target.position) < stoppingDistance 
                && Vector3.Distance(transform.position, target.position) > retreatDistance)
            {
                agent.destination = transform.position;
                Shoot();
                agent.speed = 50f;
            }
            else if (Vector3.Distance(transform.position, target.position) < retreatDistance)
            {
                agent.destination = target.position * -1;
                Shoot();
            }

        }
        else
        {
            agent.speed = agentSpeed;
            agent.destination = points[destPoint].position;
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();        
        }

      

    }

    void Shoot()
    {
        if(Time.time > startFireRate)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            startFireRate = Time.time + fireRate;
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        
    }


}