using System.Collections.Generic;
using DataBases;
using UnityEngine;

namespace WIS
{
    //Main executor of commands & constructions
    public class Executor : MonoBehaviour
    {
        #region Compilation Data

            [HideInInspector] public List<bool> isConstructionSatisfied = new List<bool>();

            [HideInInspector] public List<bool> isConstructionLooped = new List<bool>();

            [HideInInspector] public bool isConstructionStopped;

        #endregion
        
        private WinspireInteractionSystem _wis;

        private void Start() => _wis = WinspireInteractionSystem.Instance;
        
        //Execute base commands
        public void CommandExecute(string command, byte line)
        {
            if (command == _wis.wisData.commands[0]) _wis.player.MoveToX(+1);
            else if (command == _wis.wisData.commands[1]) _wis.player.MoveToX(-1);
            else if (command == _wis.wisData.commands[2]) _wis.player.MoveToY(+1);
            else if (command == _wis.wisData.commands[3]) _wis.player.MoveToY(-1);
            
            else if (command == _wis.wisData.commands[4])
            {
                if (_wis.haveAttackWeapon) _wis.player.AttackAtPoint(0);
                else
                {
                    _wis.error.ErrorExceptionAtLine(line);
                    _wis.debugger.SendErrorMessage(TerminalMessages.COMMAND_OR_CONSTRUCTION_NOT_PURCHASED);
                }
            }
            else if (command == _wis.wisData.commands[5])
            {
                if (_wis.haveAttackWeapon) _wis.player.AttackAtPoint(1);
                else
                {
                    _wis.error.ErrorExceptionAtLine(line);
                    _wis.debugger.SendErrorMessage(TerminalMessages.COMMAND_OR_CONSTRUCTION_NOT_PURCHASED);
                }
            }
            else if (command == _wis.wisData.commands[6])
            {
                if (_wis.haveAttackWeapon) _wis.player.AttackAtPoint(2);
                else
                {
                    _wis.error.ErrorExceptionAtLine(line);
                    _wis.debugger.SendErrorMessage(TerminalMessages.COMMAND_OR_CONSTRUCTION_NOT_PURCHASED);
                }
            }
            else if (command == _wis.wisData.commands[7])
            {
                if (_wis.haveAttackWeapon) _wis.player.AttackAtPoint(3);
                else
                {
                    _wis.error.ErrorExceptionAtLine(line);
                    _wis.debugger.SendErrorMessage(TerminalMessages.COMMAND_OR_CONSTRUCTION_NOT_PURCHASED);
                }
            }
        }
        
        //Execute commands in constructions
        public void ConstructionCommandExecute(string command)
        {
            //Check is condition right for executing command
            if (isConstructionSatisfied[_wis.codeLayer] && _wis.IsPastConstructionsSatisfied())
            {
                if (isConstructionStopped) return;
                
                CommandExecute(command, 0);
            }
        }

        public void IfConstructionExecute(string condition, byte line)
        {
            ConstructionExecute(condition, line);

            isConstructionLooped.Add(false);
        }
        public void WhileConstructionExecute(string condition, byte line)
        {
            ConstructionExecute(condition, line);
            isConstructionLooped.Add(true);
        }
        
        //Reading all about construction and execute
        private void ConstructionExecute(string condition, byte line)
        {
            string[] _condition = condition.Split('&'); //Condition blocks

            List<bool> isConditionRight = new List<bool>(); //List of booleans for every condition blocks

            for (byte i = 0; i < _condition.Length; i++) //Check every condition block
            {
                isConditionRight.Add(ConditionBlockExecute(_condition[i], line));
            }
            
            //Writing final result in list
            isConstructionSatisfied.Add(CheckIsConstructionSatisfies(isConditionRight));
        }

        private bool CheckIsConstructionSatisfies(IReadOnlyList<bool> isConditionRight)
        {
            for (byte i = 0; i < isConditionRight.Count; i++)
            {
                if (!isConditionRight[i]) return false;
            }

            return true;
        }

        //Breaks the condition block into small parts and checks
        private bool ConditionBlockExecute(string condition, byte line)
        {
            if (condition == "robot is on") return true;
            
            string[] _condition = condition.Split(' ');

            if (IsCommandInConditionExist(_condition[0]))
            {
                switch (_condition[1])
                {
                    case "obstacle":
                    {
                        if (IsWallCommandSatisfiesCondition(_condition[0])) return true;
                        break;
                    }
                    case "no" when _condition[2] == "obstacle":
                    {
                        if (!IsWallCommandSatisfiesCondition(_condition[0])) return true;
                        break;
                    }
                    case "enemy":
                    {
                        if (IsEnemyCommandSatisfiesCondition(_condition[0])) return true;
                        break;
                    }
                    case "no" when _condition[2] == "enemy":
                    {
                        if (!IsEnemyCommandSatisfiesCondition(_condition[0])) return true;
                        break;
                    }
                    default: //Condition object not exist
                    {
                        _wis.error.ErrorExceptionAtLine(line);
                        _wis.debugger.SendErrorMessage(TerminalMessages.CONDITION_OBJECT_NOT_EXIST((byte)(line + 1), _condition[1]));
                        break;
                    }
                }
            }
            else //Condition not correct
            {
                _wis.error.ErrorExceptionAtLine(line);
                _wis.debugger.SendErrorMessage(TerminalMessages.CONDITION_NOT_CORRECT((byte)(line + 1)));
            }

            return false;
        }
        
        //Check module conditions with wall object
        private bool IsWallCommandSatisfiesCondition(string command)
        {
            for (byte i = 0; i < _wis.wisData.conditionCommands.Length; i++)
            {
                if (command == _wis.wisData.conditionCommands[i] && _wis.player.IsPointInWall(i)) return true;
            }

            return false;
        }
        
        //Check module conditions with Enemy object
        private bool IsEnemyCommandSatisfiesCondition(string command)
        {
            for (byte i = 0; i < _wis.wisData.conditionCommands.Length; i++)
            {
                if (command == _wis.wisData.conditionCommands[i] && _wis.player.IsPointInEnemy(i)) return true;
            }

            return false;
        }
        
        //Check module is condition structure existing
        private bool IsCommandInConditionExist(string command)
        {
            for (byte i = 0; i < _wis.wisData.conditionCommands.Length; i++)
            {
                if (command == _wis.wisData.conditionCommands[i]) return true;
            }

            return false;
        }
        
        public void ReturnAllExecutes()
        {
            _wis.player.SetPlayerDefaultPosition();
            _wis.player.SetPlayerIdleAnim();
            _wis.player.isPlayerDestroyed = false;
            _wis.player.ReturnAllEnemies();

            for (byte i = 0; i < isConstructionSatisfied.Count; i++)
            {
                isConstructionSatisfied.RemoveAt(i);
                isConstructionLooped.RemoveAt(i);
            }
        }

        public void ConstructionEnd()
        {
            isConstructionSatisfied.RemoveAt(_wis.codeLayer);
            isConstructionLooped.RemoveAt(_wis.codeLayer);
            isConstructionStopped = false;
        }
    }
}
