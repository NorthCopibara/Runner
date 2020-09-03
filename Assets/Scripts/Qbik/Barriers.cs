using JokerGho5t.MessageSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qbik.Game {
    public class Barriers : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Dog"))
            {
                Message.Send("Death");
            }
        }
    }
}
