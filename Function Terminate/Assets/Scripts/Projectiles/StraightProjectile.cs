using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 target;
    private float damage;
    [Range(1, 50)][SerializeField] private float speed = 15f;


    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("player").transform;

        //target = new Vector3(player.position.x, player.position.y, player.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
    void Shoot()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "player")
        {
            if(collision.gameObject.GetComponent<PlayerHealth>())
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    public void SetDamage(float _damage) {
        this.damage = _damage;
    }

}
