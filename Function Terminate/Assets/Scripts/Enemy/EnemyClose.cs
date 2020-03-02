using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyClose : Enemy
{

    private int destPoint = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        FireRate = 1f;
        StartFireRate = Time.time;
    }



    void Update()
    {
        Move();
    }


    public override void TakeDamage(float Damage)
    {
        Hp -= Damage;
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
            agent.destination = Target.position;
            agent.speed = 50f;
        }
    }

    public override void Shoot()
    {
        // Explode
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Explode");
    }
}