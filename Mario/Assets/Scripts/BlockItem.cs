using System.Collections;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Animate());
        AudioManager.Instance.PlaySFX(AudioManager.Instance.itemShow);
    }
    
    private IEnumerator Animate()
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        CircleCollider2D physicsCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        rigidBody.isKinematic = true;
        physicsCollider.enabled = false;
        triggerCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(.25f);

        spriteRenderer.enabled = true;

        float elapsed = 0f;
        float duration = 0.5f;


        Vector3 startPos = transform.localPosition;
        Vector3 endPos =  transform.localPosition + Vector3.up;

        while(elapsed<duration)
        {
            float t = elapsed/duration;

            transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = endPos;


        rigidBody.isKinematic = false;
        physicsCollider.enabled = true;
        triggerCollider.enabled = true;
        spriteRenderer.enabled = true;
    }

}
