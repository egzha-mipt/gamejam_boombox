using UnityEngine;

public class Father : MonoBehaviour
{
    public float speed = 5f; 
    public float verticalSpeed = 1f;

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        transform.Translate(new Vector3(moveInput * speed * Time.deltaTime, 0, 0));
        transform.Translate(new Vector3(0, verticalSpeed * Time.deltaTime, 0));

        //grid limit for father
        float clampedX = Mathf.Clamp(transform.position.x, -0.5f, 8+2.2f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("KillCell works");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Father is dead!");
            KillPlayer(other.gameObject);
        } else{
            Debug.Log("Father is alive (Why?");
        }
    }

    void KillPlayer(GameObject player)
    {
        player.SetActive(false);
    }



}
