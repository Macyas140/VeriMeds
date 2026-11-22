using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditMedicine : MonoBehaviour
{

    [SerializeField] private ScreenManager screenManager;
    [SerializeField] private GameObject MedicineDetailsScreen;
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

    void OnEnable()
    {
        LoadMedicine(screenManager.CurrentMedicine);
    }

    void LoadMedicine(Medicine m)
    {
        if (m == null){
            return;
        }

        MedicineName.text = m.Name;
        DescriptionText.text = m.Details;
        Units.text = m.TotalUnits > 0 ? m.TotalUnits.ToString() : "";
        Dose.text = m.DoseAmount > 0 ? m.DoseAmount.ToString() : "";
        TimeInterval.text = m.HourInterval > 0 ? m.HourInterval.ToString() : "";
        _selectedPhoto = m.Image;
        PhotoImage.texture = m.Image;

        if(m.Image != null) PhotoImage.color = Color.white;
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

    public void SaveChanges()
    {
        
        Medicine m = screenManager.CurrentMedicine;
        if(int.TryParse(Dose.text, out int DoseAmountForm) && int.TryParse(TimeInterval.text, out int HourPeriod) && int.TryParse(Units.text, out int MaxUnits))
        {
            int newStarting = MaxUnits / DoseAmountForm;
            int diff = newStarting - m.StartingDoses;
            m.Name=MedicineName.text;
            m.Details = DescriptionText.text;
            m.StartingDoses = newStarting;
            m.RemainingDoses = Mathf.Clamp(m.RemainingDoses + diff, 0, newStarting);
            m.TotalUnits = MaxUnits;
            m.DoseAmount = DoseAmountForm;
            m.HourInterval = HourPeriod;
            m.Frequency = "Every " + HourPeriod + " Hours";
            m.Image = _selectedPhoto;
        } else
        {
            Debug.Log("Invalid Modifications");
            return;
        }

        screenManager.SwitchScreen(MedicineDetailsScreen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
