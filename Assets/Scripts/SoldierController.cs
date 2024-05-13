using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class SoldierController : MonoBehaviour
{

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _soldier;
    [SerializeField] private Transform _gun;
    public enum MyOptions {RedBullet, BlueBullet}
    [SerializeField] private MyOptions ChoiceEnemyBullet;

    private bool _isLive = true;
    public float health = 3f;
    public float speed = 2f; // Movement speed of the player
    public float rotationSpeed = 100f;
    private float horizontalInput;
    private float verticalInput;

    public float fireRate = 0.1f; // Ateş hızı (saniyede mermi sayısı)
    private float lastFireTime = 0f; // Son ateşleme zamanı

    void Start()
    {
        health = 3f;
    }
    void Update()
    {
        // Get horizontal and vertical input from keyboard or joystick
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.Space) )
        {
            if (Time.time - lastFireTime >= fireRate)
            {
                GunFire();
                // Son ateşleme zamanını güncelleyin
                lastFireTime = Time.time;
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            _soldier.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            _soldier.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // Move the player based on input
        Vector3 movement = new Vector2(horizontalInput * speed, verticalInput * speed);
        transform.Translate(movement * Time.deltaTime);
    }

    private void GunFire()
    {
        Instantiate(_bullet, new Vector2(_gun.position.x, _gun.position.y), _gun.rotation);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ChoiceEnemyBullet.ToString())
        {
            health = health - 1f;
            if(health == 0f && _isLive == true)
            {
                _isLive = false;
                Destroy(this.gameObject);
                
            }
        }
    }
}
