using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    // Player rigidbody
    private Rigidbody2D rigidBody;

    // Velocidad de movimiento
    public float runningSpeed = -6f;

    // Destroy time
    private float destroyTime = 0.0f;

    void Awake()
    {
        // Asignamos Rigidbody2D
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        destroyTime = Time.deltaTime; // Calcula el tiempo transcurrido

        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            // Le aplicamos un nuevo Vector2 con la velocidad de runningSpeed en x
            rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
        }

        // Destroy time
        if (destroyTime > 8.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
