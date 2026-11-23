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
            
            TMP_Text tiemText = card.transform.Find("MedicineNameText").GetComponent<TMP_Text>();
            if(m.RemainingDoses <= 0){
                tiemText.text = "No doses Left";
            }else if(m.CanTakeItNow){
                tiemText.text = "Take Now";
            }else{
                tiemText.text = "Next at " + m.NextDoseTime.ToString("HH:mm");
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