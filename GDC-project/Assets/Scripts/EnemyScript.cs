using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    Rigidbody rb;
    Transform transform;
    GameObject player;

    public float speed;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        transform = gameObject.GetComponent<Transform>();
        player = GameObject.Find("Tank");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = player.GetComponent<Transform>().position;

        Vector3 moveDirection = targetPosition - transform.position;

        rb.velocity = moveDirection.normalized * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
