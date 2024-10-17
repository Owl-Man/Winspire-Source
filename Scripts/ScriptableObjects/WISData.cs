using System.Text.RegularExpressions;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New WIS Data Object", menuName = "WIS Data")]
    public class WISData : ScriptableObject
    {
        public new string name;

        public string[] commands;
        public string[] conditionCommands;
        
        #region Construction Regexs

            public Regex ifConditionRegex = new Regex("if ()");
            public Regex whileConditionRegex = new Regex("while ()");

        #endregion
        
        //<sc> prefix is needed to denote about special command
        #region Special Commands

            public readonly string sc_stopProgramm = "прервать(программу)";
            public readonly string sc_stopConstruction = "прервать(конструкцию)";

        #endregion
    }
}