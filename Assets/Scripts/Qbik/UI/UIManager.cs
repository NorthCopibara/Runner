using JokerGho5t.MessageSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header ("Pause")]
    [SerializeField] Button pause;
    [SerializeField] Button play;
    [SerializeField] Button pauseMenu;
    [SerializeField] Button pauseRestart;
    [SerializeField] GameObject convasPause;

    [Header("GameOver")]
    [SerializeField] GameObject convasDeath;
    [SerializeField] Text points;
    [SerializeField] Button playNextGame;
    [SerializeField] Button menu;

    private void Start()
    {
        pause.onClick.AddListener(() => { PauseGame(); });
        play.onClick.AddListener(() => { StartGame(); });
        pauseRestart.onClick.AddListener(() => { ReStartGame(); });
        menu.onClick.AddListener(() => { GoMenu(); });
        pauseMenu.onClick.AddListener(() => { GoMenu(); });

        playNextGame.onClick.AddListener(() => { ReStartGame(); });

        Message.AddListener<MessageClass<DeathData>>("GameOverConvas", GameOverConvas);
    }

    public void PauseGame() 
    {
        Time.timeScale = 0;
        convasPause.SetActive(true);
    }

    public void StartGame() 
    {
        Time.timeScale = 1;
        convasPause.SetActive(false);
    }

    public void GameOverConvas(MessageClass<DeathData> setupData) 
    {
        Message.RemoveListener<MessageClass<DeathData>>("GameOverConvas", GameOverConvas);
        convasDeath.SetActive(true);
        points.text = $"Points: {setupData.param.pointsCount}";
    }

    public void ReStartGame() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void GoMenu() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}

public struct DeathData 
{
    public int pointsCount;

    public DeathData(int pointsCount) 
    {
        this.pointsCount = pointsCount;
    }
}
