using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(LookAtConstraint))]
public abstract class BossBlueprint : MonoBehaviour
{
    private GameObject target;
    private bool isMoving,isActive;
    private bool isUsingSpecialAttack;
    private NavMeshAgent bossController;
    private LookAtConstraint targetPlayerConstraint;
    [SerializeField] private float healthPoints;
    [SerializeField] private float damage;
    [SerializeField] private float specialDamage;
    [SerializeField] private float speed;
    [SerializeField] private float rateSpecialAttack;
    [SerializeField] private float rateNormalAttack;
    [Range(0.2f,1.0f)][SerializeField] private float lookAtWeight = 0.5f;
    
    protected float HealthPoints { get => healthPoints; set => healthPoints = value; }
    protected float Damage { get => damage; set => damage = value; }
    protected float SpecialDamage { get => specialDamage; set => specialDamage = value; }
    protected float Speed { get => speed; set => speed = value; }
    protected GameObject Target { set => target = value; get => target; }
    protected NavMeshAgent BossController { get => bossController; set => bossController = value; }
    protected LookAtConstraint TargetPlayerConstraint { get => targetPlayerConstraint; set => targetPlayerConstraint = value; }
    protected bool IsUsingSpecialAttack { get => isUsingSpecialAttack; set => isUsingSpecialAttack = value; }
    protected bool IsMoving { get => isMoving; set => isMoving = value; }
    protected float LookAtWeight { get => lookAtWeight; set => lookAtWeight = value; }
    protected bool IsActive { get => isActive; set => isActive = value; }
    protected float RateNormalAttack { get => rateNormalAttack; set => rateNormalAttack = value; }
    protected float RateSpecialAttack { get => rateSpecialAttack; set => rateSpecialAttack = value; }

    protected abstract void SpecialAttack();
    protected abstract void Attack();
    protected abstract void MoveToPosition();
    protected abstract void AimAtPlayer(bool value);
    public abstract void TargetPlayer(GameObject player);

    private void Start()
    {
        bossController = GetComponent<NavMeshAgent>();
        targetPlayerConstraint = GetComponent<LookAtConstraint>();
        targetPlayerConstraint.weight = lookAtWeight;
        bossController.speed = Speed;
        isUsingSpecialAttack = false;
        targetPlayerConstraint.enabled = false;
        isMoving = false;
    }

    public void TakeDamage(float damageTaken) {
        HealthPoints -= damageTaken;
    }

    public bool IsActivated() {
        return isActive;
    }

    protected void AddTarget(GameObject target) {
        ConstraintSource aimAtSource = new ConstraintSource();
        aimAtSource.weight = 1;
        aimAtSource.sourceTransform = target.transform;
        targetPlayerConstraint.AddSource(aimAtSource);
    }

}
