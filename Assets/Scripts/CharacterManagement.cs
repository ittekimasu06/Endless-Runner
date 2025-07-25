using UnityEngine;

public class CharacterManagement : MonoBehaviour
{
    private Animator playerAnimator;
    private GameObject selectedCharacter;

    void Start()
    {

        int selectedIndex = PlayerPrefs.GetInt("CharacterSelected");

        string[] characterTags = { "PlayerCorgi", "PlayerChihuahua", "PlayerCur", "PlayerGermanShepherd", "PlayerPug" };

        if (selectedIndex >= 0 && selectedIndex < characterTags.Length)
        {
            selectedCharacter = GameObject.FindGameObjectWithTag(characterTags[selectedIndex]);
        }

        if (selectedCharacter != null)
        {
            playerAnimator = selectedCharacter.GetComponent<Animator>();

            if (playerAnimator != null)
            {
        
                string[] runningAnimations = { "corgi_Running", "chihuahua_Running", "cur_Running", "germanshepherd_Running01", "pug_Running" };
                playerAnimator.Play(runningAnimations[selectedIndex]);
            }
            else
            {
                Debug.LogError("Animator component not found on selected character.");
            }
        }
        else
        {
            Debug.LogError("Selected character not found in the scene.");
        }
    }
}
