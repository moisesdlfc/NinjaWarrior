using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Speed wich with the background moves
    public float speed;

    private GameObject limit;

    private void Awake()
    {
        limit = GameObject.Find("Limit");
    }

    private void FixedUpdate()
    {
        // If Game State is In Game...
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            // Move the layer with specific speed in X axis
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);

            // If this X position is equal or higher than limit X position...
            if (this.transform.localPosition.x <= limit.transform.localPosition.x)
            {
                // Asign new X position in parent X position - limit position
                this.transform.localPosition = new Vector3((limit.transform.localPosition.x * -1),
                                                            this.transform.localPosition.y,
                                                            this.transform.localPosition.z);

                Debug.Log("this.transform.localPosition.x: " + this.transform.localPosition.x);
                Debug.Log("limit.transform.localPosition.x: " + limit.transform.localPosition.x);
            }
        }
        else
        {
            // Stop parallax effect
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
