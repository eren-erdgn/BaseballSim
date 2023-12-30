using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{

    private void OnEnable()
    {
        Events.OnGameStart.AddListener(ThrowBall);
    }

    private void OnDisable()
    {
        Events.OnGameStart.RemoveListener(ThrowBall);
    }
    private void ThrowBall()
    {
        Events.OnBallThrowing.Invoke();
    }
}
