using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddMedicine : MonoBehaviour
{
    [SerializeField] private ScreenManager screenManager;
    [SerializeField] private GameObject AdminMedicinesScreen;
    [SerializeField] private TMP_InputField MedicineName;
    [SerializeField] private TMP_InputField DescriptionText;
    [SerializeField] private TMP_InputField Units;
    [SerializeField] private TMP_InputField Dose;
    [SerializeField] private TMP_InputField TimeInterval;
    [SerializeField] private Button PhotoButton;
    [SerializeField] private RawImage PhotoImage;

    private Texture2D _selectedPhoto;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PhotoButton.onClick.AddListener(PickPhoto);
    }


    void PickPhoto()
    {
        NativeGallery.GetImageFromGallery(path =>
        {
            if(path == null) return;
            var tex = NativeGallery.LoadImageAtPath(path, 1024);
            if(tex !=null) SetPhoto(tex);
        });
    }


    void SetPhoto(Texture2D tex)
    {
        _selectedPhoto = tex;
        PhotoImage.texture = _selectedPhoto;
        PhotoImage.color = Color.white;
    }
    public void SaveNewMedicine()
    {
        
        

        if(int.TryParse(Dose.text, out int DoseAmountForm) && int.TryParse(TimeInterval.text, out int HourPeriod) && int.TryParse(Units.text, out int MaxUnits))
        {
             var newMedicine = new Medicine
        {
            Name=MedicineName.text,
            Details = DescriptionText.text,
            StartingDoses = MaxUnits/DoseAmountForm,
            RemainingDoses = MaxUnits/DoseAmountForm,
            TotalUnits = MaxUnits,
            DoseAmount = DoseAmountForm,
            HourInterval = HourPeriod,
            Frequency = "Every " + HourPeriod + " Hours",
            Image = _selectedPhoto
            
        };

        screenManager.CurrentPatient.Medicines.Add(newMedicine);
       // screenManager.Patients.Add(newPatient);
        MedicineName.text = "";
        DescriptionText.text = "";
        Dose.text = "";
        TimeInterval.text = "";
        Units.text = "";
        _selectedPhoto = null;
        PhotoImage.texture = null;
        } else
        {
            Debug.Log("Invalid Values");
            return;
        }

        screenManager.SwitchScreen(AdminMedicinesScreen);
        

       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
