using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MedicineDetailManager : MonoBehaviour
{
    public ScreenManager screenManager;
    public GameObject PatientDetailsScreen;
    public GameObject EditMedicineScreen;
    public TMP_Text TitleText;
    public RawImage MedicineImage;
    public TMP_Text FrequencyText;
    public TMP_Text DosesText;
    public TMP_Text DescriptionText;

    void OnEnable(){
        ShowMedicine(screenManager.CurrentMedicine);
    }

    void ShowMedicine(Medicine m){
        if (m == null){
            return;
        }

        TitleText.text = "Details of " + m.Name;
        FrequencyText.text = m.Frequency;
        DosesText.text = m.RemainingDoses + " Remaining Doses";
        DescriptionText.text = "Description: \n" + m.Details;
        MedicineImage.texture = m.Image;
        MedicineImage.gameObject.SetActive(m.Image != null);
    }

    public void GoBack(){
        screenManager.SwitchScreen(PatientDetailsScreen);
    }

    public void OpenEdit(){
        Debug.Log("Lol");
    }
}
