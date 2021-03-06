﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimboRight : GunFactory
{
    private bool Isreloading;
[SerializeField] private Rigidbody plasmaBall;
[SerializeField] private Transform cannon;
private float internalammo;
[Range(0.0f, 1.0f)] protected float fireRate = 0.3f;
protected float lastShot = 0.0f;
    private AudioSource audio;

// Start is called before the first frame update
void Start()
{
    Range = 15f;
    Damage = 10f;
    Ammo = 100;
        audio = GetComponent<AudioSource>();
}

// Update is called once per frame
void Update()
{
    if ((Input.GetButtonDown("Fire2") || InputManager.R2()) && Time.time > fireRate + lastShot )
    {
        Shoot();
            Ammo--;
    }
}
protected override void Shoot()
{
    Rigidbody PlasmaBallInstance;
    audio.Play();
    PlasmaBallInstance = Instantiate(plasmaBall, cannon.position, Quaternion.identity) as Rigidbody;
    PlasmaBallInstance.AddForce(cannon.forward * 2000);
    Destroy(PlasmaBallInstance.gameObject, 1.6f);
    lastShot = Time.time;
}
    
}