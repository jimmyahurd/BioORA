using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains methods that return CPK color given an element,
//R group bonds given an amino acid, and number of atoms
//in a passed amino acid
public class ProteinHelper
{
    private Dictionary<string, Color> colorDictionary;
    private Dictionary<string, BondData[]> RGroupBonds;

    private static ProteinHelper instance;

    private ProteinHelper()
    {
        initalize();
    }

    public static ProteinHelper getHelperInstance()
    {
        if (instance == null)
            instance = new ProteinHelper();
        return instance;
    }

    private void initalize()
    {
        //Creates R group bond dictionary
        BondData[] gly, ala, ile, leu, met, val, phe, trp, tyr, asn, cys,
            gln, ser, thr, asp, glu, arg, his, lys, pro;
        RGroupBonds = new Dictionary<string, BondData[]>();

        gly = new BondData[0];
        RGroupBonds.Add("GLY", gly);

        ala = new BondData[1];
        ala[0] = new BondData(1, 4);
        RGroupBonds.Add("ALA", ala);

        ser = new BondData[2];
        ser[0] = new BondData(1, 4);
        ser[1] = new BondData(4, 5);
        RGroupBonds.Add("SER", ser);

        leu = new BondData[4];
        leu[0] = new BondData(1, 4);
        leu[1] = new BondData(4, 5);
        leu[2] = new BondData(5, 6);
        leu[3] = new BondData(5, 7);
        RGroupBonds.Add("LEU", leu);

        lys = new BondData[5];
        lys[0] = new BondData(1, 4);
        lys[1] = new BondData(4, 5);
        lys[2] = new BondData(5, 6);
        lys[3] = new BondData(6, 7);
        lys[4] = new BondData(7, 8);
        RGroupBonds.Add("LYS", lys);

        val = new BondData[3];
        val[0] = new BondData(1, 4);
        val[1] = new BondData(4, 5);
        val[2] = new BondData(4, 6);
        RGroupBonds.Add("VAL", val);

        gln = new BondData[5];
        gln[0] = new BondData(1, 4);
        gln[1] = new BondData(4, 5);
        gln[2] = new BondData(5, 6);
        gln[3] = new BondData(6, 7);
        gln[3].makeDoubleBond();
        gln[4] = new BondData(6, 8);
        RGroupBonds.Add("GLN", gln);

        asp = new BondData[4];
        asp[0] = new BondData(1, 4);
        asp[1] = new BondData(4, 5);
        asp[2] = new BondData(5, 6);
        asp[2].makeDoubleBond();
        asp[3] = new BondData(5, 7);
        RGroupBonds.Add("ASP", asp);

        arg = new BondData[7];
        arg[0] = new BondData(1, 4);
        arg[1] = new BondData(4, 5);
        arg[2] = new BondData(5, 6);
        arg[3] = new BondData(6, 7);
        arg[4] = new BondData(7, 8);
        arg[5] = new BondData(8, 9);
        arg[6] = new BondData(8, 10);
        arg[6].makeDoubleBond();
        RGroupBonds.Add("ARG", arg);

        phe = new BondData[8];
        phe[0] = new BondData(1, 4);
        phe[1] = new BondData(4, 5);
        phe[2] = new BondData(5, 6);
        phe[3] = new BondData(5, 7);
        phe[3].makeDoubleBond();
        phe[4] = new BondData(7, 9);
        phe[5] = new BondData(6, 8);
        phe[5].makeDoubleBond();
        phe[6] = new BondData(8, 10);
        phe[7] = new BondData(9, 10);
        phe[7].makeDoubleBond();
        RGroupBonds.Add("PHE", phe);

        ile = new BondData[4];
        ile[0] = new BondData(1, 4);
        ile[1] = new BondData(4, 5);
        ile[2] = new BondData(4, 6);
        ile[3] = new BondData(6, 7);
        RGroupBonds.Add("ILE", ile);

        pro = new BondData[4];
        pro[0] = new BondData(1, 4);
        pro[1] = new BondData(4, 5);
        pro[2] = new BondData(5, 6);
        pro[3] = new BondData(6, 0);
        RGroupBonds.Add("PRO", pro);

        thr = new BondData[3];
        thr[0] = new BondData(1, 4);
        thr[1] = new BondData(4, 5);
        thr[2] = new BondData(4, 6);
        RGroupBonds.Add("THR", thr);

        tyr = new BondData[9];
        tyr[0] = new BondData(1, 4);
        tyr[1] = new BondData(4, 5);
        tyr[2] = new BondData(5, 6);
        tyr[3] = new BondData(5, 7);
        tyr[3].makeDoubleBond();
        tyr[4] = new BondData(7, 9);
        tyr[5] = new BondData(6, 8);
        tyr[5].makeDoubleBond();
        tyr[6] = new BondData(8, 10);
        tyr[7] = new BondData(9, 10);
        tyr[7].makeDoubleBond();
        tyr[8] = new BondData(10, 11);
        RGroupBonds.Add("TYR", tyr);

        glu = new BondData[5];
        glu[0] = new BondData(1, 4);
        glu[1] = new BondData(4, 5);
        glu[2] = new BondData(5, 6);
        glu[3] = new BondData(6, 7);
        glu[3].makeDoubleBond();
        glu[4] = new BondData(6, 8);
        RGroupBonds.Add("GLU", glu);

        his = new BondData[7];
        his[0] = new BondData(1, 4);
        his[1] = new BondData(4, 5);
        his[2] = new BondData(5, 6);
        his[3] = new BondData(5, 7);
        his[3].makeDoubleBond();
        his[4] = new BondData(6, 8);
        his[4].makeDoubleBond();
        his[5] = new BondData(7, 9);
        his[6] = new BondData(8, 9);
        RGroupBonds.Add("HIS", his);

        cys = new BondData[2];
        cys[0] = new BondData(1, 4);
        cys[1] = new BondData(4, 5);
        RGroupBonds.Add("CYS", cys);

        trp = new BondData[12];
        trp[0] = new BondData(1, 4);
        trp[1] = new BondData(4, 5);
        trp[2] = new BondData(5, 6);
        trp[2].makeDoubleBond();
        trp[3] = new BondData(5, 7);
        trp[4] = new BondData(6, 8);
        trp[5] = new BondData(7, 9);
        trp[5].makeDoubleBond();
        trp[6] = new BondData(8, 9);
        trp[7] = new BondData(7, 10);
        trp[8] = new BondData(9, 11);
        trp[9] = new BondData(10, 12);
        trp[9].makeDoubleBond();
        trp[10] = new BondData(11, 13);
        trp[10].makeDoubleBond();
        trp[11] = new BondData(12, 13);
        RGroupBonds.Add("TRP", trp);

        met = new BondData[4];
        met[0] = new BondData(1, 4);
        met[1] = new BondData(4, 5);
        met[2] = new BondData(5, 6);
        met[3] = new BondData(6, 7);
        RGroupBonds.Add("MET", met);

        asn = new BondData[4];
        asn[0] = new BondData(1, 4);
        asn[1] = new BondData(4, 5);
        asn[2] = new BondData(5, 6);
        asn[2].makeDoubleBond();
        asn[3] = new BondData(5, 7);
        RGroupBonds.Add("ASN", asn);

        //Creates CPK color dictionary
        colorDictionary = new Dictionary<string, Color>();
        colorDictionary.Add("H", Color.white);
        colorDictionary.Add("O", Color.red);
        colorDictionary.Add("C", Color.black);
        colorDictionary.Add("N", Color.blue);
        colorDictionary.Add("CL", new Color(0, 255, 0));
        colorDictionary.Add("S", Color.yellow);
        colorDictionary.Add("P", new Color(255, 140, 0));
        colorDictionary.Add("MG", new Color(0, 102, 0));
    }

    public BondData[] getRGroupBonds(string aminoAcidCode)
    {
        BondData[] bonds;
        if (RGroupBonds.TryGetValue(aminoAcidCode, out bonds)) return bonds;

        bonds = new BondData[0];
        return bonds;
    }

    public Color getColor(string key)
    {
        Color value;
        if (colorDictionary.TryGetValue(key, out value)) return value;

        Debug.Log("Missing color for " + key);
        return Color.magenta;
    }

    public int getNumAtoms(string aminoAcidCode)
    {
        switch (aminoAcidCode)
        {
            case "GLY": return 4;
            case "ALA": return 5;
            case "ILE": return 8;
            case "LEU": return 8;
            case "MET": return 8;
            case "VAL": return 7;
            case "PHE": return 11;
            case "TRP": return 14;
            case "TYR": return 12;
            case "ASN": return 8;
            case "CYS": return 6;
            case "GLN": return 9;
            case "SER": return 6;
            case "THR": return 7;
            case "ASP": return 8;
            case "GLU": return 9;
            case "ARG": return 11;
            case "HIS": return 10;
            case "LYS": return 9;
            case "PRO": return 7;
            default: return 0;
        }
    }
}
