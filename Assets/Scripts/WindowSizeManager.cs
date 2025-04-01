using UnityEngine;
using TMPro;
public class Options : MonoBehaviour
{
   public void WindowSizeDropdown(int index)
    {
        switch (index)
        {
            //case 0: fullscreen, case 1: windowed
            case 0:
                Screen.SetResolution(1920, 1080, true);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, false);
                break;
        }
    }
}
