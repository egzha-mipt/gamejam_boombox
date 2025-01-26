using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Father : MonoBehaviour
{
    [SerializeField]private Animator animator;
    public float speed = 5f; 
    public float moveInput;
    // private float verticalSpeed = 0f;
    private float currentTime = 0;
    public int Age=0;
    // public float rollMult = -45;
    // public float pitchMult = 30;

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        currentTime +=Time.deltaTime;
        transform.Translate(new Vector3(moveInput * speed * Time.deltaTime, 0, 0));
        // transform.Translate(new Vector3(0, verticalSpeed * Time.deltaTime, 0));
        
        //grid limit for father
        float clampedX = Mathf.Clamp(transform.position.x, -0.5f, 8+2.2f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        if (moveInput<0)
        {
            SetDirection(0);
        }else
        if (moveInput==0)
        {
            SetDirection(1);
        }else
        if (moveInput>0)
        {
            SetDirection(2);
        }
        if (Age != 2){
        if (currentTime>= 30)
        {
            currentTime = 0;
            animator.SetInteger("Age", Age);
            Age++;
        }
        }
        // transform.rotation = Quaternion.Euler(moveInput*pitchMult, -1*moveInput*rollMult,0);
        // OnCollisionEnter2D();
    }
    
    public void Die()
    {
        Debug.Log("Отец погиб!");
        // UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //Перенес логику OnCollisionEnter2D в отдельный скрипт 

    /*void OnCollisionEnter2D (Collision2D collision)
    {
        GameObject collidedWith = collision.gameObject;
        if (collidedWith.tag == "Letter"){
            Debug.Log("Отец поймал письмо!");
            Destroy(collidedWith);
        }
    }*/
        public void SetDirection(int direction)
    {
        animator.SetInteger("Direction", direction);
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
