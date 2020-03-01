using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private int damage;
    [SerializeField] private bool hasDetected = false;
    [SerializeField] private Transform target;
    [SerializeField] private float agentSpeed = 10f;
    [SerializeField] private float stoppingDistance = 5f;
    [SerializeField] private float retreatDistance = 2f;
    [SerializeField] private float detectionDistance = 10f;
    [SerializeField] private float fireRate;
    [SerializeField] private float startFireRate;
    [SerializeField] private GameObject projectile;

    public int Hp { get => hp; set => hp = value; }
    public int Damage { get => damage; set => damage = value; }
    public bool HasDetected { get => hasDetected; set => hasDetected = value; }
    public Transform Target { get => target; set => target = value; }
    public float AgentSpeed { get => agentSpeed; set => agentSpeed = value; }
    public float StoppingDistance { get => stoppingDistance; set => stoppingDistance = value; }
    public float RetreatDistance { get => retreatDistance; set => retreatDistance = value; }
    public float DetectionDistance { get => detectionDistance; set => detectionDistance = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public float StartFireRate { get => startFireRate; set => startFireRate = value; }
    public GameObject Projectile { get => projectile; set => projectile = value; }

    // enemy take damage function
    public abstract void TakeDamage(int damage);

    public abstract bool IsDead();

    public abstract void Move();

    public abstract void Shoot();
}