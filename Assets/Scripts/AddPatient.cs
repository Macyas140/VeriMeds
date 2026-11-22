using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddPatient : MonoBehaviour
{
    [SerializeField] private ScreenManager screenManager;
    [SerializeField] private GameObject UsersAdminScreen;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField lastNameInput;
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
    public void SaveNewPatient()
    {
        var newPatient = new Patient
        {
            Name = nameInput.text,
            LastName = lastNameInput.text,
            Photo = _selectedPhoto
        };

        screenManager.AddPatient(newPatient);
        nameInput.text = "";
        lastNameInput.text = "";
        _selectedPhoto = null;
        PhotoImage.texture = null;
        screenManager.SwitchScreen(UsersAdminScreen);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
