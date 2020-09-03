using JokerGho5t.MessageSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qbik.Game {
    public class Car : MonoBehaviour
    {
        [SerializeField] private float speed;
        private float dZ = 5;
        private float carNextPosition;

        private bool direction;

        private void Awake()
        {
            Message.AddListener("GetCar", GetView);
        }

        public void GetView()
        {
            Message.RemoveListener("GetCar", GetView);
            MessageClass<Car>.SendEvent("CarView", this);
        }

        public void Init(int health)
        {
            ControlSystem.fixedUpdate += FixedUpdateCar;
            carNextPosition = - dZ * health; //Привязать к жизням //Дефолтное расположение
            transform.position = new Vector3(transform.position.x, transform.position.y, carNextPosition);
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
                if (transform.position.z >= carNextPosition)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.deltaTime);
                }
            }
            else 
            {
                if (transform.position.z <= carNextPosition)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag.Equals("Dog"))
            {
                Message.Send("Death");
            }
        }

        private void OnDestroy()
        {
            ControlSystem.fixedUpdate -= FixedUpdateCar;
        }
    }
}
