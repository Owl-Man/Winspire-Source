using UnityEngine;
using UnityEngine.UI;

namespace WIS
{
    public class RobotJoystickController : MonoBehaviour
    {
        [SerializeField] private Button attackButton;

        private bool _isOnlyCodeGenerationModeActivated;

        private WinspireInteractionSystem _wis;

        private void Start()
        {
            _wis = WinspireInteractionSystem.Instance;
            if (!_wis.haveAttackWeapon) attackButton.interactable = false;
        }

        public void OnOpenJoystickButtonClick()
        {
            if (!gameObject.activeSelf) gameObject.SetActive(true);
            else gameObject.SetActive(false);
        }

        public void OnChangeModeButtonClick() =>
            _isOnlyCodeGenerationModeActivated = !_isOnlyCodeGenerationModeActivated;

        public void OnAttackButtonClick()
        {
            if (_wis.player.IsPointInEnemy(0))
            {
                if (!_isOnlyCodeGenerationModeActivated) _wis.player.AttackAtPoint(0);
                _wis.GenerateCommandAtNewLine(_wis.wisData.commands[4]);
            }
            else if (_wis.player.IsPointInEnemy(1))
            {
                if (!_isOnlyCodeGenerationModeActivated) _wis.player.AttackAtPoint(1);
                _wis.GenerateCommandAtNewLine(_wis.wisData.commands[5]);
            }
            else if (WinspireInteractionSystem.Instance.player.IsPointInEnemy(2))
            {
                if (!_isOnlyCodeGenerationModeActivated) _wis.player.AttackAtPoint(2);
                _wis.GenerateCommandAtNewLine(_wis.wisData.commands[6]);
            }
            else if (WinspireInteractionSystem.Instance.player.IsPointInEnemy(3))
            {
                if (!_isOnlyCodeGenerationModeActivated) _wis.player.AttackAtPoint(3);
                _wis.GenerateCommandAtNewLine(_wis.wisData.commands[7]);
            }
        }

        public void OnRightButtonClick()
        {
            if (!_isOnlyCodeGenerationModeActivated) _wis.player.MoveToX(1);
            _wis.GenerateCommandAtNewLine(_wis.wisData.commands[0]);
        }

        public void OnLeftButtonClick()
        {
            if (!_isOnlyCodeGenerationModeActivated) _wis.player.MoveToX(-1);
            _wis.GenerateCommandAtNewLine(_wis.wisData.commands[1]);
        }

        public void OnUpButtonClick()
        {
            if (!_isOnlyCodeGenerationModeActivated) _wis.player.MoveToY(1);
            _wis.GenerateCommandAtNewLine(_wis.wisData.commands[2]);
        }

        public void OnDownButtonClick()
        {
            if (!_isOnlyCodeGenerationModeActivated) _wis.player.MoveToY(-1);
            _wis.GenerateCommandAtNewLine(_wis.wisData.commands[3]);
        }
    }
}