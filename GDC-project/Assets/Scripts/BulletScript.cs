using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody rb;
    Transform transform;
    float birthTime;
    public float lifeDuration;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        transform = gameObject.GetComponent<Transform>();

        rb.velocity = transform.forward * speed;


        birthTime = Time.time;
    }

    private void Update()
    {
        KillOldBullets();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }


    void KillOldBullets()
    {
        bool shouldDestroy = birthTime + lifeDuration < Time.time;

        if (shouldDestroy)
        {
            Destroy(gameObject);
        }
    }
}
