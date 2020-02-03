using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Transform myTransform;

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

            //A faire 
            //Ecire le code qui affect le joueur
            Destroy(this.gameObject);
        }
    }
}
