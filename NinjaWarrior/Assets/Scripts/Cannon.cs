using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    // Bola de cañon
    public GameObject cannonBall;

    // Posición incial de la bola de cañon
    public Transform initialPositionCannonBall;

    //Lista de bolas de cañon
    private List<GameObject> cannonBalls;

    // Tiempo de disparo
    private float actualShootTime = 0.0f;

    // Cadencia de disparo
    public float shootTime = 3.0f;

    void Awake()
    {
        // Instancia cannonBalls
        cannonBalls = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        actualShootTime += Time.deltaTime;

        // Shoot time
        if (actualShootTime > shootTime)
        {
            // Create new cannon ball
            GameObject newCannonBall = (GameObject)Instantiate(cannonBall);

            // Set parent
            newCannonBall.transform.SetParent(this.gameObject.transform);

            // Establece su posición inicial
            newCannonBall.transform.position = initialPositionCannonBall.position;

            // La añade a la lista
            cannonBalls.Add(newCannonBall);

            // Reset shootTime
            actualShootTime = 0.0f;
        }
    }
}
