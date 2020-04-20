using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public abstract class Enemy : MonoBehaviour
{

    [SerializeField] private AudioClip[] damageSounds;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip deathSound;
    private AudioSource audio;
    [SerializeField]private float hp;
    [SerializeField]private int damage;
     private bool hasDetected = false;
     //private Transform target;
     private float agentSpeed = 10f;
     private float stoppingDistance = 5f;
     private float retreatDistance = 2f;
     private float detectionDistance = 10f;
     //private float attackRate;
     //private float startAttackRate;
     //private GameObject projectile;

    protected float Hp { get => hp; set => hp = value; }
    protected int Damage { get => damage; set => damage = value; }
    public bool HasDetected { get => hasDetected; set => hasDetected = value; }
    //protected Transform Target { get => target; set => target = value; }
    protected float AgentSpeed { get => agentSpeed; set => agentSpeed = value; }
    protected float StoppingDistance { get => stoppingDistance; set => stoppingDistance = value; }
    protected float RetreatDistance { get => retreatDistance; set => retreatDistance = value; }
    protected float DetectionDistance { get => detectionDistance; set => detectionDistance = value; }
    protected AudioClip[] DamageSounds { get => damageSounds; set => damageSounds = value; }
    protected AudioClip AttackSound { get => attackSound; set => attackSound = value; }
    protected AudioClip DeathSound { get => deathSound; set => deathSound = value; }
    protected AudioSource Audio { get => audio; set => audio = value; }

    //protected float AttackRate { get => AttackRate; set => AttackRate = value; }
    //protected float StartAttackRate { get => startAttackRate; set => startAttackRate = value; }
    //protected GameObject Projectile { get => projectile; set => projectile = value; }

    // enemy take damage function
    public abstract void TakeDamage(float Damage);

    //public abstract bool IsDead();

    protected abstract void Move();

    protected abstract void Attack();
}