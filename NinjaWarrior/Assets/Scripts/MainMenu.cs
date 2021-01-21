using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Comenzar juego
    public void StartGame()
    {
        // La escena se carga en modo Single para que solo exista una escena abierta a la vez
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    // Salir del juego
    public void ExitGame()
    {
        // Estos if con # permiten comprobar en función de la plataforma en la que se va a ejectuar
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
