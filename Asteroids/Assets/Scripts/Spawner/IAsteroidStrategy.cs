using UnityEngine;

public interface IAsteroidStrategy {
    void Spawn();
    void Initialize(Sprite[] asteroidSprites);
    float GetSpawnRate();

    string GetDifficultyName();
}