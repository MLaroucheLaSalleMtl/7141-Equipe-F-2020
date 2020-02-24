using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class BossBull : BossBlueprint{
    [SerializeField] private GameObject mainBody;
    [SerializeField]private Transform destination;
    [SerializeField]private Transform targeting;
    [SerializeField] private float chargeSpeed = 75;
    private bool isStunned,waiting = false;
       [SerializeField]private bool isCharging = false;
    
    [SerializeField] private BossLaserAbility[] laserHorns;

    [SerializeField] private float waitUntilMove = 5f;
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 10f;

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
            targeting.LookAt(Target.transform);
            if (!isCharging)
            {
                if (!IsMoving)
                {
                    MoveToPosition();
                    AimAtPlayer(false);
                    TargetPlayerConstraint.constraintActive = false;
                }
                else
                {

                    if (BossController.remainingDistance < 2.5f && !waiting)
                    {
                        TargetPlayerConstraint.constraintActive = true;
                        BossController.isStopped = true;
                        StartCoroutine("WaitToMove");
                        AimAtPlayer(true);
                        waiting = true;
                    }
                }
            }
            else {
                if (IsMoving && !isStunned) {
                    BossController.Move(mainBody.transform.right*chargeSpeed*Time.deltaTime);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            SpecialAttack();

        }
    }

    private void PauseLasers(bool state) {
        foreach (BossLaserAbility laser in laserHorns) {
            laser.IsPaused = state;
        }
    }

    protected override void Attack()
    {
        //unused on this boss
    }

    protected override void SpecialAttack()
    {
        isCharging = true;
        IsMoving = false;
        BossController.enabled = false;
        PauseLasers(true);
        Debug.Log("Charging");
        Invoke("ChargeAttack", 3f);
    }

    private void ChargeAttack() {
        Debug.Log("Charged");
        TargetPlayerConstraint.enabled = false;
        BossController.enabled = true;
        PauseLasers(false);
        IsMoving = true;
        Invoke("EndCharge", 2f);
    }

    private void EndCharge() {
        isCharging = false;
        IsMoving = false;
        isStunned = false;
        BossController.enabled = true;
        TargetPlayerConstraint.enabled = true;
        TargetPlayerConstraint.constraintActive = true;
        TargetPlayerConstraint.weight = 1f;
    }

    private void Stunned() {
        Invoke("EndCharge", 3f);
    }

    public override void TargetPlayer(GameObject player)
    {
        IsActive = true;
        Target = player;
        AddTarget(player);
        TargetPlayerConstraint.enabled = true;
        TargetPlayerConstraint.constraintActive= true;
        destination.SetParent(null, true);
        foreach (BossLaserAbility horn in laserHorns) {
            horn.StartFiring(RateNormalAttack, Damage, Target);
        }
    }

    protected override void MoveToPosition()
    {
        BossController.destination = CalculateNewPosition().position;
        BossController.isStopped = false;
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
        targeting.LookAt(Target.transform);
        RaycastHit hit,wallHit;
        Ray direction = new Ray(targeting.position, targeting.forward);
        if (Physics.Raycast(direction,out hit,Mathf.Infinity,(1 << LayerMask.NameToLayer("Player")))) {
            destination.position = hit.collider.gameObject.transform.position;
            destination.rotation = getDirectionFromPlayer();
            if (Physics.Raycast(Target.transform.position, destination.forward, out wallHit, maxDistance))
            {
                Debug.Log("Wall Hit ");
                destination.position = wallHit.collider.transform.position;
                destination.position += destination.forward * minDistance;
            }
            else
            {
                destination.position +=  destination.forward * maxDistance;
            }
            return destination;
        }
        return Target.transform;
    }

    private Quaternion getDirectionFromPlayer() {
        float range = Random.Range(-30f , 30f);
        destination.LookAt(mainBody.transform);
        destination.Rotate(0,range, 0, Space.Self);
        return destination.rotation;
    }

    IEnumerator WaitToMove() {
        for (float t = waitUntilMove; t > 0; t-= 0.1f) {
            if (t < 0.15) {
                IsMoving = false;
                waiting = false;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Boss Colidier Triggered");
        if (collision.gameObject.layer ==  LayerMask.NameToLayer("Ground")) {
            if(isCharging)Stunned();
        }
    }

}
