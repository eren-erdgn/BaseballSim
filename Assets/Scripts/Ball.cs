using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private bool _isBallMoving;
    private Vector3 _targetPosition;
    private Vector3 _startPosition;
    private float _speed = 8.5f;
    private float _lerpTime = 0.0f;
    private Vector3 _firstBaseCatcherPos;
    private Vector3 _nextBaseCatcherPos;
    private Vector3 _lastBaseCatcherPos;
    [SerializeField] private Transform hitArea;
    [SerializeField] private float curveHeight = 5.0f;
    private GameManager gameManager;

    private void OnEnable()
    {
        Events.OnBallHiting.AddListener(StartGoRandom);
        Events.OnBallThrowing.AddListener(StartThrow);
        Events.OnBallCatched.AddListener(ThrowFirstBaseCatcher);
        Events.OnBallGoesToNextBaseCatcher.AddListener(ThrowNextBaseCatcher);

    }

    private void OnDisable()
    {

        Events.OnBallHiting.RemoveListener(StartGoRandom);
        Events.OnBallThrowing.RemoveListener(StartThrow);
        Events.OnBallCatched.RemoveListener(ThrowFirstBaseCatcher);
        Events.OnBallGoesToNextBaseCatcher.RemoveListener(ThrowNextBaseCatcher);
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        _lastBaseCatcherPos = BaseCatcherManager.Instance.GetPosOfLastBaseCathcer(2);
    }

    // ... (other existing methods)

    void Update()
    {
        if (_isBallMoving)
        {
            _lerpTime += Time.deltaTime * _speed;
            float t = _lerpTime / Vector3.Distance(_startPosition, _targetPosition);

            // Calculate the position based on a quadratic Bezier curve
            Vector3 currentPos = CalculateBezierPoint(_startPosition, _targetPosition, curveHeight, t);

            gameObject.transform.position = currentPos;
            if (t >= 1.0f)
            {
                _isBallMoving = false;
                if(_targetPosition == _lastBaseCatcherPos && gameManager.CurrentState == GameState.Playing)
                { 
                    Debug.Log("BaseCatcher won Game Ended");
                    Events.OnBallAtLastBaseCatcher.Invoke();
                    gameManager.ChangeState(GameState.End);
                }
                if (_targetPosition == hitArea.position)
                {
                    Events.OnBallAtHitArea.Invoke();
                }
                
            }
        }
        
    }

    Vector3 CalculateBezierPoint(Vector3 start, Vector3 end, float height, float t)
    {
        // Calculate the intermediate control point
        Vector3 controlPoint = Vector3.Lerp(start, end, 0.5f) + Vector3.up * height;

        // Calculate the position on the quadratic Bezier curve
        return Mathf.Pow(1 - t, 2) * start + 2 * (1 - t) * t * controlPoint + Mathf.Pow(t, 2) * end;
    }
    void StartGoRandom()
    {
        _isBallMoving = true;
        _startPosition = transform.position;

        Renderer planeRenderer = GameObject.FindWithTag("Plane").GetComponent<Renderer>();
        float planeWidth = planeRenderer.bounds.size.x;
        float planeHeight = planeRenderer.bounds.size.z;
        float quarterWidth = planeWidth / 2f;
        float quarterHeight = planeHeight / 2f;
        float randomX = Random.Range(-quarterWidth, quarterWidth);
        float randomZ;
        if (randomX < 0)
        {
            randomZ = Random.Range(-quarterHeight, quarterHeight);
        }
        else
        {
            randomZ = Random.Range(0, quarterHeight);
        }
        _targetPosition = new Vector3(randomX, 0.25f, randomZ);
        Events.OnBallHitted.Invoke(_targetPosition);
        _lerpTime = 0.0f;


    }
    void StartThrow()
    {
        _isBallMoving = true;
        _startPosition = transform.position;
        _targetPosition = hitArea.position;
        _lerpTime = 0.0f;

    }
    void ThrowFirstBaseCatcher(Vector3 firstBaseCatcher)
    {
        _firstBaseCatcherPos = firstBaseCatcher;
        _isBallMoving = true;
        _startPosition = transform.position;
        _targetPosition = _firstBaseCatcherPos;
        _lerpTime = 0.0f;

    }
    private void ThrowNextBaseCatcher(Vector3 nextBaseCatcher)
    {
        
        _nextBaseCatcherPos = nextBaseCatcher;
        _isBallMoving = true;
        _startPosition = transform.position;
        _targetPosition = _nextBaseCatcherPos;
        _lerpTime = 0.0f;
    }
}