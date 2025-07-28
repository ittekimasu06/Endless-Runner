using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionDetect : MonoBehaviour
{
    private Animator playerAnimator;
    private GameObject Player;
    private GameObject Canvas;
    private GameObject fadeOut;
    private GameObject gameOverPanel;
    private Button restartButton;
    private Button mainMenuButton;
    private GameObject MainCamera;
    private TMPro.TMP_Text result;
    private TMPro.TMP_Text highScoreText; // Thêm để hiển thị high score
    [SerializeField] GameObject collisionFX;

    private bool hasCollided = false;

    void Start()
    {
        int selectedIndex = PlayerPrefs.GetInt("CharacterSelected");
        string[] characterTags = { "PlayerCorgi", "PlayerChihuahua", "PlayerCur", "PlayerGermanShepherd", "PlayerPug" };

        if (selectedIndex >= 0 && selectedIndex < characterTags.Length)
        {
            GameObject playerModel = GameObject.FindGameObjectWithTag(characterTags[selectedIndex]);
            if (playerModel != null)
            {
                playerAnimator = playerModel.GetComponent<Animator>();
            }
        }

        Player = GameObject.FindGameObjectWithTag("Player");
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        fadeOut = Canvas.transform.Find("FadeOut")?.gameObject;
        gameOverPanel = Canvas.transform.Find("GameOverPanel")?.gameObject;
        result = gameOverPanel?.transform.Find("Result")?.GetComponent<TMPro.TMP_Text>();
        highScoreText = gameOverPanel?.transform.Find("HighScoreText")?.GetComponent<TMPro.TMP_Text>();

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
        if (!hasCollided && other.CompareTag("Player"))
        {
            hasCollided = true;
            StartCoroutine(CollisionEnd());
        }
    }

    IEnumerator CollisionEnd()
    {
        foreach (SegmentMove segment in FindObjectsOfType<SegmentMove>())
        {
            segment.enabled = false;
        }

        Instantiate(collisionFX, transform.position, Quaternion.identity)
            .GetComponent<AudioSource>()?.Play();

        if (Player != null)
        {
            Player.GetComponent<CubeMovement>().enabled = false;
        }

        int selectedIndex = PlayerPrefs.GetInt("CharacterSelected");
        string[] angryAnimations = {
            "corgi_AngryStart",
            "chihuahua_AngryStart",
            "cur_AngryStart",
            "germanshepherd_AngryStart",
            "pug_AngryStart"
        };

        if (playerAnimator != null && selectedIndex >= 0 && selectedIndex < angryAnimations.Length)
        {
            playerAnimator.Play(angryAnimations[selectedIndex]);
        }

        if (MainCamera != null)
        {
            MainCamera.GetComponent<Animator>().Play("CollisionCam");
        }

        yield return new WaitForSeconds(1);
        if (fadeOut != null) fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            int totalBones = PlayerPrefs.GetInt("TotalBones", 0);
            totalBones += MasterLevelInfo.boneCount;
            PlayerPrefs.SetInt("TotalBones", totalBones);

            // Lưu high score theo map
            int mapIndex = PlayerPrefs.GetInt("SelectedMapIndex", 0);
            string key = "HighScore_" + mapIndex;
            int previousHighScore = PlayerPrefs.GetInt(key, 0);
            if (MasterLevelInfo.boneCount > previousHighScore)
            {
                PlayerPrefs.SetInt(key, MasterLevelInfo.boneCount);
            }

            PlayerPrefs.Save();

            if (result != null)
            {
                result.text = "You Scored: " + MasterLevelInfo.boneCount;
            }

            if (highScoreText != null)
            {
                highScoreText.text = "High Score: " + PlayerPrefs.GetInt(key, 0);
            }

            MasterLevelInfo.boneCount = 0;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
