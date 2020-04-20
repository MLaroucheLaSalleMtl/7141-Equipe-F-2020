using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ammoText : MonoBehaviour
{
    [SerializeField] private Text aText;
    [SerializeField] private GameObject bTextHolder;
    [SerializeField] private Text bText;
    private GunFactory gunObject;
    private GunFactory gunObject2;
    private bool isAkimbo;

    public void SetGun(GameObject gun) {
        bTextHolder.SetActive(false);
        isAkimbo = false;
        if (gun.GetComponent<GunFactory>()) {
            gunObject = gun.GetComponent<GunFactory>();
        }
    }

    public void SetGun(GameObject gun, bool val)
    {
        isAkimbo = true;
        bTextHolder.SetActive(val);
        if (gun.GetComponentInChildren<KimboRight>())
        {
            gunObject = gun.GetComponentInChildren<KimboRight>();
        }
        if (gun.GetComponentInChildren<KimboLeft>())
        {
            gunObject2 = gun.GetComponentInChildren<KimboLeft>();
        }
    }
    void Update()
    {
        if (gunObject != null) {
            aText.text = "" + gunObject.Ammo.ToString();
            if (isAkimbo)
            {
                bText.text = gunObject2.Ammo.ToString();
            }
        }
        
    }
}
