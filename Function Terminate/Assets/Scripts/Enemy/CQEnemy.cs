using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CQEnemy : MonoBehaviour
{
    private float speed = 5;
    private float range = 5;
    private float attackFrequency;
    private float attackDamage;
    private float enemyHP=100;
    [SerializeField] private GameObject detected;
    private Transform playerPosition;
    private NavMeshAgent cQEnemy;
    [SerializeField] private GameObject attackThing;
    private bool Dead = false;
    void Start()
    {
        cQEnemy = GetComponent<NavMeshAgent>();
        playerPosition = GameObject.FindGameObjectWithTag("player").transform;

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
            Attack();
        }
        if (enemyHP <= 0)
        {
            Dead = true;
            Die();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (CompareTag("Damage"))
        {
            Debug.Log("Collision");
            enemyHP -= 10;
        }
    }
    private void Attack()
    {
        attackThing.GetComponent<Animation>().Play("BasicAttackAnimation");

    }
    private void Die()
    {
        
        this.GetComponent<Animation>().Play("CQDead");
        detected.SetActive(false);
        Destroy(this.gameObject, 2f);
    }
}
