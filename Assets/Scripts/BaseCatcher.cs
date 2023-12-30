using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCatcher : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private BaseCatcher _baseCatcher;
    private float _pickupDistance = 1.5f;
    [SerializeField] private Transform ballHolder;
    BaseCatcherManager manager;
    private Vector3 _nextBaseCatcherPos;
    private bool _isGameEnded = false;
    private bool isBallLooping = true;
    // Start is called before the first frame update
    void Start()
    {
        _baseCatcher = GetComponent<BaseCatcher>();
        manager = BaseCatcherManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.GetIndexOfBaseCatcher(_baseCatcher) == 2)
        {
            _isGameEnded = true;
        }

        if (_isGameEnded == false && isBallLooping == true)
        {
            
            if (Vector3.Distance(transform.position, ball.transform.position) <= _pickupDistance)
            {
                ball.transform.parent = ballHolder;
                ball.transform.localPosition = Vector3.zero;
                ball.transform.parent = null;
                ThrowNextBaseCatcher();

            }
        }
        
    }
    private void ThrowNextBaseCatcher()
    {
        
        isBallLooping = false;
        _nextBaseCatcherPos = manager.GetPosOfNextBaseCathcer(manager.GetIndexOfBaseCatcher(_baseCatcher));
        Events.OnBallGoesToNextBaseCatcher.Invoke(_nextBaseCatcherPos);
    }
}
