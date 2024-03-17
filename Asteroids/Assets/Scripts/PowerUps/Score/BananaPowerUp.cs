using UnityEngine;

public class BananaPowerUp: MonoBehaviour, ScorePowerUp {
    public float maxLifetime = 10.0f;

    public void Start(){
        Destroy(this.gameObject, this.maxLifetime);
    }
}