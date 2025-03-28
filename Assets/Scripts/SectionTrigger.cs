using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject[] roadSegments;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            // Pick a random segment from the array
            int randomIndex = Random.Range(0, roadSegments.Length);

            Vector3 spawnPosition = new Vector3(0, 0, transform.position.z + 80);
            Instantiate(roadSegments[randomIndex], spawnPosition, Quaternion.identity);
        }
    }
}