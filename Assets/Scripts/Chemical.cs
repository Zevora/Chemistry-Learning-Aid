
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Properties of a chemical: 
//  CID (Identifier for pubChem)
//  molecular formula 
//  molecule weight
//  acidic pKa
//  melting point
//  boiling point

public class Chemical 
{
    // All properties of a chemical that PubChem is able to find
    // Stored as a string due to API calls not always returning float / int values, 
    // setting as string allows it to continue to be stored without forced uniformity (parsing the float values) that may leave off crucial info
    // Some values may not be able to retrieved at all
    private string name;
    private string CID;
    private string molecularFormula;
    private string molecularWeight;
    private string pka;
    private string meltingPoint;
    private string boilingPoint;

    public Chemical() 
    {
        this.name = "No Name Set";
        this.CID = "No cid Set";
        this.molecularFormula = "No formula Set";
        this.molecularWeight = "No weight Set";
        this.pka = "No acidic pka Set";
        this.meltingPoint = "No Meltingpoint Set";
        this.boilingPoint = "No Boilingpoint Set";
    }

    public Chemical(ref string name, ref string CID, ref string molecularFormula, ref string molecularWeight, ref string pka, ref string meltingPoint, ref string boilingPoint)
    {
        this.name = name;
        this.CID = CID;
        this.molecularFormula = molecularFormula;
        this.molecularWeight = molecularWeight;
        this.pka = pka;
        this.meltingPoint = meltingPoint;
        this.boilingPoint = boilingPoint;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetCid(string CID)
    {
        this.CID = CID;
    }

    public void SetMolecularFormula(string molecularFormula)
    {
        this.molecularFormula = molecularFormula;
    }

    public void SetMolecularWeight(string molecularWeight)
    {
        this.molecularWeight = molecularWeight;
    }

    public void SetPka(string pka)
    {
        this.pka = pka;
    }

    public void SetMeltingPoint(string meltingPoint)
    {
        this.meltingPoint = meltingPoint;
    }

    public void SetBoilingPoint(string boilingPoint)
    {
        this.boilingPoint = boilingPoint;
    }

    // All Get Functions
    public string GetName()
    {
        return name;
    }

    public string GetCid()
    {
        return CID;
    }

    public string GetMolecularFormula()
    {
        return molecularFormula;
    }

    public string GetMolecularWeight()
    {
        return molecularWeight;
    }

    public string GetPka()
    {
        return pka;
    }

    public string GetMeltingPoint()
    {
        return meltingPoint;
    }

    public string GetBoilingPoint()
    {
        return boilingPoint;
    }

    public string GetAllProperties()
    {
        string allProperties = "Name: " + name + "\t";
        allProperties += "CID: " + CID + "\t";
        allProperties += "Molecular Formula: " + molecularFormula + "\t";
        allProperties += "Molecular Weight: " + molecularWeight + "\t";
        allProperties += "Acidic pKa: " + pka + "\t";
        allProperties += "Melting Point: " + meltingPoint + "\t";
        allProperties += "Boiling Point: " + boilingPoint;
        return allProperties;
    }
}
