using UnityEngine;

public class SegmentMove : MonoBehaviour
{
    public static float speed = 8f;  // tốc độ là static → giữ chung
    public static float acceleration = 0.02f; // cũng nên để static để dùng chung

    void Update()
    {
        speed += acceleration * Time.deltaTime;
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
