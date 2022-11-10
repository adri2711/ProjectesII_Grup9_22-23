using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IntSpawner
{
    public Transform spawnedObject;
    public int minSpawnTime;
    public int maxSpawnTime;
    public Vector2 spawnPos;
    public float spawnVariance;
    public bool running = false;
    private bool active = true;

    public IntSpawner(Transform spawnedObject, int minSpawnTime, int maxSpawnTime, Vector2 spawnPos, float spawnVariance = 0f)
    {
        this.spawnedObject = spawnedObject;
        this.minSpawnTime = minSpawnTime;
        this.maxSpawnTime = maxSpawnTime;
        this.spawnPos = spawnPos;
        this.spawnVariance = spawnVariance;
    }
    public IEnumerator SpawnThread()
    {
        running = true;
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        if (active)
        {
            Transform intTransform = GameObject.Instantiate(spawnedObject, new Vector2(0, 0), Quaternion.identity, GameObject.Find("IntManager").transform);
            Vector2 intPos = new Vector2(spawnPos.x + Random.Range(-spawnVariance, spawnVariance), spawnPos.y + Random.Range(-spawnVariance, spawnVariance));
            intTransform.gameObject.GetComponent<Interruption>().SetPosition(intPos);
        }
        running = false;
    }

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }

}
