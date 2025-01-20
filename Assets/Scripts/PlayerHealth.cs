using System.Collections;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public bool GameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(health <= 0) {
            GameOver = true;
        }
        if(health > 0 ) {
            GameOver = false;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy") {
            health -= 1;
            Debug.Log("Health: " + health);
        }
    }
}
