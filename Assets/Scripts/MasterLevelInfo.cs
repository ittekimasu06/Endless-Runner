using UnityEngine;

public class MasterLevelInfo : MonoBehaviour
{
    public static int boneCount = 0;
    [SerializeField] GameObject boneDisplay;
    
    void Update()
    {
        boneDisplay.GetComponent<TMPro.TMP_Text>().text = "Bones: " + boneCount;
    }
}
