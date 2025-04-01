using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField]public GameObject totalBonesText; 

    void Start()
    {
        int totalBones = PlayerPrefs.GetInt("TotalBones", 0);
        totalBonesText.GetComponent<TMPro.TMP_Text>().text = "Total Bones: " + totalBones;
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
