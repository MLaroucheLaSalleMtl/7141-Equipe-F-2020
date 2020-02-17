using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : GunFactory
{ 
   
    [SerializeField] private GameObject Beam;

    private void Start()
    {
        Damage = 10f;
        Ammo = 100;
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
        if (Input.GetButton("Fire1") && Ammo >= 1)
        {
            Shoot( );
            InvokeRepeating("DecreaseAmmo", .5f, .5f);
            Debug.Log(Ammo);

        }

        if(Input.GetButtonUp("Fire1"))
        {
            Beam.SetActive(false);
            CancelInvoke("DecreaseAmmo");
        }
        
    }

    public void AddAmmo(int nbAmmo) {
        Ammo += nbAmmo;
    }


}
