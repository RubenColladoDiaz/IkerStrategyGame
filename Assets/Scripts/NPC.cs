using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    public Vector2 direccionMovimiento;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direccionMovimiento = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; // Esto son valores de -1 a 1

        if (Input.GetAxisRaw("Horizontal") < 0.0f)
        {
            transform.localScale = new Vector3(3.0f, 3.0f, 1.0f);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0.0f)
        {
            transform.localScale = new Vector3(-3.0f, 3.0f, 1.0f);
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
}
