using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.W)))
            {
                SceneManager.LoadScene("StartScene");
            }
        }
    }

}
