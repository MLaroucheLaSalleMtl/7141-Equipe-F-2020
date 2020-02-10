using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : GunFactory
{ 

    [SerializeField] private new float Damage = 10f;
    [SerializeField] private new int Ammo = 100;
    [SerializeField] private GameObject Beam;

    // [SerializeField] private Camera FpsCam;
    private void Start()
    {
        Damage = 10f;
        Ammo = 100;
    }

    protected override void Shoot( )
    {
        Beam.SetActive(true);
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && Ammo >= 1)
        {
            Shoot( );

        }

        if(Input.GetButtonUp("Fire1"))
        {
            Beam.SetActive(false);
        }
    }

    public void AddAmmo(int nbAmmo) {
        Ammo += nbAmmo;
    }


}
