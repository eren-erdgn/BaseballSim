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
    private GameManager gameManager;

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
        gameManager = GameManager.Instance;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        
        if (isMoving && agent.remainingDistance <= agent.stoppingDistance)
        {
            
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
            if (currentTargetIndex == 0 )
            {
                
                isMoving = false;
                if (gameManager.CurrentState == GameState.Playing)
                {
                    Debug.Log("Pitcher won Game Ended");
                    Events.OnPitcherAtLastBase.Invoke();
                    gameManager.ChangeState(GameState.End);
                }
                
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