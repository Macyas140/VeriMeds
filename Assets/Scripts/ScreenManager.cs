using System.Collections.Generic;
using System;
using UnityEngine;



public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject StartScreen;
    public List<Patient> Patients = new List<Patient>();
    public event Action onPatientsChanged;
    public Patient CurrentPatient {get; private set;}
    
    //[SerializeField] private GameObject PatientsAdmin;
    public List<Patient> Patients = new List<Patient>();

    private GameObject _current_Screen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _current_Screen = StartScreen;
        Patients.Add(new Patient("Bruno Palacios"));
        Patients.Add(new Patient("Gael Rodriguez"));
        Patients.Add(new Patient("Diego Lopez"));
        Patients.Add(new Patient("Diego Arias"));
        onPatientsChanged?.Invoke();
        Patient macy = new Patient("Macy Ass");
        macy.AddMedicine(new Medicine { Name = "Paracetamol", RemainingDoses = 3, StartingDoses = 12 });
        macy.AddMedicine(new Medicine { Name = "Omeprazol", RemainingDoses = 12, StartingDoses = 14 });
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
}
