using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Qbik.Game
{
    public class Points
    {
        private Text pointsText;
        private Text xText;

        private List<GameObject> healthImage;

        public Points(Text pointsText, Text xText, List<GameObject> healthImage) 
        {
            this.pointsText = pointsText;
            this.xText = xText;
            this.healthImage = healthImage;
        }

        public void Init() 
        {
            xText.text = "x1";
            pointsText.text = "0";
        }

        public void SetHealthImage(int health) 
        {
            switch (health) 
            {
                case 0:
                    foreach (var x in healthImage)
                        x.SetActive(false);
                    break;
                case 1:
                    healthImage[1].SetActive(false);
                    healthImage[2].SetActive(false);
                    break;
                case 2:
                    healthImage[1].SetActive(true);
                    healthImage[2].SetActive(false);
                    break;
                case 3:
                    foreach (var x in healthImage)
                        x.SetActive(true);
                    break;
            }
        }

        public void SetPoints(int points) 
        {
            pointsText.text = $"{points}";
        }

        public void SetX(int x) 
        {
            xText.text = $"x{x}";
        }
    }
}
