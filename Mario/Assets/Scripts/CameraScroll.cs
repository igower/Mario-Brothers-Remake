using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    private Transform marioPos;

    public float height = 6f;
    public float undergroundHeight = -10f;

    private void Awake()
    {
        marioPos = GameObject.FindWithTag("Mario").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, marioPos.position.x);
        transform.position = cameraPosition;
    }

    public void SetUnderground(bool underground)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergroundHeight : height;
        transform.position = cameraPosition;
    }
}
