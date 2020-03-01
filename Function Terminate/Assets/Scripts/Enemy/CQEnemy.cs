using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CQEnemy : MonoBehaviour
{
    [SerializeField]private float speed = 5;
    [SerializeField]private float range = 5;
    [SerializeField]private float attackFrequency;
    [SerializeField]private float attackDamage = 10;
    [SerializeField]private float enemyHP=100;
    private bool playerInRange = false;
    private GameObject target;
    [SerializeField] private GameObject detected;
    private Transform playerPosition;
    private NavMeshAgent cQEnemy;
    [SerializeField] private GameObject attackThing;
    private bool Dead = false;

    void Start()
    {
        cQEnemy = GetComponent<NavMeshAgent>();
        playerPosition = GameObject.FindGameObjectWithTag("player").transform;
        StartCoroutine("WaitToAttack");
        GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x, GetComponent<BoxCollider>().size.y, range);
        GetComponent<BoxCollider>().center = new Vector3(0, 0, range/2);
    }



    void Update()
    {
        if (Vector3.Distance(transform.position, playerPosition.position) > range && !Dead)
        {
            cQEnemy.destination = playerPosition.position;
            detected.SetActive(true);
        }
        if (Vector3.Distance(transform.position, playerPosition.position) < range && !Dead)
        {
            cQEnemy.destination = this.transform.position;
        }
        if (enemyHP <= 0)
        {
            Dead = true;
            Die();
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Debug.Log("Collision");
            enemyHP -= 10;
        }
    }
    private void Attack()
    {
        attackThing.GetComponent<Animation>().Play("BasicAttackAnimation");
        if (target.gameObject.GetComponent<PlayerHealth>()) {
            target.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
    private void Die()
    {
        
        this.GetComponent<Animation>().Play("CQDead");
        detected.SetActive(false);
        Destroy(this.gameObject, 2f);
    }


    IEnumerator WaitToAttack()
    {
        float x = 0;
        while(x >= 0)
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
        Debug.Log("How did I get here?");
    }

}
