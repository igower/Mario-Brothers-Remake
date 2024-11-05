using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BlockHit : MonoBehaviour
{

    public GameObject item;
    public int maxHits = -1;
    public Sprite emptyBlock;

    private bool animating;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!animating && collision.gameObject.CompareTag("Mario")&& maxHits!=0)
        {
            if(collision.transform.DotTest(transform, Vector2.up))
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        maxHits--;

        if(maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }

        if(item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
    

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        animating = true;

        Vector3 startPos = transform.localPosition;
        Vector3 upPos = startPos + Vector3.up * 0.5f;

        yield return Move(startPos, upPos);
        yield return Move(upPos, startPos);

        animating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {  
        float elapsed = 0f;
        float duration = .125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }
}
