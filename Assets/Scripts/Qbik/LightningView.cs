using JokerGho5t.MessageSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningView : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Dog"))
        {
            Message.Send("AddHealth");
            gameObject.SetActive(false);
        }
    }
}
