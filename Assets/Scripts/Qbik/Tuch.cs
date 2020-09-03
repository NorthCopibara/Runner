using JokerGho5t.MessageSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Qbik.Game.GUI
{
    public class Tuch : MonoBehaviour
    {
        [SerializeField] Button leftTuch;
        [SerializeField] Button rightTuch;
        [SerializeField] Button jumpTuch;

        [SerializeField] GameObject checkLeft;
        [SerializeField] GameObject checkRight;
        [SerializeField] GameObject bangLeft;
        [SerializeField] GameObject bangRight;

        private bool leftButtonIsActive;
        private bool rightButtonIsActive;
        private WhatButton whatButton;

        private float timeNextCheckMin;
        private float timeNextCheckMax;

        private float timeTuchCheckMin;
        private float timeTuchCheckMax;

        private void Awake()
        {
            Message.AddListener("GetTuch", GetView);
        }

        public void GetView()
        {
            Message.RemoveListener("GetTuch", GetView);
            MessageClass<Tuch>.SendEvent("TuchView", this);
        }

        public void StartTuch(float timeNextCheckMin, float timeNextCheckMax, float timeTuchCheckMin, float timeTuchCheckMax)
        {
            if (!ChecUiElements())
                return;

            leftTuch.onClick.AddListener(() => { Left(); });
            rightTuch.onClick.AddListener(() => { Right(); });
            jumpTuch.onClick.AddListener(() => { JumpTuch(); });

            Message.AddListener("StopJump", StopJump);
            Message.AddListener("StartJump", StartJump);

            this.timeNextCheckMin = timeNextCheckMin;
            this.timeNextCheckMax = timeNextCheckMax;
            this.timeTuchCheckMin = timeTuchCheckMin;
            this.timeTuchCheckMax = timeTuchCheckMax;

            StartCoroutine(NextState(RandomNextTime()));
        }

        public void StopJump() 
        {
            jumpTuch.interactable = false;
        }

        public void StartJump()
        {
            jumpTuch.interactable = true;
        }

        public void JumpTuch() 
        {
            Message.Send("JumpDog");
        }

        public void SetTimeCheck(float timeNextCheckMin, float timeNextCheckMax) 
        {
            this.timeNextCheckMin = timeNextCheckMin;
            this.timeNextCheckMax = timeNextCheckMax;
        }

        public void SetTimeTuch(float timeTuchCheckMin, float timeTuchCheckMax) 
        {
            this.timeTuchCheckMin = timeTuchCheckMin;
            this.timeTuchCheckMax = timeTuchCheckMax;
        }

        private bool ChecUiElements()
        {
            if (leftTuch == null || rightTuch == null || checkLeft == null || checkRight == null)
            {
                Debug.Log("Not found UI elements");
                return false;
            }
            return true;
        }

        private void Left()
        {
            if (!leftButtonIsActive)
                Message.Send("MissClick");
            else
            {
                Message.Send("GoodClick");
                leftButtonIsActive = false;
                checkLeft.SetActive(false);
                bangLeft.SetActive(true);
                StartCoroutine(Bang());
            }
        }

        private void Right()
        {
            if (!rightButtonIsActive)
                Message.Send("MissClick");
            else 
            {
                Message.Send("GoodClick");
                rightButtonIsActive = false;
                checkRight.SetActive(false);
                bangRight.SetActive(true);
                StartCoroutine(Bang());
            }
        }

        private float RandomNextTime()
        {
            float time = Random.Range(timeNextCheckMin, timeNextCheckMax);

            return time;
        }

        private float RandomCheckTime()
        {
            float time = Random.Range(timeTuchCheckMin, timeTuchCheckMax); 

            return time;
        }

        private WhatButton RandomButton()
        {
            int rand = Random.Range(0, 1000);

            if (rand < 500)
                return WhatButton.Left;
            else
                return WhatButton.Right;
        }

        IEnumerator NextState(float time)
        {
            yield return new WaitForSeconds(time);
            whatButton = RandomButton();
            StartCoroutine(ActiveState(RandomCheckTime()));
        }

        IEnumerator ActiveState(float time)
        {
            switch (whatButton)
            {
                case WhatButton.Left:
                    checkLeft.SetActive(true);
                    leftButtonIsActive = true;
                    break;
                case WhatButton.Right:
                    checkRight.SetActive(true);
                    rightButtonIsActive = true;
                    break;
            }

            yield return new WaitForSeconds(time);

            if (leftButtonIsActive == true || rightButtonIsActive == true)
                Message.Send("MissClick"); //Не успел нажать

            leftButtonIsActive = false;
            rightButtonIsActive = false;
            checkLeft.SetActive(false);
            checkRight.SetActive(false);

            StartCoroutine(NextState(RandomNextTime()));
        }

        IEnumerator Bang() 
        {
            yield return new WaitForSeconds(1f);
            bangLeft.SetActive(false);
            bangRight.SetActive(false);
        }

        public void StopGame() 
        {
            StopAllCoroutines();
        }

        private void OnDestroy()
        {
            Message.RemoveListener("StopJump", StopJump);
            Message.RemoveListener("StartJump", StartJump);
        }
    }

    enum WhatButton
    {
        Left, Right, Middle
    }
}