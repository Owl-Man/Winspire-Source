namespace AndroidIntegrationTools
{
    using UnityEngine;
    using Unity.Notifications.Android;

    public class Notification : MonoBehaviour
    {
        bool ispaused;

        int timeForNotification;

        private void Start() => CreateNotificationChannel();

        private void OnApplicationPause (bool pauseStatus) 
        {
            ispaused = pauseStatus;

            if (ispaused) 
            {
                TrySendNotification();
            }
        }

        private void OnApplicationQuit () => TrySendNotification();

        public void TrySendNotification()
        {
            int isSending = Random.Range(1, 7); //Шанс отправки уведомления

            if (isSending == 1) 
            {
                SendNotification();
            }
            
            return;
        }

        public void CreateNotificationChannel() 
        {
            var channel = new AndroidNotificationChannel()
            {
                Id = "channel_id",
                Name = "Winspire",
                Importance = Importance.High,
                Description = "Generic notifications",
            };

            AndroidNotificationCenter.RegisterNotificationChannel(channel);
        }

        public void SendNotification() 
        {
            var notification = new AndroidNotification();
            notification.Title = "Вин ждет тебя!";
            notification.Text = "Возвращайтесь скорее и помогите Вин продвинуться дальше!";
            notification.LargeIcon = "icon_0";
            notification.SmallIcon = "icon_1";
            timeForNotification = Random.Range(550, 2600);
            notification.FireTime = System.DateTime.Now.AddMinutes(timeForNotification);

            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }
}