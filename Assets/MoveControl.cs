using UnityEngine;

public class MoveControl : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction; 
    private Rigidbody2D rb;
    private CollectibleManager collectibleManager;
    public Vector2 startPoint;
    //List <char> collectedLetters;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collectibleManager = FindObjectOfType<CollectibleManager>();
        CollectibleManager.OnCollectibleCollected += OnCollectibleCollected;
    }

    private void OnDestroy()
    {
        CollectibleManager.OnCollectibleCollected -= OnCollectibleCollected;
    }

    private void Update()
    {
        direction = Vector2.up;//Siempre se moverà en esa direcciòn

        // Detecta la direccion a la que gira
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 360f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 270f);
        }
    }


    private void FixedUpdate()
    {
        // Mover el jugador constantemente
        transform.Translate(direction * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Letra"))
        {
            Collectible collectible = collision.GetComponent<Collectible>();
            if (collectible != null)
            {
                collectible.Collect();
                collectibleManager.CollectibleCollected(); // Notificamos al CollectibleManager que el coleccionable ha sido recolectado
            }
        }
        if (collision.CompareTag("Respawn"))
            Die();
    }
    void Die()
    {
       
         Respawn();

    }
    void Respawn()
    {
        transform.position = startPoint;
        speed = 5f;
        
    }

    private void OnCollectibleCollected()
    {
        // Aquí puedes implementar cualquier lógica adicional que desees cuando se recolecte un objeto coleccionable
        speed += 0.5f;
    }
}
