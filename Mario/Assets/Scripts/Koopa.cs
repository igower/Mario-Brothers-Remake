using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellVersion;
    public float shellSpeed = 12f;

    private bool inShell;
    private bool shellMoving;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!inShell && collision.gameObject.CompareTag("Mario"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if(player.star)
            {
                Hit();
            }
            else if(collision.transform.DotTest(transform, Vector2.down))
            {
                Shell();
            }
            else
            {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(inShell && other.CompareTag("Mario"))
        {
            if(!shellMoving)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                Player player = other.GetComponent<Player>();
                if(player.star)
                {
                    Hit();
                }
                else{
                    player.Hit();
                }
            }
        }
        else if(!inShell && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void Shell()
    {

        inShell = true;
        GetComponent<EnemyMovements>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;

        GetComponent<SpriteRenderer>().sprite = shellVersion;

    }
    private void PushShell(Vector2 direction)
    {
        shellMoving = true;

        GetComponent<Rigidbody2D>().isKinematic = false;

        EnemyMovements movement = GetComponent<EnemyMovements>();
        movement.direction = direction.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.stomp);
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
        GameManagement.Instance.incScore(200);

    }

    
}
