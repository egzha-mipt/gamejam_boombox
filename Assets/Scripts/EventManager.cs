using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private void Awake()
    {
        // Создаём синглтон
        if (Instance == null)
        {
            Debug.Log("EventManager создан");
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public event System.Action<Vector2> OnPosteCellTriggered;

    public void PosteCellTriggered(Vector2 position)
    {
        OnPosteCellTriggered?.Invoke(position);
    }
}
