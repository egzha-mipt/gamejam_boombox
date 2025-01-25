using UnityEngine;

public class KillCell : MonoBehaviour
{
    void Start()
    {
        Debug.Log("kill sell is ready");
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Father is dead!");
            KillPlayer(other.gameObject);
        } else{
            Debug.Log("Father is alive!");
        }
    }

    void KillPlayer(GameObject player)
    {
        player.SetActive(false);
    }
}
