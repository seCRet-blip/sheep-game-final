using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public bool canSpawn = true; 
    public GameObject sheepPrefab; 
    public List<Transform> sheepSpawnPositions = new List<Transform>(); 
    public float startingSpawnTime = 3f;
    public float minSpawnTime = 1f;
    public float spawnTimeDecreaseRate = 0.05f;

    private List<GameObject> sheepList = new List<GameObject>();
    private float currentSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        currentSpawnTime = startingSpawnTime;
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position; 
        GameObject sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation); 
        sheepList.Add(sheep); 
        sheep.GetComponent<Sheep>().SetSpawner(this); 

        Debug.Log("Spawn rate: " + currentSpawnTime);
    }

    private IEnumerator SpawnRoutine() 
    {
        while (canSpawn) 
        {
            SpawnSheep(); 
            yield return new WaitForSeconds(currentSpawnTime); 
            currentSpawnTime -= spawnTimeDecreaseRate;
            currentSpawnTime = Mathf.Max(currentSpawnTime, minSpawnTime);
        }
    }

    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }

    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList)
        {
            Destroy(sheep);
        }

        sheepList.Clear();
    }
}
