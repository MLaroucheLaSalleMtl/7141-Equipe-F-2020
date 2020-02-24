using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : GunFactory
{ 
   
    [SerializeField] private GameObject Beam;

    private void Start()
    {
        Damage = 10f;
        Ammo = 1000;
    }

    protected override void Shoot( )
    {
        Beam.SetActive(true);
    }
    private void DecreaseAmmo()
    {
        Ammo--;
       
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && Ammo >= 0)
        {
            Shoot( );
            InvokeRepeating("DecreaseAmmo", 0f, 1f);
           

        }
        else
        {
            Beam.SetActive(false);
            CancelInvoke("DecreaseAmmo");
        }
       
        
    }

    public void AddAmmo(int nbAmmo) {
        Ammo += nbAmmo;
    }


}
