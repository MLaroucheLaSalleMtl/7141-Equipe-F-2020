using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBalls : MonoBehaviour
{
    [Range(15.0f, 50.0f)] private float Damage = 25f;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private AudioClip explosionSound;
    Transform CollisionTransform;
    private void OnCollisionEnter(Collision collision)
    {
        ParticleSystem ExplosionInstance;
      
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "WeakSpot")
        {
            CollisionTransform = this.transform;
            if (collision.gameObject.GetComponent<Enemy>())
                collision.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
                if (collision.gameObject.GetComponent<WeakSpot>())
                {
                    WeakSpot target = collision.gameObject.GetComponent<WeakSpot>();
                    target.SendDamage(Damage);
                }
                if (collision.gameObject.GetComponent<BossBlueprint>())
                {
                    BossBlueprint target = collision.gameObject.GetComponent<BossBlueprint>();
                    target.TakeDamage(Damage);
                }
            
            ExplosionInstance = Instantiate(explosion, CollisionTransform.position , Quaternion.identity) as ParticleSystem;
            AudioSource.PlayClipAtPoint(explosionSound, this.gameObject.transform.position);
            Destroy(this.gameObject);
            //Destroy(explosion.gameObject, 0.5f);
        }
        else if (collision.gameObject.tag=="Untagged")
        {
            CollisionTransform = this.transform;
            ExplosionInstance = Instantiate(explosion, CollisionTransform.position, Quaternion.identity) as ParticleSystem;
            AudioSource.PlayClipAtPoint(explosionSound, this.gameObject.transform.position);
            Destroy(this.gameObject);
            //Destroy(explosion.gameObject, 0.5f);
        }
    }
   
}
