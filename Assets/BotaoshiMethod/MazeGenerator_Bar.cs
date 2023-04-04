using System;

namespace ConsoleApplication1
{
    public class MazeGenerator_Bar
    {
        // �ʘH�E�Ǐ��
        const int Path = 0;
        const int Wall = 1;

        // �_�|���@�ɂ����H����
        public static int[,] GenerateMaze(int width, int height)
        {
            // 5�����̃T�C�Y�ł͐����ł��Ȃ�
            if (height < 5 || width < 5) throw new ArgumentOutOfRangeException();
            if (width % 2 == 0) width++;
            if (height % 2 == 0) height++;

            // �w��T�C�Y�Ő������O����ǂɂ���
            var maze = new int[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                        maze[x, y] = Wall; // �O���͂��ׂĕ�
                    else
                        maze[x, y] = Path;  // �O���ȊO�͒ʘH

            // �_�𗧂āA�|��
            var rnd = new Random();
            for (int x = 2; x < width - 1; x += 2)
            {
                for (int y = 2; y < height - 1; y += 2)
                {
                    maze[x, y] = Wall; // �_�𗧂Ă�

                    // �|����܂ŌJ��Ԃ�
                    while (true)
                    {
                        // 1�s�ڂ̂ݏ�ɓ|����
                        int direction;
                        if (y == 2)
                            direction = rnd.Next(4);
                        else
                            direction = rnd.Next(3);

                        // �_��|�����������߂�
                        int wallX = x;
                        int wallY = y;
                        switch (direction)
                        {
                            case 0: // �E
                                wallX++;
                                break;
                            case 1: // ��
                                wallY++;
                                break;
                            case 2: // ��
                                wallX--;
                                break;
                            case 3: // ��
                                wallY--;
                                break;
                        }
                        // �ǂ���Ȃ��ꍇ�̂ݓ|���ďI��
                        if (maze[wallX, wallY] != Wall)
                        {
                            maze[wallX, wallY] = Wall;
                            break;
                        }
                    }
                }
            }
            return maze;
        }

        // �f�o�b�O�p���\�b�h
        public void DebugPrint(int[,] maze)
        {
            Console.WriteLine($"Width: {maze.GetLength(0)}");
            Console.WriteLine($"Height: {maze.GetLength(1)}");
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                for (int x = 0; x < maze.GetLength(0); x++)
                {
                    Console.Write(maze[x, y] == Wall ? "��" : "�@");
                }
                Console.WriteLine();
            }
        }
    }
}