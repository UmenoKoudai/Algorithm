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
    Position _start;�@//�X�^�[�g�̃|�W�V����
    Position _goal;�@//�S�[���̃|�W�V����

    //�}�b�v�̉��̍L�����擾
    int MapWidth
    {
        get { return _map.GetLength(0); }
    }

    //�}�b�v�̏c�̍L�����擾
    int MapHeight
    {
        get { return _map.GetLength(1); }
    }

    //�T�����\�b�h
    public void Search(int[,] map, Position start, Position goal)
    {
        _map = map; //�}�b�v�f�[�^
        _visitedArray = new int[MapWidth * MapHeight];�@//�T���ς݂̏ꏊ���i�[����z��
        //_start = new Position(1, 1);�@//�X�^�[�g�̃|�W�V����
        //_goal = new Position(MapWidth - 2, MapHeight - 2);�@//�S�[���̃|�W�V����
        _start = start;
        _goal = goal;

        bool isGoal = false;
        Queue<Position> queue = new Queue<Position>();
        queue.Enqueue(_start);

        _visitedArray = Enumerable.Repeat(-1, _visitedArray.Length).ToArray();
        _visitedArray[ToIndex(_start)] = ToIndex(_start);

        while(queue.Count > 0 && !isGoal)
        {
            Position tartget = queue.Dequeue();
            foreach(Direction dir in Enum.GetValues(typeof(Direction)))
            {
                Position nextCell = new Position(tartget.x, tartget.y);
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
                            Debug.Log("�S�[���ɒ����܂���");
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

    //�S�[���܂ł̃��[�g��z��Ɋi�[
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
            Position cell = ToCell(index);
            _map[cell.x, cell.y] = ROUTE;
        }
    }

    //�K��ς݂̃f�[�^��ݒ�
    void SetVisited(Position fromCell, Position toCell)
    {
        int fromIndex = ToIndex(fromCell);
        int toIndex = ToIndex(toCell);
        _visitedArray[toIndex] = fromIndex;
    }

    //Position�^(X,Y�̃N���X)��z��̃C���f�b�N�X�ɕϊ�
    int ToIndex(Position cell)
    {
        return cell.x + MapWidth * cell.y;
    }

    //�z��̃C���f�b�N�X��Position�^(X,Y�̃N���X)�ɕϊ�
    Position ToCell(int index)
    {
        return new Position(index % MapWidth, index % MapHeight);
    }

    //�����̏��
    enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
    }
}
