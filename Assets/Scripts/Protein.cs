using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Used to store references for all atoms and bonds in a protein
 * Will also be used to manipulate the protein
 */
public class Protein : MonoBehaviour
{
    public GameObject atom;

    private GameObject[] atoms;
    private GameObject[] bonds;

    private ProteinHelper helper;

    void Start()
    {}

    // Update is called once per frame
    void Update()
    {

    }

    public void makeProtein(string filename)
    {
        //Uses PDBReader to read a file
        //Then extracts the information needed to instantiate the protein
        PDBReader reader = new PDBReader();
        reader.readFile(filename);
        AtomData[] proteinAtoms = reader.getProteinAtoms();
        AtomData[] extraAtoms = reader.getExtraAtoms();
        string[] aminoAcidCodes = reader.getAminoAcidCodes();
        BondData[] extraBonds = reader.getExtraBonds();

        //Gets instance of ProteinHelper
        helper = ProteinHelper.getHelperInstance();

        //Initalizes array of atoms
        atoms = new GameObject[proteinAtoms.Length + extraAtoms.Length];
        int i = 0;

        //Instantiates atoms present in amino acids and adds object to array of Atoms
        foreach (AtomData atom in proteinAtoms)
            atoms[i++] = createAtom(atom.x, atom.y, atom.z, helper.getColor(atom.element));

        //Instantiates atoms not present in amino acids and adds object to array of Atoms
        foreach (AtomData atom in extraAtoms)
            atoms[i++] = createAtom(atom.x, atom.y, atom.z, helper.getColor(atom.element));

        //Instantiates bonds present in amino acid sequence and adds them to array of bonds
        List<GameObject> bonds = new List<GameObject>();
        BondData[] bondData;
        i = 0;
        int previousEnd = 0;
        foreach(string aminoAcid in aminoAcidCodes)
        {
            //Instantiates bonds in the backbone of the amino acid
            bonds.Add(createBond(atoms[i], atoms[i + 1]));
            bonds.Add(createBond(atoms[i + 1], atoms[i + 2]));
            bonds.Add(createBond(atoms[i + 2], atoms[i + 3])); //should be a double bond in future
            
            //Instantiates bond connecting current amino acid to previous one
            if (previousEnd != 0)
            {
                bonds.Add(createBond(atoms[previousEnd], atoms[i]));
            }

            //Grabs bonds in R group of current amino acid
            bondData = helper.getRGroupBonds(aminoAcid);

            //Instantiates each of the bonds in the R group
            foreach(BondData bond in bondData)
            {
                bonds.Add(createBond(atoms[i + bond.atom1Index], atoms[i + bond.atom2Index]));
            }

            //Increases iterator and stores carboxyl end for next amino acid
            previousEnd = i + 2;
            i += helper.getNumAtoms(aminoAcid);
        }

        //Instantiates bonds not present in amino acid sequence and adds them to list of bonds
        foreach(BondData bond in extraBonds)
        {
            bonds.Add(createBond(atoms[bond.atom1Index], atoms[bond.atom2Index]));
        }
        this.bonds = bonds.ToArray();
    }

    //Uses coordinates and CPK color to create an atom object
    private GameObject createAtom(float x, float y, float z, Color color)
    {
        var newAtom = Instantiate(atom, new Vector3(x, y, z), Quaternion.identity);
        newAtom.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        return newAtom;
    }

    //Instantiates a Bond object between the passed Atom objects
    private GameObject createBond(GameObject atom1, GameObject atom2)
    {
        GameObject bond = new GameObject();
        bond.transform.position = atom1.transform.position;
        bond.AddComponent<LineRenderer>();
        LineRenderer lr = bond.GetComponent<LineRenderer>();
        lr.material.color = Color.gray;
        //lr.startColor = Color.gray;
        //lr.endColor = Color.gray;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, atom1.transform.position);
        lr.SetPosition(1, atom2.transform.position);
        return bond;
    }

    //Deletes protein and all atoms and bonds in protein
    public void delete()
    {
        foreach(GameObject atom in atoms)
        {
            Destroy(atom);
        }
        foreach(GameObject bond in bonds)
        {
            Destroy(bond);
        }
        Destroy(this);
    }
}
