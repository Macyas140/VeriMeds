using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class PatientsDetail : MonoBehaviour
{
    public Image Background;
    public TMP_Text NameText;
    public TMP_Text DosesText;

    public Color NormalColor = new Color(1f, 0.92f, 0.92f);
    public Color LowDosesColor = new Color(0.63f, 0.08f, 0.1f);
    public int LowDosesThreshold = 3;
    private Medicine medicine;
    private Action<Medicine> onClick;
    
    public void Setup(Medicine m, Action<Medicine> clickCallback){
        medicine = m;
        onClick = clickCallback;
        NameText.text = m.Name;
        DosesText.text = "It remains " + m.RemainingDoses + " dosis";
        Background.color = m.RemainingDoses <= LowDosesThreshold ? LowDosesColor : NormalColor;

        Button btn = GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => onClick?.Invoke(medicine));
    }
}
