using UnityEngine;

public class CollectableRotate : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 1f;

    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
}
