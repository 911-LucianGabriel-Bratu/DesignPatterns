using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player playerInstance;

    public ParticleSystem explosion;
    public int lives = 3;
    public float respawnInvulnerabilityTime = 3.0f;
    public float respawnTime = 3.0f;
    public int score = 0;

    private void Start()
    {
        playerInstance = Player.Instance;
        
        if(playerInstance == null)
        {
            Debug.LogError("Player instance not found!");
        }
    }

    public void AsteroidDestroyed(Asteroid asteroid){
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if(asteroid.size < 0.75f){
            score += 100;
        }
        else if(asteroid.size < 1.2f){
            score += 50;
        }
        else{
            score += 25;
        }
    }
    public void PlayerDied(){ //maybe change to static?
        this.explosion.transform.position = this.playerInstance.transform.position;
        this.explosion.Play();

        this.lives--;

        if(this.lives <= 0){
            GameOver();
        }
        else{
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    private void Respawn(){
        this.playerInstance.transform.position = Vector3.zero;
        this.playerInstance.gameObject.layer = LayerMask.NameToLayer("Player Respawn");
        this.playerInstance.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), this.respawnInvulnerabilityTime);
    }

    private void TurnOnCollisions(){
        this.playerInstance.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver(){
        this.lives = 3;
        this.score = 0;

        Invoke(nameof(Respawn), this.respawnTime);
    }
}
