using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
     [Header("velocity")]
      public float velocidadMovimiento;
    private float velocidadInicial;
    public Vector2 direccionMovimiento;
   
    public AudioClip walkSound;
    public AudioClip runSound;

    private Rigidbody2D rb;
     [Header("Sounds")]
    private AudioSource audioSource;
    private Animator animator;

    //EstoyCorriendo
    private bool isRun;
    //Estoy caminando
    private bool isMove;
    //Declara que es la primera vez que se instancia el script
    private bool firstTime = true;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(PlayerPrefs.HasKey("FirstTimeKey"));
        // Comprueba si existe la Key de PlayerPrefs y asi saber si es el primer spawneo
        if (PlayerPrefs.HasKey("FirstTimeKey"))
        {
            firstTime = PlayerPrefs.GetInt("FirstTimeKey") == 1;
        }

        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        velocidadInicial = velocidadMovimiento;

        //Importante para que se aplique la posicion de la puerta de salida de la tienda solo cuando la scene actual sea la inicial.
        //Si esto no funciona correctamente, se spawnea al personaje siempre en la posicion de spawnPoint independientemente de la escena.
        if (!firstTime && SceneManager.GetActiveScene().name == "StartScene")
        {
            float spawnPointX = PlayerPrefs.GetFloat("SpawnPointX");
            float spawnPointY = PlayerPrefs.GetFloat("SpawnPointY");
            float spawnPointZ = PlayerPrefs.GetFloat("SpawnPointZ");
            rb.transform.position = new Vector3(spawnPointX, spawnPointY, spawnPointZ);
        }

        firstTime = false;
        // Crea la clave FirstTimeKey y si esta no es 1 la transforma a 0, en la proxima instancia del script la clave
        // ya existe y hace caso a nuestras posiciones de PlayerPrefs.
        PlayerPrefs.SetInt("FirstTimeKey", firstTime ? 1 : 0);
        PlayerPrefs.Save(); // Es importante guardar los cambios en PlayerPrefs
   
    }

    // Update is called once per frame
    void Update()
    {

        direccionMovimiento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; // Esto son valores de -1 a 1

        if (Input.GetAxisRaw("Horizontal") < 0.0f)
        {
            transform.localScale = new Vector3(-3.0f, 3.0f, 1.0f);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0.0f)
        {
            transform.localScale = new Vector3(3.0f, 3.0f, 1.0f);
        }

      
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            velocidadMovimiento = velocidadInicial * 2;

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {

            velocidadMovimiento = velocidadInicial;

        }
       
        //Sonidos Caminar
        if ((direccionMovimiento.x != 0 || direccionMovimiento.y != 0) && velocidadMovimiento != velocidadInicial * 2)
        {
            if(isRun){
                audioSource.Stop();
            }
            isMove=true;
            isRun=false;

             if(!audioSource.isPlaying && isMove){
                audioSource.clip = walkSound;
                audioSource.Play();
                audioSource.volume = 0.25f;
                
            }
            
        } else if ((direccionMovimiento.x != 0 || direccionMovimiento.y != 0 && velocidadMovimiento == velocidadInicial * 2))
        {
           
            if(isMove){
                
                audioSource.Stop();
               
            }
            isMove=false;
            isRun=true;
             if(!audioSource.isPlaying && isRun){
                    audioSource.clip = runSound;
                    audioSource.Play();
                }
        }else{
            isMove=false;
            isRun=false;
            audioSource.Stop();
           //Dani:Realmente esto de abajo es un experimiento que quiero comentar en grupo
           //StartCoroutine(DetenerCaminarDespuesDeEspera());
        }
        if (direccionMovimiento.x != 0 || direccionMovimiento.y != 0)
        {
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }

    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direccionMovimiento * velocidadMovimiento * Time.fixedDeltaTime);
        // Mover el RigidBody2D del jugador en la direccion especificada a X velocidad y se multiplica por el tiempo
        // para hacer que el movimiento sea independiente de la velocidad de fotogramas y suave incluso si la tasa de fotogramas fluct�a.
        // Esto ultimo es una frikada, se movia raro y un tio ha puesto eso en internet RAULLLLLLLLLL
    }

    //Dani:Para que no suene raro al parar y seguir hare esta sigüiente función
    IEnumerator DetenerCaminarDespuesDeEspera()
    {
        yield return new WaitForSeconds(0.2f);
        audioSource.Stop();
        //Debug.Log("Para sonido");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }


}
