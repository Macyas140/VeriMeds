using System.Collections.Generic;
using UnityEngine;



public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject StartScreen;
    public List<Patient> Patients = new List<Patient>();
    
    //[SerializeField] private GameObject PatientsAdmin;

    private GameObject _current_Screen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _current_Screen = StartScreen;
        Patients.Add(new Patient("Bruno Palacios"));
        Patients.Add(new Patient("Gael Rodriguez"));
        Patients.Add(new Patient("Diego Lopez"));
        Patients.Add(new Patient("Diego Arias"));

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
}
