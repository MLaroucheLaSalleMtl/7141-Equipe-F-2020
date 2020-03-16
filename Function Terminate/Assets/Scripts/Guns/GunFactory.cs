using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunFactory : MonoBehaviour
{
    [SerializeField]private float damage;
    [SerializeField]private float range;
    [SerializeField]private int ammo;
    [SerializeField]private float fireRate;

    protected float Damage { get => damage; set => damage = value; }
    protected float Range { get => range; set => range = value; }
    public int Ammo { get => ammo; set => ammo = value; }
    protected float FireRate { get => fireRate; set => fireRate = value; }

    protected abstract void Shoot() ;


    public void AddAmmo(int nbAmmo)
    {
        Ammo += nbAmmo;
    }
}
