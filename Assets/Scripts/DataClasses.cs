using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Medicine
{
    public string Name;
    public string Details;
    public int RemainingDoses;

    public int StartingDoses;
    public Texture2D Image;

    public void RestoreDoses()
    {
        
    }
}

[Serializable]
public class Patient
{
    public string Name;
    public string LastName;
    public Texture2D Photo;
    public List<Medicine> Medicines = new List<Medicine>();

    public void AddMedicine()
    {
        
    }

    public Patient(string Name){
        this.Name = Name;
        this.Photo = Photo;
    }
}
