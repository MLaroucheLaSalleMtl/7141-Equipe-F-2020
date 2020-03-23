using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyStatic : Enemy
{  
    private NavMeshAgent agent;
    private AudioSource audio;
    private Transform target;
    
    float AttackRate = 1f;
    float StartAttackRate;
    public GameObject Projectile;
    void Start()
         
    {
        target = GameObject.FindGameObjectWithTag("player").transform;
        agent = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
       
        DetectionDistance = 20f;
        HasDetected = false;
        StartAttackRate = Time.time;
        Damage = 100;
        Hp = 150f;
    }

    // enemy take damage function
    public override void TakeDamage(float Damage)
    {
        this.Hp -= Damage;
        if (IsDead())
        {
            Destroy(this.gameObject);
            GameObject lootobj = Instantiate(Projectile, transform.position, Quaternion.identity);
        }
    }

    // check if the enemy is dead
    public bool IsDead()
    {
        return (this.Hp <= 0);
    }
    
    void LateUpdate()
    {
        if (Vector3.Distance(transform.position, target.position) <= DetectionDistance)
        {
            HasDetected = true;
        }
       
            

        if (HasDetected)
        {
            //transform.LookAt(target);
            //Invoke("LookPlayer", 1f);
            transform.LookAt(target);
            Attack();
        }
    }

    void LookPlayer()
    {
        transform.LookAt(target);
    }

    protected override void Attack()
    {
        if (Time.time > StartAttackRate)
        {
            audio.Play();
            GameObject newProjectile =  Instantiate(Projectile, transform.position, Quaternion.identity);
            newProjectile.transform.LookAt(target.transform);
            newProjectile.GetComponent<StraightProjectile>().SetDamage(Damage);
            StartAttackRate = Time.time + AttackRate;
        }
    }



    protected override void Move()
    {
        throw new System.NotImplementedException();
    }
}