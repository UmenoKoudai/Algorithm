using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : InstanceSystem<Player>
{
    [SerializeField] int _moveSpeed;
    [SerializeField] float _stopDistance;
    Rigidbody2D _rb;
    List<Position> _route = new List<Position>();
    int _moveCount;
    bool _isGoal;

    public List<Position> Route { get => _route; set => _route = value; }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //if (BreadthSearch.Instance.IsGoal)
        //{
        //    int nowCount = _moveCount % _route.Count;
        //    var distance = Vector3.Distance(transform.position, new Vector3(_route[_route.Count - 1 - nowCount].x, _route[_route.Count - 1 - nowCount].y, 0));
        //    if (distance > _stopDistance)
        //    {
        //        var dir = (new Vector3(_route[_route.Count - 1 - nowCount].x, _route[_route.Count - 1 - nowCount].y, 0) - transform.position).normalized;
        //        _rb.velocity = dir * _moveSpeed;
        //    }
        //    else
        //    {
        //        _moveCount++;
        //    }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isGoal = true;
        collision.GetComponent<IAction>().Action();
    }
}
