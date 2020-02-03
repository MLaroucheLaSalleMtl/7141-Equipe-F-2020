using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] private float GunDamage = 10f;
    [SerializeField] private float GunRange = 150f;
    [SerializeField] private Camera FpsCam;
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }
    private void Shoot()
    {
        RaycastHit Hit;
        if(Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out Hit, GunRange))
        {
            Debug.Log(Hit.transform.name);
        }
    }
}
