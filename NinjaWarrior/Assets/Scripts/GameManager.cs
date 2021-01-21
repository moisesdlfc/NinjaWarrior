using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    // Singleton: Creamos solo una instancia del Game Manager, a traves de sharedInstance
    public static GameManager sharedInstance;

    // Estado de juego actual
    public GameState currentGameState = GameState.menu;

    // Crea botón Retry
    private GameObject retryButton;

    // Crea botón BackToMenu
    private GameObject backToMenuButton;

    // Crea imagen Game Over
    private GameObject gameOverImage;

    // Muestreo de puntuación en pantalla
    private GameObject scoreVisible;

    // Calcula la puntuación
    private float score;

    // Muestreo de puntuación maxScore
    private GameObject maxScoreVisible;

    // UI health
    public GameObject[] healthUI;

    void Awake()
    {
        // Asignamos el singleton
        sharedInstance = this;

        // Referencia a botón Retry
        retryButton = GameObject.Find("RetryButton");
        retryButton.SetActive(false);

        // Referencia a botón BackToMenu
        backToMenuButton = GameObject.Find("BackToMenuButton");
        backToMenuButton.SetActive(false);

        // Referencia gameOver y lo desactiva
        gameOverImage = GameObject.Find("GameOver");
        gameOverImage.SetActive(false);

        // Referencia scoreVisible
        scoreVisible = GameObject.Find("Score");

        // Referencia maxScoreVisible
        maxScoreVisible = GameObject.Find("MaxScore");

        // Inicializa score
        score = 0.0f;
    }

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        // Update player health UI
        switch (PlayerController.sharedInstance.playerHealth)
        {
            case 0:

                foreach (GameObject health in healthUI)
                {
                    health.SetActive(false);
                }

                break;

            case 1:

                healthUI[0].SetActive(true);
                healthUI[1].SetActive(false);
                healthUI[2].SetActive(false);

                break;

            case 2:

                healthUI[0].SetActive(true);
                healthUI[1].SetActive(true);
                healthUI[2].SetActive(false);

                break;

            case 3:

                foreach (GameObject health in healthUI)
                {
                    health.SetActive(true);
                }

                break;
        }

        // Calcula score
        if (currentGameState == GameState.inGame)
        {
            score = GetDistance();

            // Lo muestra en pantalla
            scoreVisible.GetComponent<UnityEngine.UI.Text>().text = score.ToString().Split('.')[0] + " m";

        }

        // Comprueba Max Score y lo guarda en caso de ser necesario
        // Obtenemos la máxima puntuación, si no existe 0 por defecto
        float currentMaxScore = PlayerPrefs.GetInt("maxScore", 0);

        // Si la máxima puntuación < score establece nuevo record
        if (currentMaxScore < score)
        {
            PlayerPrefs.SetInt("maxScore", (int)score);
        }

        // Muestra max score por pantalla
        maxScoreVisible.GetComponent<UnityEngine.UI.Text>().text = PlayerPrefs.GetInt("maxScore", 0) + " m";
    }

    // Método encargado de iniciar el juego
    public void StartGame()
    {
        // Cambiamos el estado del juego a inGame
        SetGameState(GameState.inGame);
    }

    // Método encargado de finalizar el juego
    public void GameOver()
    {
        // Cambiamos el estado del juego a gameOver
        SetGameState(GameState.gameOver);
    }

    // Método encargado de volver al menú
    public void BackToMenu()
    {
        // Cambiamos el estado del juego a menu
        SetGameState(GameState.menu);
    }

    // Método encargado de cambiar el estado del juego
    void SetGameState(GameState newGameState)
    {

        if (newGameState == GameState.menu)
        {
            // Preparar la escena de Unity para mostrar el menú
            // La escena se carga en modo Single para que solo exista una escena abierta a la vez
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

        } else if (newGameState == GameState.inGame)
        {
            // Preparar la escena de Unity para mostrar el juego
            // Reiniciamos el nivel si el jugador ya se ha movido un poco
            if (PlayerController.sharedInstance.transform.position.x > 5)
            {
                // Eliminamos todos los bloques
                LevelGenerator.sharedInstance.RemoveAllBlocks();
                // Generamos los bloques iniciales
                LevelGenerator.sharedInstance.GenerateInitialLevelBlocks();
            }

            // Reiniciamos al jugador
            PlayerController.sharedInstance.StartGame();

            // Oculta los botones del menú
            retryButton.SetActive(false);
            backToMenuButton.SetActive(false);

            // Ocultamos imagen pantalla Game Over
            gameOverImage.SetActive(false);

            // Reseteamos la posición de la cámara
            GameObject camara = GameObject.FindGameObjectWithTag("MainCamera");
            CameraFollow cameraFollow = camara.GetComponent<CameraFollow>();
            cameraFollow.ResetCameraPosition();

            // Desactiva todos los bonus
            PlayerController.sharedInstance.TurnOffBonus();

            // Resetea score
            score = 0.0f;

        } else if (newGameState == GameState.gameOver)
        {
            // Preparar la escena de Unity para gameOver

            // Muestra el botón Retry
            retryButton.SetActive(true);

            // Muestra el botón BackToMenu
            backToMenuButton.SetActive(true);

            // Muestra imagen pantalla Game Over
            gameOverImage.SetActive(true);
        }

        // Asignamos al estado de juego actual el nuevo estado de juego
        this.currentGameState = newGameState;
    }

    // Calcula la distancia recorrida por el jugador
    float GetDistance()
    {
        float travelledDistance = Vector2.Distance(new Vector2(PlayerController.sharedInstance.startPosition.x, 0), new Vector2(PlayerController.sharedInstance.transform.position.x, 0));

        return travelledDistance;
    }
}
