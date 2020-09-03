using JokerGho5t.MessageSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qbik.Game {
    public class Car 
    {
        private float speed = 5;
        private float dZ = 5;
        private float carNextPosition;

        private bool direction;

        private GameObject car;

        public Car(GameObject car) 
        {
            this.car = car;
        }

        public void Init(int health)
        {
            ControlSystem.fixedUpdate += FixedUpdateCar;
            carNextPosition = - dZ * health; //Привязать к жизням //Дефолтное расположение
            car.transform.position = new Vector3(car.transform.position.x, car.transform.position.y, carNextPosition);
        }

        public void SetCarSpeed(float speed) 
        {
            this.speed = speed;
        }

        public void SetState(int health, bool direction) //Зависит от количества жизней 
        {
            this.direction = direction; //Если false, то приближаемся, иначе отдаляемся
            carNextPosition = - dZ * health;
        }

        private void FixedUpdateCar()
        {
            if (direction)
            {
                if (car.transform.position.z >= carNextPosition)
                {
                    car.transform.position = new Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z - speed * Time.deltaTime);
                }
            }
            else 
            {
                if (car.transform.position.z <= carNextPosition)
                {
                    car.transform.position = new Vector3(car.transform.position.x, car.transform.position.y, car.transform.position.z + speed * Time.deltaTime);
                }
            }
        }

        public void Destroy()
        {
            ControlSystem.fixedUpdate -= FixedUpdateCar;
        }
    }
}
