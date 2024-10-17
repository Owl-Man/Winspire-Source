using System.Collections;
using AndroidIntegrationTools;
using DataBases;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Shop
{
    public class InstrumentsShopPage : MonoBehaviour
    {
        [Header ("Shop Costs")]
        [SerializeField] private int WhileBoostCost;
        [SerializeField] private int ElectrogunBoostCost;

        [Header ("Other")]
        [SerializeField] private Button WhileBuyButton, ElectrogunBuyButton;
        [SerializeField] private GameObject WhileBuyedIcon, ElectrogunBuyedIcon;

        private ShopManager _shop;

        private void Start()
        {
            _shop = ShopManager.Instance;
            StartCoroutine(SyncValues());
        }

        public void OnBuyWhileBoostButtonClick()
        {
            if (_shop.GetBalanceValue() >= WhileBoostCost)
            {
                _shop.WithdrawMicrochipsFromBalance(WhileBoostCost);
                PlayerPrefs.SetInt(PPKeys.IS_WHILE_BOOST_ACTIVATED_KEY, 1);
                StartCoroutine(SyncValues());
            }
            else ToastSystem.Send("Недостаточно микрочипов");
        }

        public void OnElectrogunBoostButtonClick()
        {
            if (_shop.GetBalanceValue() >= ElectrogunBoostCost)
            {
                _shop.WithdrawMicrochipsFromBalance(ElectrogunBoostCost);
                PlayerPrefs.SetInt(PPKeys.IS_ELECTROGUN_BOOST_ACTIVATED_KEY, 1);
                StartCoroutine(SyncValues());
            }
            else ToastSystem.Send("Недостаточно микрочипов");
        }
    
        private IEnumerator SyncValues()
        {
            if (PlayerPrefs.GetInt(PPKeys.IS_WHILE_BOOST_ACTIVATED_KEY, 0) == 1)
            {
                WhileBuyedIcon.SetActive(true);
                WhileBuyButton.interactable = false;
            }
        
            if (PlayerPrefs.GetInt(PPKeys.IS_ELECTROGUN_BOOST_ACTIVATED_KEY, 0) == 1)
            {
                ElectrogunBuyedIcon.SetActive(true);
                ElectrogunBuyButton.interactable = false;
            }

            yield return null;
        }
    }
}