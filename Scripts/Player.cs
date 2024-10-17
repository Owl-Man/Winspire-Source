using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject player, Points;

    [SerializeField] private GameObject[] Enemy;

    [SerializeField] private Point[] points;

    [SerializeField] private Animator anim;

    [SerializeField] private Vector3 DefaultPlayerPosition, DefaultPointsPosition;

    [SerializeField] private float PlayerTranslateGridSize = 0.5f;
    [SerializeField] private string DieAnimationKey;

    [SerializeField] private float DieTime = 0.2f;

    [HideInInspector] public bool isPlayerDestroyed;

    public bool IsPointInWall(byte point) => points[point].isWallContact;

    public bool IsPointInEnemy(byte point) => points[point].isEnemyNear;

    public void AttackAtPoint(byte id)
    {
        if (IsPointAccess(id) == false || isPlayerDestroyed) return;

        if (points[id].enemyObject != null) points[id].enemyObject.SetActive(false);
    }

    public void MoveToX(sbyte direction)
    {
        if (IsDirectionAccess(direction) == false || isPlayerDestroyed) return;

        player.transform.Translate(PlayerTranslateGridSize * direction, 0, 0);

        Points.transform.Translate(PlayerTranslateGridSize * direction, 0, 0);
    }

    public void MoveToY(sbyte direction)
    {
        if (IsDirectionAccess(direction) == false || isPlayerDestroyed) return;

        player.transform.Translate(0, PlayerTranslateGridSize * direction, 0);

        Points.transform.Translate(0, PlayerTranslateGridSize * direction, 0);
    }

    private bool IsDirectionAccess(sbyte direction)
    {
        if (direction != 1 && direction != -1)
        {
            Debug.Log("Direction is not access (input variable can be only 1 or -1)");
            return false;
        }
        
        return true;
    }
    
    private bool IsPointAccess(byte pointId)
    {
        if (pointId == 0 || pointId == 1 || pointId == 2 || pointId == 3) return true;
        
        return false;
    }

    public void SetPlayerDefaultPosition() 
    {
        player.transform.localPosition = DefaultPlayerPosition;
        Points.transform.localPosition = DefaultPointsPosition;
    }

    public void SetPlayerIdleAnim() => anim.Play("Idle");

    public void ReturnAllEnemies()
    {
        for (byte i = 0; i < Enemy.Length; i++)
        {
            Enemy[i].SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Obstacle")) StartCoroutine(Destroying());
    }

    public void DestroyPlayer() => StartCoroutine(Destroying());

    private IEnumerator Destroying()
    {
        if (!isPlayerDestroyed)
        {
            isPlayerDestroyed = true;

            if (string.IsNullOrWhiteSpace(DieAnimationKey) == false) anim.Play(DieAnimationKey);

            yield return new WaitForSeconds(DieTime);
        }
    }
}
