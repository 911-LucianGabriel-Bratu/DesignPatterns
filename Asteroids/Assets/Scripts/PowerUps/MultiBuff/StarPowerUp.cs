using UnityEngine;

public class StarPowerUp: MonoBehaviour, PlayerBuffPowerUp, ScorePowerUp {
    public float maxLifetime = 10.0f;
    public int Score {get; set;}
    public int Lives {get; set;}
    public float Speed {get; set;}

    public void Start(){
        Destroy(this.gameObject, this.maxLifetime);
    }
}