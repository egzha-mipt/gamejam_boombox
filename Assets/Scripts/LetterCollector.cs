using UnityEngine;
using UnityEngine.UI;

public class LetterCollector : MonoBehaviour
{
    [Header("Настройки")]
    public Image[] letterSlots; // Слоты для картинок
    public Sprite[] letterImages; // Картинки писем
    public string letterTag = "Letter"; // Тег писем
    public AudioSource catchSound; // Звук при сборе письма (опционально)

    private int lettersCollected = 0; // Количество пойманных писем

    void Start()
    {
        // Убедимся, что слоты пустые в начале
        foreach (var slot in letterSlots)
        {
            slot.sprite = null;
            slot.enabled = false; // Скрываем, если пустой
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     // Проверяем, столкнулись ли с объектом с тегом "Letter"
    //     if (other.CompareTag(letterTag))
    //     {
    //         CollectLetter();
    //     }
    // }
    
    public void CollectLetter()
    {
        if (lettersCollected < letterSlots.Length)
        {
            // Добавляем изображение в свободный слот
            letterSlots[lettersCollected].sprite = letterImages[lettersCollected];
            letterSlots[lettersCollected].enabled = true; // Показываем слот
            lettersCollected++;

            // Проигрываем звук (если есть)
            if (catchSound != null)
            {
                catchSound.Play();
            }

            // Удаляем объект письма
            // Destroy(letter);
        }
        else
        {
            Debug.Log("Все слоты заполнены!");
        }
    }
}
