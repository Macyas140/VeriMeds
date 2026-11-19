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

    public void AddMedicine(Medicine medicine)
    {
        Medicines.Add(medicine);
    }

    public Patient(string name)
    {
        Name = name;
    }

    public Patient()
    {
        
    }

    public Patient(string name, List<Medicine> medicines, Texture2D photo){
        Name = name;
        Photo = photo;
        Medicines = medicines;
    }
}
