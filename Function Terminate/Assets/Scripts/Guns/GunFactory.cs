using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunFactory : MonoBehaviour
{
    protected float Damage;
    protected float Range;
    protected int Ammo;
    protected float FireRate;
    protected abstract void Shoot() ;

}
