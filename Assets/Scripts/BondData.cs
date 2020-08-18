using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stores indices of connected atoms
//Also stores whether or not bond is a double bond
public class BondData
{
    public int atom1Index;
    public int atom2Index;
    public bool doubleBond;

    public BondData(int index1, int index2)
    {
        atom1Index = index1;
        atom2Index = index2;
        doubleBond = false;
    }

    public void makeDoubleBond()
    {
        doubleBond = true;
    }
}
