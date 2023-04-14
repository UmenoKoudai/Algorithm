using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] int _moveSpeed;
    [SerializeField] float _stopDistance;
    Rigidbody2D _rb;
    Queue<Position> route = new Queue<Position>();
    Position _nextPosition;
    bool _isGoal;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(BreadthSearch.Instance.IsGoal)
        {
            route =  BreadthSearch.Instance.SetRoute();
            _nextPosition = route.Dequeue();
            while(!_isGoal)
            {
                var distance = Vector3.Distance(transform.position, new Vector3(_nextPosition.x, _nextPosition.y, 0));
                if(distance > _stopDistance)
                {
                    var dir = (new Vector3(_nextPosition.x, _nextPosition.y, 0) - transform.position).normalized;
                    _rb.velocity = dir * _moveSpeed;
                }
                else
                {
                    _nextPosition = route.Dequeue();
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isGoal = true;
        collision.GetComponent<IAction>().Action();
    }
}
