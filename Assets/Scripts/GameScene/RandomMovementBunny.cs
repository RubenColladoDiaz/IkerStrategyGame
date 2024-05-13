using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovementBunny : MonoBehaviour
{
    public Vector2 minPosition = new Vector2(-8f, -5f);
    public Vector2 maxPosition = new Vector2(8f, 5f);

    public float tiempoEspera = 5f;

    void Start()
    {
        StartCoroutine(Movimiento());
    }

    IEnumerator Movimiento()
    {
        while (true)
        {
            float randomX = Random.Range(minPosition.x, maxPosition.x);
            float randomY = Random.Range(minPosition.y, maxPosition.y);
            Vector2 randomPosition = new Vector2(randomX, randomY);

            // Teleportar al objeto a la nueva posición aleatoria
            transform.position = randomPosition;

            yield return new WaitForSeconds(tiempoEspera);
        }
    }
}
