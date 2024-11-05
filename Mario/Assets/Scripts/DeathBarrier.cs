using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Mario"))
        {
            other.gameObject.SetActive(false);
            GameManagement.Instance.ResetLevel(3f);
        }
        else{
            Destroy(other.gameObject);
        }
    }
}
