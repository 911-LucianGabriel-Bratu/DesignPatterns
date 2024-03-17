using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public Bullet bulletPrefab; // change here for modifications
    public float thrust_speed = 1.0f;
    public float turn_speed = 1.0f;
    private bool _thrusting;
    private float _turn_direction;
    private Rigidbody2D _rigidBody;

    private void Awake(){
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    private void Update()
    {
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            _turn_direction = 1.0f;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            _turn_direction = -1.0f;
        }
        else{
            _turn_direction = 0.0f;
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            Shoot();
        }
    }

    private void FixedUpdate(){
        if(_thrusting){
            _rigidBody.AddForce(this.transform.up * thrust_speed);
        }
        if(_turn_direction != 0.0f){
            _rigidBody.AddTorque(_turn_direction * turn_speed);
        }
    }

    private void Shoot(){
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Asteroid"){
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = 0;
            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
