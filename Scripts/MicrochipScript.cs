using UnityEngine;

public class MicrochipScript : MonoBehaviour
{
    [SerializeField] private CompleteLevelScript _completeLevelScript;

    private bool _isPickedUp;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_isPickedUp) return;
            _isPickedUp = true;
            
            _completeLevelScript.Reward++;
            Destroy(gameObject);
        }
    }
}