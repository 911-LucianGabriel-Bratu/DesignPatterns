using UnityEngine;

public class Level1Gun : MonoBehaviour, Gun 
{
    public Bullet bulletPrefab;
    public void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }
}