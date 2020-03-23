using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyClose : Enemy
{

    private int destPoint = 0;
    private NavMeshAgent agent;
    Transform Target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Target = GameObject.FindGameObjectWithTag("player").transform;
    }



    void Update()
    {
        Move();
    }


    public override void TakeDamage(float Damage)
    {
        Hp -= Damage;
    }

    public bool IsDead()
    {
        return (Hp <= 0);
    }
   

    protected  override void Move()
    {
        if (Vector3.Distance(transform.position, Target.position) <= DetectionDistance)
            HasDetected = true;
        if (HasDetected)
        {
            agent.destination = Target.position;
            agent.speed = 50f;
        }
    }

    protected override void Attack()
    {
        // Explode
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Explode");
    }
}