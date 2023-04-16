using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    [Header("マップの全体サイズ")]
    [SerializeField, Range(5, 50)] int _mapSize;
    List<Position> _pathPosition = new List<Position>();
    Position _goalPosition;
    Position _startPosition;
    int[,] _map;
    int _width;
    int _height;

    public int Width { get => _width; }
    public int Height { get => _height; }

    private void Awake()
    {
        _width = _mapSize;
        _height = _mapSize;
    }

    private void Start()
    {
        _map = MapMethod.Generator(_width, _height);
        MapCreate(_map);
    }

    public void Create()
    {
        _map = MapMethod.Generator(_width, _height);
        MapCreate(_map);
    }

    void MapCreate(int[,] map)
    {
        int y = 0;
        GameObject path = (GameObject)Resources.Load("Path");
        GameObject wall = (GameObject)Resources.Load("Wall");
        for(int i = 0; i < map.GetLength(1); i++)
        {
            int x = 0;
            for (int j = 0; j < map.GetLength(0); j++)
            {
                if(map[i, j] == MapMethod.WALL)
                {
                    Instantiate(wall, new Vector2(x, y), transform.rotation);
                }
                else
                {
                    _pathPosition.Add(new Position(x, y));
                    Instantiate(path, new Vector2(x, y), transform.rotation);
                }
                x += 1;
            }
            y += 1;
        }
        PlayerSpawn();
    }

    void PlayerSpawn()
    {
        GameObject player = (GameObject)Resources.Load("Player");
        int random = Random.Range(0, _pathPosition.Count);
        _startPosition = _pathPosition[random];
        if (FindObjectsOfType<Player>().Length > 0)
        {
            FindObjectOfType<Player>().transform.position = new Vector3(_startPosition.x, _startPosition.y, 0);
        }
        else
        {
            Instantiate(player, new Vector3(_startPosition.x, _startPosition.y, 0), transform.rotation);
        }
        GoalSpawn();
    }

    void GoalSpawn()
    {
        GameObject goal = (GameObject)Resources.Load("Goal");
        _goalPosition = new Position(_width - 2, _height - 2);
        while (_goalPosition == _startPosition)
        {
            int random = Random.Range(0, _pathPosition.Count);
            _goalPosition = _pathPosition[random];
        }
        Instantiate(goal, new Vector3(_goalPosition.x, _goalPosition.y, 0), transform.rotation);
        //BreadthSearch.Instance.Search(_map, _startPosition, _goalPosition);
    }

    public void AutoMove()
    {
        BreadthSearch.Instance.Search(_map, _startPosition, _goalPosition);
    }
}
