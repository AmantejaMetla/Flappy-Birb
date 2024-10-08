using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipeUp;
    public GameObject pipeDown;
    public GameObject pipe;          // Additional type of pipe
    public float spawnInterval = 2f;  // Time interval between spawns
    public float gapHeight = 3f;      // Vertical gap between top and bottom pipes
    public float minY = -1f;          // Minimum y position for pipe spawning
    public float maxY = 1f;           // Maximum y position for pipe spawning
    public float destroyXPosition = -10f;  // X position at which pipes should be destroyed

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            // Randomly decide which type of pipe(s) to spawn
            float rand = Random.value;
            if (rand < 0.33f)
            {
                SpawnPipe(pipeUp);
            }
            else if (rand < 0.99f)
            {
                SpawnPipe(pipeDown);
            }
            else
            {
                SpawnPipePair();
            }

            timer = spawnInterval;
        }
    }

    void SpawnPipe(GameObject pipePrefab)
    {
        // Determine the spawn position
        Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(minY, maxY), transform.position.z);
        Instantiate(pipePrefab, spawnPosition, Quaternion.identity);
    }

    void SpawnPipePair()
    {
        // Randomly determine the y-position of the gap center within the allowed range
        float gapCenterY = Random.Range(minY + gapHeight / 2f, maxY - gapHeight / 2f);

        // Calculate positions for the top and bottom pipes based on gapHeight
        Vector3 pipeUpPosition = new Vector3(transform.position.x, gapCenterY + gapHeight / 2f, transform.position.z);
        Vector3 pipeDownPosition = new Vector3(transform.position.x, gapCenterY - gapHeight / 2f, transform.position.z);

        // Instantiate the pipes
        GameObject newPipeUp = Instantiate(pipeUp, pipeUpPosition, Quaternion.identity);
        GameObject newPipeDown = Instantiate(pipeDown, pipeDownPosition, Quaternion.identity);

        // Start coroutines to destroy pipes after they go off-screen
        StartCoroutine(DestroyPipeAfterTime(newPipeUp));
        StartCoroutine(DestroyPipeAfterTime(newPipeDown));
    }


    IEnumerator DestroyPipeAfterTime(GameObject pipe)
    {
        while (pipe.transform.position.x > destroyXPosition)
        {
            yield return null;
        }
        Destroy(pipe);
    }
}





