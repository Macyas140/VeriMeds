using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Android;
using Unity.Notifications.Android;

public class PushNotifications : MonoBehaviour
{
    [SerializeField] public ScreenManager screenManager;
    public int Threshold = 3;
    

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        var channel = new AndroidNotificationChannel()
        {
        Id = "dose_check",
        Name = "Low Dose Notifications",
        Importance = Importance.High,
        Description = "Low Dose alarm for caretakers"
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var doseChannel = new AndroidNotificationChannel(){
            Id = "dose_time",
            Name = "Dose Reminders",
            Importance = Importance.High,
            Description = "Reminds when its time to take the medicine"
        };
        AndroidNotificationCenter.RegisterNotificationChannel(doseChannel);
    }

    public void LowDoseChecker()
    {
        if (screenManager._isAdmin)
        {
            foreach(Patient pat in screenManager.Patients){
                foreach (Medicine med in pat.Medicines)
                {
                    if (med.RemainingDoses <= Threshold)
                    {
                        Debug.Log("NOTI -> " + pat.Name + " | " + med.Name + " | " + med.RemainingDoses);
                        var Noti = new AndroidNotification
                        {
                            Title = "LOW DOSES",
                            Text = pat.Name + " only has "+ med.RemainingDoses + " doses left of " + med.Name,
                            FireTime = DateTime.Now.AddSeconds(1)
                        };

                        AndroidNotificationCenter.SendNotification(Noti,"dose_check");
                    }   
                }
            }
        }
        else
        {
            return;
        }
    }

    public void ScheduleDoseReminder(Medicine med){
        CancelDoseReminder(med);
        if(med.RemainingDoses <= 0){
            return;
        }
        DateTime fireTime = med.NextDoseTime;
        if(fireTime <= DateTime.Now){
            return;
        }

        var noti = new AndroidNotification{
            Title = "Time to take your Medicine",
            Text = "Its time to take " + med.Name,
            FireTime = fireTime
        };
        med.ReminderId = AndroidNotificationCenter.SendNotification(noti, "dose_time");
    }

    public void CancelDoseReminder(Medicine med){
        if(med.ReminderId != 1){
            AndroidNotificationCenter.CancelScheduledNotification(med.ReminderId);
            med.ReminderId = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
