using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass
{
    public int i { get; set; }
    public float f { get; set; }
    public bool b { get; set; }
    public Vector2 v2 { get; set; }
    public Vector3 v3 { get; set; }

    public TestClass(int i, float f, bool b, Vector2 v2, Vector3 v3)
    {
        this.i = i;
        this.f = f;
        this.b = b;
        this.v2 = v2;
        this.v3 = v3;
    }
    public TestClass() : this(1, 0.5f, true, new Vector2(0, 0), new Vector3(0, 0, 0)) { }
}
