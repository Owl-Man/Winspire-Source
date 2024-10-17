//<-----------------------Database of messages for debugger terminal---------------->

namespace DataBases
{
    public static class TerminalMessages
    {
        public static string CONDITION_OBJECT_NOT_EXIST(byte line, string _object) => string.Format(LocalizationManager.Instance.GetLocalizedValue("CONDITION_OBJECT_NOT_EXIST"), _object, line);

        public static string CONDITION_NOT_CORRECT(byte line) => string.Format(LocalizationManager.Instance.GetLocalizedValue("CONDITION_NOT_CORRECT"), line);

        public static string CONSTRUCTION_END_NOT_FINDED(byte line) => string.Format(LocalizationManager.Instance.GetLocalizedValue("CONSTRUCTION_END_NOT_FINDED"), line);

        public static string CONSTRUCTION_LINK_DOES_NOT_EXIST(byte line) => string.Format(LocalizationManager.Instance.GetLocalizedValue("CONSTRUCTION_LINK_DOES_NOT_EXIST"), line);

        public static string COMMAND_DOES_NOT_EXIST(byte line, string _object) => string.Format(LocalizationManager.Instance.GetLocalizedValue("COMMAND_DOES_NOT_EXIST"), _object, line);

        public static string ROBOT_BROKE = LocalizationManager.Instance.GetLocalizedValue("ROBOT_BROKE");

        public static string CODE_COMPLETE_BUT_TARGET_NOT_FINISH = LocalizationManager.Instance.GetLocalizedValue("CODE_COMPLETE_BUT_TARGET_NOT_FINISH");

        public static string CODE_AND_TARGET_COMPLETE = LocalizationManager.Instance.GetLocalizedValue("CODE_AND_TARGET_COMPLETE");

        public static string COMMAND_OR_CONSTRUCTION_NOT_PURCHASED = LocalizationManager.Instance.GetLocalizedValue("COMMAND_OR_CONSTRUCTION_NOT_PURCHASED");

        public static string PAST_MESSAGE = LocalizationManager.Instance.GetLocalizedValue("PAST_MESSAGE");
        public static string NEW_MESSAGE = LocalizationManager.Instance.GetLocalizedValue("NEW_MESSAGE");
    }
}
