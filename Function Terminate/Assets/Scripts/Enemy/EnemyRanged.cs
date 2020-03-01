using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyRanged : Enemy
{

    private AudioSource audio;
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
        // the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
        GotoNextPoint();
        //fireRate = startFireRate;
        FireRate = 1f;
        StartFireRate = Time.time;
       
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


    void LateUpdate()
    {
        Move();
    }


    public override void TakeDamage(int damage)
    {
        Hp -= damage;
    }

    public override bool IsDead()
    {
        return (Hp <= 0);
    }

    public override void Move()
    {
        if (Vector3.Distance(transform.position, Target.position) <= DetectionDistance)
            HasDetected = true;


        if (HasDetected)
        {

            if (Vector3.Distance(transform.position, Target.position) > StoppingDistance)
            {
                agent.destination = Target.position;
                agent.speed = 15f;
            }
            else if (Vector3.Distance(transform.position, Target.position) < StoppingDistance
                && Vector3.Distance(transform.position, Target.position) > RetreatDistance)
            {
                agent.destination = transform.position;
                transform.LookAt(Target);
                Shoot();
                agent.speed = 50f;
            }
            else if (Vector3.Distance(transform.position, Target.position) < RetreatDistance)
            {
                agent.destination = Target.position * -1;
                transform.LookAt(Target);
                Shoot();
            }

        }
        else
        {
            agent.speed = AgentSpeed;
            agent.destination = points[destPoint].position;
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
    }

    public override void Shoot()
    {
        if (Time.time > StartFireRate)
        {
            GameObject newProjectile = Instantiate(Projectile, transform.position, Quaternion.identity);
            audio.Play();
            newProjectile.transform.LookAt(Target.transform);
            newProjectile.GetComponent<StraightProjectile>().SetDamage(Damage);
            StartFireRate = Time.time + FireRate;
        }
    }
}