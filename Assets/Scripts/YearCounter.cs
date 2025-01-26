using UnityEngine;
using TMPro; // Для использования TextMeshPro

public class YearCounter : MonoBehaviour
{
    public TextMeshProUGUI yearText; // Ссылка на текстовый объект TextMeshPro для отображения количества лет

    private float elapsedTime = 0f; // Прошедшее время
    private int yearsPassed = 0; // Сколько лет прошло
    private const float secondsPerYear = 4.5f; // Секунд в одном году

    void Update()
    {
        // Увеличиваем прошедшее время на время текущего кадра
        elapsedTime += Time.deltaTime;

        // Проверяем, прошло ли ещё 5 секунд (один "год")
        if (elapsedTime >= secondsPerYear)
        {
            // Увеличиваем количество лет
            yearsPassed++;

            // Сбрасываем прошедшее время
            elapsedTime -= secondsPerYear;

            // Обновляем текст на UI
            if (yearText != null)
            {
                yearText.text = $" {yearsPassed} \n year";
            }
        }
    }
}