using System.Collections;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{
    void Start()
    {
        GameManagement.Instance.AddCoin();
        StartCoroutine(Animate());

    }
    private IEnumerator Animate()
    {

        Vector3 startPos = transform.localPosition;
        Vector3 upPos = startPos + Vector3.up * 2f;

        yield return Move(startPos, upPos);
        yield return Move(upPos, startPos);
        Destroy(gameObject);
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {  
        float elapsed = 0f;
        float duration = .25f;

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

