using UnityEngine;

public class ApplePowerUp: MonoBehaviour, PlayerBuffPowerUp {
    public float maxLifetime = 10.0f;

    public void Start(){
        Destroy(this.gameObject, this.maxLifetime);
    }
}