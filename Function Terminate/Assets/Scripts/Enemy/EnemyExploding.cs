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
        Audio = GetComponent<AudioSource>();
        Target = GameObject.FindGameObjectWithTag("player").transform;
        DetectionDistance = 30f;
        Damage = 25;
        Hp = 1;
    }



    void Update()
    {
        Move();
    }


    public override void TakeDamage(float Damage)
    {
        Hp -= Damage;
        if (IsDead())
        {
            AudioSource.PlayClipAtPoint(DeathSound, this.gameObject.transform.position);
            Dice dice = Dice.Get_Instance();
            int random = dice.Next(1, lootObjs.Length);
            Vector3 i = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            GameObject lootobj = Instantiate(lootObjs[random], i, Quaternion.identity);
            GameObject obj = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(obj, 1.9f);
            Destroy(gameObject);
        }
        if (DamageSounds.Length > 0)
        {
            int snd = Random.Range(0, DamageSounds.Length);
            Audio.PlayOneShot(DamageSounds[snd]);
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
            
            if (Vector3.Distance(transform.position, Target.position) <= .2f)
            {
                Attack();
            }
        }
    }

    protected override void Attack()
    {
        GameObject obj = Instantiate(explosionEffect, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(DeathSound, this.gameObject.transform.position);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15f);
        Destroy(obj, 1.9f);
        //foreach (Collider hit in colliders)
        //{
        //    Rigidbody rb = hit.GetComponent<Rigidbody>();

        //    if (rb != null)
        //        rb.AddExplosionForce(20f, transform.position, 15f, 3.0F, ForceMode.Impulse);
        //}
        Target.gameObject.GetComponent<PlayerHealth>().TakeDamage(Damage);
       
        Dice dice = Dice.Get_Instance();
        int random = dice.Next(1, lootObjs.Length);
        Vector3 i = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        GameObject lootobj = Instantiate(lootObjs[random], i, Quaternion.identity);
        Destroy(gameObject);

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "player")
    //    {
    //        Attack();
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Attack();
        }
    }

}