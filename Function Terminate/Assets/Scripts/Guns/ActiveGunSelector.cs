using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveGunSelector : MonoBehaviour
{
    private int nbGunAdded = 0;
    private int maxNbGuns = 2;
    private GameObject[] selectedGuns;

    [SerializeField] GameObject laserRifle;
    [SerializeField] GameObject handgun;
    [SerializeField] GameObject railGun;
    [SerializeField] GameObject akimbo;
    [SerializeField] Button btnLaserRifle;
    [SerializeField] Button btnHandgun;
    [SerializeField] Button btnRailGun;
    [SerializeField] Button btnAkimbo;
    [SerializeField] private ammoText text;
    // Start is called before the first frame update
    void Start()
    {
        selectedGuns = new GameObject[maxNbGuns];
        UnEquipAll();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) )
        {
            EquipGunAT(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipGunAT(1);
        }
    }

    public void SelectRailGun()
    {
        if (nbGunAdded < maxNbGuns) {
            UnEquipAll();
            selectedGuns[nbGunAdded] = railGun;
            text.SetGun(railGun);
            nbGunAdded++;
            railGun.SetActive(true);
            btnRailGun.enabled = false;
        }
    }
    public void SelectLaserRifle()
    {
        if (nbGunAdded < maxNbGuns)
        {
            UnEquipAll();
            selectedGuns[nbGunAdded] = laserRifle;
            laserRifle.SetActive(true);
            text.SetGun(laserRifle);
            nbGunAdded++;
            btnLaserRifle.enabled = false;
        }
    }
    public void SelectAkimbo()
    {
        if (nbGunAdded < maxNbGuns)
        {
            UnEquipAll();
            selectedGuns[nbGunAdded] = akimbo;
            akimbo.SetActive(true);
            text.SetGun(akimbo,true);
            nbGunAdded++;
            btnAkimbo.enabled = false;
        }
    }
    public void SelectHandgun()
    {
        if (nbGunAdded < maxNbGuns)
        {
            UnEquipAll();
            selectedGuns[nbGunAdded] = handgun;
            handgun.SetActive(true);
            text.SetGun(handgun);
            nbGunAdded++;
            btnHandgun.enabled = false;
        }
    }

    private void UnEquipAll() {
        railGun.SetActive(false);
        handgun.SetActive(false);
        laserRifle.SetActive(false);
        akimbo.SetActive(false);
    }

    private void EquipGunAT(int val) {
        if (val < selectedGuns.Length) {
            if (selectedGuns[val] != null) {
                UnEquipAll();
                selectedGuns[val].SetActive(true);
                if (selectedGuns[val] == akimbo)
                {
                    text.SetGun(selectedGuns[val],true);
                }
                else {
                    text.SetGun(selectedGuns[val]);
                }
                
            }
        }
    }

}
