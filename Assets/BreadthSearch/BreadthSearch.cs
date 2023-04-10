using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BreadthSearch : InstanceSystem<BreadthSearch>
{
    const int ROUTE = 99;
    int[,] _map;
    int[] _visitedArray;
    Cell _start;
    Cell _goal;

    int MapWidth
    {
        get { return _map.GetLength(0); }
    }

    int MapHeight
    {
        get { return _map.GetLength(1); }
    }

    public BreadthSearch(int[,] map)
    {
        this._map = map;
    }

    private class Cell
    {
        public int x;
        public int y;
        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public void Search()
    {
        _visitedArray = new int[MapWidth * MapHeight];
        _start = new Cell(1, 1);
        _goal = new Cell(MapWidth - 2, MapHeight - 2);

        bool isGoal = false;
        Queue<Cell> queue = new Queue<Cell>();
        queue.Enqueue(_start);

        _visitedArray = Enumerable.Repeat(-1, _visitedArray.Length).ToArray();
        _visitedArray[ToIndex(_start)] = ToIndex(_start);

        while(queue.Count > 0 && !isGoal)
        {
            Cell tartget = queue.Dequeue();
            foreach(Direction dir in Enum.GetValues(typeof(Direction)))
            {
                Cell nextCell = new Cell(tartget.x, tartget.y);
                switch(dir)
                {
                    case Direction.Up:
                        nextCell.y--;
                        break;
                    case Direction.Right:
                        nextCell.x++;
                        break;
                    case Direction.Down:
                        nextCell.y++;
                        break;
                    case Direction.Left:
                        nextCell.x--;
                        break;
                }
                if(nextCell.x >= 0 && nextCell.y >= 0 && nextCell.x <= MapWidth && nextCell.y <= MapHeight)
                {
                    if(_visitedArray[ToIndex(nextCell)] < 0 && _map[nextCell.x, nextCell.y] == MapMethod.PATH)
                    {
                        SetVisited(tartget, nextCell);
                        if(nextCell.x == _goal.x && nextCell.y == _goal.y)
                        {
                            Debug.Log("ƒS[ƒ‹‚É’…‚«‚Ü‚µ‚½");
                            queue.Clear();
                            queue.Enqueue(nextCell);
                            isGoal = true;
                            break;
                        }
                        else
                        {
                            queue.Enqueue(nextCell);
                        }
                    }
                }
            }
        }
        if(isGoal)
        {
            SetRoute();
        }
    }

    void SetRoute()
    {
        int startIndex = ToIndex(_start);
        int goalIndex = ToIndex(_goal);
        int beforeIndex = _visitedArray[goalIndex];
        List<int> route = new List<int>();
        while(beforeIndex >= 0 && beforeIndex != startIndex)
        {
            route.Add(beforeIndex);
            beforeIndex = _visitedArray[beforeIndex];
        }

        foreach(var index in route)
        {
            Cell cell = ToCell(index);
            _map[cell.x, cell.y] = ROUTE;
        }
    }

    void SetVisited(Cell fromCell, Cell toCell)
    {
        int fromIndex = ToIndex(fromCell);
        int toIndex = ToIndex(toCell);
        _visitedArray[toIndex] = fromIndex;
    }

    int ToIndex(Cell cell)
    {
        return cell.x + MapWidth * cell.y;
    }

    Cell ToCell(int index)
    {
        return new Cell(index % MapWidth, index % MapHeight);
    }
}
enum Direction
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3,
}
