using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeOfCollectable
{
    pluma,
    deathCoin,
    potion
}

public class Collectable : MonoBehaviour
{
    // Tipo de coleccionable
    public TypeOfCollectable type;

    // Tiempo pasado desde la última detección de colisión
    float timeSinceLastCollision = 0.0f;

    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            // Conteo
            timeSinceLastCollision += Time.deltaTime;
        }
    }

    // Hide collectable gameobject
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    // Recoge el coleccionable
    public void Collection()
    {
        switch (type)
        {
            case TypeOfCollectable.pluma:
                // Jump bonus of PlayerController
                PlayerController.sharedInstance.SetJumpBonus();
                break;
            case TypeOfCollectable.deathCoin:
                // Death coin bonus of PlayerController
                PlayerController.sharedInstance.SetDeathCoinBonus();
                break;
            case TypeOfCollectable.potion:
                // Player earn 1 health
                PlayerController.sharedInstance.EarnHealth(1);
                break;
        }

        Hide();
    }

    // Detecta colisión
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Player" && timeSinceLastCollision > 0.1f)
        {
            Collection();

            // Reseteo de timeSinceLastCollision
            timeSinceLastCollision = 0.0f;
        }
    }
}
