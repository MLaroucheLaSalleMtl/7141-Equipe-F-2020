using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyStatic : Enemy
{  
    private NavMeshAgent agent;
    //private AudioSource audio;
    private Transform target;
    private bool hit = false;
    
    float AttackRate = 1f;
    float StartAttackRate;
    public GameObject Projectile;
    void Start()
         
    {
        target = GameObject.FindGameObjectWithTag("player").transform;
        agent = GetComponent<NavMeshAgent>();
        Audio = GetComponent<AudioSource>();
       
        DetectionDistance = 20f;
        HasDetected = false;
        StartAttackRate = Time.time;
        Damage = 50;
        Hp = 150f;
    }

    // enemy take damage function
    public override void TakeDamage(float Damage)
    {
        this.Hp -= Damage;
        hit = true;
        if (IsDead())
        {
            AudioSource.PlayClipAtPoint(DeathSound, this.gameObject.transform.position);
            Destroy(this.gameObject);
            GameObject lootobj = Instantiate(Projectile, transform.position, Quaternion.identity);
        }
        if (DamageSounds.Length > 0)
        {
            int snd = Random.Range(0, DamageSounds.Length);
            Audio.PlayOneShot(DamageSounds[snd]);
        }
    }

    // check if the enemy is dead
    public bool IsDead()
    {
        return (this.Hp <= 0);
    }
    
    void LateUpdate()
    {
        if (Vector3.Distance(transform.position, target.position) <= DetectionDistance || hit)
        {
            HasDetected = true;
        }
        else
        {
            HasDetected = false;
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
            Audio.Play();
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