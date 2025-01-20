using System.Collections;
using UnityEngine;

public class SegmentCreator : MonoBehaviour
{
public GameObject[] segment;

//public GameObject StarterSegments;


[SerializeField]
int zPos = 208;

[SerializeField] 
//bool creatingSegment = false;

public CollisionChecker collisionChecker;

[SerializeField]int segmentNum;

void Start() {
    zPos = 208;
}



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if(collisionChecker.isColliding == true) 
        {
            Debug.Log(zPos);


            createSegment();
        }
    }

    public void createSegment() {
        
            segmentNum = Random.Range(0, segment.Length);
            Instantiate(segment[segmentNum], new Vector3(0, -0.5f, zPos), Quaternion.identity);

        zPos += 52;

        collisionChecker.isColliding = false;


        
    }



    IEnumerator SegmentGen() {
        segmentNum = Random.Range(0, segment.Length);
        Instantiate(segment[segmentNum], new Vector3(0, 0, zPos), Quaternion.identity);

        zPos += 48;

        yield return new WaitForSeconds(4);
        //creatingSegment = false;


    }

    /*

    public void PlaceStartSegments ()
    {
        Instantiate(StarterSegments, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void DestroyStartSegments ()
    {
        Destroy(StarterSegments);
    }
    */
}
