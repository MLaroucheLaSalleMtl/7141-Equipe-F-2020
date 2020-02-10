using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{

    /// <summary>
    /// A faire
    /// </summary>

    private int enemyType = 0;
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    [SerializeField] bool hasDetected = false;
    [SerializeField] private Transform target;
    [SerializeField] private float agentSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        if(enemyType == 11)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.autoBraking = false;
            GotoNextPoint();
        }
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
