using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    // Rigid body
    private Rigidbody2D rigidBody;

    // Posicion inicial
    public Transform sawInitialPosition;

    // Posicion final
    public Transform sawFinalPosition;

    // Bool para saber dirección de movimiento
    private bool moveRight = true;

    // Velocidad
    public float sawVelocity = 2.0f;

    void Awake()
    {
        // Asignamos Rigidbody2D
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Si la cuchilla tiene que moverse hacia la derecha...
        if (moveRight)
        {
            if (this.transform.position.x < sawFinalPosition.position.x)
            {
                // Le aplicamos un nuevo Vector2 con la velocidad de runningSpeed en x
                rigidBody.velocity = new Vector2(sawVelocity, rigidBody.velocity.y);
            } else
            {
                moveRight = false;
            }
        } else if (!moveRight) // Si la cuchilla tiene que moverse hacia la izquierda...
        {
            if (this.transform.position.x > sawInitialPosition.position.x)
            {
                // Le aplicamos un nuevo Vector2 con la velocidad de runningSpeed en x
                rigidBody.velocity = new Vector2(-sawVelocity, rigidBody.velocity.y);
            } else
            {
                moveRight = true;
            }
        }
         
    }
}
