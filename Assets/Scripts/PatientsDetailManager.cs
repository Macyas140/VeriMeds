using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class PatientsDetailManager : MonoBehaviour
{
    public ScreenManager screenManager;
    public GameObject PatientsListScreen;
    public TMP_Text NameText;
    public Transform medicinesContainer;
    public GameObject MedicineCardPrefab;
    public GameObject MedicineDetailsScreen;
    public RawImage PhotoImage;

    private List<GameObject> cards = new List<GameObject>();
    
   void OnEnable()
    {
        Debug.Log("3) OnEnable de " + gameObject.name + ". Paciente: " +
              (screenManager.CurrentPatient != null ? screenManager.CurrentPatient.Name : "NULL"));
        ShowPatient(screenManager.CurrentPatient);
    }

    public void ShowPatient(Patient p)
    {
        if(p == null)
        {
            Debug.LogWarning("No hay paciente seleccionado.");
            return;
        }
        foreach(GameObject card in cards){
            Destroy(card);
        }
        cards.Clear();
        NameText.text = p.Name;
        PhotoImage.texture = screenManager.GetPatientPhoto(p);
        PhotoImage.color = Color.white;
        foreach(Medicine m in p.Medicines){
            GameObject card = Instantiate(MedicineCardPrefab, medicinesContainer);
            card.GetComponent<PatientsDetail>().Setup(m, OnMedicineClicked);
            cards.Add(card);
        }
    }

    void OnMedicineClicked(Medicine medicine){
        screenManager.SelectMedicine(medicine, MedicineDetailsScreen);
        Debug.Log("Opening Medicine: " + medicine.Name);
    }
}
