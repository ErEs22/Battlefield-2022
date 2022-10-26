using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawner : MonoBehaviour
{
    public static ReSpawner Instance;

    public Transform[] reSpawnPositions;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public Vector3 GetReSpawnPosition()
    {
        return reSpawnPositions[Random.Range(0, reSpawnPositions.Length)].position;
    }
}
