using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stores coordinates of an atom as well as its element
public class AtomData
{
    public string element;
    public float x, y, z;

    public AtomData(string element, float x, float y, float z)
    {
        this.element = element;
        this.x = x;
        this.y = y;
        this.z = z;
    }
}
