using System.Collections;
using System.Collections.Generic;
using AndroidIntegrationTools;
using DataBases;
using ScriptableObjects;
using UnityEngine.UI;
using UnityEngine;

//All classes in namespace WIS arе parts of Winspire Interaction System
namespace WIS
{
    //Main Compilation Controller
    public class WinspireInteractionSystem : MonoBehaviour
    {
        #region Components
        
            [SerializeField] private Executor executor;

            public WISData wisData;
        
            public ErrorSystem error;
            public Player player; 
            public DebuggerSystem debugger;

        #endregion

        #region States

            private bool _isProgramRunning, _isTaskCompleted, _isLineConstruction;

            private bool _haveWhileConstruction;
            [HideInInspector] public bool haveAttackWeapon;

        #endregion
        
        public InputField Terminal;

        [HideInInspector] public sbyte codeLayer = -1;
        public const sbyte defaultCodeLayer = -1;
        
        public static WinspireInteractionSystem Instance;

        private void Awake() => Instance = this;

        private IEnumerator Start()
        {
            yield return null;
            
            _haveWhileConstruction = PlayerPrefs.GetInt(PPKeys.IS_WHILE_BOOST_ACTIVATED_KEY) == 1;
            
            haveAttackWeapon = PlayerPrefs.GetInt(PPKeys.IS_ELECTROGUN_BOOST_ACTIVATED_KEY) == 1;
        }

        public void SetTaskCompleteState(bool state) => _isTaskCompleted = state;

        //Call main compiling process
        public void Compile(string[] code, float timeToShowCompilation)
        {
            if (_isProgramRunning) return;
            
            StopCompilation(); //Stop past compilation
            
            _isProgramRunning = true;

            StartCoroutine(Compiling(code, timeToShowCompilation));
        }

        //Stop main compiling process
        public void StopCompilation()
        {
            executor.ReturnAllExecutes();
            error.ClearAllErrorMarks();

            _isProgramRunning = false;

            _isLineConstruction = false;

            codeLayer = defaultCodeLayer;
        }

        //Main Compiling Process
        private IEnumerator Compiling(IReadOnlyList<string> code, float timeToShowCompilation)
        {
            List<byte> constructionLines = new List<byte>();

            float _timeToShowCompilation;

            for (byte i = 0; i < code.Count; i++) //Code reading
            {
                //Check has end construction in the last line in construction
                if (codeLayer > defaultCodeLayer && i + 1 == code.Count && code[i] != "end")
                {
                    error.ErrorExceptionAtLine(i);
                    debugger.SendErrorMessage(TerminalMessages.CONSTRUCTION_END_NOT_FINDED((byte)(i + 1)));
                    break;
                }

                //Main line executing
                if (!string.IsNullOrWhiteSpace(code[i]) && _isProgramRunning && !player.isPlayerDestroyed)
                {
                    _timeToShowCompilation = timeToShowCompilation;

                    ConstructionAnalysis(code[i], i); //Check availability construction

                    if (_isLineConstruction)
                    {
                        if (executor.isConstructionLooped[codeLayer]) constructionLines.Add(i);
                        
                        _timeToShowCompilation = 0;
                    }
                    else if (HasCommandIn(code[i])) //Check availability familiar command
                    {
                        if (codeLayer > defaultCodeLayer) //Command in construction
                        {
                            if (executor.isConstructionSatisfied[codeLayer])
                                debugger.SendMessage("Executing command: " + code[i]);
                            
                            yield return new WaitForSeconds(0.001f);
                            executor.ConstructionCommandExecute(code[i]);
                        }
                        else //Default command
                        {
                            debugger.SendMessage("Executing command: " + code[i]);
                            executor.CommandExecute(code[i], i);
                        }
                    }
                    else if (code[i] == "end")
                    {
                        if (executor.isConstructionLooped[codeLayer])
                        {
                            if (executor.isConstructionSatisfied[codeLayer] && IsPastConstructionsSatisfied())
                            {
                                i = (byte)(constructionLines[codeLayer] - 1);
                            }

                            constructionLines.RemoveAt(codeLayer);
                        }
                        
                        executor.ConstructionEnd();
                        codeLayer--;
                        
                        _timeToShowCompilation = 0;
                    }
                    else if (executor.isConstructionStopped == false) //Command does not exist
                    {
                        error.ErrorExceptionAtLine(i);

                        debugger.SendErrorMessage(TerminalMessages.COMMAND_DOES_NOT_EXIST((byte)(i + 1), code[i]));

                        break;
                    }

                    yield return new WaitForSeconds(_timeToShowCompilation);
                }
            }
            
            //Robot has broken
            if (player.isPlayerDestroyed)
            {
                debugger.SendErrorMessage(TerminalMessages.ROBOT_BROKE);
            }

            //Compiling finished successfully
            if (error.hasErrorInCode == false && _isProgramRunning && !player.isPlayerDestroyed)
            {
                debugger.SendMessage(!_isTaskCompleted
                    ? TerminalMessages.CODE_COMPLETE_BUT_TARGET_NOT_FINISH
                    : TerminalMessages.CODE_AND_TARGET_COMPLETE);
            }

            _isProgramRunning = false;
        }

        private void ConstructionAnalysis(string line, byte idLine)
        {
            if (wisData.ifConditionRegex.IsMatch(line))
            {
                string[] lineCommands = line.Split('(', ')');

                ConstructionFound("if", lineCommands[1]);

                executor.IfConstructionExecute(lineCommands[1], idLine);
            }
            else if (wisData.whileConditionRegex.IsMatch(line))
            {
                if (_haveWhileConstruction)
                {
                    string[] lineCommands = line.Split('(', ')');

                    ConstructionFound("while", lineCommands[1]);

                    executor.WhileConstructionExecute(lineCommands[1], idLine);
                }
                else
                {
                    debugger.SendErrorMessage(TerminalMessages.COMMAND_OR_CONSTRUCTION_NOT_PURCHASED);
                    error.ErrorExceptionAtLine(idLine);
                }
            }
            else _isLineConstruction = false;
        }

        private void ConstructionFound(string construction, string condition)
        {
            _isLineConstruction = true;
            codeLayer++;
            
            debugger.SendMessage("Found the keyword: " + construction + ". Condition: " + condition +
                                 ". Construction level: " + codeLayer);
        }

        public bool HasCommandIn(string line)
        {
            for (byte i = 0; i < wisData.commands.Length; i++)
            {
                if (wisData.commands[i] == line)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsPastConstructionsSatisfied()
        {
            for (sbyte i = 0; i < codeLayer; i++)
            {
                if (!executor.isConstructionSatisfied[i]) return false;
            }

            return true;
        }

        public void GenerateCommandAtNewLine(string command)
        {
            CodeDrawer.Instance.OnStartEdit();
            
            string[] textSplitted = Terminal.text.Split('\n');
            
            if (textSplitted[0] != "") Terminal.text = Terminal.text + '\n' + command;
            else Terminal.text = command;
            
            CodeDrawer.Instance.OnEndEdit();
        }

        public void CleanTerminal()
        {
            Terminal.text = string.Empty;
            ToastSystem.Send("Terminal cleared");
        }
    }
}