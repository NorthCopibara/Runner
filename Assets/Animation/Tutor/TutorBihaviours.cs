using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorBihaviours : MonoBehaviour
{
    [SerializeField] GameObject firstText;
 
    Animator anim;
    int step;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void NextStep() 
    {
        if (step == 0)
            firstText.SetActive(false);

        step++;
        anim.SetTrigger($"Step_{step}");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
}
