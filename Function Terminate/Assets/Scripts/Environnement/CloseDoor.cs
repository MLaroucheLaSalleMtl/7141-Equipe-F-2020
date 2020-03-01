using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Animator))]
public class CloseDoor : MonoBehaviour
{
    private Animator anim;
    private bool hasBeenClosedOnce = false;

    private void Start()
    {
        hasBeenClosedOnce = false;
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player") {
            if (!hasBeenClosedOnce)
            {
                hasBeenClosedOnce = true;
                anim.SetBool("IsClosed", true);
            }
        }
    }

    public void OpenDoor() {
        anim.SetBool("IsClosed", false);
    }

}
