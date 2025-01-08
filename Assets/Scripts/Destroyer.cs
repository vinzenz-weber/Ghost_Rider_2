using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    private string parentName;
    // Update is called once per frame
    void Update()
    {
        parentName = transform.name;
        StartCoroutine(DestroySegment());
    }

    IEnumerator DestroySegment() {
        if(parentName == "Segment(Clone)") {
            yield return new WaitForSeconds(10);
            Destroy(gameObject);
        }
    }
}
