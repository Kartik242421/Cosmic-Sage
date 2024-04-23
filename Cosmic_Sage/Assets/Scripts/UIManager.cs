using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject pauseMenu;
    bool isPaused = false;

    int score;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreDisplay();
        TogglePauseMenu(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = score.ToString();
    }

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;

        UpdateScoreDisplay();
    }

    void TogglePauseMenu(bool isPaused)
    {
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
        this.isPaused = isPaused;
    }

    public void TogglePause()
    {
        TogglePauseMenu(!isPaused);
    }
}