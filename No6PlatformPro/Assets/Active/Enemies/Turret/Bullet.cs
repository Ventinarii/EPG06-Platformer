using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 500, lifeTime = 100,dmg=10;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed * Time.deltaTime;
    }
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.GetComponent<RobotContoledByPlayer>().hp -= dmg;
        }
        Destroy(gameObject);
    }
}
