using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwitchToggle : MonoBehaviour 
{
   [SerializeField] private Toggle toggle;
   [SerializeField] private Image backgroundImage, handleImage;
   
   [SerializeField] private RectTransform uIHandleRectTransform;
   
   [SerializeField] private Color backgroundActiveColor, handleActiveColor;
   [SerializeField] private Color backgroundDefaultColor, handleDefaultColor;

   private Vector2 _handlePosition;

   private void Start() 
   {
      _handlePosition = uIHandleRectTransform.anchoredPosition;

      toggle.onValueChanged.AddListener(OnSwitch);

      OnSwitch(toggle.isOn);
   }

   private void OnSwitch(bool on)
   {
      on = !on;
      
      uIHandleRectTransform.DOAnchorPos(on ? _handlePosition * -1 : _handlePosition, .4f).SetEase(Ease.InOutBack);

      backgroundImage.DOColor(on ? backgroundDefaultColor : backgroundActiveColor, .6f);

      handleImage.DOColor(on ? handleDefaultColor : handleActiveColor, .4f);
   }

   private void OnDestroy() => toggle.onValueChanged.RemoveListener(OnSwitch);
}
