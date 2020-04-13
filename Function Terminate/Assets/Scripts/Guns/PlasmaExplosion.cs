using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaExplosion : MonoBehaviour
{
    [Range(5.0f, 30.0f)] private float Damage = 30f;
   
    Transform CollisionTransform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            
            if (other.gameObject.GetComponent<Enemy>())
            {
                Debug.Log("Collision");
                other.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
            }
            Destroy(this.gameObject, 0.5f);

        }
    }

}
