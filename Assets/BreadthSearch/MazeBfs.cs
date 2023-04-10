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

    // 15*15 �̓���m�F�p���H�̔z����쐬���܂��B
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

// ���H���D��T��
public class MazeBfs
{
    private int[,] Maze;            // ���H
    private int[] VisitedArray;     // �K��ϔz��
    private Cell Start;             // ���H�X�^�[�g
    private Cell Goal;              // ���H�S�[��

    // ���H�̉���
    private int MazeWidth
    {
        get { return this.Maze.GetLength(0); }
    }

    // ���H�̍���
    private int MazeHeight
    {
        get { return this.Maze.GetLength(1); }
    }

    // �R���X�g���N�^
    public MazeBfs(int[,] maze)
    {
        this.Maze = maze;
        this.VisitedArray = new int[MazeWidth * MazeHeight];
        this.Start = new Cell(1, 1);
        this.Goal = new Cell(MazeWidth - 2, MazeHeight - 2);
    }

    // �T������
    public void Search()
    {
        var isGoaled = false;
        var queue = new Queue<Cell>();
        queue.Enqueue(Start);

        // �K��ϔz��� -1 �ŏ�����
        VisitedArray = Enumerable.Repeat(-1, VisitedArray.Length).ToArray();
        VisitedArray[ToIndex(Start)] = ToIndex(Start);

        // �T���҂��̃Z�����Ȃ��Ȃ�܂ő�����
        while (queue.Count > 0 && !isGoaled)
        {
            // �T���Ώۂ̃Z�������o��
            var target = queue.Dequeue();

            // �Ώۂ̃Z������㉺���E�̃Z����T������
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                // ���̒T���Z�����쐬����
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
                // �T�����Z�����͈͓�
                if (nextCell.X >= 0 && nextCell.Y >= 0 && nextCell.X < MazeWidth && nextCell.Y < MazeHeight)
                {
                    // ���T���̏ꍇ���ʘH�̏ꍇ�̂݃L���[�ɋl�߂�Ɠ����ɒT���Ϗ��ݒ�
                    if (VisitedArray[ToIndex(nextCell)] < 0
                        && Maze[nextCell.X, nextCell.Y] == Path)
                    {
                        // �T���Ϗ��
                        SetVisited(target, nextCell);
                        if (nextCell.X == Goal.X && nextCell.Y == Goal.Y)
                        {
                            // �T����₪�S�[���̏ꍇ�����ɔ����邽�߂ɒT�������폜���Ĕ�����
                            // console.log('�S�[����������܂����B���߂łƂ� ...');
                            queue.Clear();
                            queue.Enqueue(nextCell);
                            isGoaled = true;
                            break;
                        }
                        else
                        {
                            // �L���[�ɋl�߂�
                            queue.Enqueue(nextCell);
                        }
                    }
                }
            }
        }
        // �T�����ʂ�z��ɐݒ�
        if (isGoaled)
        {
            SetRoute();
        }
    }

    // �S�[���ւ̃��[�g��2�����z��ɐݒ�
    public void SetRoute()
    {
        // �K��ς̔z�񂩂�S�[���܂ł̃��[�g��ݒ肷��
        var startIndex = ToIndex(Start);
        var goalIndex = ToIndex(Goal);
        var beforeIndex = VisitedArray[goalIndex];
        var route = new List<int>();

        while (beforeIndex >= 0 && beforeIndex != startIndex)
        {
            // �S�[������X�^�[�g�ւ̃��[�g�����ǂ�
            route.Add(beforeIndex);
            beforeIndex = VisitedArray[beforeIndex];
        }

        // �S�[���ւ̃��[�g��ݒ�
        foreach (var index in route)
        {
            var cell = ToCell(index);
            Maze[cell.X, cell.Y] = Route;
        }
    }

    // �K��σf�[�^�̐ݒ���s��
    private void SetVisited(Cell fromCell, Cell toCell)
    {
        var fromIndex = ToIndex(fromCell);
        var toIndex = ToIndex(toCell);
        VisitedArray[toIndex] = fromIndex;
    }

    // Cell��1�����z��̃C���f�b�N�X�ɕϊ�
    private int ToIndex(Cell cell)
    {
        return cell.X + MazeWidth * cell.Y;
    }

    // 1�����z��̃C���f�b�N�X���Z���ɕϊ�
    private Cell ToCell(int index)
    {
        return new Cell(index % MazeWidth, index / MazeWidth);
    }

    // �ʘH�E�Ǐ��
    const int Path = 0;
    const int Wall = 1;
    const int Route = 99;

    // �Z�����
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

    // ����
    private enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }
}