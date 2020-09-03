using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button play;
    private void Start()
    {
        play.onClick.AddListener(() => { StartGame(); });
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Sound() 
    {
    }
}
