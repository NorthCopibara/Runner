using Qbik.Game.GUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qbik.Game
{
    public struct GameModel
    {
        public float paralacsSpeed;
        public float animDogSpeed;
        public int health;

        public float timeNextCheckMin;
        public float timeNextCheckMax;
        public float timeTuchCheckMin;
        public float timeTuchCheckMax;

        public Paralacs paralacs;
        public Dog dog;
        public Tuch tuch;
        public Car car;
        public Points points;

        public GameModel(float paralacsSpeed, float animDogSpeed, int health, 
                            float timeNextCheckMin, float timeNextCheckMax, float timeTuchCheckMin, float timeTuchCheckMax,
                            Paralacs paralacs, Dog dog, Tuch tuch, Car car, Points points)
        {
            this.paralacsSpeed = paralacsSpeed;
            this.animDogSpeed = animDogSpeed;
            this.health = health;

            this.timeNextCheckMin = timeNextCheckMin;
            this.timeNextCheckMax = timeNextCheckMax;
            this.timeTuchCheckMin = timeTuchCheckMin;
            this.timeTuchCheckMax = timeTuchCheckMax;

            this.paralacs = paralacs;
            this.dog = dog;
            this.tuch = tuch;
            this.car = car;
            this.points = points;
        }
    }
}
