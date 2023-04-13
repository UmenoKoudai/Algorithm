using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] int _moveSpeed;
    Rigidbody2D _rb;
    List<Position> routeList = new List<Position>();
    int _moveIndex = 
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(BreadthSearch.Instance.IsGoal)
        {
            routeList =  BreadthSearch.Instance.SetRoute();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<IAction>().Action();
    }
}
