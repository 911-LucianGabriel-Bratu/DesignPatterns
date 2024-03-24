using UnityEngine;

public class MultiBuffPowerUpFactory : MonoBehaviour, IPowerUpFactory
{
    public StarPowerUp starPowerUpPrefab;
    public StarBuilder starBuilder = new StarBuilder();
    public float spawnRate = 2.0f;
    public float spawnDistance = 5.0f;
    public float trajectory_variance = 5.0f;
    public int spawnAmount = 1;
    public void spawnPowerUp()
    {
        float randomNumber = Random.value;

        if(randomNumber <= 0.05f){
            for(int i = 0; i < spawnAmount; i++){
                Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
                Vector3 spawnPoint = this.transform.position + spawnDirection;

                float variance = Random.Range(-this.trajectory_variance, this.trajectory_variance);
                Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

                Star star = starBuilder.SetScoreCount().SetSpeedCount().SetLivesCount().GetStar();
                StarPowerUp starPowerUp = Instantiate(this.starPowerUpPrefab, spawnPoint, rotation);
                starPowerUp.Score = star.score;
                starPowerUp.Lives = star.lives;
                starPowerUp.Speed = star.speed;
            }
        }
    }

    public float GetSpawnRate(){
        return this.spawnRate;
    }
}