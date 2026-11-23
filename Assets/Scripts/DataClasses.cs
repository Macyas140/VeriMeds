using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Medicine
{
    public string Name;
    public string Frequency; 
    public string Details;
    public int RemainingDoses;
    public int StartingDoses;
    public int HourInterval;
    public int TotalUnits;
    public int DoseAmount;
    public Texture2D Image;
    public long LastTakenMedicine;
    public static int MinutesPerHour = 60;
    public int ReminderId = -1;

    public void RestoreDoses()
    {
        
    }


    public DateTime NextDoseTime{
        get{
            if(LastTakenMedicine == 0){
                return DateTime.MinValue;
            }
            return new DateTime(LastTakenMedicine).AddMinutes(HourInterval * MinutesPerHour);
        }
    }

    public bool CanTakeItNow => RemainingDoses > 0 && DateTime.Now >= NextDoseTime;

    public TimeSpan TimeUntilNextDose{
        get{
            TimeSpan t = NextDoseTime - DateTime.Now;
            return t > TimeSpan.Zero ? t : TimeSpan.Zero;
        }
    }

    public bool TryToTakeDose(){
        if(!CanTakeItNow){
            return false;
        }
        RemainingDoses--;
        LastTakenMedicine = DateTime.Now.Ticks;
        return true;
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
