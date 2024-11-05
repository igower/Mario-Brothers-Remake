using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatVersion;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Mario"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if(player.star)
            {
                Hit();
            }
            else if(collision.transform.DotTest(transform, Vector2.down))
            {
                Flat();
            }
            else
            {
                player.Hit();
            }
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void Flat()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyMovements>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;

        AudioManager.Instance.PlaySFX(AudioManager.Instance.stomp);
        GetComponent<SpriteRenderer>().sprite = flatVersion;
        Destroy(gameObject, 0.5f);
        GameManagement.Instance.incScore(200);

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
