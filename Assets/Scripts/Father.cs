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
        transform.Translate(new Vector3(0, verticalSpeed * Time.deltaTime, 0));

        //grid limit for father
        float clampedX = Mathf.Clamp(transform.position.x, -0.5f, 8+2.2f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // transform.rotation = Quaternion.Euler(moveInput*pitchMult, -1*moveInput*rollMult,0);
        // OnCollisionEnter2D();
    }
    
    public void Die()
    {
        Debug.Log("Игрок погиб!");
        // UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnCollisionEnter2D (BoxCollider2D collision)
    {
        GameObject collidedWith = collision.gameObject;
        Debug.Log("OnCollisionEnter2D works");
        Debug.Log(collidedWith.tag);
        if (collidedWith.tag == "KillCell"){
            Debug.Log("OnCollisionEnter2D works");
            Destroy(collidedWith);
        }
    }

}
