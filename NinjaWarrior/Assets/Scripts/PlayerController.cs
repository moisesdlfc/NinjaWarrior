using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Singleton
    public static PlayerController sharedInstance;

    [Header("Player Settings")]
    // Player health
    public int playerHealth = 3;

    // Initial player health
    public const int INITIAL_PLAYER_HEALTH = 3;

    // Max player health
    public const int MAX_PLAYER_HEALTH = 3;

    // Min player health
    public const int MIN_PLAYER_HEALTH = 0;

    // Fuerza de salto
    public float jumpForce = 50f;

    // Velocidad de movimiento
    public float runningSpeed = 3.5f;

    // Player rigidbody
    private Rigidbody2D rigidBody;

    // Esta variable almacena la capa del suelo
    public LayerMask groundLayer;

    // Animator
    public Animator animator;

    // Guardamos la posición inicial del jugador
    public Vector3 startPosition;

    // Player previous position
    private Vector3 previousPosition;

    // Tiempo que el jugador ha pasado en la misma posición
    private float timeInSamePosition = 0.0f;

    // BONUS
    private bool jumpBonus = false; // Jump bonus
    private bool deathCoinBonus = false; // Death coin bonus
    private float timeOfBonus = 0.0f; // Duration of bonus
    private float timeWithBonus = 0.0f; // Time with bonus

    [Header("Audio")]
    public AudioClip deathSound;
    public AudioClip jumpSound;

    // Awake: solo ocurre 1 vez
    private void Awake()
    {
        // Asignamos el singleton
        sharedInstance = this;

        // Asignamos Rigidbody2D
        rigidBody = GetComponent<Rigidbody2D>();

        // Asignamos la posición inicial de nuestro personaje a startPosition
        startPosition = this.transform.position;

        // Inicializamos previous position
        previousPosition = startPosition;
    }

    // Método para iniciar al jugador en el juego
    public void StartGame()
    {
        // Configuramos los booleanos de la animación
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);

        // Reset player health
        playerHealth = INITIAL_PLAYER_HEALTH;

        // Reset to the initial player position
        this.transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            // Saltar si el usuario pulsa la tecla 'W'
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Jump();
            }

            // Asignamos a la animación el mismo valor que el método isTouchingTheGround()
            animator.SetBool("isGrounded", IsTouchingTheGround());

            // Comprueba si hay algun bonus activo
            if (timeOfBonus > 0.0f)
            {
                timeWithBonus += Time.deltaTime; // Actualiza el tiempo transcurrido con bonus

                // Si ya ha pasado el tiempo desactiva todos los bonus
                if (timeWithBonus > timeOfBonus)
                {
                    TurnOffBonus();
                }
            }
        }
    }

    // Se llama a intervalos fijos de tiempo, lo que evita problemas con los fps
    // Es el sitio perfecto para aplicar fuerzas constantes
    void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            // Si la velocidad del player es < runningSpeed...
            if (rigidBody.velocity.x < runningSpeed)
            {
                // Le aplicamos un nuevo Vector2 con la velocidad de runningSpeed en x
                rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
            }

            // Evita que el jugador se quede atascado por un choque horizontal
            // Si el jugador lleva más de 1 segundo en la misma posición lo mata
            if (CheckStuckPlayer() > 0.5f)
            {
                Kill();
            }
        }
    }

    // Saltar
    void Jump()
    {
        if (IsTouchingTheGround())
        {
            if (jumpBonus)
            {
                // F = m * a   --->   a = F/m
                // Multiplicamos la componente vertial por jumpForce en modo impulso
                rigidBody.AddForce(Vector2.up * jumpForce * 1.3f, ForceMode2D.Impulse);
            }
            else
            {
                // F = m * a   --->   a = F/m
                // Multiplicamos la componente vertial por jumpForce en modo impulso
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            // Play jump sound with 0.3 volume
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.volume = 0.3f;
            audioSource.PlayOneShot(jumpSound);
        }
    }

    // Player earn health
    public void EarnHealth(int gain)
    {
        Debug.Log("Earn " + gain + " health points");

        playerHealth += gain; // Gain health

        // If player health is higher than limit, set MAX_PLAYER_HEALTH value to playerHealth
        if (playerHealth > MAX_PLAYER_HEALTH)
        {
            playerHealth = MAX_PLAYER_HEALTH;
        }
    }

    // Remove player health
    public void TakeDamage(int damage)
    {
        if (!deathCoinBonus)
        {
            Debug.Log("Player takes " + damage + " damage");

            playerHealth -= damage; // Take damage

            // If player health is less or equal to 0, kill the player
            if (playerHealth <= MIN_PLAYER_HEALTH)
            {
                Kill();
            }
        }
    }

    // Matar al jugador
    public void Kill()
    {
        if (!deathCoinBonus)
        {
            Debug.Log("Kill player");

            // Cambiamos la animación del jugador
            animator.SetBool("isAlive", false);

            // Play dead efect with 1 volume
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.volume = 1.0f;
            audioSource.PlayOneShot(deathSound);

            // Cambiamos el estado de juego a Game Over
            GameManager.sharedInstance.GameOver();
        }
        
    }

    // Devuelve true si el personaje está tocando el suelo o falso en caso contario
    bool IsTouchingTheGround()
    {
        if(Physics2D.Raycast(this.transform.position, Vector2.down, 0.15f, groundLayer))
        {
            return true;
        } else
        {
            return false;
        }
    }

    // Comprueba si el jugador se ha quedado atascado (se encuentra en las mismas coordenadas)
    float CheckStuckPlayer()
    {
        if (this.transform.position == previousPosition)
        {
            timeInSamePosition += Time.deltaTime;
        }
        else // si no, guarda la posición actual y resetea timeInSamePosition
        {
            previousPosition = this.transform.position;
            timeInSamePosition = 0.0f;
        }

        return timeInSamePosition;
    }

    // Configura el jump bonus
    public void SetJumpBonus()
    {
        jumpBonus = true;
        timeOfBonus = 10.0f;
    }

    // Configura Death Coin
    public void SetDeathCoinBonus()
    {
        deathCoinBonus = true;
        timeOfBonus = 10.0f;
    }

    // Apaga todos los bonus
    public void TurnOffBonus()
    {
        jumpBonus = false;
        deathCoinBonus = false;

        // Reset times
        timeOfBonus = 0.0f;
        timeWithBonus = 0.0f;
    }
}
