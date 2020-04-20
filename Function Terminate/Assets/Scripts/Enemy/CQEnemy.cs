
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CQEnemy : Enemy
{
    
    [SerializeField] private float speed = 10;
    [SerializeField] private float range = 5;
    [SerializeField] private float attackFrequency;
    [SerializeField] private float attackDamage = 20;
    [SerializeField] private float enemyHP = 100f;
    private bool playerInRange = false;
    public GameObject target;
    [SerializeField] private GameObject detected;
    private Transform playerPosition;
    private NavMeshAgent cQEnemy;
    [SerializeField] private GameObject attackThing;
    private bool Dead = false;
    [SerializeField] private GameObject[] lootObjs;

    void Start()
    {
        cQEnemy = GetComponent<NavMeshAgent>();
        playerPosition = GameObject.FindGameObjectWithTag("player").transform;
        StartCoroutine("WaitToAttack");
        GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x, GetComponent<BoxCollider>().size.y, range);
        GetComponent<BoxCollider>().center = new Vector3(0, 0, range / 2);
        Audio = GetComponent<AudioSource>();
    }



    void Update()
    {
        Move();
        if (enemyHP <= 0 && !Dead)
        {
            Dead = true;
            Die();
        }
    }
   
    protected override void Move()
    {
        if (Vector3.Distance(transform.position, playerPosition.position) > range && !Dead)
        {
            cQEnemy.destination = playerPosition.position;
            detected.SetActive(true);
        }
        if (Vector3.Distance(transform.position, playerPosition.position) < range && !Dead)
        {
            cQEnemy.destination = this.transform.position;
            WaitToAttack();

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            playerInRange = true;
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            playerInRange = false;
            target = null;
        }
    }

    public override void TakeDamage(float Damage)
    {
        this.enemyHP -= Damage;
        if (DamageSounds.Length > 0) {
            int snd = Random.Range(0, DamageSounds.Length);
            Audio.PlayOneShot(DamageSounds[snd]);
        }
    }

    protected override void Attack()
    {
        attackThing.GetComponent<Animation>().Play("BasicAttackAnimation");
        Audio.PlayOneShot(AttackSound);
        if (target.gameObject.GetComponent<PlayerHealth>())
        {
            target.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
    private void Die()
    {

        //this.GetComponent<Animation>().Play("CQDeath");
        detected.SetActive(false);
        Destroy(this.gameObject, 2f);
        Dice dice = Dice.Get_Instance();
        int random = dice.Next(1, lootObjs.Length);
        Vector3 i = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        GameObject lootobj = Instantiate(lootObjs[random], i, Quaternion.identity);
        Audio.PlayOneShot(DeathSound);

    }


    IEnumerator WaitToAttack()
    {
        float x = 0;
        while (x >= 0)
        {
            if (playerInRange)
            {
                if (x > attackFrequency)
                {
                    x = 0;
                    Attack();
                }
                x += 0.1f;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }




}

