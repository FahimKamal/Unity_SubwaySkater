using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldGenerator : MonoBehaviour
{
    // Configurable Fields
    [SerializeField] private int firstChunkSpawnPos = -10;
    [SerializeField] private int chunkOnScreen = 5;
    [SerializeField] private float deSpawnDistance = 5;

    [SerializeField] private List<GameObject> chunkPrefabs;
    [SerializeField] private Transform camTransform;

    // GamePlay
    private float _chunkSpawnZ;
    private Queue<Chunk> _activeChunks = new Queue<Chunk>();
    private List<Chunk> _chunkPool = new List<Chunk>();

    private void Start()
    {
        // Check if we have empty chunkPrefab list
        if (chunkPrefabs.Count == 0)
        {
            Log("No Chunk prefab found on the world generator, please assign some chunks.");
        }
        
        // Try to assign the cameraTransform.
        if (!camTransform)
        {
            camTransform = Camera.main.transform;
            Log("We've assigned cameraTransform automatically to the Camera.main");
        }
        
        ResetWorld();
    }

    public void ScanPosition()
    {
        var cameraZ = camTransform.transform.position.z;
        var lastChunk = _activeChunks.Peek();
        if (cameraZ >= lastChunk.transform.position.z + lastChunk.chuckLength + deSpawnDistance)
        {
            SpawnNewChunk();
            DeleteLastChunk();
        }
    }

    private void SpawnNewChunk()
    {
        // Get a random index for which prefab to spawn.
        var randomIndex = Random.Range(0, chunkPrefabs.Count);

        // Dose it already exist within our pool
        Chunk chunk = _chunkPool.Find(x => !x.gameObject.activeSelf && x.name == (chunkPrefabs[randomIndex].name + "(Clone)"));
        
        // Create a chunk, if were not able to find one to reuse
        if (!chunk)
        {
            var go = Instantiate(chunkPrefabs[randomIndex], transform);
            chunk = go.GetComponent<Chunk>();
        }

        // Place the object and show it
        chunk.transform.position = new Vector3(0, 0, _chunkSpawnZ);
        _chunkSpawnZ += chunk.chuckLength;
        
        // Store the value, to reuse in our pool
        _activeChunks.Enqueue(chunk);
        chunk.ShowChunk();
    }

    private void DeleteLastChunk()
    {
        var chunk = _activeChunks.Dequeue();
        chunk.HideChunk();
        _chunkPool.Add(chunk);
    }

    public void ResetWorld()
    {
        // Reset the chunkSpawn Z
        _chunkSpawnZ = firstChunkSpawnPos;

        // Deactivate all active chunks and send them in pool
        for (var i = _activeChunks.Count; i != 0; i--)
        {
            DeleteLastChunk();
        }


        // Spawn new chunks from first chunk spawn position.
        for (var i = 0; i < chunkOnScreen; i++)
        {
            SpawnNewChunk();
        }
    }

    private void Log(string message)
    {
        Debug.Log(message);
    }
}
