using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyStatic : MonoBehaviour
{

  
    private NavMeshAgent agent;
    private AudioSource audio;
    [SerializeField] bool hasDetected = false;
    [SerializeField] private Transform target;
    [SerializeField] private float agentSpeed = 10f;
    [SerializeField] private float fireRate;
    [SerializeField] private float startFireRate;
    [SerializeField] private float DetectionDistance = 10f;
    [SerializeField] private GameObject projectile;
    // enemy hp
    [SerializeField] private float hp = 150;
    // enemy damage 1/4 player's max health ?
    [SerializeField] private int damage = 100;




    // enemy take damage function
    public void TakeDamage(float Damage)
    {
        this.hp -= Damage;
        if (IsDead())
        {
            Destroy(this.gameObject);

        }
    }

    // check if the enemy is dead
    public bool IsDead()
    {
        return (this.hp <= 0);
    }


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
        fireRate = 1f;
        startFireRate = Time.time;
    }


    void LateUpdate()
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
            //Invoke("LookPlayer", 1f);
            transform.LookAt(target);
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
            audio.Play();
            GameObject newProjectile =  Instantiate(projectile, transform.position, Quaternion.identity);
            newProjectile.transform.LookAt(target.transform);
            newProjectile.GetComponent<StraightProjectile>().SetDamage(damage);
            startFireRate = Time.time + fireRate;
        }
    }



    private void OnCollisionEnter(Collision collision)
    {

    }


}