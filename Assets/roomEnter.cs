using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class roomEnter : MonoBehaviour
{
    void Update() {
        
    }
    void  OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Room1", LoadSceneMode.Single);
        }
    }
}
