using System.Collections;
using Unity.VisualScripting;
//                             using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public bool GameOver = false;
    private bool isInvincible = false; // Tracks invincibility status
    public float invincibilityDuration = 3f; // Duration of invincibility in seconds

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GameOver = true;
        }
        else
        {
            GameOver = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && !isInvincible)
        {
            health -= 1;
            Debug.Log("Health: " + health);
            StartCoroutine(InvincibilityCooldown());
        }
    }

    private IEnumerator InvincibilityCooldown()
    {
        isInvincible = true; // Enable invincibility
        yield return new WaitForSeconds(invincibilityDuration); // Wait for the invincibility duration
        isInvincible = false; // Disable invincibility
    }
}