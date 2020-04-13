using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyRanged : Enemy
{

    private AudioSource audio;
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public Transform Target;
    float AttackRate = 1f;
    float StartAttackRate;
    public GameObject Projectile;

    [SerializeField] private GameObject[] lootObjs;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
        // the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
        GotoNextPoint();
        //fireRate = startFireRate;
        StartAttackRate = Time.time;
        Target = GameObject.FindGameObjectWithTag("player").transform;
        DetectionDistance = 40;
        StoppingDistance = 20;
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;
        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void LateUpdate()
    {
        Move();
    }


    public override void TakeDamage(float Damage)
    {
        Hp -= Damage;
        if (IsDead())
        {

            Dice dice = Dice.Get_Instance();
            int random = dice.Next(1, lootObjs.Length);
            Vector3 i = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            GameObject lootobj = Instantiate(lootObjs[random], i, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public bool IsDead()
    {
        return (Hp <= 0);
    }

    protected override void Move()
    {
        if (Vector3.Distance(transform.position, Target.position) <= DetectionDistance)
            HasDetected = true;


        if (HasDetected)
        {

            if (Vector3.Distance(transform.position, Target.position) > StoppingDistance)
            {
                agent.destination = Target.position;
                agent.speed = 15f;
            }
            else if (Vector3.Distance(transform.position, Target.position) < StoppingDistance
                && Vector3.Distance(transform.position, Target.position) > RetreatDistance)
            {
                agent.destination = transform.position;
                transform.LookAt(Target);
                Attack();
                agent.speed = 50f;
            }
            else if (Vector3.Distance(transform.position, Target.position) < RetreatDistance)
            {
                agent.destination = Target.position * -1;
                transform.LookAt(Target);
                Attack();
            }

        }
        else
        {
            if (points.Length > 0) {
                agent.speed = AgentSpeed;
                agent.destination = points[destPoint].position;
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    GotoNextPoint();
            }
        }
    }

    protected override void Attack()
    {
        if (Time.time > StartAttackRate)
        {
            GameObject newProjectile = Instantiate(Projectile, transform.position, Quaternion.identity);
            audio.Play();
            newProjectile.transform.LookAt(Target.transform);
            newProjectile.GetComponent<StraightProjectile>().SetDamage(Damage);
            StartAttackRate = Time.time + AttackRate;
        }
    }
}