using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyStatic : MonoBehaviour
{

  
    private NavMeshAgent agent;
    [SerializeField] bool hasDetected = false;
    [SerializeField] private Transform target;
    [SerializeField] private float agentSpeed = 10f;
    [SerializeField] private float fireRate;
    [SerializeField] private float startFireRate;
    [SerializeField] private float DetectionDistance = 10f;
    [SerializeField] private GameObject projectile;
    // enemy hp
    [SerializeField] private int hp = 150;
    // enemy damage 1/4 player's max health ?
    [SerializeField] private int damage = 100;




    // enemy take damage function
    public void TakeDamage(int damage)
    {
        this.hp -= damage;
    }

    // check if the enemy is dead
    public bool IsDead()
    {
        return (this.hp <= 0);
    }


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //fireRate = startFireRate;
        fireRate = 1f;
        startFireRate = Time.time;
    }


    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, target.position) <= DetectionDistance)
        {
            hasDetected = true;
        }
        else
        {
            hasDetected = false;
        }
            

        if (hasDetected)
        {
            //transform.LookAt(target);
            Invoke("LookPlayer", 1f);
            Shoot();
        }
    }

    void LookPlayer()
    {
        transform.LookAt(target);
    }

    void Shoot()
    {
        if (Time.time > startFireRate)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            startFireRate = Time.time + fireRate;
        }
    }



    private void OnCollisionEnter(Collision collision)
    {

    }


}