﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ammoText : MonoBehaviour
{
    [SerializeField] private Text aText;
    [SerializeField] private GunScript gunObject;
    void Update()
    {
        aText.text = "" + gunObject.Ammo.ToString();
    }
}