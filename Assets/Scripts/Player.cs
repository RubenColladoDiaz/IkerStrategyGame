using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float velocidadMovimiento;
    private float velocidadInicial;
    public Vector2 direccionMovimiento;
    public AudioClip walkSound;
    public AudioClip runSound;

    private Rigidbody2D rb;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        velocidadInicial = velocidadMovimiento;
    }

    // Update is called once per frame
    void Update()
    {

        direccionMovimiento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; // Esto son valores de -1 a 1

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            velocidadMovimiento = velocidadInicial * 2;
            if (direccionMovimiento.x != 0 || direccionMovimiento.y != 0)
            {
                audioSource.clip = runSound;
                audioSource.Play();
                Debug.Log("Suena correr");
            }

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {

            velocidadMovimiento = velocidadInicial;

        }
        else if (direccionMovimiento.x != 0 || direccionMovimiento.y != 0)
        {
            audioSource.clip = walkSound;
            audioSource.Play();
            Debug.Log("Suena caminar");
        }
        else
        {
            audioSource.Stop();
            Debug.Log("Para sonido");
        }

    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direccionMovimiento * velocidadMovimiento * Time.fixedDeltaTime);
        // Mover el RigidBody2D del jugador en la direccion especificada a X velocidad y se multiplica por el tiempo
        // para hacer que el movimiento sea independiente de la velocidad de fotogramas y suave incluso si la tasa de fotogramas fluctúa.
        // Esto ultimo es una frikada, se movia raro y un tio ha puesto eso en internet RAULLLLLLLLLL
    }
}
