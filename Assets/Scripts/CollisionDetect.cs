using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionDetect : MonoBehaviour
{
    private Animator playerAnimator;
    private Camera mainCamera;
    private GameObject Player;
    private GameObject Canvas;
    private GameObject fadeOut;
    private GameObject gameOverPanel;
    private Button restartButton;
    private Button mainMenuButton;
    private TMPro.TMP_Text result;
    [SerializeField] GameObject collisionFX;

    private void Start()
    {
        // Find the player object by tag
        GameObject playerModel = GameObject.FindGameObjectWithTag("PlayerModel");
        Player = GameObject.FindGameObjectWithTag("Player");
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        fadeOut = Canvas.transform.Find("FadeOut")?.gameObject;
        gameOverPanel = Canvas.transform.Find("GameOverPanel")?.gameObject;
        result = gameOverPanel?.transform.Find("Result")?.GetComponent<TMPro.TMP_Text>();

        if (playerModel != null)
        {
            playerAnimator = playerModel.GetComponent<Animator>();
        }

        mainCamera = Player.GetComponentInChildren<Camera>();

        if (gameOverPanel != null)
        {
            restartButton = gameOverPanel.transform.Find("RestartButton")?.GetComponent<Button>();
            mainMenuButton = gameOverPanel.transform.Find("MainMenuButton")?.GetComponent<Button>();
            gameOverPanel.SetActive(false); 

            if (restartButton != null)
                restartButton.onClick.AddListener(RestartGame);

            if (mainMenuButton != null)
                mainMenuButton.onClick.AddListener(GoToMainMenu);
        }
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
            yield return new WaitForSeconds(1);
            if(fadeOut != null)
            {
                fadeOut.SetActive(true);
            }
            yield return new WaitForSeconds(2);
            if (gameOverPanel != null)
            {
                if (result != null)
                {
                    result.text = "You Scored: " + MasterLevelInfo.boneCount;
                }
                gameOverPanel.SetActive(true);
            }
        }
    }

    // Function to restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Function to go back to the main menu
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Change "MainMenu" to your actual main menu scene name
    }
}
