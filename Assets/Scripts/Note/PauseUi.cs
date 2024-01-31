using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public GameObject pausePanel;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    public void TogglePauseUI(bool show)
    {
        pausePanel.SetActive(show);
    }
}
