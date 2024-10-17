using System.Collections;
using AndroidIntegrationTools;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Shop
{
    public class CustomizationShopPage : MonoBehaviour
    {
        [SerializeField] private Image headChooseImage, bodyChooseImage, legsChooseImage;

        [SerializeField] private GameObject headLock, bodyLock, legsLock;

        [SerializeField] private RobotParts _robotParts;

        private int _currentHead, _currentBody, _currentLegs;

        private const int Cost = 3;

        private IEnumerator Start()
        {
            yield return null;
            _currentHead = PlayerPrefs.GetInt("HeadID");
            yield return null;
            _currentBody = PlayerPrefs.GetInt("BodyID");
            yield return null;
            _currentLegs = PlayerPrefs.GetInt("LegsID");
            
            headChooseImage.sprite = _robotParts.headIcons[_currentHead];
            bodyChooseImage.sprite = _robotParts.bodyIcons[_currentBody];
            legsChooseImage.sprite = _robotParts.legsIcons[_currentLegs];
        }

        public void OnHeadArrowButtonClick(int changes)
        {
            if (_currentHead + changes < 0 || _currentHead + changes > _robotParts.headIcons.Length - 1) return;

            _currentHead += changes;
            headChooseImage.sprite = _robotParts.headIcons[_currentHead];
            
            if (_currentHead > 0 && 
                PlayerPrefs.GetInt("isHead" + _currentHead + "Unlocked", 0) == 0)
            {
                headLock.SetActive(true);
                return;
            }

            headLock.SetActive(false);
            PlayerPrefs.SetInt("HeadID", _currentHead);
        }

        public void OnHeadBuyButtonClick()
        {
            if (ShopManager.Instance.GetBalanceValue() >= Cost)
            {
                ShopManager.Instance.WithdrawMicrochipsFromBalance(Cost);
                PlayerPrefs.SetInt("isHead" + _currentHead + "Unlocked", 1);
                PlayerPrefs.SetInt("HeadID", _currentHead);
                headLock.SetActive(false);
            }
            else ToastSystem.Send("Недостаточно микрочипов");
        }
        
        public void OnBodyArrowButtonClick(int changes)
        {
            if (_currentBody + changes < 0 || _currentBody + changes > _robotParts.bodyIcons.Length - 1) return;
            
            _currentBody += changes;
            bodyChooseImage.sprite = _robotParts.bodyIcons[_currentBody];
            
            if (_currentBody > 0 && 
                PlayerPrefs.GetInt("isBody" + _currentBody + "Unlocked", 0) == 0)
            {
                bodyLock.SetActive(true);
                return;
            }
            
            bodyLock.SetActive(false);
            PlayerPrefs.SetInt("BodyID", _currentBody);
        }

        public void OnBodyBuyButtonClick()
        {
            if (ShopManager.Instance.GetBalanceValue() >= Cost)
            {
                ShopManager.Instance.WithdrawMicrochipsFromBalance(Cost);
                PlayerPrefs.SetInt("isBody" + _currentBody +"Unlocked", 1);
                PlayerPrefs.SetInt("BodyID", _currentBody);
                bodyLock.SetActive(false);
            }
            else ToastSystem.Send("Недостаточно микрочипов");
        }
        
        public void OnLegsArrowButtonClick(int changes)
        {
            if (_currentLegs + changes < 0 || _currentLegs + changes > _robotParts.legsIcons.Length - 1) return;
            
            _currentLegs += changes;
            legsChooseImage.sprite = _robotParts.legsIcons[_currentLegs];
            
            if (_currentLegs > 0 &&
                PlayerPrefs.GetInt("isLegs" + _currentLegs + "Unlocked", 0) == 0)
            {
                legsLock.SetActive(true);
                return;
            }
            
            legsLock.SetActive(false);
            PlayerPrefs.SetInt("LegsID", _currentLegs);
        }
        
        public void OnLegsBuyButtonClick()
        {
            if (ShopManager.Instance.GetBalanceValue() >= Cost)
            {
                ShopManager.Instance.WithdrawMicrochipsFromBalance(Cost);
                PlayerPrefs.SetInt("isLegs" + _currentLegs +"Unlocked", 1);
                PlayerPrefs.SetInt("LegsID", _currentLegs);
                legsLock.SetActive(false);
            }
            else ToastSystem.Send("Недостаточно микрочипов");
        }
    }
}