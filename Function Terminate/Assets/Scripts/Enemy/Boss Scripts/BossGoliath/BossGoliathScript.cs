using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BossGoliathScript : BossBlueprint {
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private Transform[] fireLocations;
    [SerializeField]private GoliathMainBody mainBody;
    [SerializeField] private PlayerFlowing[] followTargets;


    private int attackPositioninUse = 0;
    private int DamageAttackRate = 1;

// Start is called before the first frame update
    void Start(){
        DamageAttackRate = 1;
        attackPositioninUse = 0;
    }
    public override void TargetPlayer(GameObject player)
    {
        IsActive = true;
        Target = player;
        mainBody.TargetPlayer(Target.transform);
        foreach (PlayerFlowing target in followTargets) {
            target.TargetPlayer(Target.transform);
        }
        ConstraintSource aimAtSource = new ConstraintSource();
        aimAtSource.weight = 1;
        aimAtSource.sourceTransform = Target.transform;
        GetComponent<LookAtConstraint>().AddSource(aimAtSource);
        GetComponent<LookAtConstraint>().constraintActive = true;
        foreach (Transform t in fireLocations) {
            if (t.GetComponent<LookAtConstraint>())
            {
                t.GetComponent<LookAtConstraint>().AddSource(aimAtSource);
                t.GetComponent<LookAtConstraint>().constraintActive = true;
            }
        }
        StartCoroutine("WaitToAttack");
    }

    protected override void AimAtPlayer(bool value)
    {
        throw new System.NotImplementedException();
    }

    protected override void Attack()
    {
        GameObject projectile = Instantiate(fireBallPrefab, fireLocations[attackPositioninUse].transform.position, fireLocations[attackPositioninUse].transform.rotation);
        projectile.GetComponent<FireBallScript>().SetDamage(Damage);
        attackPositioninUse++;
        if (attackPositioninUse >= fireLocations.Length) {
            attackPositioninUse = 0;
        }
    }

    protected override void MoveToPosition()
    {
        throw new System.NotImplementedException();
    }

    protected override void SpecialAttack()
    {
        throw new System.NotImplementedException();
    }

// Update is called once per frame
    void Update()
    {
        if (HealthPoints < HealthPoints * 0.75) {
            DamageAttackRate = 2;
        } else if (HealthPoints < HealthPoints * 0.5) {
            DamageAttackRate = 3;
        }
        else if (HealthPoints < HealthPoints * 0.25)
        {
            DamageAttackRate = 4;
        }
    }

    IEnumerator WaitToAttack()
    {
        do {
            for (float t = (RateNormalAttack / 2) / DamageAttackRate; t > 0; t -= 0.1f)
            {
                if (t < 0.05)
                {
                    Attack();
                }
                yield return new WaitForSeconds(0.1f);
            }
        }while (true) ;
    }


}
