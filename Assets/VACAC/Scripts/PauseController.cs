using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public Text buttonText;

    private bool isPaused = false;

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            buttonText.text = "Resume";
        }
        else
        {
            Time.timeScale = 1f;
            buttonText.text = "Pause";
        }
    }
}