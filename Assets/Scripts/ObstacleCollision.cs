using UnityEngine;
using System.Collections;

public class ObstacleCollision : MonoBehaviour
{
    public float pushBackDistance = 2.0f;     // Distance to push back
    public float pushDuration = 0.4f;         // Duration for the pushback (seconds)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PushBack(other.transform));
        }
    }

    private IEnumerator PushBack(Transform player)
    {
        Vector3 startPosition = player.position;
        Vector3 targetPosition = player.position + new Vector3(0, 0, -pushBackDistance);
        float elapsedTime = 0f;

        while (elapsedTime < pushDuration)
        {
            // Smoothly interpolate the player's position over time
            player.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / pushDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the player reaches the exact target position
        player.position = targetPosition;
        Debug.Log("Push Back Complete!");
    }
}
