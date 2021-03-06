﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : GunFactory
{

    [SerializeField] Camera cam;
    [SerializeField] private GameObject impact;
    private Animator anim;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        Ammo = 50;
        Range = 50f;
        Damage = 25f;
        anim = gameObject.GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Fire1") || InputManager.R2()) && Ammo > 0)
        {
            Shoot();
        }
    }

    protected override void Shoot()
    {
        RaycastHit hit;
        Ammo--;
        anim.Play("tilt");
        audio.Play();
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Range))
        {
            Debug.Log("Hit " + hit.transform.name);
            //anim.SetTrigger("tilt");
            
            if (hit.collider.gameObject.tag == "Enemy")
            {
                if (hit.collider.gameObject.GetComponent<Enemy>())
                {
                    Enemy target = hit.collider.gameObject.GetComponent<Enemy>();
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
            Debug.DrawRay(transform.position, transform.forward, Color.green,5f);
            GameObject obj = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            //impact.transform.localscale = new Vector3(5f, 5f, 5f); 
            Destroy(obj, .1f);
        }
    }


}


