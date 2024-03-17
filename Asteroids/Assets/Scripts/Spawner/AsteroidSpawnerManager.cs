using UnityEngine;
using UnityEngine.UI;

public class AsteroidSpawnerManager : MonoBehaviour
{
    private IAsteroidStrategy[] spawners;
    public Sprite[] asteroidSprites;
    public Text difficultyText;
private int currentSpawnerIndex = 0;

    private void Start()
    {
        // Initialize spawner array with different strategies
        GameObject[] spawnerObjects = GameObject.FindGameObjectsWithTag("AsteroidSpawner");
        spawners = new IAsteroidStrategy[spawnerObjects.Length];
        for (int i = 0; i < spawnerObjects.Length; i++)
        {
            spawners[i] = spawnerObjects[i].GetComponent<IAsteroidStrategy>();
        }

        // Initialize spawners with asteroidSprites
        foreach (var spawner in spawners)
        {
            spawner.Initialize(asteroidSprites);
        }

        // Update difficulty text initially
        UpdateDifficultyText();
        InvokeRepeating(nameof(SpawnAsteroids), this.spawners[currentSpawnerIndex].GetSpawnRate(), this.spawners[currentSpawnerIndex].GetSpawnRate());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            CycleSpawner();
            UpdateDifficultyText();
            CancelInvoke();
            InvokeRepeating(nameof(SpawnAsteroids), this.spawners[currentSpawnerIndex].GetSpawnRate(), this.spawners[currentSpawnerIndex].GetSpawnRate());
        }
    }

    private void CycleSpawner()
    {
        currentSpawnerIndex = (currentSpawnerIndex + 1) % spawners.Length;
    }

    private void UpdateDifficultyText()
    {
        difficultyText.text = $"Difficulty: {spawners[currentSpawnerIndex].GetType().Name}";
    }

    public void SpawnAsteroids()
    {
        spawners[currentSpawnerIndex].Spawn();
    }
}
