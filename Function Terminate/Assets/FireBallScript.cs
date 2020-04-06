using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    [SerializeField] private float mouvementSpeed = 30f;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float damage = 0f;


    // Start is called before the first frame update
    void Start()
    {
        fireBall.active = true;
        explosion.active = false;
        PlayParticles(explosion.GetComponents<ParticleSystem>());
        Destroy(this.gameObject,10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * mouvementSpeed * Time.deltaTime;
    }

    public void SetDamage(float _damage) {
        this.damage = _damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        fireBall.active = false;
        explosion.transform.parent = null;
        explosion.transform.position = collision.GetContact(0).point;
        explosion.active = true;
        Collider[] explosionHits = Physics.OverlapSphere(explosion.transform.position, 5, 1 << LayerMask.NameToLayer("Player"));
        foreach (Collider hit in explosionHits) {
            if (hit.gameObject.tag == "player") {
                if (hit.gameObject.GetComponent<PlayerHealth>()) {
                    hit.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                    hit.gameObject.GetComponent<Rigidbody>().AddExplosionForce(50f, explosion.transform.position, 5);
                }
            }
        }
        PlayParticles(explosion.GetComponents<ParticleSystem>());
        explosion.GetComponent<DestroyEffect>().Destroy();
        Destroy(this.gameObject);
    }

    private void PlayParticles(ParticleSystem[] systems) {
        foreach (ParticleSystem sys in systems)
        {
            sys.Play();
        }
    }


}
