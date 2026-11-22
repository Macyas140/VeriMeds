using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PatientMedicinesManager : MonoBehaviour{
    public ScreenManager ScreenManager;
    public TMP_Text NameText;
    public Transform MedicinesContainer;
    public GameObject MedicineCardPrefab;
    public GameObject MedicineInfoPatientScreen;
    public RawImage PhotoImage;

    private List<GameObject> cards = new List<GameObject>();
    void OnEnable(){
        Patient macy = ScreenManager.Patients.Find(p => p.Name == "Macy Ass");
        if (macy == null && ScreenManager.Patients.Count > 0){
            macy = ScreenManager.Patients[ScreenManager.Patients.Count - 1];
        }
        ShowPatient (macy);
    }

    public void ShowPatient(Patient p){
        if (p == null){
            Debug.LogWarning("The pacient is unavaliable");
            return;
        }
        foreach (GameObject card in cards){
            Destroy(card);
        }
        cards.Clear();
        NameText.text = "Hello, " + p.Name.Split(' ')[0] + "!";

        if (PhotoImage != null){
            PhotoImage.texture = ScreenManager.GetPatientPhoto(p);
            PhotoImage.color = Color.white;
        }

        foreach (Medicine m in p.Medicines){
            GameObject card = Instantiate(MedicineCardPrefab, MedicinesContainer);
            card.GetComponent<PatientsDetail>().Setup(m, OnMedicineClicked);
            
            if (m.Name.ToLower().Contains("paracetamol")){
                card.transform.Find("MedicineNameText").GetComponent<TMP_Text>().text = "In 4 hours (16:00)";
            }
            else if (m.Name.ToLower().Contains("omeprazol")){
                card.transform.Find("MedicineNameText").GetComponent<TMP_Text>().text = "In 2 hours (14:00)";
            }
            else{
                card.transform.Find("MedicineNameText").GetComponent<TMP_Text>().text = "In 6 hours (18:00)";
            }

            card.transform.Find("DosesText").GetComponent<TMP_Text>().text = m.Name.ToUpper();
            card.GetComponent<Image>().color = new Color32(234, 227, 236, 255);
            cards.Add(card);
        }
    void OnMedicineClicked(Medicine medicine){
        ScreenManager.SelectMedicine(medicine, MedicineInfoPatientScreen);
    }
    }
}