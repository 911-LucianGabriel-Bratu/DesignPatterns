using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class EasyAsteroidSpawner : MonoBehaviour, IAsteroidStrategy
{
    public Asteroid asteroidPrefab;
    private Sprite[] asteroidSprites;
    public float spawnRate = 2.0f;
    public float trajectory_variance = 15.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 15.0f;
    private void Start(){
    }

    public void Spawn(){
        for(int i = 0; i < spawnAmount; i++){
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectory_variance, this.trajectory_variance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            Sprite sprite = this.asteroidSprites[Random.Range(0, this.asteroidSprites.Length)];
            asteroid.SetSprite(sprite);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }

    public void Initialize(Sprite[] asteroidSprites)
    {
        this.asteroidSprites = asteroidSprites;
    }

    public float GetSpawnRate(){
        return this.spawnRate;
    }
}
