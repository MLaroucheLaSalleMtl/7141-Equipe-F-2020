using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUpdate : MonoBehaviour
{

    public static int textammo;

  
    void Update()
    {
        //textammo = GunScript.ammo;
        this.GetComponent<Text>().text = "" + textammo;
    }
}
