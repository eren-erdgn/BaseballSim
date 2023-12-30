using UnityEngine;
using UnityEngine.AI;

public class Catcher : MonoBehaviour
{
    [SerializeField] private float pickupDistance = 1.5f; // Set this distance as needed

    private bool _isClosest;
    private NavMeshAgent agent;
    private Transform ballTransform;
    [SerializeField] private Transform ballHolder;
    private Vector3 _ballPos;
    private Vector3 _ballDropPos;
    private bool _isBallGoesToFirstBase = false;
    [SerializeField] private BaseCatcher firstBaseCatcher;
    private Vector3 firstBaseCatcherPos;
    public bool IsClosest { get => _isClosest; set => _isClosest = value; }
    public Vector3 BallDropPos { get => _ballDropPos; set => _ballDropPos = value; }
    public Vector3 BallPos { get => _ballPos; set => _ballPos = value; }
    public Transform BallTransform { get => ballTransform; set => ballTransform = value; }
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_isClosest && _ballDropPos != Vector3.zero && _isBallGoesToFirstBase == false)
        {
            agent.SetDestination(_ballDropPos);

            // Check if the ball is close enough to the catcher
            if (Vector3.Distance(transform.position, _ballPos) <= pickupDistance)
            {
                ballTransform.parent = ballHolder;
                ballTransform.localPosition = Vector3.zero;
                ballTransform.parent = null;
                ThrowBallFirstBaseCatcher();

            }
        }
    }
    private void ThrowBallFirstBaseCatcher()
    {
        Transform firstBaseCatcherTransform = firstBaseCatcher.GetComponent<Transform>();
        firstBaseCatcherPos = new Vector3(firstBaseCatcherTransform.position.x, firstBaseCatcherTransform.position.y, firstBaseCatcherTransform.position.z);
        Events.OnBallCatched.Invoke(firstBaseCatcherPos);
        _isBallGoesToFirstBase = true;
    }
}