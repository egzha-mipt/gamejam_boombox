using System.Runtime.Serialization;
using UnityEngine;

public class Father : MonoBehaviour
{
    public float speed = 5f; 
    private float verticalSpeed = 0f;

    // public float rollMult = -45;
    // public float pitchMult = 30;

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        transform.Translate(new Vector3(moveInput * speed * Time.deltaTime, 0, 0));
        // transform.Translate(new Vector3(0, verticalSpeed * Time.deltaTime, 0));

        //grid limit for father
        float clampedX = Mathf.Clamp(transform.position.x, -0.5f, 8+2.2f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // transform.rotation = Quaternion.Euler(moveInput*pitchMult, -1*moveInput*rollMult,0);
        // OnCollisionEnter2D();
    }
    
    public void Die()
    {
        Debug.Log("Отец погиб!");
        // UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        GameObject collidedWith = collision.gameObject;
        if (collidedWith.tag == "Letter"){
            Debug.Log("Отец поймал письмо!");
            Destroy(collidedWith);
        }
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        // GameObject collidedWith = collision.gameObject;
        GameObject collidedCell = collision.gameObject;
        if (collidedCell.tag == "KillCell")
        {
            Die();
        }

        if (collision.CompareTag("PosteCell"))
        {
            Debug.Log("Отец пришел на почту!");

            EventManager.Instance.PosteCellTriggered(collision.transform.position);
        }
        
    }

}
