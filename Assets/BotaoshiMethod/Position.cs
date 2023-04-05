using System;

public class Position
{
    public float x { get; set; }
    public float y { get; set; }

    public Position(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public Position() : this(0, 0) { }
}
