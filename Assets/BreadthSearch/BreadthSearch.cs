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
    Position _start;　//スタートのポジション
    Position _goal;　//ゴールのポジション
    bool _isGoal = false; //ゴールしているかの判定

    public bool IsGoal { get => _isGoal; }

    //マップの横の広さを取得
    int MapWidth
    {
        get { return _map.GetLength(0); }
    }

    //マップの縦の広さを取得
    int MapHeight
    {
        get { return _map.GetLength(1); }
    }

    //探索メソッド
    public void Search(int[,] map, Position start, Position goal)
    {
        _map = map; //マップデータ
        _visitedArray = new int[MapWidth * MapHeight];　//探索済みの場所を格納する配列
        _start = start; //スタートのポジション
        _goal = goal; //ゴールのポジション
        GameObject route = (GameObject)Resources.Load("Route");
        Queue<Position> queue = new Queue<Position>();
        queue.Enqueue(_start);

        //探索済みの配列に初期値(-1)を代入
        _visitedArray = Enumerable.Repeat(-1, _visitedArray.Length).ToArray();
        //スタート地点にスタートの数字を代入
        _visitedArray[ToIndex(_start)] = ToIndex(_start);

        //queueに値があったら(基本スタートが入ってる)&&今ゴールまで探索していなければ
        while(queue.Count > 0 && !_isGoal)
        {
            //今queueに入っている最下層(要素0)を出して変数に代入する
            Position tartget = queue.Dequeue();
            //上下左右に移動できるか確認
            foreach(Direction dir in Enum.GetValues(typeof(Direction)))
            {
                //移動先の場所を保管する為の変数、その為targetに格納した前回の場所を最初に格納する
                Position nextCell = new Position(tartget.x, tartget.y);
                switch(dir)
                {
                    case Direction.Up:　//上
                        nextCell.y--;
                        break;
                    case Direction.Right:　//右
                        nextCell.x++;
                        break;
                    case Direction.Down:　//下
                        nextCell.y++;
                        break;
                    case Direction.Left:　//左
                        nextCell.x--;
                        break;
                }
                //マップサイズ内の数値だったら
                if(nextCell.x >= 0 && nextCell.y >= 0 && nextCell.x <= MapWidth && nextCell.y <= MapHeight)
                {
                    //探索先の配列にある数値が0未満(-1だったら)　&&　マップデータで見て通路だったら
                    if(_visitedArray[ToIndex(nextCell)] < 0 && _map[nextCell.x, nextCell.y] == MapMethod.PATH)
                    {
                        //探索した場所を配列に格納
                        SetVisited(tartget, nextCell);
                        //探索した場所がゴールと同じ場所だったら終了
                        if(nextCell.x == _goal.x && nextCell.y == _goal.y)
                        {
                            Debug.Log("ゴールに着きました");
                            queue.Clear();
                            queue.Enqueue(nextCell);
                            _isGoal = true;
                            break;
                        }
                        else
                        {
                            Instantiate(route, new Vector3(nextCell.x, nextCell.y, 0), transform.rotation);
                            //次のtargetになる場所を格納
                            queue.Enqueue(nextCell);
                        }
                    }
                }
            }
        }
        //ゴール地点まで行ったらスタートからゴールまでのルートを表示する
        //if(_isGoal)
        //{
        //    SetRoute();
        //}
    }

    //ゴールから逆算してスタートまでのルートを求める
    public List<Position> SetRoute()
    {
        //スタート地点をインデックスに変換
        int startIndex = ToIndex(_start);　//11
        //ゴール地点をインデックスに変換
        int goalIndex = ToIndex(_goal);　//88
        //ゴールのインデックスに格納されている数値を代入
        int beforeIndex = _visitedArray[goalIndex];　//87
        List<int> routeIndex = new List<int>();
        List<Position> routePosition = new List<Position>();
        //配列の要素数が0以上　&&　今参照している配列の数値がスタートと同じじゃなければ
        while(beforeIndex >= 0 && beforeIndex != startIndex)
        {
            //ルートの情報を格納
            routeIndex.Add(beforeIndex);
            //次のルート情報をセット
            beforeIndex = _visitedArray[beforeIndex];
        }

        //計算したルートをPosition型に変換してマップデータにルート情報を反映
        foreach(var index in routeIndex)
        {
            Position cell = ToCell(index);
            routePosition.Add(cell);
        }
        return routePosition;
    }

    //訪問済みのデータを設定
    void SetVisited(Position fromCell, Position toCell)
    {
        //今いる場所
        int fromIndex = ToIndex(fromCell);
        //探索した場所
        int toIndex = ToIndex(toCell);
        //探索した場所に今の場所の情報を格納
        _visitedArray[toIndex] = fromIndex;
    }

    //Position型(X,Yのクラス)を配列のインデックスに変換
    int ToIndex(Position cell)
    {
        return cell.x + MapWidth * cell.y;
    }

    //配列のインデックスをPosition型(X,Yのクラス)に変換
    Position ToCell(int index)
    {
        return new Position(index % MapWidth, index / MapWidth);
    }

    //方向の情報
    enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
    }
}
