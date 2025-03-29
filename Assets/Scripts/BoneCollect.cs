using UnityEngine;

public class BoneCollect : MonoBehaviour
{
    [SerializeField] private GameObject boneFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            Debug.Log("Bone collected!");

            GameObject sfx = Instantiate(boneFX, transform.position, Quaternion.identity);
            AudioSource audio = sfx.GetComponent<AudioSource>();
            audio.Play();

            MasterLevelInfo.boneCount++;

            Destroy(sfx, audio.clip.length);
            Destroy(gameObject);
        
        }
    }
}
