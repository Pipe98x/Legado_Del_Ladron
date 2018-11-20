using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour {

    [SerializeField]
    private float speed, jumpForce;
    [SerializeField]
    private float starttime, endtime, swipeTime, swipeDistance, maxtimeSwipe, mindistanceSwipe;
    private Vector2 startposicion, endposicion;
    private Rigidbody2D rig;
    private Animator anim;
    private bool grounded, sliding, onLeftDoor, onRightDoor;
    private CapsuleCollider2D collider;
    private GameObject leftDoor, rigthDoor;
    [SerializeField]
    private NivelManager manager;
    [SerializeField]
    private AudioSource audio;
	// Use this for initialization
	void Start () {

        rig = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        collider = gameObject.GetComponent<CapsuleCollider2D>();
	}

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y < -6f)
        {
            manager.GameOver();
        }
        if (manager.iniciar)
        {
            if (rig.velocity.y == 0)
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }

            if (rig.velocity.y < 0)
            {
                anim.SetInteger("Estado", 4);
                audio.Stop();
            }
            else if (rig.velocity.y > 0)
            {
                anim.SetInteger("Estado", 2);
                audio.Stop();
            }

            if (rig.velocity.y == 0)
            {
                anim.SetInteger("Estado", 1);
                audio.Play();
            }

            if (sliding)
            {
                anim.SetInteger("Estado", 3);
                audio.Stop();
            }

            gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        

        if (Input.touchCount > 0 && manager.iniciar)
        {

            Touch touch = Input.GetTouch(0); // toma el primer toque y lo almacena en touch

            if (touch.phase == TouchPhase.Began)
            {  // cuando empieza a tcar la pantalla
                starttime = Time.time;              // almacena el momento en que la tocó
                startposicion = touch.position;     // almacena la posicion en que la tocó


            }
            else if (touch.phase == TouchPhase.Ended)
            { // cuando deja de tocar la pantalla
                endtime = Time.time;
                endposicion = touch.position;

                swipeDistance = (endposicion - startposicion).magnitude;
                swipeTime = endtime - starttime;

                if (swipeTime < maxtimeSwipe && swipeDistance > mindistanceSwipe)
                {
                    Vector2 distance = endposicion - startposicion;

                    if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
                    {
                        if (distance.x < 0)
                        {
                            Izquierda();
                        }
                        else
                        {
                            Derecha();
                        }
                    }
                    else
                    {
                        if (distance.y < 0)
                        {
                            Slide();
                        }
                        else
                        {
                            Jump();
                        }
                    }

                }

            }
        }
    }

    private void Jump()
    {
        if (grounded && !sliding)
        {
            rig.AddForce(Vector2.up * jumpForce);
        }
    }

    private void Slide()
    {
        if (grounded)
        {
            sliding = true;
            collider.direction = CapsuleDirection2D.Horizontal;
            collider.offset = new Vector2(0.01745274f, -0.1590944f);
            collider.size = new Vector2(0.6396891f, 0.3215004f);
            Invoke("StandUp", 1f);
        }
    }

    private void StandUp()
    {
        sliding = false;
        collider.direction = CapsuleDirection2D.Vertical;
        collider.offset = new Vector2(0.0512001f, 0f);
        collider.size = new Vector2(0.2443626f, 0.6399999f);
        Debug.Log("levantarse");
    }

    private void Izquierda()
    {
        if (onLeftDoor)
        {
            leftDoor.GetComponent<Animator>().SetInteger("Estado", 1);
            onLeftDoor = false;
        }
    }

    private void Derecha()
    {
        if (onRightDoor)
        {
            Debug.Log("Derecha");
            rigthDoor.GetComponent<Animator>().SetInteger("Estado", 1);
            onRightDoor = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Izquierda")
        {
            onLeftDoor = true;
            leftDoor = collision.gameObject;
        }

        if(collision.gameObject.tag == "Derecha")
        {
            onRightDoor = true;
            rigthDoor = collision.gameObject;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onRightDoor = false;
        onLeftDoor = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Mercader")
        {
            Debug.Log("Colission");
            manager.GameOver();
        }
    }
}
