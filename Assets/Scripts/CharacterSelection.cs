using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    private int index;
    private GameObject[] characterList;
    public GameObject totalBonesText;
    private GameObject confirmButton;
    private GameObject PriceText;

    private int[] unlockCosts = { 0, 280, 360, 580, 720 }; //giá của từng nhân vật
    private bool[] isUnlocked;
    private Button button;

    void Start()
    {
        confirmButton = GameObject.FindGameObjectWithTag("ConfirmButton");
        button = confirmButton.GetComponent<Button>();
        PriceText = GameObject.FindGameObjectWithTag("Price");

        int totalBones = PlayerPrefs.GetInt("TotalBones", 0);
        totalBonesText.GetComponent<TMPro.TMP_Text>().text = "Total Bones: " + totalBones;

        //lòa các nhân vật được mở khóa
        isUnlocked = new bool[unlockCosts.Length];
        isUnlocked[0] = true; //mặc định corgi là free

        for (int i = 1; i < unlockCosts.Length; i++)
        {
            isUnlocked[i] = PlayerPrefs.GetInt("CharacterUnlocked_" + i, 0) == 1;
        }

        index = PlayerPrefs.GetInt("CharacterSelected", 0);
        LoadCharacters();
        
        UpdateConfirmButton();
    }

    private void LoadCharacters()
    {
        characterList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        if (characterList[index])
        {
            characterList[index].SetActive(true);
        }
    }

    public void ToggleLeft()
    {
        characterList[index].SetActive(false);
        index = (index - 1 + characterList.Length) % characterList.Length;
        characterList[index].SetActive(true);
        UpdateConfirmButton();
    }

    public void ToggleRight()
    {
        characterList[index].SetActive(false);
        index = (index + 1) % characterList.Length;
        characterList[index].SetActive(true);
        UpdateConfirmButton();
    }

    private void UpdateConfirmButton()
    {
        int totalBones = PlayerPrefs.GetInt("TotalBones", 0);

        if (isUnlocked[index])
        {
            button.interactable = true;
            PriceText.GetComponent<TMPro.TMP_Text>().text = "Unlocked";
        }
        else if (totalBones >= unlockCosts[index])
        {
            button.interactable = true;
            PriceText.GetComponent<TMPro.TMP_Text>().text = "Unlocked";
        }
        else
        {
            button.interactable = false;
            PriceText.GetComponent<TMPro.TMP_Text>().text = "Need " + unlockCosts[index] + " Bones to unlock!";
        }
    }

    public void ConfirmButton()
    {
        int totalBones = PlayerPrefs.GetInt("TotalBones", 0);

        if (!isUnlocked[index] && totalBones >= unlockCosts[index])
        {
            //trừ số bones đã mua nhân vật
            totalBones -= unlockCosts[index];
            PlayerPrefs.SetInt("TotalBones", totalBones);
            PlayerPrefs.SetInt("CharacterUnlocked_" + index, 1);
            isUnlocked[index] = true;
        }

        PlayerPrefs.SetInt("CharacterSelected", index);
        SceneManager.LoadSceneAsync(3);
    }

    public void BackButton()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
