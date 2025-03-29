using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Find all active map segments and disable their SegmentMove script
            SegmentMove[] mapSegments = FindObjectsOfType<SegmentMove>();

            foreach (SegmentMove segment in mapSegments)
            {
                segment.enabled = false;
            }

            Debug.Log("Map movement stopped!");
        }
    }
}