using System.Collections.Generic;
using System;
using UnityEngine;



public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject StartScreen;
    [SerializeField] private PushNotifications push;
    public List<Patient> Patients = new List<Patient>();
    public event Action onPatientsChanged;
    public Patient CurrentPatient {get; private set;}
    public Medicine CurrentMedicine {get; private set;}
    public Texture2D DefaultPatientPhoto;
    
    //[SerializeField] private GameObject PatientsAdmin;

    private GameObject _current_Screen;
    public bool _isAdmin;   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _current_Screen = StartScreen;
        Patients.Add(new Patient("Bruno Palacios"));
        Patients.Add(new Patient("Gael Rodriguez"));
        Patients.Add(new Patient("Diego Lopez"));
        Patients.Add(new Patient("Diego Arias"));
        onPatientsChanged?.Invoke();
        Patient macy = new Patient("Emilio Macías");
        macy.AddMedicine(new Medicine { Name = "Paracetamol", HourInterval = 12, RemainingDoses = 2, StartingDoses = 12, Frequency = "Every 12 Hours", Details = "500mg tablets. Take alongside dinner." });
        macy.AddMedicine(new Medicine { Name = "Omeprazol", HourInterval = 8, RemainingDoses = 12, StartingDoses = 14, Frequency = "Every 16 Hours", Details = "20mg capsules. Take after every meal." });
        AddPatient(macy);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchScreen(GameObject Target){
        _current_Screen.SetActive(false);
        Target.SetActive(true);
        _current_Screen = Target;
        Debug.Log(_isAdmin);
        
    }

    public void AddPatient(Patient patient)
    {
        Patients.Add(patient);
        onPatientsChanged?.Invoke();
    }

    public void RemovePatient(Patient patient)
    {
        Patients.Remove(patient);
        onPatientsChanged?.Invoke();
    }

    public void SelectPatient(Patient patient, GameObject MEDICINE_ADMIN)
    {
        CurrentPatient = patient;
        SwitchScreen(MEDICINE_ADMIN);

    }

    public void SelectMedicine(Medicine medicine, GameObject Target){
        CurrentMedicine = medicine;
        SwitchScreen(Target);
    }

    public Texture GetPatientPhoto(Patient p){
        return (p != null && p.Photo != null) ? p.Photo : DefaultPatientPhoto;
    }

    public void SetCaretaker(bool val)
    {
        _isAdmin = val;
        if (val)
        {
            push.LowDoseChecker();
        }
    }
}
