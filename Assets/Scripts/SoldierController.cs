using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class SoldierController : MonoBehaviour
{

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _soldier;
    [SerializeField] private Transform _gun;
    public enum MyOptions { RedBullet, BlueBullet }
    [SerializeField] private MyOptions ChoiceEnemyBullet;

    private bool _isLive = true;
    public float health = 3f;
    public float speed = 2f; // Movement speed of the player
    public float rotationSpeed = 100f;
    public float horizontalInput;
    public float verticalInput;
    public int rotateDirect;
    public int isFire;

    public float fireRate = 0.1f; // Ateş hızı (saniyede mermi sayısı)
    private float lastFireTime = 0f; // Son ateşleme zamanı

    void Start()
    {
        health = 3f;
    }

    void Update()
    {
        if (isFire == 1)
        {
            if (Time.time - lastFireTime >= fireRate)
            {
                GunFire();
                // Son ateşleme zamanını güncelleyin
                lastFireTime = Time.time;
            }
        }
    }

    private void GunFire()
    {
        Instantiate(_bullet, new Vector2(_gun.position.x, _gun.position.y), _gun.rotation);
    }

    public void SoldierMovement()
    {
        // Move the player based on input
        Vector3 movement = new Vector2(horizontalInput * speed, verticalInput * speed);
        transform.Translate(movement * Time.deltaTime);

        _soldier.Rotate(0f, 0f, rotateDirect * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ChoiceEnemyBullet.ToString())
        {
            health = health - 1f;
            if (health == 0f && _isLive == true)
            {
                _isLive = false;
                Destroy(this.gameObject);

            }
        }
    }
}
