using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    public static void Main()
    {
        var maze = CreateMaze();
        var bfs = new MazeBfs(maze);
        bfs.Search();
    }

    // 15*15 の動作確認用迷路の配列を作成します。
    public static int[,] CreateMaze()
    {
        return new int[15, 15] {
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,0,0,0,1,0,0,0,0,0,0,0,0,0,1},
                {1,1,1,0,1,1,1,0,1,1,1,0,1,0,1},
                {1,0,1,0,0,0,1,0,0,0,1,0,1,0,1},
                {1,0,1,1,1,0,1,1,1,0,1,0,1,1,1},
                {1,0,1,0,0,0,0,0,0,0,1,0,0,0,1},
                {1,0,1,0,1,1,1,1,1,1,1,1,1,0,1},
                {1,0,0,0,0,0,0,0,1,0,1,0,0,0,1},
                {1,1,1,1,1,0,1,0,1,0,1,0,1,1,1},
                {1,0,0,0,1,0,1,0,0,0,1,0,0,0,1},
                {1,0,1,0,1,0,1,1,1,0,1,1,1,1,1},
                {1,0,1,0,1,0,0,0,1,0,1,0,0,0,1},
                {1,0,1,0,1,1,1,0,1,0,1,0,1,0,1},
                {1,0,1,0,0,0,0,0,1,0,0,0,1,0,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
            };
    }
}

// 迷路幅優先探索
public class MazeBfs
{
    private int[,] Maze;            // 迷路
    private int[] VisitedArray;     // 訪問済配列
    private Cell Start;             // 迷路スタート
    private Cell Goal;              // 迷路ゴール

    // 迷路の横幅
    private int MazeWidth
    {
        get { return this.Maze.GetLength(0); }
    }

    // 迷路の高さ
    private int MazeHeight
    {
        get { return this.Maze.GetLength(1); }
    }

    // コンストラクタ
    public MazeBfs(int[,] maze)
    {
        this.Maze = maze;
        this.VisitedArray = new int[MazeWidth * MazeHeight];
        this.Start = new Cell(1, 1);
        this.Goal = new Cell(MazeWidth - 2, MazeHeight - 2);
    }

    // 探索処理
    public void Search()
    {
        var isGoaled = false;
        var queue = new Queue<Cell>();
        queue.Enqueue(Start);

        // 訪問済配列を -1 で初期化
        VisitedArray = Enumerable.Repeat(-1, VisitedArray.Length).ToArray();
        VisitedArray[ToIndex(Start)] = ToIndex(Start);

        // 探索待ちのセルがなくなるまで続ける
        while (queue.Count > 0 && !isGoaled)
        {
            // 探索対象のセルを取り出す
            var target = queue.Dequeue();

            // 対象のセルから上下左右のセルを探索する
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                // 次の探索セルを作成する
                var nextCell = new Cell(target.X, target.Y);
                switch (dir)
                {
                    case Direction.Up:
                        nextCell.Y -= 1;
                        break;
                    case Direction.Right:
                        nextCell.X += 1;
                        break;
                    case Direction.Down:
                        nextCell.Y += 1;
                        break;
                    case Direction.Left:
                        nextCell.X -= 1;
                        break;
                }
                // 探索候補セルが範囲内
                if (nextCell.X >= 0 && nextCell.Y >= 0 && nextCell.X < MazeWidth && nextCell.Y < MazeHeight)
                {
                    // 未探索の場合かつ通路の場合のみキューに詰めると同時に探索済情報設定
                    if (VisitedArray[ToIndex(nextCell)] < 0
                        && Maze[nextCell.X, nextCell.Y] == Path)
                    {
                        // 探索済情報
                        SetVisited(target, nextCell);
                        if (nextCell.X == Goal.X && nextCell.Y == Goal.Y)
                        {
                            // 探索候補がゴールの場合すぐに抜けるために探索候補を削除して抜ける
                            // console.log('ゴールが見つかりました。おめでとう ...');
                            queue.Clear();
                            queue.Enqueue(nextCell);
                            isGoaled = true;
                            break;
                        }
                        else
                        {
                            // キューに詰める
                            queue.Enqueue(nextCell);
                        }
                    }
                }
            }
        }
        // 探索結果を配列に設定
        if (isGoaled)
        {
            SetRoute();
        }
    }

    // ゴールへのルートを2次元配列に設定
    public void SetRoute()
    {
        // 訪問済の配列からゴールまでのルートを設定する
        var startIndex = ToIndex(Start);
        var goalIndex = ToIndex(Goal);
        var beforeIndex = VisitedArray[goalIndex];
        var route = new List<int>();

        while (beforeIndex >= 0 && beforeIndex != startIndex)
        {
            // ゴールからスタートへのルートをたどる
            route.Add(beforeIndex);
            beforeIndex = VisitedArray[beforeIndex];
        }

        // ゴールへのルートを設定
        foreach (var index in route)
        {
            var cell = ToCell(index);
            Maze[cell.X, cell.Y] = Route;
        }
    }

    // 訪問済データの設定を行う
    private void SetVisited(Cell fromCell, Cell toCell)
    {
        var fromIndex = ToIndex(fromCell);
        var toIndex = ToIndex(toCell);
        VisitedArray[toIndex] = fromIndex;
    }

    // Cellを1次元配列のインデックスに変換
    private int ToIndex(Cell cell)
    {
        return cell.X + MazeWidth * cell.Y;
    }

    // 1次元配列のインデックスをセルに変換
    private Cell ToCell(int index)
    {
        return new Cell(index % MazeWidth, index / MazeWidth);
    }

    // 通路・壁情報
    const int Path = 0;
    const int Wall = 1;
    const int Route = 99;

    // セル情報
    private struct Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    // 方向
    private enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }
}