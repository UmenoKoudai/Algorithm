using UnityEngine;
using System;

public class MapGenerater : MonoBehaviour
{
    const int PATH = 0;
    const int WALL = 1;
    void Start()
    {
        
    }

    public int[,] Generater(int width, int height)
    {
        //Map�T�C�Y��5X5�ȉ���������쐬�ł��Ȃ�
        if(height < 5 || width < 5)
        {
            throw new ArgumentOutOfRangeException();
        }
        //Map�T�C�Y�͊�łȂ���΂ł��Ȃ�
        if(height % 2 == 0)
        {
            height++;
        }
        if(width % 2 == 0)
        {
            width++;
        }

        int[,] map = new int[width, height];
        //Map�̎��͂�ǂň͂�
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    map[x, y] = WALL;
                }
                else
                {
                    map[x, y] = PATH;
                }
            }
        }
        return map;
    }
}
