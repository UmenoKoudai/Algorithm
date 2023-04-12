using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    [Header("マップの全体サイズ")]
    [SerializeField, Tooltip("マップサイズ"), Range(20, 50)] int _mapSize;
    [Header("インスタンスするオブジェクト")]
    [SerializeField, Tooltip("通路のオブジェクト")] GameObject _path;
    [SerializeField, Tooltip("壁のオブジェクト")] GameObject _wall;
    [SerializeField, Tooltip("プレイヤーのオブジェクト")] GameObject _player;
    [SerializeField, Tooltip("ゴールのオブジェクト")] GameObject _goal;
    List<Position> _pathPosition = new List<Position>();
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

    void MapCreate(int[,] map)
    {
        int y = 0;
        for(int i = 0; i < map.GetLength(1); i++)
        {
            int x = 0;
            for (int j = 0; j < map.GetLength(0); j++)
            {
                if(map[i, j] == MapMethod.WALL)
                {
                    Instantiate(_wall, new Vector2(x, y), transform.rotation);
                }
                else
                {
                    _pathPosition.Add(new Position(x, y));
                    Instantiate(_path, new Vector2(x, y), transform.rotation);
                }
                x += 1;
            }
            y += 1;
        }
        PlayerSpawn();
    }

    void PlayerSpawn()
    {
        int random = Random.Range(0, _pathPosition.Count);
        Vector3 playerPosition = new Vector3(_pathPosition[random].x, _pathPosition[random].y, 0);
        if (FindObjectsOfType<Player>().Length > 0)
        {
            FindObjectOfType<Player>().transform.position = playerPosition;
        }
        else
        {
            Instantiate(_player, playerPosition, transform.rotation);
        }
        GoalSpawn(playerPosition);
    }

    void GoalSpawn(Vector3 playerPosition)
    {
        int random = Random.Range(0, _pathPosition.Count);
        Vector3 goalPosition = new Vector3(_pathPosition[random].x, _pathPosition[random].y, 0);
        if (goalPosition != playerPosition)
        {
            Instantiate(_goal, goalPosition, transform.rotation);
        }
    }
}
