using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyExploding : Enemy
{

    private int destPoint = 0;
    private NavMeshAgent agent;
    Transform Target;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] private GameObject[] lootObjs;




    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Target = GameObject.FindGameObjectWithTag("player").transform;
        DetectionDistance = 20f;
        Damage = 25;
    }



    void Update()
    {
        Move();
    }


    public override void TakeDamage(float Damage)
    {
        Debug.Log("Taking damage");
        Hp -= Damage;
        if (IsDead())
        {
            Dice dice = Dice.Get_Instance();
            int random = dice.Next(1, 6);
            Vector3 i = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            GameObject lootobj = Instantiate(lootObjs[random], i, Quaternion.identity);
            GameObject obj = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(obj, 1.9f);
            Destroy(gameObject);

        }
    }

    public bool IsDead()
    {
        return (Hp <= 0);
    }


    protected override void Move()
    {
        if (Vector3.Distance(transform.position, Target.position) <= DetectionDistance)
            HasDetected = true;
            agent.speed = 100f;
        if (HasDetected)
        {
           
            agent.destination = Target.position;
            transform.LookAt(Target);
            
            if (Vector3.Distance(transform.position, Target.position) <= 2f)
            {
                Attack();
            }
        }
    }

    protected override void Attack()
    {
        GameObject obj = Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15f);
        Destroy(obj, 1.9f);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(20f, transform.position, 15f, 3.0F, ForceMode.Impulse);
        }
        Target.gameObject.GetComponent<PlayerHealth>().TakeDamage(Damage);
       
        Dice dice = Dice.Get_Instance();
        int random = dice.Next(1, 6);
        Vector3 i = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        GameObject lootobj = Instantiate(lootObjs[random], i, Quaternion.identity);
        Destroy(gameObject);

    }

}