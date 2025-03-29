using UnityEngine;

public class SegmentMove : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, -10) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
