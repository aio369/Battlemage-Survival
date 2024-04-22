using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinManager : MonoBehaviour
{
    [SerializeField] GameObject winMessagePanel;
    PauseManager pauseManager;
    [SerializeField] DataContainer dataContainer;
    [SerializeField] GameObject pauseButton;
    [SerializeField] FlagsTable flagsTable;

    private void Start()
    {
        pauseManager = GetComponent<PauseManager>();
    }
    public void Win(string stageID)
    {
        winMessagePanel.SetActive(true);
        pauseManager.PauseGame();
        pauseButton.SetActive(false);
        Flag flag = flagsTable.GetFlag(stageID);
        if (flag != null)
        {
            flag.state = true;
        }
    }
}
