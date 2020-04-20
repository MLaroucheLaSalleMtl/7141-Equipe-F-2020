using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : GunFactory
{
    [SerializeField] private Camera playerCamera;  
    //[SerializeField] private GameObject barrel;
    [SerializeField] private GameObject Beam;
    private bool firing = false;
    private AudioSource audio;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        Range = 15f;
        Damage = 25f;
        Ammo = 1000;
        
    }

    protected override void Shoot( )
    {
        RaycastHit hit;
        audio.Play();
        //Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward*Range, Color.red, 5f);
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Range))
        {
            Debug.DrawLine(hit.point, hit.point, Color.black, 5f);
            if (hit.collider.gameObject.tag == "Enemy")
            {
                if (hit.collider.gameObject.GetComponent<Enemy>())
                {
                    Enemy target = hit.collider.gameObject.GetComponent<Enemy>();
                    target.TakeDamage(Damage);
                }
            }
                //if (hit.collider.gameObject.tag == "CQEnemy") {
                //    if (hit.collider.gameObject.GetComponent<CQEnemy>()) {
                //        CQEnemy target = hit.collider.gameObject.GetComponent<CQEnemy>();
                //        target.TakeDamage(Damage);
                //    }

                //}
                //if (hit.collider.gameObject.tag == "EnemyStatic")
                //{
                //    if (hit.collider.gameObject.GetComponent<EnemyStatic>())
                //    {
                //        EnemyStatic target = hit.collider.gameObject.GetComponent<EnemyStatic>();
                //        target.TakeDamage(Damage);
                //    }
                //}
                //if (hit.collider.gameObject.tag == "EnemyRanged")
                //{
                //    if (hit.collider.gameObject.GetComponent<EnemyRanged>())
                //    {
                //        EnemyRanged target = hit.collider.gameObject.GetComponent<EnemyRanged>();
                //        target.TakeDamage(Damage);
                //    }

                //}
                //if (hit.collider.gameObject.tag == "EnemyExploding")
                //{
                //    Debug.Log("EnemyExploding hit");
                //    if (hit.collider.gameObject.GetComponent<EnemyExploding>())
                //    {
                //        EnemyExploding target = hit.collider.gameObject.GetComponent<EnemyExploding>();
                //        target.TakeDamage(Damage);
                //    }

                //}
                if (hit.collider.gameObject.tag == "WeakSpot")
            {
                if (hit.collider.gameObject.GetComponent<WeakSpot>())
                {
                    WeakSpot target = hit.collider.gameObject.GetComponent<WeakSpot>();
                    target.SendDamage(Damage);
                }

            }





        }


    }
    private void DecreaseAmmo()
    {
        Ammo--;
       
    }
    void Update()
    {
        if ((Input.GetButton("Fire1") || InputManager.R2()) && Ammo > 0)
        {
            Beam.SetActive(true);
            if (!firing)
            {
                InvokeRepeating("Shoot", 0f, .5f);
                firing = true;
                //Shoot();
                InvokeRepeating("DecreaseAmmo",0f, 0.5f);
            }
           

        }
        else
        {
            firing = false;
            audio.Stop();
            CancelInvoke("Shoot");
            Beam.SetActive(false);
            CancelInvoke("DecreaseAmmo");
        }
       
        
    }

    


}
