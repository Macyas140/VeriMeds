using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PatientMedicineDetailManager : MonoBehaviour{
    public ScreenManager screenManager;
    public GameObject PatientMedicinesScreen;
    public TMP_Text TitleText;
    public RawImage MedicineImage;
    public TMP_Text FrequencyText;
    public TMP_Text DosesText;
    public TMP_Text DescriptionText;

    void OnEnable(){
        ShowMedicine(screenManager.CurrentMedicine);
    }
    void ShowMedicine (Medicine m){
        if (m == null) return;

        TitleText.text = m.Name;
        DosesText.text = "<b>Dose:</b> 1 pill (500 mg)";
        if (m.Name.ToLower().Contains("paracetamol"))
        {
            FrequencyText.text = "<b>Time:</b> 16:00";
        }
        else if (m.Name.ToLower().Contains("omeprazol"))
        {
            FrequencyText.text = "<b>Time:</b> 14:00";
        }
        else
        {
            FrequencyText.text = "<b>Time:</b> " + m.Frequency;
        }
        DescriptionText.text = "<b>Description:</b> " + m.Details;
        if (m.Image != null) MedicineImage.texture = m.Image;
    }
    public void TakeMedicine(){
        Medicine m = screenManager.CurrentMedicine;
        if (m != null && m.RemainingDoses > 0){
            m.RemainingDoses--;
            DosesText.text = "<b>Dose:</b> " + m.RemainingDoses + " remaining";
        }
    }
    public void GoBack(){
        screenManager.SwitchScreen(PatientMedicinesScreen);
    }
}