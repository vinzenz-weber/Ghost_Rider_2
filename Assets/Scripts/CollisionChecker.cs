using UnityEngine;

public class CollisionChecker : MonoBehaviour
{

public bool isColliding = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "SegmentEnd") {
            isColliding = true;
            Debug.Log("Colliding");
        }
    }
}
