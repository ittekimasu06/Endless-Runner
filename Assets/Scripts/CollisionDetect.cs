using System.Collections;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    private Animator playerAnimator;
    private Camera mainCamera;
    private GameObject Player;
    private GameObject Canvas;
    private GameObject fadeOut;
    [SerializeField] GameObject collisionFX;

    private void Start()
    {
        // Find the player object by tag
        GameObject playerModel = GameObject.FindGameObjectWithTag("PlayerModel");
        Player = GameObject.FindGameObjectWithTag("Player");
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        fadeOut = Canvas.transform.Find("FadeOut").gameObject;

        if (playerModel != null)
        {
            playerAnimator = playerModel.GetComponent<Animator>();
        }

        mainCamera = Player.GetComponentInChildren<Camera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(CollisionEnd(other));
    }

    IEnumerator CollisionEnd(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Stop all active map segments
            SegmentMove[] mapSegments = FindObjectsOfType<SegmentMove>();
            foreach (SegmentMove segment in mapSegments)
            {
                segment.enabled = false;
            }

            GameObject sfx = Instantiate(collisionFX, transform.position, Quaternion.identity);
            AudioSource audio = sfx.GetComponent<AudioSource>();
            audio.Play();

            Player.GetComponent<CubeMovement>().enabled = false;

            // Play the animation if animator is found
            if (playerAnimator != null)
            {
                playerAnimator.Play("corgi_AngryStart");
                Debug.Log("Animation Played!");
            }
            mainCamera.GetComponent<Animator>().Play("CollisionCam");
            yield return new WaitForSeconds(3);
            fadeOut.SetActive(true);
        }
    }
}
