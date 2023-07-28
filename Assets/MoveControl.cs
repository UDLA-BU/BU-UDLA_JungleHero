using System;
using UnityEngine;
using UnityEngine.UI;

public class MoveControl: MonoBehaviour
{
    public float speed = 0f;
    private Vector2 direction; 
    private Rigidbody2D rb;
    private CollectibleManager collectibleManager;
    public Vector2 startPoint;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private Animator anim;

    private LevelManager levelManager;

    private void Awake()
    {
        anim= GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collectibleManager = FindObjectOfType<CollectibleManager>();
        CollectibleManager.OnCollectibleCollected += OnCollectibleCollected;
        CollectibleManager.OnWordCollected += OnWordCollected;

        // Obtener referencia al LevelManager
        levelManager = FindObjectOfType<LevelManager>();
    }


    private void Start()
    {
        speed = 2;
        anim.SetFloat("speed",0);
    }
    private void OnDestroy()
    {
        CollectibleManager.OnCollectibleCollected -= OnCollectibleCollected;
    }



    private void Update()
    {

        direction = Vector2.up;//Siempre se mover� en esa direcci�n
       
        // Detecta la direccion a la que gira haciendo swipe
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            //speed = 2;
            anim.SetFloat("speed", 1);
            endTouchPosition = Input.GetTouch(0).position;

            Vector2 inputVector = endTouchPosition - startTouchPosition;
            if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
            {
                if (inputVector.x > 0)
                {
                    RightSwipe();
                }
                else
                {
                    LeftSwipe();
                }
            }
            else
            {
                if (inputVector.y > 0)
                {
                    UpSwipe();
                }
                else
                {
                    DownSwipe();
                }
            }
        }

        // Detecta la direccion a la que gira con teclas
        /*if (Input.GetKey(KeyCode.W))
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
            transform.rotation = Quaternion.Euler(0f, 0f, 270f);*/
    }

    private void UpSwipe()
    {
        //print("up");
        transform.rotation = Quaternion.Euler(0f, 0f, 360f);
    }
    private void DownSwipe()
    {
        //print("down");
        transform.rotation = Quaternion.Euler(0f, 0f, 180f);
    }
    private void LeftSwipe()
    {
        //print("left");
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    }
    private void RightSwipe()
    {
        //print("right");
        transform.rotation = Quaternion.Euler(0f, 0f, 270f);
    }
    

    private void FixedUpdate()
    {
        transform.Translate(direction * speed * Time.fixedDeltaTime);// Mover el jugador constantemente
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
        Debug.Log("You died");

        // Reiniciar el temporizador al morir
        levelManager.ResetTimer();

    }
    void Respawn()
    {
        transform.position = startPoint;
        speed = 2f;
        // Reiniciar el temporizador al reiniciar el nivel
        levelManager.ResetTimer();

    }

    private void OnCollectibleCollected()
    {

        // Agregar totalScore al puntaje actual del jugador y mostrarlo en la interfaz de usuario.
        speed += 0.5f;

        // Llamar al m�todo OnCollectibleCollected del LevelManager
        levelManager.OnCollectibleCollected();

    }
    private void OnWordCollected()
    {
        //Aqui poner lo que necesitas
    }



}
