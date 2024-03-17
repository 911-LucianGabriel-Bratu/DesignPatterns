using UnityEngine;

public class ScorePowerUpFactory : MonoBehaviour, IPowerUpFactory
{
    public BananaPowerUp bananaPowerUpPrefab;
    public float spawnRate = 2.0f;
    public float spawnDistance = 5.0f;
    public float trajectory_variance = 5.0f;
    public int spawnAmount = 1;
    public void spawnPowerUp()
    {
        float randomNumber = Random.value;

        if(randomNumber <= 0.2f){
            for(int i = 0; i < spawnAmount; i++){
                Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
                Vector3 spawnPoint = this.transform.position + spawnDirection;

                float variance = Random.Range(-this.trajectory_variance, this.trajectory_variance);
                Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

                Instantiate(this.bananaPowerUpPrefab, spawnPoint, rotation);
            }
        }
    }

    public float GetSpawnRate(){
        return this.spawnRate;
    }
}