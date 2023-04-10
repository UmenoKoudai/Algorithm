using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAreaChnage : MonoBehaviour
{
    BoxCollider2D _gameAreaCollider;
    MapGenerator _mapGererator;


    private void Start()
    {
        _mapGererator = FindObjectOfType<MapGenerator>();
        _gameAreaCollider = GetComponent<BoxCollider2D>();
        int width = _mapGererator.Width;
        int height = _mapGererator.Height;
        if (width % 2 == 0 || height % 2 == 0)
        {
            width++;
            height++;
            _gameAreaCollider.size = new Vector2(width, height);
            transform.position = new Vector3(width / 2, height / 2, 0);
        }
        else
        {
            _gameAreaCollider.size = new Vector2(width, height);
            transform.position = new Vector3(width / 2, height / 2, 0);
        }
    }
}
