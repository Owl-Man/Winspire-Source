using UnityEngine;
using WIS;

public class ExitScript : MonoBehaviour
{
    [SerializeField] private GameObject CompletePanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            WinspireInteractionSystem.Instance.SetTaskCompleteState(true);
            WinspireInteractionSystem.Instance.StopCompilation();
            CompletePanel.SetActive(true);
        }
    }
}
