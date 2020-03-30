using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGoliathScript : BossBlueprint {

    [SerializeField]private GoliathMainBody mainBody;


// Start is called before the first frame update
    void Start(){

    }
    public override void TargetPlayer(GameObject player)
    {
        IsActive = true;
        Target = player;
        mainBody.TargetPlayer(Target.transform);
    }

    protected override void AimAtPlayer(bool value)
    {
        throw new System.NotImplementedException();
    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
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
        if (Input.GetKeyDown(KeyCode.F)) {
            TargetPlayer(GameObject.FindGameObjectWithTag("player"));
        }
    }

}
