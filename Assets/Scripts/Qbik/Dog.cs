using JokerGho5t.MessageSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog 
{
    Rigidbody rbDog;
    Animator animDog;

    private GameObject dog;

    public Dog(GameObject dog) 
    {
        this.dog = dog;
    }

    public void Init(float speedAnim) 
    {
        Message.AddListener("JumpDog", Jump);
        Message.AddListener("FailDog", Fail);

        rbDog = dog.GetComponent<Rigidbody>();
        animDog = dog.GetComponent<Animator>();
        SetAnimSpeed(speedAnim);
        animDog.SetTrigger("Run");
    }

    public void Jump() 
    {
        rbDog.AddForce(dog.transform.up * 40);
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

    public void Destroy()
    {
        Message.RemoveListener("JumpDog", Jump); 
        Message.RemoveListener("FailDog", Fail);
    }
}
