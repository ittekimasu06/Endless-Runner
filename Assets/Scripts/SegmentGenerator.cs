using System;
using System.Collections;
using UnityEngine;


public class SegmentGenerator : MonoBehaviour
{
    public GameObject[] segment;
    

    [SerializeField] int zPos = 80;
    [SerializeField] bool creatingSegment = false;
    [SerializeField] int segmentCount = 0;

    void Update()
    {
        if(creatingSegment == false)
        {
            creatingSegment = true;
            StartCoroutine(SegmentGen());
        }
    }
    

    IEnumerator SegmentGen()
    {
        segmentCount = UnityEngine.Random.Range(0, segment.Length);
        Instantiate(segment[segmentCount], new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += 80;
        yield return new WaitForSeconds(5);
        creatingSegment = false;
    }

}
