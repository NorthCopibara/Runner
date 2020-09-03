using JokerGho5t.MessageSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameAnimate : MonoBehaviour
{
    public void StartGame() 
    {
        Message.Send("StartGame");
    }
}
