using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherManager : MonoBehaviour
{
    [SerializeField] private Catcher[] catchers;


    [SerializeField] private Ball ball;
    private Vector3 _ballDropPos;
    Catcher closestCatcher = null;
    private void OnEnable()
    {
        Events.OnBallHitted.AddListener(AssignCatcherToCatch);
    }
    private void OnDisable()
    {
        Events.OnBallHitted.RemoveListener(AssignCatcherToCatch);
    }
    
    private void Update()
    {
        if(closestCatcher != null)
        {
            closestCatcher.BallTransform = ball.transform;
            closestCatcher.BallPos = new Vector3(ball.transform.position.x, ball.transform.position.y, ball.transform.position.z);
        }
    }
    void AssignCatcherToCatch(Vector3 ballDropPosition)
    {
        
        _ballDropPos = ballDropPosition;
        float closestDistance = Mathf.Infinity;

        foreach (Catcher catcher in catchers)
        {
            float distanceToCatcher = Vector3.Distance(catcher.transform.position, _ballDropPos);
            if (distanceToCatcher < closestDistance)
            {
                closestDistance = distanceToCatcher;
                closestCatcher = catcher;
            }
        }
        closestCatcher.BallDropPos = ballDropPosition;
        closestCatcher.IsClosest = true;
    }

}
