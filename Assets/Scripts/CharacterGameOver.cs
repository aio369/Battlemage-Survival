using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    [SerializeField] GameObject weaponParent;
    [SerializeField] GameObject pauseButton;

    public void GameOver()
    {
        Debug.Log("Game Over");
        GetComponent<PlayerMove>().enabled = false;
        gameOverPanel.SetActive(true);
        weaponParent.SetActive(false);
        pauseButton.SetActive(false);
    }
}
