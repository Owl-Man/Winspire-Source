using UnityEngine;
using MonoCacheSystem;

public class EnemyControllerAI : MonoCache
{
    [Header("Values")]
    
    [SerializeField] private bool SpriteFlipBool;

    [SerializeField] private bool isYPatrol;
    
    [SerializeField] private float speed, patrolDistance;

    [SerializeField] private Transform point;
    
    bool chill, angry, goback, movingRight;

    [Header("Components")]
    
    [SerializeField] private SpriteRenderer sprite;

    private void Start() => Physics2D.queriesStartInColliders = false;

    public override void OnTick() => Movement();

    private void Movement()
    {
        if (isYPatrol)
        {
            if (transform.position.y > point.position.y + patrolDistance)
            {
                movingRight = false;
            }
            else if (transform.position.y < point.position.y - patrolDistance)
            {
                movingRight = true;
            }
            if (movingRight)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y  + speed * Time.deltaTime);
                sprite.flipX = SpriteFlipBool;
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y  - speed * Time.deltaTime);
                sprite.flipX = !SpriteFlipBool;
            }
        }
        else
        {
            if (transform.position.x > point.position.x + patrolDistance)
            {
                movingRight = false;
            }
            else if (transform.position.x < point.position.x - patrolDistance)
            {
                movingRight = true;
            }

            if (movingRight)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                sprite.flipX = SpriteFlipBool;
            }
            else
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
                sprite.flipX = !SpriteFlipBool;
            }
        }
    }
}
