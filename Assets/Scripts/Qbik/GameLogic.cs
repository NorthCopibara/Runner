using JokerGho5t.MessageSystem;
using Qbik.Game.GUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qbik.Game
{
    public class GameLogic
    {
        private Paralacs paralacs;
        private Dog dog;
        private Tuch tuch;
        private Car car;
        private Points points;

        private float defoultParalacsSpeed;
        private float defoultDogAnimSpeed;
        private int health;

        private int xPoints = 1;
        private int pointsCount;
        private int time;

        public GameLogic(GameModel model)
        {
            paralacs = model.paralacs;
            dog = model.dog;
            tuch = model.tuch;
            car = model.car;
            points = model.points;

            health = model.health;
            defoultParalacsSpeed = model.paralacsSpeed;
            defoultDogAnimSpeed = model.animDogSpeed;

            if (!CheckParam())
                return;

            paralacs.Init(defoultParalacsSpeed + 5 * health);
            dog.Init(defoultDogAnimSpeed + 0.3f * health);
            car.Init(model.health);
            points.Init();

            ControlSystem.fixedUpdate += TimePoint;

            Message.AddListener("MissClick", MissClick);
            Message.AddListener("GoodClick", GoodClick);
            Message.AddListener("Death", Death);

            tuch.StartTuch(model.timeNextCheckMax, model.timeNextCheckMin,
                           model.timeTuchCheckMax, model.timeTuchCheckMin);
        }

        private bool CheckParam() 
        {
            if (paralacs == null || dog == null || tuch == null || points == null)
            {
                Debug.Log("Not found params");
                return false;
            }
            return true;
        }

        private void AddPoints() 
        {
            if (time > 2) 
            {
                time = 0;
                pointsCount += 10 * xPoints; //Поставил просто дефолтный множитель
                points.SetPoints(pointsCount);
            }
        }

        public void TimePoint() 
        {
            time++;
            AddPoints();
        }

        public void GoodClick() 
        {
            switch (xPoints) 
            {
                case 1:
                    xPoints = 2;
                    break;
                case 2:
                    xPoints = 4;
                    break;
                case 4:
                    xPoints = 8;
                    break;
            }
            points.SetX(xPoints);
        }

        public void MissClick() 
        {
            health--;
            points.SetX(1);
            if (health <= 0)
            {
                Message.Send("FailDog");
                paralacs.SetSpeed(0);
                tuch.StopGame();
                car.SetCarSpeed(15);
            }
            else 
            {
                Debug.Log("Miss");
                paralacs.SetSpeed(defoultParalacsSpeed + 5 * health);
                dog.SetAnimSpeed(defoultDogAnimSpeed + 0.3f * health);
                //Смена состояния машины
            }
            points.SetHealthImage(health - 1);
            car.SetState(health, false);
        }

        public void Death() 
        {
            Time.timeScale = 0;
            DeathData deathData = new DeathData(pointsCount);
            MessageClass<DeathData>.SendEvent("GameOverConvas", deathData);
            Debug.Log("Death");
        }

        public void Destroy() 
        {
            ControlSystem.fixedUpdate -= TimePoint;

            Message.RemoveListener("MissClick", MissClick);
            Message.RemoveListener("GoodClick", GoodClick);
            Message.RemoveListener("Death", Death);
        }
    }
}
