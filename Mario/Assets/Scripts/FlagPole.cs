using System.Collections;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    public Transform flag;
    public Transform bottomPole;
    public Transform castle;
    public float speed = 6f;
    public int nextWorld = 1;
    public int nextStage = 2;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Mario"))
        {
            GameManagement.Instance.incScore(2000);
            GameManagement.Instance.ticking = false;
            StartCoroutine(MoveTo(flag, bottomPole.position));
            StartCoroutine(LevelCompleteSequence(other.transform));
        }
    }

    private IEnumerator LevelCompleteSequence(Transform player)
    {
        player.GetComponent<PlayerMovements>().enabled = false;
        yield return MoveTo(player, bottomPole.position);
        yield return MoveTo(player, player.position + Vector3.right);
        yield return MoveTo(player, player.position + Vector3.right + Vector3.down);
        yield return MoveTo(player, castle.position);

        player.gameObject.SetActive(false);
        GameManagement.Instance.incScore(GameManagement.Instance.time);

        yield return new WaitForSeconds(1.5f);

        GameManagement.Instance.LoadLevel(nextWorld, nextStage);
    }

    private IEnumerator MoveTo(Transform subject, Vector3 endPos)
    {
        while(Vector3.Distance(subject.position, endPos)>0.125f)
        {
            subject.position = Vector3.MoveTowards(subject.position, endPos, speed * Time.deltaTime);
            yield return null;
        }

        subject.position = endPos;
    }

}
