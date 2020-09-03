using JokerGho5t.MessageSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qbik.Game
{
    public class Paralacs : MonoBehaviour
    {
        [SerializeField] private List<GameObject> tiles;
        [SerializeField] private float tagPosition;

        private List<Rigidbody> rb = new List<Rigidbody>();

        private float speed;

        private void Awake()
        {
            Message.AddListener("GetParalacs", GetView);
        }

        public void GetView()
        {
            Message.RemoveListener("GetParalacs", GetView);
            MessageClass<Paralacs>.SendEvent("ParalacsView", this);
        }

        public void Init(float _speed) 
        {
            speed = _speed;

            for (int i = 0; i < tiles.Count; i++)
            {
                Rigidbody rb;
                rb = tiles[i].GetComponent<Rigidbody>();
                this.rb.Add(rb);
            }
            Move();

            ControlSystem.fixedUpdate += FixedUpdateParalacs;
        }

        public void SetSpeed(float speed) 
        {
            this.speed = speed;
        }

        private void FixedUpdateParalacs()
        {
            Move();
            foreach (GameObject x in tiles)
            {
                if (x.transform.localPosition.z < -tagPosition * 2)
                {
                    x.transform.localPosition = new Vector3(x.transform.localPosition.x, x.transform.localPosition.y, tagPosition * 2 * (tiles.Count - 1));
                }
            }
        }

        private void Move()
        {
            if (rb != null)
            {
                foreach (Rigidbody x in rb)
                    x.velocity = new Vector3(x.velocity.x, x.velocity.y, -speed);
            }
        }

        private void Stope()
        {
            if (rb != null)
            {
                foreach (Rigidbody x in rb)
                    x.velocity = Vector3.zero;
            }
        }

        private void OnDestroy()
        {
            ControlSystem.fixedUpdate -= FixedUpdateParalacs;
        }
    }
}
