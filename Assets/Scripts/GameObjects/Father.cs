using System.Runtime.Serialization;
using UnityEngine;

public class Father : MonoBehaviour
{
    public float speed = 5f; 
    public int totalCatchedLetters = 0;
    public int lettersToWin = 6; // Количество писем для победы


    // private float verticalSpeed = 0f;

    // public float rollMult = -45;
    // public float pitchMult = 30;

    public GameObject winScreen; // Экран победы (префаб или UI Canvas)
    public GameObject loseScreen; // Экран поражения (префаб или UI Canvas)

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
        ShowLoseScreen();
        // UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Win()
    {
        Debug.Log("Отец выиграл! Купил хлеб!");
        ShowWinScreen(); // Показываем экран победы
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        GameObject collidedWith = collision.gameObject;
        if (collidedWith.tag == "Letter"){
            Debug.Log("Отец поймал письмо!");
            totalCatchedLetters++;
            Destroy(collidedWith);
        }
        if (totalCatchedLetters >= lettersToWin)
        {
            Win();
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

    void ShowWinScreen()
    {
        Debug.Log("Экран выигрыша сработал");
        // Активируем экран победы
        if (winScreen != null)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0f; // Останавливаем игру
        }
    }

    void ShowLoseScreen()
    {
        Debug.Log("Экран проигрыша сработал");
        // Активируем экран поражения
        if (loseScreen != null)
        {
            loseScreen.SetActive(true);
            Time.timeScale = 0f; // Останавливаем игру
        }
    }

}
