using UnityEngine;

public class Level2Gun : MonoBehaviour, Gun
{
    public Bullet bulletPrefab;
    public void Shoot()
    {
        float offsetAngle = 15f;

        float offsetDistance = 0.5f;

        Bullet bullet1 = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet1.Project(this.transform.up);

        Quaternion rotation = Quaternion.Euler(0, 0, offsetAngle) * this.transform.rotation;

        Vector3 offsetPosition = this.transform.position + this.transform.up * offsetDistance;

        Bullet bullet2 = Instantiate(this.bulletPrefab, offsetPosition, rotation);
        bullet2.Project(this.transform.up);
    }
}