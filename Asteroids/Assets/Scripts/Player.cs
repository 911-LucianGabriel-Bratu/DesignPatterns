using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IScoreObserver
{
    public static Player Instance { get; private set; }
    public Gun currentGun;
    public GameObject level1Gun;
    public GameObject level2Gun;
    public float default_thrust_speed = 1.0f;
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
        currentGun = level1Gun.GetComponent<Level1Gun>();
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
        currentGun.Shoot();
    }


    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Asteroid"){
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = 0;
            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
        else if (collision.gameObject.CompareTag("BananaPowerUp"))
        {
            GameManager.Instance.IncreaseScore(100);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("ApplePowerUp"))
        {
            this.thrust_speed += 0.5f;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("StarPowerUp"))
        {
            GameManager.Instance.IncreaseLives(collision.gameObject.GetComponent<StarPowerUp>().Lives);
            GameManager.Instance.IncreaseScore(collision.gameObject.GetComponent<StarPowerUp>().Score);
            this.thrust_speed += collision.gameObject.GetComponent<StarPowerUp>().Speed;
            Destroy(collision.gameObject);
        }
    }
    
    public void OnScoreThresholdReached(){
        currentGun = level2Gun.GetComponent<Level2Gun>();
    }

    public void OnScoreReset(){
        currentGun = level1Gun.GetComponent<Level1Gun>();
    }

    public void SetDefaultThrustSpeed(){
        this.thrust_speed = this.default_thrust_speed;
    }
}