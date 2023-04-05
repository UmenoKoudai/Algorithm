using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("マップのサイズ横")] int _width;
    [SerializeField, Tooltip("マップのサイズ縦")] int _height;
    int[,] _map;

    void Start()
    {
        _map = MapMethod.Generator(_width, _height);
        Print(_map);
    }

    void Print(int [,] map)
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                Console.Write(map[x, y] == 1 ? "■" : "　");
            }
            Console.WriteLine();
        }
    }
}
