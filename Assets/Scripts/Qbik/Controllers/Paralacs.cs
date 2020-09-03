using JokerGho5t.MessageSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qbik.Game
{
    public class Paralacs
    {
        private List<Rigidbody> rb = new List<Rigidbody>();
        private float speed;

        private ParalacsView view;

        public Paralacs(ParalacsView view) 
        {
            this.view = view;
        }

        public void Init(float _speed) 
        {
            speed = _speed;

            for (int i = 0; i < view.tiles.Count; i++)
            {
                Rigidbody rb;
                rb = view.tiles[i].GetComponent<Rigidbody>();
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
            foreach (GameObject x in view.tiles)
            {
                if (x.transform.localPosition.z < -view.tagPosition * 2)
                {
                    x.transform.localPosition = new Vector3(x.transform.localPosition.x, x.transform.localPosition.y, view.tagPosition * 2 * (view.tiles.Count - 1));
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

        public void Destroy()
        {
            ControlSystem.fixedUpdate -= FixedUpdateParalacs;
        }
    }
}
