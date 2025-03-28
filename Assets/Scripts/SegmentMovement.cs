using UnityEngine;
using System.Collections;

public class LevelControl : MonoBehaviour
{
    public GameObject[] segments; // Danh sach cac segment
    public float speed = 10f; // Toc do di chuyen cua segment
    public float segmentLength = 80f; // Do dai cua moi segment

    private float spawnZ = 0f;
    private int currentIndex = 0;
    private GameObject currentSegment;

    private void Start()
    {
        // Spawn segment dau tien
        SpawnSegment();
    }

    private void Update()
    {
        // Di chuyen segment ve phia nguoi choi
        if (currentSegment != null)
        {
            currentSegment.transform.Translate(Vector3.back * speed * Time.deltaTime);

            // Kiem tra khi den giua segment
            if (currentSegment.transform.position.z <= spawnZ - (segmentLength / 2))
            {
                SpawnSegment();
            }

            // Huy segment cu sau 3 giay khi di qua
            if (currentSegment.transform.position.z <= -segmentLength)
            {
                Destroy(currentSegment, 3f);
            }
        }
    }

    private void SpawnSegment()
    {
        currentSegment = Instantiate(segments[currentIndex % segments.Length], new Vector3(0, 0, spawnZ), Quaternion.identity);
        spawnZ += segmentLength;
        currentIndex++;
    }
}
