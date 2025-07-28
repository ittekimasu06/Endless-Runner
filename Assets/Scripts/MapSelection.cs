using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour
{
    private int index;
    private GameObject[] mapList;
    private GameObject confirmButton;
    private GameObject mapNameText;
    private GameObject highScoreText;

    // index trong scene list
    private int[] mapSceneIndexes = { 3, 4, 5 };
    private string[] mapNames = { "City", "Gas Town", "Forest" };

    private void Start()
    {
        confirmButton = GameObject.FindGameObjectWithTag("ConfirmButton");
        mapNameText = GameObject.FindGameObjectWithTag("MapNameText");
        highScoreText = GameObject.FindGameObjectWithTag("HighScoreText");

        index = PlayerPrefs.GetInt("MapSelected", 0);
        LoadMaps();
        UpdateUI();
    }

    private void LoadMaps()
    {
        mapList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            mapList[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject map in mapList)
        {
            map.SetActive(false);
        }

        if (mapList[index])
        {
            mapList[index].SetActive(true);
        }
    }

    public void ToggleLeft()
    {
        mapList[index].SetActive(false);
        index = (index - 1 + mapList.Length) % mapList.Length;
        mapList[index].SetActive(true);
        UpdateUI();
    }

    public void ToggleRight()
    {
        mapList[index].SetActive(false);
        index = (index + 1) % mapList.Length;
        mapList[index].SetActive(true);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (mapNameText != null)
        {
            string mapName = (index >= 0 && index < mapNames.Length) ? mapNames[index] : "Unknown";
            mapNameText.GetComponent<TMPro.TMP_Text>().text = mapName;
        }

        if (highScoreText != null)
        {
            string key = "HighScore_" + index;
            int highScore = PlayerPrefs.GetInt(key, 0);
            highScoreText.GetComponent<TMPro.TMP_Text>().text = "High Score: " + highScore.ToString();
        }
    }

    public void ConfirmButton()
    {
        PlayerPrefs.SetInt("MapSelected", index);
        SceneManager.LoadSceneAsync(mapSceneIndexes[index]);
    }

    public void BackButton()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
