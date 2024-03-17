using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player playerInstance;
    private List<IScoreObserver> scoreObservers = new List<IScoreObserver>();
    public ParticleSystem explosion;
    public int lives = 3;
    public float respawnInvulnerabilityTime = 3.0f;
    public float respawnTime = 3.0f;
    public int score = 0;
    public Text scoreText;
    public Text livesText;
    public static GameManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseScore(int points)
    {
        score += points;
        UpdateScoreText();
    }


    private void Start()
    {
        playerInstance = Player.Instance;
        
        if(playerInstance == null)
        {
            Debug.LogError("Player instance not found!");
        }
        RegisterScoreObserver(playerInstance);
        UpdateScoreText();
        UpdateLivesText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {score}";

        if (score >= 3000)
        {
            NotifyScoreObservers();
        }
    }
    private void UpdateLivesText()
    {
        livesText.text = $"Lives: {lives}";
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
        UpdateScoreText();
    }
    public void PlayerDied(){
        this.explosion.transform.position = this.playerInstance.transform.position;
        this.explosion.Play();

        this.lives--;

        UpdateLivesText();

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
        UpdateScoreText();
        UpdateLivesText();
        NotifyResetScoreObservers();
        playerInstance.SetDefaultThrustSpeed();

        Invoke(nameof(Respawn), this.respawnTime);
    }

    private void RegisterScoreObserver(IScoreObserver observer)
    {
        scoreObservers.Add(observer);
    }

    private void NotifyScoreObservers()
    {
        foreach (var observer in scoreObservers)
        {
            observer.OnScoreThresholdReached();
        }
    }

    private void NotifyResetScoreObservers(){
        foreach (var observer in scoreObservers)
        {
            observer.OnScoreReset();
        }
    }
}
