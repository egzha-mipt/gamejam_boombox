using UnityEngine;

public class Letter : MonoBehaviour
{
    public float speed = 3f;
    public float lifetime = 5f;

    private Rigidbody2D rb;
    private Vector2 direction = Vector2.down; // вниз

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (rb == null)
        {
            Debug.LogError("На объекте 'Letter' отсутствует Rigidbody2D!");
        }
    }

    void Start()
    {
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }

        Destroy(gameObject, lifetime);
    }

    void Update(){
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
            transform.Rotate(0, 0, 360 * Time.deltaTime * 0.75f); //крутилка
        }
    }

    public void SetDirection(Vector2 newDirection)
    {
        if (newDirection != Vector2.zero)
        {
            direction = newDirection.normalized; 
        }
    }
}
