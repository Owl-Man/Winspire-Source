using UnityEngine;

namespace WIS
{
    public class ErrorSystem : MonoBehaviour
    {
        [SerializeField] private GameObject[] LineErrorMarks;

        [HideInInspector] public bool hasErrorInCode;

        public void ErrorExceptionAtLine(byte lineId)
        {
            LineErrorMarks[lineId].SetActive(true);
            hasErrorInCode = true;
        }

        public void ClearAllErrorMarks()
        {
            for (byte i = 0; i < LineErrorMarks.Length; i++)
            {
                LineErrorMarks[i].SetActive(false);
            }

            hasErrorInCode = false;
        }
    }
}
