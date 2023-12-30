using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pitcher : MonoBehaviour
{
    [SerializeField] private Transform[] targets;
    NavMeshAgent agent;
    private int currentTargetIndex = 0;
    private bool isMoving = false;

    private void OnEnable()
    {
        Events.OnBallAtHitArea.AddListener(HitBall);
    }

    private void OnDisable()
    {
        Events.OnBallAtHitArea.RemoveListener(HitBall);
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        
        if (isMoving && agent.remainingDistance <= agent.stoppingDistance)
        {
            
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
            if (currentTargetIndex == 0)
            {
                isMoving = false;
               
            }
            else
            {
                SetDestination(targets[currentTargetIndex]);
            }
        }
    }

    private void SetDestination(Transform target)
    {
        agent.destination = target.position;
    }

    void StartMoving()
    {
        isMoving = true;
        SetDestination(targets[currentTargetIndex]);
    }

    private void HitBall()
    {
        StartMoving();
        Events.OnBallHiting.Invoke();
    }

}