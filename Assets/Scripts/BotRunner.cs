using System.Collections;
using UnityEngine;

public class BotRunner : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float runSpeed = 3f;
    [SerializeField] private float stopTime = 2f;
    [SerializeField] private float runDistance = 2f;  

    private BehaviourNode _behaviourTree;
    private bool _isStopped = false;

    private void Start()
    {
        _behaviourTree = new SequenceNode(
            new DistanceCondition(transform, _player, 2f),
            new MoveAwayFromPlayer(transform, _player, runSpeed, runDistance)
        );
    }

    private void Update()
    {
        if (!_isStopped && _behaviourTree.Execute())
        {
            StartCoroutine(StopBot());
        }
    }

    private IEnumerator StopBot()
    {
        _isStopped = true;
        yield return new WaitForSeconds(stopTime);  
        _isStopped = false;
    }
}