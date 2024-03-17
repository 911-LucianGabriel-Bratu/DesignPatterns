using UnityEngine;

public class PowerUpSpawnerManager: MonoBehaviour {
    private IPowerUpFactory[] powerUpFactories;
    public float spawnRate = 2.0f;

    private void Start()
    {
        GameObject[] factoryObjects = GameObject.FindGameObjectsWithTag("PowerUpFactory");
        powerUpFactories = new IPowerUpFactory[factoryObjects.Length];

        for (int i = 0; i < factoryObjects.Length; i++)
        {
            powerUpFactories[i] = factoryObjects[i].GetComponent<IPowerUpFactory>();
        }

        InvokeRepeating(nameof(SpawnPowerUps), spawnRate, spawnRate);
    }

    public void SpawnPowerUps()
    {
        for(int i = 0; i < powerUpFactories.Length; i++){
            powerUpFactories[i].spawnPowerUp();
        }
    }
}