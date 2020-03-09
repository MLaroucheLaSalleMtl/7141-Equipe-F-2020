using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (LineRenderer))]
public class BossLaserAbility : MonoBehaviour
{
    [SerializeField] private GameObject targeting;
    [SerializeField] private Transform[] targetingFieldPoints;
    [SerializeField] private GameObject mainBody;

    private AudioSource audio;
    private GameObject target;
    private bool isFiring = false;

    private bool isPaused;
    private LineRenderer laser;
    [Range(0.1f,0.5f)][SerializeField] private float delay = 0.1f;
    private float fireRate;
    private float damage;

    public float FireRate { set => fireRate = value; }
    public float Damage {set => damage = value; }
    private bool IsFiring { get => isFiring; set => isFiring = value; }
    public GameObject Target { set => target = value; }
    public bool IsPaused { get => isPaused; set => isPaused = value; }

    public void StartFiring(float _firerate,float _damage, GameObject _target) {
        laser = GetComponent<LineRenderer>();
        audio = GetComponent<AudioSource>();
        laser.enabled = false;
        FireRate = _firerate;
        Damage = _damage;
        Target = _target;
        IsPaused = false;
        StartCoroutine("Firing");
        
    }

    // Update is called once per frame
    void Update()
    {
        PointsCheck();
        if (IsFiring)
        {
            //CheckAngle();
            RangeCheck();
        }
    }

    private bool PointsCheck() {
        Transform direction = targeting.transform;
        foreach (Transform t in targetingFieldPoints) {
            direction.LookAt(t);
            RaycastHit hit;
            if (Physics.Raycast(targeting.transform.position, direction.forward*30f,out hit)) {
                if (hit.collider.gameObject.tag == "player") {
                    IsFiring = !IsFiring;
                    return true;
                }
            }
        }
        return false;
    }

    private void RangeCheck() {
        Transform direction = targeting.transform;
        direction.LookAt(target.transform);
        RaycastHit hit;
        if (Physics.Raycast(targeting.transform.position, direction.forward * 30f, out hit,Mathf.Infinity, 1 << LayerMask.NameToLayer("Player")))
        {
            if (hit.collider.gameObject.tag == "player" && hit.distance > 30f)
            {
                isFiring = false;
            }
        }
     }

    private void CheckAngle() {
        if ((Mathf.Acos(targeting.transform.localRotation.y) * (180 / Mathf.PI)) - 90  > 45 && (Mathf.Acos(targeting.transform.localRotation.y) * (180 / Mathf.PI)) - 90 < -45) {
            isFiring = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player") {
            IsFiring = !IsFiring;
        }
    }

    IEnumerator Firing() {
        float x = 0;
        while (x >= 0) {
            if (!IsPaused) {
                if (x >= (fireRate - delay)) 
                {
                    x = 0;
                    if (IsFiring)
                    {
                        Shoot();
                    }
                }
                x += 0.1f;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Shoot() {
        targeting.transform.LookAt(target.transform);
        Invoke("Fire", delay);
    }

    private void Fire() {
        
        RaycastHit hit;
        Ray laserDirection =  new Ray(targeting.transform.position, targeting.transform.forward);
        audio.Play();
        if (Physics.Raycast(laserDirection, out hit,100f, (1 << LayerMask.NameToLayer("Player")| 1 << LayerMask.NameToLayer("Ground"))))
        {
            Shootlaser(hit.distance);
            if (hit.collider.gameObject.tag == "player")
            {
                if (hit.collider.gameObject.GetComponent<PlayerHealth>()) {
                    hit.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                }
            }
        }
    }

    private void Shootlaser(float length) {
        laser.enabled = true;
        Ray shootLaser = new Ray();
        shootLaser.origin = targeting.transform.position;
        shootLaser.direction = targeting.transform.forward;
        Vector3[] points = new Vector3[2];
        points[0] = targeting.transform.position;
        points[1] = shootLaser.origin + shootLaser.direction * length;
        laser.positionCount = 2;
        laser.SetPositions(points);
        Invoke("StopLaser", 0.2f);
    }

    private void StopLaser() {
        laser.enabled = false;
    }

}
