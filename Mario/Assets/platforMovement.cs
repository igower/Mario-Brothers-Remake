using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platforMovement : MonoBehaviour
{
    private float speed = 1f;
    private float startPos;
    private float endPos;
    public int move;

    public void Awake()
    {
        startPos = transform.position.y - move;
        endPos = transform.position.y + move;
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
