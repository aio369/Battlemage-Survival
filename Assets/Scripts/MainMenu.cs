using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject pauseButton;
    PauseManager pauseManager;

    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panel.activeInHierarchy == false)
            {
                OpenMenu();
            }
            else {
                CloseMenu();
            }
        }
    }

    public void CloseMenu()
    {
        pauseManager.UnPauseGame();
        panel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void OpenMenu()
    {
        pauseManager.PauseGame();
        panel.SetActive(true);
        pauseButton.SetActive(false);
    }
}
