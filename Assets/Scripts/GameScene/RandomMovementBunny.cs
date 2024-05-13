using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovementBunny : MonoBehaviour
{
    private Vector2[] posicionesRandom = new Vector2[]
    {
        new Vector2(-4.15f, -1.69f),
        new Vector2(-6.39f, -0.96f),
        new Vector2(-2.16f, -0.96f),
        new Vector2(-2.78f, -2.99f),
        new Vector2(-7.47f, -3.88f), // PUNTOS PROVISIONALES, HABRA QUE HACERLOS RANDOM DENTRO DE UN RANGO
        new Vector2(-5.94f, -2.15f),
        new Vector2(-4.15f, -1.69f)
    };

    public float tiempoEspera = 5f;

    void Start()
    {
        StartCoroutine(Movimiento());
    }

    IEnumerator Movimiento()
    {
        while (true)
        {

            foreach (Vector2 position in posicionesRandom)
            {
                transform.position = position;

                yield return new WaitForSeconds(tiempoEspera);
            }

        }
    }
}
