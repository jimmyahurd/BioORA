using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Used to parse through a PDB file and pull out amino acids present in the protein as well as coordinates and elements of atoms
 */
public class PDBReader
{
    List<AtomData> proteinAtoms, extraAtoms;
    List<string> aminoAcidCodes;
    List<BondData> extraBonds;

    Dictionary<int, int> numBondsDictionary;
    int atomStartingLine, numExtraStart;
    string[] lines;

    public PDBReader()
    {}

    //Used to read a pdb file
    //Creates lists of data for atom creation as well as amino acid codes
    //Lists will be converted to arrays and used by a protein to make atoms and bonds
    public void readFile(string file) {
        //initalizes lists
        proteinAtoms = new List<AtomData>();
        extraAtoms = new List<AtomData>();
        aminoAcidCodes = new List<string>();
        extraBonds = new List<BondData>();

        numBondsDictionary = new Dictionary<int, int>();
        atomStartingLine = 0;

        int atomIndex1, atomIndex2;

        lines = System.IO.File.ReadAllLines(file);
        findAtomStartingLine();
        foreach (string line in lines){
            string[] entries = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (entries[0].Equals("SEQRES"))
            {
                //Adds all amino acid codes on the line
                for (int i = 4; i < entries.Length; i++)
                    aminoAcidCodes.Add(entries[i]);
            }
            else if (entries[0].Equals("ATOM"))
            {
                //Grabs element and coordinates of atom on the line
                proteinAtoms.Add(new AtomData(entries[11], float.Parse(entries[6]), float.Parse(entries[7]), float.Parse(entries[8])));
            }
            else if (entries[0].Equals("HETATM"))
            {
                //Grabs element and coordinates of atom on the line
                extraAtoms.Add(new AtomData(entries[11], float.Parse(entries[6]), float.Parse(entries[7]), float.Parse(entries[8])));
            }
            else if (entries[0].Equals("CONECT"))
            {
                atomIndex1 = getAtomIndex(entries[1]);
                for(int i = getNumBonds(atomIndex1) + 2; i < entries.Length; i++)
                {
                    atomIndex2 = getAtomIndex(entries[i]);
                    extraBonds.Add(new BondData(atomIndex1, atomIndex2));
                    addToNumBondsDictionary(atomIndex2);
                }
            }
        }
    }

    private void findAtomStartingLine()
    {
        int i;
        for(i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith("ATOM") || lines[i].StartsWith("HETATM"))
            {
                atomStartingLine = i;
                break;
            }
        }
        while (!lines[i++].StartsWith("ATOM")) ;
        numExtraStart = i - atomStartingLine - 1;
    }

    private int getAtomIndex(String index)
    {
        int value = int.Parse(index) - 1;
        if(value < proteinAtoms.Count)
        {
            if (lines[atomStartingLine + value].StartsWith("HETATM"))
                return value + proteinAtoms.Count;
            else
                return value - numExtraStart;
        }
        return value - 1;
    }

    private int getNumBonds(int index)
    {
        int value;
        if (numBondsDictionary.TryGetValue(index, out value)) return value;
        return 0;
    }

    private void addToNumBondsDictionary(int index)
    {
        if (numBondsDictionary.ContainsKey(index)) numBondsDictionary[index] += 1;
        else numBondsDictionary.Add(index, 1);
    }

    public AtomData[] getProteinAtoms()
    {
        return proteinAtoms.ToArray();
    }

    public AtomData[] getExtraAtoms()
    {
        return extraAtoms.ToArray();
    }

    public string[] getAminoAcidCodes()
    {
        return aminoAcidCodes.ToArray();
    }

    public BondData[] getExtraBonds()
    {
        return extraBonds.ToArray();
    }
}
