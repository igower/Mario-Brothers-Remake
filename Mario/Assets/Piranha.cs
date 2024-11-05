using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Piranha : MonoBehaviour
{
    private float speed = 1f;
    private float startPos;
    private float endPos;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Mario"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.Hit();
        }
    }

    public void Awake()
    {
        startPos = transform.position.y;
        endPos = transform.position.y -4;
    }
    public void FixedUpdate()
    {
        float y = transform.position.y;
        y = Mathf.MoveTowards(y, endPos, speed * Time.deltaTime);
        if (Mathf.Abs(y-endPos)<0.2)
        {
            float temp = endPos;
            endPos = startPos;
            startPos = temp;
        }
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }


}