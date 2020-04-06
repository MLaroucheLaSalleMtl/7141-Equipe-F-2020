using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlowing : MonoBehaviour
{
    Vector3 newPos;
    private Transform target;
    private bool isTargeting = false;
    private Vector3 originalPos;
    [Range(0f, 1f)] [SerializeField] private float lerpRate = 0.02f;
    [Range(-20f,20f)][SerializeField] private float offSet = 0;
    [Range(0,20f)][SerializeField] private float maxDisplacement = 0;


    public void TargetPlayer(Transform newtarget)
    {
        target = newtarget;
        isTargeting = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPos = this.transform.position;
        isTargeting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTargeting) {
            if (maxDisplacement == 0)
            {
                newPos = new Vector3(target.transform.position.x + offSet, this.transform.position.y, this.transform.position.z);
            }
            else {
                if (target.transform.position.x-originalPos.x < maxDisplacement && target.transform.position.x - originalPos.x > -maxDisplacement) {
                    newPos = new Vector3(target.transform.position.x + offSet, this.transform.position.y, this.transform.position.z);
                }
            }
            this.transform.position = Vector3.Lerp(this.transform.position, newPos, lerpRate);
        }
    }
}
