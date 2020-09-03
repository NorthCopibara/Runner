using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Qbik.Game.Save;
using Qbik;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button play;
    [SerializeField] Text record;

    private void Start()
    {
        play.onClick.AddListener(() => { StartGame(); });

        int pointRecord = 0;
        if (!ChekJson.ChekJsonData("SaveRecord"))
            pointRecord = GetJson<SaveData>.GetJsonData("SaveRecord")[0].recordPoints;

        record.text = $"Record points: {pointRecord}";
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Sound() 
    {
    }
}
