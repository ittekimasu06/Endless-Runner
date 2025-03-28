using UnityEngine;

public class BoneCollect : MonoBehaviour
{
    [SerializeField] AudioSource boneFX;

    void OnTriggerEnter(Collider other)
    {
        boneFX.Play();
        this.gameObject.SetActive(false);
    }
}
