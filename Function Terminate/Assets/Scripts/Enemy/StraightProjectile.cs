using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 target;
    [SerializeField] private float speed = 15f;
    [SerializeField] Enemy E;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").transform;

        target = new Vector3(player.position.x, player.position.y , player.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
    void Shoot()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed);
        Destroy(gameObject, .3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "player")
        {
            //Debug.Log("Did Hit");
            Destroy(gameObject);
        }

        
    }
}
