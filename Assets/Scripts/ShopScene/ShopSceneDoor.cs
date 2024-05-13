using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopSceneDoor : MonoBehaviour
{
    public Transform spawnPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (Input.GetKey(KeyCode.DownArrow) || (Input.GetKey(KeyCode.S)))
            {
                PlayerPrefs.SetFloat("SpawnPointX", -7.48f);
                PlayerPrefs.SetFloat("SpawnPointY", 3.06f);
                PlayerPrefs.SetFloat("SpawnPointZ", 0f);

                SceneManager.LoadScene("StartScene");
            }

        }
    }

}
