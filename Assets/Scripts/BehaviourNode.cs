using UnityEngine;

// Базовый класс для всех узлов поведения
public abstract class BehaviourNode
{
    public abstract bool Execute();
}

public class DistanceCondition : BehaviourNode
{
    private Transform _player;
    private Transform _bot;
    private float _distanceThreshold;

    public DistanceCondition(Transform bot, Transform player, float distanceThreshold)
    {
        _bot = bot;
        _player = player;
        _distanceThreshold = distanceThreshold;
    }

    public override bool Execute()
    {
        float distance = Vector3.Distance(_bot.position, _player.position);
        Debug.Log("Distance to player: " + distance);
        return distance <= _distanceThreshold;
    }
}

public class MoveTowardsPlayer : BehaviourNode
{
    private Transform _bot;
    private Transform _player;
    private float _speed;
    private float _moveDistance;  
    private Vector3 _startPosition;
    private bool _isMoving;

    public MoveTowardsPlayer(Transform bot, Transform player, float speed, float moveDistance)
    {
        _bot = bot;
        _player = player;
        _speed = speed;
        _moveDistance = moveDistance;
        _isMoving = false;
    }

    public override bool Execute()
    {
        if (!_isMoving)
        {
            _startPosition = _bot.position;
            _isMoving = true;
        }
        
        Vector3 direction = (_player.position - _bot.position).normalized;
        direction.y = 0;
        
        _bot.Translate(direction * _speed * Time.deltaTime, Space.World);
        
        float distanceMoved = Vector3.Distance(_startPosition, _bot.position);

        if (distanceMoved >= _moveDistance)
        {
            _isMoving = false;
            return true;  
        }

        return false;  
    }
}

public class MoveAwayFromPlayer : BehaviourNode
{
    private Transform _bot;
    private Transform _player;
    private float _speed;
    private float _moveDistance;  
    private Vector3 _startPosition;
    private bool _isMoving;

    public MoveAwayFromPlayer(Transform bot, Transform player, float speed, float moveDistance)
    {
        _bot = bot;
        _player = player;
        _speed = speed;
        _moveDistance = moveDistance;
        _isMoving = false;
    }

    public override bool Execute()
    {
        if (!_isMoving)
        {
            _startPosition = _bot.position;
            _isMoving = true;
        }
        
        Vector3 direction = (_bot.position - _player.position).normalized;
        direction.y = 0;
        
        _bot.Translate(direction * _speed * Time.deltaTime, Space.World);
        
        float distanceMoved = Vector3.Distance(_startPosition, _bot.position);

        if (distanceMoved >= _moveDistance)
        {
            _isMoving = false;
            return true;  
        }

        return false;  
    }
}

// Узел-последователь (Sequence) для последовательных действий
public class SequenceNode : BehaviourNode
{
    private BehaviourNode[] _nodes;

    public SequenceNode(params BehaviourNode[] nodes)
    {
        _nodes = nodes;
    }

    public override bool Execute()
    {
        foreach (var node in _nodes)
        {
            if (!node.Execute())
                return false;  
        }
        return true;
    }
}
