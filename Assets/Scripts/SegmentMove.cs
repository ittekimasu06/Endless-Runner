using UnityEngine;

public class SegmentMove : MonoBehaviour
{
    public static float speed = 8f;  // Initial speed
    public float acceleration = 0.02f; // How much speed increases per second

    void Update()
    {
        speed += acceleration * Time.deltaTime;  // Increase speed gradually
        transform.position += new Vector3(0, 0, -speed) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
