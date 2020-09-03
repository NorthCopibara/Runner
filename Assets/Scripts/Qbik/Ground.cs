using JokerGho5t.MessageSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qbik.Game {
    public class Ground : MonoBehaviour
    {
        //Костыль на прыжек
        private bool state;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag.Equals("Dog") && state)
            {
                Message.Send("StartJump");
                state = false;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.transform.tag.Equals("Dog") && !state)
            {
                Message.Send("StopJump");
                state = true; 
            }
        }
    }
}
