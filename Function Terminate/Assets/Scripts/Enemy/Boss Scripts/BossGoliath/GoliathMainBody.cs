using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoliathMainBody : MonoBehaviour
{
    private Transform target;
    [SerializeField]private Transform targetRotation;
    private bool isTargeting = false;
    [Range(0f,1f)][SerializeField] private float lerpRate = 0.20f;

    private void Start()
    {
        targetRotation.rotation = transform.rotation;
        isTargeting = false;
    }

    public void TargetPlayer(Transform newtarget) {
        target = newtarget;
        isTargeting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTargeting)
        {
            targetRotation.LookAt(target);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation.rotation, lerpRate);
        }
    }
}
