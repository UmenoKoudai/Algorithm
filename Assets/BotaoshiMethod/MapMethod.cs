using System;

public static class MapMethod
{
    const int PATH = 0;　//通路
    const int WALL = 1;　//壁

    public static int[,] Generator(int width, int height)
    {
        //Mapサイズが5X5以下だったら作成できない
        if(height < 5 || width < 5)
        {
            throw new ArgumentOutOfRangeException();
        }
        //Mapサイズは奇数でなければできない
        if(height % 2 == 0)
        {
            height++;
        }
        if(width % 2 == 0)
        {
            width++;
        }

        int[,] map = new int[width, height];
        //Mapの周囲を壁で囲む
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

        var random =  new Random();
        //偶数の場所に棒を立てる
        //1111111        1111111
        //1000001        1000001
        //1000001        1010101
        //1000001    >   1000001
        //1000001        1010101
        //1000001        1000001
        //1111111        1111111
        for (int x = 2; x < width - 1; x += 2)
        {
            for(int y = 2; y < height - 1; y += 2)
            {
                map[x, y] = WALL;
                while(true)
                {
                    int direction;
                    //一列目だけ上に倒れるようにする
                    if(y == 2)
                    {
                        direction = random.Next(4);
                    }
                    else
                    {
                        direction = random.Next(3);
                    }
                    //左右上下のどちらかに棒を倒す
                    int wallX = x;
                    int wallY = y;
                    switch(direction)
                    {
                        case 0:
                            wallX++;　//右
                            break;
                        case 1:
                            wallY++;　//下
                            break;
                        case 2:
                            wallX--;　//左
                            break;
                        case 3:
                            wallY--;　//上
                            break;
                    }
                    //倒した先にすでに壁がなければ壁を作る
                    if(map[wallX, wallY] != WALL)
                    {
                        map[wallX, wallY] = WALL;
                        break;
                    }
                }
            }
        }
        return map;
    }
}
