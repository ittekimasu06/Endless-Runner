using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour
{
    private int index;
    private GameObject[] mapList;
    private GameObject confirmButton;
    private GameObject mapNameText;

    // index trong scene list
    private int[] mapSceneIndexes = { 3, 4, 5 };

    private void Start()
    {
        confirmButton = GameObject.FindGameObjectWithTag("ConfirmButton");
        mapNameText = GameObject.FindGameObjectWithTag("MapNameText");

        index = PlayerPrefs.GetInt("MapSelected", 0);
        LoadMaps();
        UpdateUI();
    }

    private void LoadMaps()
    {
        mapList = new GameObject[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            mapList[i] = transform.GetChild(i).gameObject;
        }

        foreach(GameObject map in mapList)
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
            mapNameText.GetComponent<TMPro.TMP_Text>().text = "Map " + (index + 1);
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
