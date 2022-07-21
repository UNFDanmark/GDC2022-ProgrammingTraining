using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankScript : MonoBehaviour
{
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float jumpForce = 1f;
    public GameObject bullet;
    public GameObject model;
    public AudioClip canon;
    public AudioClip pewPistol;

    public float CooldownTimer;

    float lastShotTime = -99;
    Rigidbody rb;
    Transform transform;
    Animator modelAnimator;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        transform = gameObject.GetComponent<Transform>();
        audioSource =  gameObject.GetComponent<AudioSource>();
        modelAnimator = model.GetComponent<Animator>();

        // Ignorer collison mellem lag 6 (player) og lag 7 (bullet)
        Physics.IgnoreLayerCollision(6, 7);
    }

    // Update is called once per frame
    void Update()
    {
        Movehandler();
        JumpHandler();
        ShootHandler();
    }

    void JumpHandler()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up*jumpForce);
            audioSource.PlayOneShot(pewPistol);
        }
    }

    // skyd når der bliver klikket.
    void ShootHandler()
    {
        bool pressedShoot = Input.GetButton("Fire1");
        bool isCooldownDone = lastShotTime + CooldownTimer < Time.time;
        
        if (pressedShoot && isCooldownDone)
        {
            lastShotTime = Time.time;
            Instantiate(bullet, transform.position, transform.rotation);
            modelAnimator.SetTrigger("shoot");
            audioSource.PlayOneShot(canon);
        }
    }


    //Får tanken til at bevøæge sig.
    void Movehandler()
    {
        //Gem den gamle vertikale hastighed
        float oldUpSpeed = rb.velocity.y;

        //Bestem den fremadrettede hastighed
        Vector3 forwardMovement = speed * Input.GetAxis("Vertical") * transform.forward;

        //Bestem den nye bevægelsesvektor
        rb.velocity = new Vector3(forwardMovement.x, oldUpSpeed, forwardMovement.z);

        //rotér tanken
        transform.Rotate(transform.up * Input.GetAxis("Horizontal"));

        bool isAorDPressed = Input.GetAxis("Horizontal") != 0;
        bool isWorSPressed = Input.GetAxis("Vertical") != 0;

        if (isAorDPressed || isWorSPressed)
        {
            modelAnimator.SetBool("isDriving", true);
        } else
        {
            modelAnimator.SetBool("isDriving", false);
        }
    }
}
