using System;
using System.Runtime.Serialization;
using UnityEngine;

public class Father : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource lose;
    public float speed = 5f; 
    public int Age;

    public int totalCatchedLetters = 0;
    private float agingInterval = 30f;
    private float timer;
    public int lettersToWin = 6; // Количество писем для победы
    [SerializeField]private Animator animator;


    // private float verticalSpeed = 0f;

    // public float rollMult = -45;
    // public float pitchMult = 30;

    public GameObject winScreen; // Экран победы (префаб или UI Canvas)
    public GameObject loseScreen; // Экран поражения (префаб или UI Canvas)
    public LetterCollector letterCollector;

    void Start()
    {   
        timer=agingInterval;
        //экраны в изначально положение (неактивные)
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        letterCollector = GetComponent<LetterCollector>();
    }
    
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
            timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            AgeUp(); // Увеличиваем возраст
            timer = agingInterval; // Сбрасываем таймер
        }
        // transform.rotation = Quaternion.Euler(moveInput*pitchMult, -1*moveInput*rollMult,0);
        // OnCollisionEnter2D();
    }
    public void SetDirection(int direction)
    {
        animator.SetInteger("Direction", direction);
    }
      private void AgeUp()
    {
        // Увеличиваем возраст, но не превышаем максимальный возраст (2)
        if (Age < 2)
        {
            Age++;
            animator.SetInteger("AGE",Age); // Устанавливаем параметр в Animator
            Debug.Log("Возраст увеличен: " + Age);
        }
        else
        {
            Debug.Log("Игрок уже достиг максимального возраста.");
        }
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
            letterCollector.CollectLetter();
            Destroy(collidedWith);
        }
        if (totalCatchedLetters >= lettersToWin)
        {
            Win();
        }
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Letter"){
            Debug.Log("Отец поймал письмо!");
            totalCatchedLetters++;
            Destroy(collision);
        }
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
        audioSource.mute = true;
        lose.Play();

        // Активируем экран поражения
        if (loseScreen != null)
        {
            loseScreen.SetActive(true);
            Time.timeScale = 0f; // Останавливаем игру
        }
    }

}
