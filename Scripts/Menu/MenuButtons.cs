using System.Collections;
using UnityEngine;

namespace Menu
{
    public class MenuButtons : MonoBehaviour
    {
        [SerializeField] private GameObject ShopPanel, SettingsPanel, LevelsPanel, QuitPanel;

        [SerializeField] private float TimeForCloseWindow = 0.35f;

        public void OnPlayButtonClick() => LevelsPanel.SetActive(true);

        public void OnBackForLevelsPanelButtonClick() => StartCoroutine(CloseLevelsWindow());

        public void OnShopButtonClick() => ShopPanel.SetActive(true);

        public void OnBackForShopButtonClick() => StartCoroutine(CloseShopWindow());

        public void OnSettingsButtonClick() => SettingsPanel.SetActive(true);

        public void OnBackForSettingsButtonClick() => StartCoroutine(CloseSettingsWindow());

        public IEnumerator CloseSettingsWindow()
        {
            yield return new WaitForSeconds(TimeForCloseWindow);
            SettingsPanel.SetActive(false);
        }

        public void OnExitButtonClick() => QuitPanel.SetActive(true);

        public void OnYesForExitButtonClick() => Application.Quit();

        public void OnNoForExitButtonClick() => QuitPanel.SetActive(false);

        private IEnumerator CloseShopWindow() 
        {
            yield return new WaitForSeconds(TimeForCloseWindow);
            ShopPanel.SetActive(false);
        }
        
        private IEnumerator CloseLevelsWindow() 
        {
            yield return new WaitForSeconds(TimeForCloseWindow);
            LevelsPanel.SetActive(false);
        }
    }
}
