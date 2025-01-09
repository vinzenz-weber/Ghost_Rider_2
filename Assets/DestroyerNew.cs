using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyerNew : MonoBehaviour
{
    private GameObject parentObject;

    void Start()
    {
        parentObject = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player") {
            //  Destroy(parentObject);
            StartCoroutine(Destroyer());
        }
    }

    IEnumerator Destroyer() {
        yield return new WaitForSeconds(2);
        Destroy(parentObject);
    }
}
