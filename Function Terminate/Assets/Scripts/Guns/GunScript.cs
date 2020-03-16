using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : GunFactory
{
    [SerializeField] private Camera playerCamera;  
    //[SerializeField] private GameObject barrel;
    [SerializeField] private GameObject Beam;

    private void Start()
    {
        Range = 15f;
        Damage = 10f;
        Ammo = 1000;
        
    }

    protected override void Shoot( )
    {
        RaycastHit hit;
        Beam.SetActive(true);
        //Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward*Range, Color.red, 5f);
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Range))
        {
            Debug.DrawLine(hit.point, hit.point, Color.black, 5f);
            if (hit.collider.gameObject.tag == "CQEnemy") {
                if (hit.collider.gameObject.GetComponent<CQEnemy>()) {
                    CQEnemy target = hit.collider.gameObject.GetComponent<CQEnemy>();
                    target.TakeDamage(Damage);
                }

            }
            if (hit.collider.gameObject.tag == "EnemyStatic")
            {
                if (hit.collider.gameObject.GetComponent<EnemyStatic>())
                {
                    EnemyStatic target = hit.collider.gameObject.GetComponent<EnemyStatic>();
                    target.TakeDamage(Damage);
                }
            }
            if (hit.collider.gameObject.tag == "EnemyRanged")
            {
                if (hit.collider.gameObject.GetComponent<EnemyRanged>())
                {
                    EnemyRanged target = hit.collider.gameObject.GetComponent<EnemyRanged>();
                    target.TakeDamage(Damage);
                }

            }
            if (hit.collider.gameObject.tag == "WeakSpot")
            {
                if (hit.collider.gameObject.GetComponent<WeakSpot>())
                {
                    WeakSpot  target = hit.collider.gameObject.GetComponent<WeakSpot>();
                    target.SendDamage(Damage);
                }

            }


            //EnemyStatic target2 = hit.transform.GetComponent<EnemyStatic>();
            //EnemyRanged target3 = hit.transform.GetComponent<EnemyRanged>();
            //WeakSpot BossTarget = hit.transform.GetComponent<WeakSpot>();
            //if (target.gameObject.CompareTag("EnemyStatic"))
            //{
            //    target2.TakeDamage(Damage);//Demo HAVE TO CHANGE ENEMY BP

            //}
            //if (target.gameObject.CompareTag("EnemyRanged"))
            //{
            //    target3.TakeDamage(Damage);//Demo HAVE TO CHANGE ENEMY BP


            //}

            //if (target.gameObject.CompareTag("WeakSpot"))
            //{
            //    BossTarget.SendDamage(Damage);
            //}



        }


    }
    private void DecreaseAmmo()
    {
        Ammo--;
       
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && Ammo >= 0)
        {
            Shoot();
            Invoke("DecreaseAmmo", 1f);
           

        }
        else
        {
            Beam.SetActive(false);
            CancelInvoke("DecreaseAmmo");
        }
       
        
    }

    


}
