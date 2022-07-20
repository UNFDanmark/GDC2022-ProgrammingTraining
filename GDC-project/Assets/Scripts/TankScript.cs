using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankScript : MonoBehaviour
{
    public float speed = 1f;
    public float turnSpeed = 1f;
    public GameObject bullet;

    public float CooldownTimer;

    float lastShotTime = -99;
    Rigidbody rb;
    Transform transform;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        transform = gameObject.GetComponent<Transform>();

        // Ignorer collison mellem lag 6 (player) og lag 7 (bullet)
        Physics.IgnoreLayerCollision(6, 7);
    }

    // Update is called once per frame
    void Update()
    {

        Movehandler();

        ShootHandler();
    }

    // skyd n�r der bliver klikket.
    void ShootHandler()
    {

        

        bool pressedShoot = Input.GetButton("Fire1");
        bool isCooldownDone = lastShotTime + CooldownTimer < Time.time;
        
        if (pressedShoot && isCooldownDone)
        {
            lastShotTime = Time.time;
            
            Instantiate(bullet, transform.position, transform.rotation);
        }


    }


    //F�r tanken til at bev��ge sig.
    void Movehandler()
    {
        //Gem den gamle vertikale hastighed
        float oldUpSpeed = rb.velocity.y;

        //Bestem den fremadrettede hastighed
        Vector3 forwardMovement = speed * Input.GetAxis("Vertical") * transform.forward;

        //Bestem den nye bev�gelsesvektor
        rb.velocity = new Vector3(forwardMovement.x, oldUpSpeed, forwardMovement.z);

        //rot�r tanken
        transform.Rotate(transform.up * Input.GetAxis("Horizontal"));
    }
}
