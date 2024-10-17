using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AndroidIntegrationTools
{
    public class AndroidButtonsEvent : MonoBehaviour
    {
        [SerializeField] private Button quitButton;
        
        private List<GameObject> _openedWindows = new List<GameObject>();
        private List<Button> _backButtons = new List<Button>();
        
        private void Update() 
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //back button press
            {
                if (_openedWindows.Count > 0) _backButtons[_backButtons.Count - 1].onClick?.Invoke();
                else if (quitButton != null) quitButton.onClick?.Invoke();
            }
        }

        public void AddOpenedWindow(GameObject window) => _openedWindows.Add(window);

        public void AddBackButton(Button backButton) => _backButtons.Add(backButton);

        public void RemoveOpenedWindow()
        {
            _openedWindows.RemoveAt(_openedWindows.Count - 1);
            _backButtons.RemoveAt(_backButtons.Count - 1);
        }
    }
}