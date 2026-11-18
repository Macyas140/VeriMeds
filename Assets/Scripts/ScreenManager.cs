using System.Collections.Generic;
using UnityEngine;



public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject StartScreen;
    //[SerializeField] private GameObject PatientsAdmin;
    public List<Patient> Patients = new List<Patient>();

    private GameObject _current_Screen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _current_Screen = StartScreen;
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
