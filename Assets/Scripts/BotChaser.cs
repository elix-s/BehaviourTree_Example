using System.Collections;
using UnityEngine;

public class BotChaser : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _chaseSpeed = 3f;
    [SerializeField] private float _stopTime = 2f;
    [SerializeField] private float _chaseDistance = 2f;

    private BehaviourNode _behaviourTree;
    private bool _isStopped = false;

    void Start()
    {
        _behaviourTree = new SequenceNode(
            new DistanceCondition(transform, _player, 2f),
            new MoveTowardsPlayer(transform, _player, _chaseSpeed, _chaseDistance)
        );
    }

    void Update()
    {
        if (!_isStopped && _behaviourTree.Execute())
        {
            StartCoroutine(StopBot());
        }
    }

    private IEnumerator StopBot()
    {
        Debug.Log("Bot stopped");
        _isStopped = true;
        yield return new WaitForSeconds(_stopTime);
        Debug.Log("Bot moving again");
        _isStopped = false;
    }
}