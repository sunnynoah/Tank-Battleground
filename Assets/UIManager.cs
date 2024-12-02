using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider p1HealthDisp;
    public Slider p2HealthDisp;

    public HealthManager p1Health;
    public HealthManager p2Health;

    [Header("Game Ending")]
    public GameObject uiParent;
    public GameObject endParent;

    public TextMeshProUGUI winText;

    [Header("Buttons")]
    public Button rematchButton;
    public Button quitButton;

    private void Start()
    {
        Cursor.visible = false;

        rematchButton.onClick.AddListener(StartRematch);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        p1HealthDisp.value = p1Health.health / 100;
        p2HealthDisp.value = p2Health.health / 100;
    }

    public void EndGame(string winner)
    {
        Cursor.visible = true;
        winText.text = $"{winner.ToUpper()} WON THE GAME!";
        uiParent.SetActive(false);
        endParent.SetActive(true);
    }

    void StartRematch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
