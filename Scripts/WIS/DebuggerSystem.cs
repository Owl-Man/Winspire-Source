using DataBases;
using TMPro;
using UnityEngine;

namespace WIS
{
    public class DebuggerSystem : MonoBehaviour
    {
        [SerializeField] private TMP_Text Debugger;

        private const string BlueColor = "#6E96D1";
        private const string RedColor = "#FF5171";

        private string _pastMessage = "";
        private bool _isPastMessageError;

        public void SendMessage(string message)
        {
            if (_pastMessage != "")
            {
                if (_isPastMessageError)
                {
                    Debugger.text = string.Format("<color={0}>{1}</color>: \n<color={2}>{3}</color>\n\n<color={0}>{4}</color>: {5}",
                        BlueColor, TerminalMessages.PAST_MESSAGE, RedColor, _pastMessage, TerminalMessages.NEW_MESSAGE, message);
                }
                else
                {
                    Debugger.text = string.Format("<color={0}>{1}</color>: \n<color=black>{2}</color>\n\n<color={0}>{3}</color>: {4}",
                        BlueColor, TerminalMessages.PAST_MESSAGE, _pastMessage, TerminalMessages.NEW_MESSAGE, message);
                }
            }
            else
            {
                Debugger.text = "<color=" + BlueColor +"> " + TerminalMessages.NEW_MESSAGE + " : </color>" + '\n' + message;
            }

            _pastMessage = message;
            _isPastMessageError = false;
            Debugger.color = new Color(0.22f, 0.22f, 0.22f);
        }

        public void SendErrorMessage(string message)
        {
            if (_pastMessage != "")
            {
                if (_isPastMessageError)
                {
                    Debugger.text = "<color=" + BlueColor + "> " + TerminalMessages.PAST_MESSAGE +"</color>: " + '\n' +
                                    "<color=" + RedColor + ">" + _pastMessage + "</color>" + '\n' + '\n' +
                                    "<color=" + BlueColor + "> " + TerminalMessages.NEW_MESSAGE + "</color>:" + '\n' +
                                    "<color=" + RedColor + ">" + message + "</color>";
                }
                else
                {
                    Debugger.text = "<color=" + BlueColor + ">" + TerminalMessages.PAST_MESSAGE + "</color>: " + '\n' +
                                    "<color=black>" + _pastMessage + "</color>" + '\n' + '\n' +
                                    "<color=" + BlueColor + ">" + TerminalMessages.NEW_MESSAGE + "</color>:" + '\n' +
                                    "<color=" + RedColor + ">" + message + "</color>";
                }
            }
            else
            {
                Debugger.text = "<color=" + BlueColor +">" + TerminalMessages.NEW_MESSAGE + "</color>:" + '\n' +
                                "<color=" + RedColor + ">" + message + "</color>";
            }

            _pastMessage = message;
            _isPastMessageError = true;
        }
    }
}