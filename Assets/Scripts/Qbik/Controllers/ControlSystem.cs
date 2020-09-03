using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Qbik.Game;
using Qbik.Game.Setup;
using JokerGho5t.MessageSystem;
using Qbik.Game.GUI;

namespace Qbik
{
    public class ControlSystem : MonoBehaviour
    {
        #region Deligate
        public delegate void UpdateController();
        public static event UpdateController update;

        public delegate void FixedUpdateController();
        public static event FixedUpdateController fixedUpdate;
        #endregion

        GameLogic gameLogic;
        GameModel gameModel;

        #region Start
        void Start()
        {
            Message.AddListener("StartGame", StartGame);
        }

        public void StartGame() 
        {
            Message.RemoveListener("StartGame", StartGame);
            gameModel = new GameModel();

            Message.AddListener<MessageClass<Tuch>>("TuchView", InitTuch);
            Message.Send("GetTuch");

            Message.AddListener<MessageClass<GameData>>("SetupGame", InitSetupGame);
            Message.Send("GetSetup");

            InitGame();
        }

        private bool CheckView()
        {
            if (gameModel.paralacs == null)
            {
                Debug.Log("Not found paralacs!");
                return false;
            }

            if (gameModel.dog == null)
            {
                Debug.Log("Not found dog!");
                return false;
            }

            return true;
        }

        public void InitTuch(MessageClass<Tuch> tuch) 
        {
            Message.RemoveListener<MessageClass<Tuch>>("TuchView", InitTuch);
            gameModel.tuch = tuch.param;
        }

        public void InitSetupGame(MessageClass<GameData> setupData)
        {
            gameModel.animDogSpeed = setupData.param.defoultSpeedAnimDog;
            gameModel.paralacsSpeed = setupData.param.defoultSpeedParalacs;
            gameModel.health = setupData.param.health;

            gameModel.points = setupData.param.points;
            gameModel.car = setupData.param.car;
            gameModel.paralacs = setupData.param.paralacs;
            gameModel.dog = setupData.param.dog;

            gameModel.timeNextCheckMax = setupData.param.timeNextCheckMax;
            gameModel.timeNextCheckMin = setupData.param.timeNextCheckMin;
            gameModel.timeTuchCheckMax = setupData.param.timeTuchCheckMax;
            gameModel.timeTuchCheckMin = setupData.param.timeTuchCheckMin;
        }

        private void InitGame() 
        {
            if (CheckView())
                gameLogic = new GameLogic(gameModel);
        }
        #endregion

        #region Update
        private void Update()
        {
            update?.Invoke();
        }

        private void FixedUpdate()
        {
            fixedUpdate?.Invoke();
        }
        #endregion

        private void OnDestroy()
        {
            gameLogic?.Destroy();
        }
    }
}
