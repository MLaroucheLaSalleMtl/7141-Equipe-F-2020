using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class LaserRifleGunScript : GunFactory
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject barrelEdge;
    [SerializeField] private LineRenderer laser;

    [SerializeField] private LayerMask layerEnemy;
    [SerializeField] private LayerMask layerGround;

    private bool canFire = true;
    // Start is called before the first frame update
    void Start()
    {
        
        laser.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        laser.SetPosition(0, barrelEdge.transform.position);
        if (Input.GetButtonDown("Fire1") && Ammo > 0)
        {
            Shoot();
        }
    }


    protected override void Shoot()
    {
        Ammo -= 2;
        Ray ray;
        ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity , (layerEnemy | layerGround),QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.gameObject.tag == "untagged")
            {
                Shootlaser();
            }
            else
            {
                Shootlaser(hit.point);
                if (hit.collider.gameObject.tag == "CQEnemy")
                {
                    if (hit.collider.gameObject.GetComponent<CQEnemy>())
                    {
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
                        WeakSpot target = hit.collider.gameObject.GetComponent<WeakSpot>();
                        target.SendDamage(Damage);
                    }

                }
            }
        }
    }


    private void Shootlaser()
    {
        Ray direction = new Ray(barrelEdge.transform.position, barrelEdge.transform.forward);
        laser.enabled = true;
        Vector3[] points = new Vector3[2];
        points[0] = barrelEdge.transform.position;
        points[1] = direction.origin + direction.direction * 100;
        laser.positionCount = 2;
        laser.SetPositions(points);
        Invoke("StopLaser", 0.2f);
    }

    private void Shootlaser(Vector3 hit)
    {
        laser.enabled = true;
        Vector3[] points = new Vector3[2];
        points[0] = barrelEdge.transform.position;
        points[1] = hit;
        laser.positionCount = 2;
        laser.SetPositions(points);
        Invoke("StopLaser", 0.2f);
    }


    private void StopLaser()
    {
        laser.enabled = false;
    }

}
