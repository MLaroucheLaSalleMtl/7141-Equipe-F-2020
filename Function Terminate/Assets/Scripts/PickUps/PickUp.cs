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
        myTransform.Rotate(0.5f, 0.1f, -0.1f, Space.Self);
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
                    //ToDo
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
    Ammo,Health,Power,Defense
}