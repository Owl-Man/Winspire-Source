using System.Collections;
using DataBases;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Shop
{
    public class ShopManager : MonoBehaviour
    {
        [Header("Pages")]
        [SerializeField] private GameObject[] InstrumentPageObjects;
        [SerializeField] private GameObject[] CustomizationPageObjects;
        
        [Header("Other")]
        [SerializeField] private Text balanceText;

        [SerializeField] private Image backIcon, instrumentButtonImage, customizationButtonImage;

        [SerializeField] private Sprite instrumentIcon, customizationIcon;

        [SerializeField] private Color pressed, unpressedBlue, unpressedRed;

        [SerializeField] private GameObject customizationLock;

        private bool _isCustomizationAvailable;
        private int _balance;

        public static ShopManager Instance;

        private void Awake() => Instance = this;

        private IEnumerator Start()
        {
            yield return null;
            
            _isCustomizationAvailable = true;
            
            for (int i = 1; i < 5; i++)
            {
                if (PlayerPrefs.GetInt("isLevel" + i + "Completed") != 1)
                {
                    _isCustomizationAvailable = false;
                    break;
                }

                yield return null;
            }
            
            UpdateBalance();
        }

        public int GetBalanceValue() => _balance;
        
        private void UpdateBalance()
        {
            _balance = PlayerPrefs.GetInt(PPKeys.COINS_KEY, 0);
            balanceText.text = _balance.ToString();
        }

        public void WithdrawMicrochipsFromBalance(int count)
        {
            if (_balance < count) return;
            
            PlayerPrefs.SetInt(PPKeys.COINS_KEY, _balance - count);
            UpdateBalance();
        }
        
        public void ChangePageToInstruments()
        {
            customizationLock.SetActive(false);
            
            backIcon.sprite = instrumentIcon;
            instrumentButtonImage.color = pressed;
            customizationButtonImage.color = unpressedRed;
            
            for (int i = 0; i < CustomizationPageObjects.Length; i++)
            {
                CustomizationPageObjects[i].SetActive(false);
            }
            
            for (int i = 0; i < InstrumentPageObjects.Length; i++)
            {
                InstrumentPageObjects[i].SetActive(true);
            }
        }
        
        public void ChangePageToCustomization()
        {
            backIcon.sprite = customizationIcon;
            instrumentButtonImage.color = unpressedBlue;
            customizationButtonImage.color = pressed;

            for (int i = 0; i < InstrumentPageObjects.Length; i++)
            {
                InstrumentPageObjects[i].SetActive(false);
            }

            if (!_isCustomizationAvailable)
            {
                customizationLock.SetActive(true);
                return;
            }
            
            for (int i = 0; i < CustomizationPageObjects.Length; i++)
            {
                CustomizationPageObjects[i].SetActive(true);
            }
        }
    }
}