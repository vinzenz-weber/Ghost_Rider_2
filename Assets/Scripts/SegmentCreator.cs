using System.Collections;
using UnityEngine;

public class SegmentCreator : MonoBehaviour
{
public GameObject[] segment;

public GameObject StarterSegments;


[SerializeField]
int zPos = 144;

[SerializeField] 
//bool creatingSegment = false;

public CollisionChecker collisionChecker;

[SerializeField]int segmentNum;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if(collisionChecker.isColliding == true) {
            
            createSegment();
        }
    }

    public void createSegment() {
        
            segmentNum = Random.Range(0, segment.Length);
            Instantiate(segment[segmentNum], new Vector3(0, 0, zPos), Quaternion.identity);

        zPos += 48;

        collisionChecker.isColliding = false;


        
    }



    IEnumerator SegmentGen() {
        segmentNum = Random.Range(0, segment.Length);
        Instantiate(segment[segmentNum], new Vector3(0, 0, zPos), Quaternion.identity);

        zPos += 48;

        yield return new WaitForSeconds(4);
        //creatingSegment = false;


    }

    public void PlaceStartSegments ()
    {
        Instantiate(StarterSegments, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void DestroyStartSegments ()
    {
        Destroy(StarterSegments);
    }
}
