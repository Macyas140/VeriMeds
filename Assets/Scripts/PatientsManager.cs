using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class PatientsManager : MonoBehaviour
{
    public TMP_InputField SearchBar;
    public Transform contentScrollView;
    public GameObject PatientPrefab;
    public ScreenManager screenManager;

   // public List<Patient> PacientList = new List<Patient>();
    private List<GameObject> Buttons = new List<GameObject>();
    public GameObject PatientDetails;


    void OnEnable()
    {
        screenManager.onPatientsChanged += RefreshList;
        SearchBar.onValueChanged.AddListener(SearchPatients);
        RefreshList();
        
    }

    void Start()
    {
        RefreshList();
       
    }

    void OnDisable()
    {
        screenManager.onPatientsChanged -= RefreshList;
        SearchBar.onValueChanged.RemoveListener(SearchPatients);
    }

    void RefreshList()
    {
        SearchPatients(SearchBar.text);
    }

    void GenerateButtons(List<Patient> showPatients){
        foreach(GameObject button in Buttons){
            Destroy(button);
        }
        Buttons.Clear();

        foreach(Patient p in showPatients){
            GameObject newButton = Instantiate(PatientPrefab, contentScrollView);
            TMP_Text textButton = newButton.GetComponentInChildren<TMP_Text>();
            if(textButton != null){
                textButton.text = p.Name + " " + p.LastName;
            }
            Patient actualPacient = p;
            Button btn = newButton.GetComponent<Button>();
            btn.onClick.AddListener(() => PacientClick(actualPacient));
            Buttons.Add(newButton);
        }
    }

    void SearchPatients(string text){
        if(String.IsNullOrEmpty(text)){
            GenerateButtons(screenManager.Patients);
        }else{
            List<Patient> Searched = screenManager.Patients.FindAll(p => p.Name.ToLower().Contains(text.ToLower()));
            GenerateButtons(Searched);
        }
    }

    void PacientClick(Patient pacient){
        screenManager.SelectPatient(pacient, PatientDetails);
        //Debug.Log("Opening Pacient: " + pacient.Name);
    }
}
