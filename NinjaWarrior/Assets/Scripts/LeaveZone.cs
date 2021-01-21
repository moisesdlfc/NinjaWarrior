using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveZone : MonoBehaviour
{
    // Tiempo pasado desde la última destrucción
    float timeSinceLastDestruction = 0.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checkea si hace mas de 1 segundo desde la ultima vez que se detecto colision
        // Checkea si el que ha colisionado es el jugador
        if(timeSinceLastDestruction > 1.0f && collision.gameObject.name == "Player")
        {
            // Añadimos un nuevo bloque
            LevelGenerator.sharedInstance.AddLevelBlock();

            // Eliminamos el bloque más antiguo
            LevelGenerator.sharedInstance.RemoveOldestLevelBlock();

            // Reseteamos timeSinceLastDestruction
            timeSinceLastDestruction = 0.0f;
        }
    }

    private void Update()
    {
        timeSinceLastDestruction += Time.deltaTime;
    }
}
