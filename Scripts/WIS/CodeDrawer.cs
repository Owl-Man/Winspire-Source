using UnityEngine;
using UnityEngine.UI;

namespace WIS
{
    //Terminal code visualization system
    public class CodeDrawer : MonoBehaviour
    {
        [HideInInspector] public string[] originalCode;

        private sbyte _codeLayer = -1;

        private const byte MaxCodeLength = 50;

        public static CodeDrawer Instance;
        private WinspireInteractionSystem _wis;

        private void Awake() => Instance = this;

        private void Start()
        {
            _wis = WinspireInteractionSystem.Instance;
            originalCode = _wis.Terminal.text.Split('\n');
        }

        public void OnStartEdit() => ReturnOriginalCode();

        public void OnEndEdit() => DrawCode();
        
        private void DrawCode()
        {
            if (_wis.Terminal.text == "") return;
            
            _wis.Terminal.textComponent.color = Color.white;
            
            originalCode = _wis.Terminal.text.Split('\n');

            _wis.Terminal.text = string.Empty;
            _codeLayer = WinspireInteractionSystem.defaultCodeLayer;

            for (byte i = 0; i < originalCode.Length && i < MaxCodeLength; i++)
            {
                if (IsLineConstruction(originalCode[i]))
                {
                    DrawLine("#6E96D1", i);
                    _codeLayer++;
                }
                else if (originalCode[i] == "end")
                {
                    _codeLayer--;
                    DrawLine("#6E96D1", i);
                }
                else if (_wis.HasCommandIn(originalCode[i]))
                {
                    DrawLine("orange", i);
                }
                else
                {
                    DrawLine("white", i);
                }
            }
        }
        
        private void ReturnOriginalCode()
        {
            if (_wis.Terminal.text == "") return;
            
            _wis.Terminal.text = string.Empty;
            
            for (byte i = 0; i < originalCode.Length; i++)
            {
                if (i == 0) _wis.Terminal.text += originalCode[i];
                else _wis.Terminal.text = _wis.Terminal.text + '\n' + originalCode[i];
            }
        }

        private void DrawLine(string color, byte line)
        {
            if (line == 0) //First line of code
            {
                _wis.Terminal.text = _wis.Terminal.text + DrawLineLayer() +
                                    "<color=" + color + ">" + originalCode[line] + "</color>";
            }
            else
            {
                _wis.Terminal.text = _wis.Terminal.text + '\n' + DrawLineLayer() +
                                    "<color=" + color + ">" + originalCode[line] + "</color>";
            }
        }
        
        private string DrawLineLayer() //Draw layer tabs
        {
            string tabDrawer = "";
            
            if (_codeLayer > WinspireInteractionSystem.defaultCodeLayer)
            {
                for (sbyte i = WinspireInteractionSystem.defaultCodeLayer; i < _codeLayer; i++)
                {
                    tabDrawer += "---";
                }
            }
            return tabDrawer;
        }

        private bool IsLineConstruction(string line)
        {
            if (_wis.wisData.ifConditionRegex.IsMatch(line) 
                || _wis.wisData.whileConditionRegex.IsMatch(line))
            {
                return true;
            }
            
            return false;
        }
    }
}