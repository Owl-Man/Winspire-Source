using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player player;

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            yield return new WaitForSeconds(0.1f);
            player.DestroyPlayer();
        }
    }
}