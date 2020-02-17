using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class BossBull : BossBlueprint{
    [SerializeField]private Transform destination1;
    [SerializeField]private Transform destination2;
    private bool waiting = false;


    [SerializeField] private float waitUntilMove = 5f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 15f;

    private void Start()
    {
        BossController = GetComponent<NavMeshAgent>();
        TargetPlayerConstraint = GetComponent<LookAtConstraint>();
        TargetPlayerConstraint.weight = LookAtWeight;
        BossController.speed = Speed;
        IsUsingSpecialAttack = false;
        TargetPlayerConstraint.enabled = false;
        IsActive = false;
        IsMoving = false;  
    }

    private void Update()
    {
        if (IsActive) {
            if (!IsMoving)
            {
                MoveToPosition();
                AimAtPlayer(false);
                TargetPlayerConstraint.constraintActive = false;
                Debug.Log("Boss Destination is  = " + BossController.destination);
            }
            else {

                if (BossController.remainingDistance < 0.2f && !waiting) {
                    TargetPlayerConstraint.constraintActive = true;
                    StartCoroutine("WaitToMove");
                    AimAtPlayer(true);
                    waiting = true;
                }
            }

        }
    }


    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void SpecialAttack()
    {
        throw new System.NotImplementedException();
    }

    public override void TargetPlayer(GameObject player)
    {
        IsActive = true;
        Target = player;
        AddTarget(player);
        TargetPlayerConstraint.enabled = true;
        TargetPlayerConstraint.constraintActive= true;
    }

    protected override void MoveToPosition()
    {
        //BossController.destination = CalculateNewPosition().position;
        int rng = Random.Range(1, 3);
        if (rng < 2) BossController.destination = destination1.transform.position;
        if(rng >=2) BossController.destination = destination1.transform.position;
        IsMoving = true;
    }

    protected override void AimAtPlayer(bool value)
    {
        if (value)
        {
            TargetPlayerConstraint.weight = LookAtWeight;
            
        }
        else
        {
            TargetPlayerConstraint.weight = 0.1f;

        }
    }

    private Transform CalculateNewPosition() {
        Transform newPosition;
        RaycastHit hit,wallHit;
        if (Physics.Raycast(transform.position,Target.transform.position,out hit,Mathf.Infinity)) {
            newPosition = hit.transform;
            newPosition.rotation = getDirectionFromPlayer();
            if (Physics.Raycast(newPosition.position, newPosition.forward, out wallHit, minDistance))
            {
                //newPosition.position = newPosition.forward * (minDistance - 6f) + newPosition.position;
                newPosition.Translate(newPosition.forward * (minDistance - 6f), Space.World);
            }
            else
            {
                //newPosition.position = newPosition.forward * (minDistance) + newPosition.position;
                newPosition.Translate(newPosition.forward * minDistance, Space.World);
            }
            return newPosition;
        }
        return null;
    }

    private Quaternion getDirectionFromPlayer() {
        Transform myTransPosition = this.transform;
        myTransPosition.LookAt(Target.transform);
        float range = Random.Range(-30f, 30f);
        myTransPosition.Rotate(myTransPosition.rotation.x, myTransPosition.rotation.y+180f+range, myTransPosition.rotation.z, Space.Self);
        return myTransPosition.rotation;
    }

    IEnumerator WaitToMove() {
        for (float t = waitUntilMove; t >0; t-= 0.1f) {
            if (t < 0.1) {
                IsMoving = false;
                waiting = false;
            }
            Debug.Log("Time left = " + t);
            yield return new WaitForSeconds(0.1f);
        }
    }

}
