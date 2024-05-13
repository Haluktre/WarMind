using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    private float speed = 5f;
    public enum MyOptions { RedTeam, BlueTeam }
    [SerializeField] private MyOptions ChoiceEnemy;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector2(1 * speed, 0f);
        transform.Translate(movement * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == ChoiceEnemy.ToString() || collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);   
        }
    }
}
