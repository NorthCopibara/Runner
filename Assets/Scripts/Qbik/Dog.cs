using JokerGho5t.MessageSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    Rigidbody rbDog;
    Animator animDog;

    private void Awake()
    {
        Message.AddListener("GetDog", GetView);
    }

    public void GetView()
    {
        Message.RemoveListener("GetDog", GetView);
        MessageClass<Dog>.SendEvent("DogView", this);
    }

    public void Init(float speedAnim) 
    {
        Message.AddListener("JumpDog", Jump);
        Message.AddListener("FailDog", Fail);

        rbDog = GetComponent<Rigidbody>();
        animDog = GetComponent<Animator>();
        SetAnimSpeed(speedAnim);
        animDog.SetTrigger("Run");
    }

    public void Jump() 
    {
        rbDog.AddForce(transform.up * 40);
        animDog.SetTrigger("Jump");
    }

    public void Fail()
    {
        animDog.SetTrigger("Fail");
    }

    public void SetAnimSpeed(float speed) 
    {
        animDog.speed = speed;
    }

    private void OnDestroy()
    {
        Message.RemoveListener("JumpDog", Jump); 
        Message.RemoveListener("FailDog", Fail);
    }
}
