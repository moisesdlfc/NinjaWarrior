using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Singleton
    public static LevelGenerator sharedInstance;

    // First block
    public LevelBlock firstBlock;

    // Level blocks
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();

    // Current blocks
    public List<LevelBlock> currentBlocks = new List<LevelBlock>();

    // Start point
    public Transform levelStartPoint;

    private void Awake()
    {
        // Asignamos el singleton
        sharedInstance = this;
    }

    private void Start()
    {
        // TEST
        //GenerateAllLevelBlocks();

        // Genera los bloques iniciales al inicio
        GenerateInitialLevelBlocks();
    }

    // Añade bloques
    public void AddLevelBlock()
    {
        // Generamos un numero aleatorio con rango en cantidad de bloques disponibles
        int randomNumberBlock = Random.Range(0, allTheLevelBlocks.Count);

        // Creamos randomBlock
        LevelBlock randomBlock;

        // Le asigna una posición
        Vector3 spawnPosition = Vector3.zero;

        if (currentBlocks.Count == 0)
        {
            // Si estamos en el primer bloque instanciamos el firstBlock
            randomBlock = (LevelBlock)Instantiate(firstBlock);

            spawnPosition = levelStartPoint.position;
        } else
        {
            // Si no lo creamos de manera aleatoria
            randomBlock = (LevelBlock)Instantiate(allTheLevelBlocks[randomNumberBlock]);

            spawnPosition = currentBlocks[currentBlocks.Count - 1].exitPoint.position;
        }

        // Asignamos el randomBlock como hijo del LevelGenerator
        randomBlock.transform.SetParent(this.transform, false);

        // Corregimos la posición
        Vector3 correction = new Vector3(spawnPosition.x - randomBlock.startPoint.position.x,
                                         spawnPosition.y - randomBlock.startPoint.position.y,
                                         0);

        randomBlock.transform.position = correction;

        // Lo añade a la lista currentBlocks
        currentBlocks.Add(randomBlock);
    }

    // Elimina los bloques anteriores al jugador
    public void RemoveOldestLevelBlock()
    {
        // Referencio el bloque más antiguo
        LevelBlock oldestBlock = currentBlocks[0];

        // Lo elimino de la lista
        currentBlocks.Remove(oldestBlock);

        // Destruimos el objeto de la escena
        Destroy(oldestBlock.gameObject);
    }

    // Elimina todos los bloques generados
    public void RemoveAllBlocks()
    {
        while(currentBlocks.Count > 0)
        {
            RemoveOldestLevelBlock();
        }
    }

    // Genera los bloques iniciales
    public void GenerateInitialLevelBlocks()
    {
        for(int i = 0; i < 3; i++)
        {
            AddLevelBlock();
        }
    }

    // Genera todos los tipos de bloques disponibles
    private void GenerateAllLevelBlocks()
    {
        // Generamos un numero aleatorio con rango en cantidad de bloques disponibles
        //for(int i = (allTheLevelBlocks.Count - 1); i >= 0; i--)
        for (int i = 0; i < allTheLevelBlocks.Count; i++)
        {
            // Creamos currentBlock
            LevelBlock levelBlock;

            // Le asigna una posición
            Vector3 spawnPosition = Vector3.zero;

            if (currentBlocks.Count == 0)
            {
                // Si estamos en el primer bloque lo instanciamos en la posición inicial
                levelBlock = (LevelBlock)Instantiate(allTheLevelBlocks[i]);

                spawnPosition = levelStartPoint.position;
            }
            else
            {
                // Si no lo creamos a continuación del anterior
                levelBlock = (LevelBlock)Instantiate(allTheLevelBlocks[i]);

                spawnPosition = currentBlocks[currentBlocks.Count - 1].exitPoint.position;
            }

            // Asignamos el levelBlock como hijo del LevelGenerator
            levelBlock.transform.SetParent(this.transform, false);

            // Corregimos la posición
            Vector3 correction = new Vector3(spawnPosition.x - levelBlock.startPoint.position.x,
                                             spawnPosition.y - levelBlock.startPoint.position.y,
                                             0);

            levelBlock.transform.position = correction;

            // Lo añade a la lista currentBlocks
            currentBlocks.Add(levelBlock);
        }
    }
}
