using System.Collections;
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
    public Texture DefaultMedicineImage;
    public Button TakeButton;
    public PushNotifications push;

    void OnEnable(){
        ShowMedicine(screenManager.CurrentMedicine);
        StartCoroutine(RefreshLoop());
    }
    void ShowMedicine (Medicine m){
        if (m == null) return;

        TitleText.text = m.Name;
        DescriptionText.text = "<b>Description:</b> " + m.Details;
        MedicineImage.texture = m.Image != null ? m.Image : DefaultMedicineImage;
        MedicineImage.gameObject.SetActive(MedicineImage.texture != null);

        RefreshTimeInfo(m);
    }

    IEnumerator RefreshLoop(){
        while(true){
            RefreshTimeInfo(screenManager.CurrentMedicine);
            yield return new WaitForSeconds(1f);
        }
    }

    void RefreshTimeInfo(Medicine m){
        if(m == null){
            return;
        }
        DosesText.text = "<b>Dose Left:</b> " + m.RemainingDoses;

        if(m.RemainingDoses <= 0){
            FrequencyText.text = "<b>No doses left</b>";
            TakeButton.interactable = false;
        }else if(m.CanTakeItNow){
            FrequencyText.text = "<b>It's time to take it</b>";
            TakeButton.interactable = true;
        }else{
            System.TimeSpan t = m.TimeUntilNextDose;
            FrequencyText.text = "<b>Next dose at " + m.NextDoseTime.ToString("HH:mm") + "</b> (in " + string.Format("{0:D2}:{1:D2}:{2:D2}", (int)t.TotalHours, t.Minutes, t.Seconds) + ")";
            TakeButton.interactable = false;
        }
    }

    public void TakeMedicine(){
        Medicine m = screenManager.CurrentMedicine;
        if(m == null){
            return;
        }
        if(m.TryToTakeDose()){
            push.ScheduleDoseReminder(m);
            RefreshTimeInfo(m);
        }
    }
    public void GoBack(){
        screenManager.SwitchScreen(PatientMedicinesScreen);
    }
}