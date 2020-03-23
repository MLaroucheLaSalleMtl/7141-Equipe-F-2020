using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Transform myTransform;
    [SerializeField] private int ammount;
    [SerializeField] private PichUpType type;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // changed y and z angles
        myTransform.Rotate(0.1f, 0f, 0f, Space.Self);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            switch (type) {
                case PichUpType.Ammo:
                    if (other.gameObject.GetComponentInChildren<GunFactory>()) {
                        other.gameObject.GetComponentInChildren<GunFactory>().AddAmmo(ammount);
                    }
                    break;
                case PichUpType.Health:
                    if (other.gameObject.GetComponent<PlayerHealth>())
                    {
                        other.gameObject.GetComponent<PlayerHealth>().AddHealth(ammount);
                    }
                    break;
                case PichUpType.Defense:
                    if (other.gameObject.GetComponent<PlayerHealth>())
                    {
                        other.gameObject.GetComponent<PlayerHealth>().AddDefence(ammount);
                    }
                    break;
                case PichUpType.Speed:
                    if (other.gameObject.GetComponent<CharacterMovement>())
                    {
                        other.gameObject.GetComponent<CharacterMovement>().SS(10f);
                    }
                    break;
                case PichUpType.Upgrade:
                    if (other.gameObject.GetComponent<PlayerHealth>())
                    {

                    }
                    break;
                case PichUpType.Power:
                    //ToDo
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}

internal enum PichUpType{
    Ammo,Health,Power,Defense,Upgrade,Speed
}