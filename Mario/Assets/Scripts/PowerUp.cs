using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type
    {
        Coin,
        ExtraLife,
        Mushroom,
        Star,
    }

    public Type type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Mario"))
        {
            Collect(other.gameObject);
        } 
    }

    private void Collect(GameObject player)
    {
        switch(type)
        {
            case Type.Coin:
                GameManagement.Instance.AddCoin();
                break;
            case Type.ExtraLife:
                GameManagement.Instance.AddLife();
                GameManagement.Instance.incScore(1000);
                break;
            case Type.Mushroom:
                player.GetComponent<Player>().Grow();
                GameManagement.Instance.incScore(500);
                break;
            case Type.Star:
                player.GetComponent<Player>().StarPower();
                GameManagement.Instance.incScore(500);
                break;
        }

        Destroy(gameObject);
    }
}
