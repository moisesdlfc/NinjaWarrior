using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageZone
{
    oneDamage,
    twoDamage,
    threeDamage,
    kill
}

public class KillTrigger : MonoBehaviour
{
    public DamageZone type; // Type of damage zone

    private float timeWithoutDamage = 0.0f;

    private void Update()
    {
        timeWithoutDamage += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player collision is detected with the KillTrigger zone and 1 seconds have passed without damage,
        // damage or kill the player
        if (collision.tag == "Player" && timeWithoutDamage > 1.0f)
        {
            Debug.Log("Kill - " + GameManager.sharedInstance.currentGameState);
            switch (type)
            {
                // 1 damage zone
                case DamageZone.oneDamage:

                    PlayerController.sharedInstance.TakeDamage(1);

                    break;

                // 2 damage zone
                case DamageZone.twoDamage:

                    PlayerController.sharedInstance.TakeDamage(2);

                    break;

                // 3 damage zone
                case DamageZone.threeDamage:

                    PlayerController.sharedInstance.TakeDamage(3);

                    break;

                // Kill the player zone
                case DamageZone.kill:

                    PlayerController.sharedInstance.Kill();

                    break;
            }

            timeWithoutDamage = 0.0f; // Reset timeWithoutDamage
        }
    }
}
