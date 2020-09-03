using JokerGho5t.MessageSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Qbik.Game.Setup
{
    public class SetupGame : MonoBehaviour
    {
        [Header("Множитель коэффициента")]
        [SerializeField] private int health;
        [Header("Коэффициент (+5)")]
        [SerializeField] private float defoultSpeedParalacs;
        [Header("Коэффициент (+0,3)")]
        [SerializeField] private float defoultSpeedAnimDog;

        [Space (5)]
        [Header("Дефолтные параметры таймера")]
        [SerializeField] private float timeNextCheckMin;
        [SerializeField] private float timeNextCheckMax;
        [SerializeField] private float timeTuchCheckMin;
        [SerializeField] private float timeTuchCheckMax;

        [Space(5)]
        [Header("Text начисления очков")]
        [SerializeField] private Text textPoints;
        [SerializeField] private Text textX;
        [SerializeField] private List<GameObject> healthImage;

        private void Awake()
        {
            Message.AddListener("GetSetup", GetView);
        }

        public void GetView()
        {
            Points points = new Points(textPoints, textX, healthImage);
            GameData data = new GameData(health, defoultSpeedParalacs, defoultSpeedAnimDog,
                                        timeNextCheckMin, timeNextCheckMax, timeTuchCheckMin, timeTuchCheckMax, points);

            Message.RemoveListener("GetSetup", GetView);
            MessageClass<GameData>.SendEvent("SetupGame", data);
        }
    }

    public struct GameData
    {
        public int health;
        public float defoultSpeedParalacs;
        public float defoultSpeedAnimDog;

        public float timeNextCheckMin;
        public float timeNextCheckMax;
        public float timeTuchCheckMin;
        public float timeTuchCheckMax;

        public Points points;

        public GameData(int health, float defoultSpeedParalacs, float defoultSpeedAnimDog,
                        float timeNextCheckMin, float timeNextCheckMax, float timeTuchCheckMin, float timeTuchCheckMax,
                        Points points)
        {
            this.health = health;
            this.defoultSpeedParalacs = defoultSpeedParalacs;
            this.defoultSpeedAnimDog = defoultSpeedAnimDog;
            this.timeNextCheckMin = timeNextCheckMin;
            this.timeNextCheckMax = timeNextCheckMax;
            this.timeTuchCheckMin = timeTuchCheckMin;
            this.timeTuchCheckMax = timeTuchCheckMax;
            this.points = points;
        }
    }
}
