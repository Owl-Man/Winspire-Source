using System.Collections;
using DataBases;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class SettingsPanelScript : MonoBehaviour
    {
        [SerializeField] private Toggle helpButtonToggle, firstQuideToggle, soundToggle;

        private IEnumerator Start()
        {
            helpButtonToggle.onValueChanged.AddListener(OnToggleHelpButtonClick);
            soundToggle.onValueChanged.AddListener(OnToggleSoundButtonClick);
            firstQuideToggle.onValueChanged.AddListener(OnToggleFirstGuideButtonClick);
        
            if (PlayerPrefs.GetInt(PPKeys.IS_HELP_BUTTON_ACTIVATED_KEY, 1) == 1) helpButtonToggle.isOn = true;
            else helpButtonToggle.isOn = false;

            yield return null;
        
            if (PlayerPrefs.GetInt(PPKeys.IS_GUIDE_SHOWING_ONLY_FIRST_TIME_KEY, 1) == 1) firstQuideToggle.isOn = true;
            else firstQuideToggle.isOn = false;

            if (SoundScript.instance.isSoundActivated) soundToggle.isOn = true;
            else soundToggle.isOn = false;
        }

        public void OnToggleHelpButtonClick(bool isOn)
        {
            if (isOn) PlayerPrefs.SetInt(PPKeys.IS_HELP_BUTTON_ACTIVATED_KEY, 1);
            else PlayerPrefs.SetInt(PPKeys.IS_HELP_BUTTON_ACTIVATED_KEY, 0);
        }
    
        public void OnToggleFirstGuideButtonClick(bool isOn)
        {
            if (isOn) PlayerPrefs.SetInt(PPKeys.IS_GUIDE_SHOWING_ONLY_FIRST_TIME_KEY, 1);
            else PlayerPrefs.SetInt(PPKeys.IS_GUIDE_SHOWING_ONLY_FIRST_TIME_KEY, 0);
        }

        public void OnToggleSoundButtonClick(bool isOn)
        {
            if (isOn) PlayerPrefs.SetInt(PPKeys.IS_SOUND_ACTIVATED_KEY, 1);
            else PlayerPrefs.SetInt(PPKeys.IS_SOUND_ACTIVATED_KEY, 0);
        
            SoundScript.instance.Initialize();
        }

        private void OnDestroy()
        {
            helpButtonToggle.onValueChanged.RemoveListener(OnToggleHelpButtonClick);
            soundToggle.onValueChanged.RemoveListener(OnToggleSoundButtonClick);
            firstQuideToggle.onValueChanged.RemoveListener(OnToggleFirstGuideButtonClick);
        }
    }
}