using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JokerGho5t.MessageSystem
{
    public class GameEventMessage : Message
    {
        #region Constants

        private const string NO_GAME_EVENT = "None";

        #endregion

        #region Properties

        /// <summary>
        /// Returns TRUE if this message contains a reference to a custom Object
        /// </summary>
        public bool HasCustomObject => CustomObject != null;

        /// <summary>
        /// Returns TRUE if this message contains a game event string.
        /// If FALSE, then this message was used only to send a GameObject reference.
        /// </summary>
        public bool HasGameEvent => !EventName.Equals(NO_GAME_EVENT);

        /// <summary>
        /// Returns TRUE if this message contains a reference to the source GameObject that sent it
        /// </summary>
        public bool HasSource => Source != null;

        #endregion

        #region Public Variables

        /// <summary>
        /// The game event string sent with this message.
        /// If "None", then no game event is considered to have been passed with this message.
        /// </summary>
        public readonly string EventName;

        /// <summary>
        /// The source GameObject reference that sent this message.
        /// If null, then no GameObject reference was passed with this message.
        /// </summary>
        public readonly GameObject Source;

        /// <summary>
        /// A custom Object reference passed with this message
        /// </summary>
        public readonly Object CustomObject;

        #endregion

        #region Constuctors

        /// <summary> Initializes a new instance of the class with the passed game event string value </summary>
        /// <param name="gameEvent"> The game event string that will get sent with this message </param>
        public GameEventMessage(string gameEvent)
        {
            EventName = gameEvent;
            Source = null;
            CustomObject = null;
        }

        /// <summary>
        /// Initializes a new instance of the class with a GameObject reference and no game event string.
        /// <para/> This overload can be used to send only a GameObject reference.
        /// </summary>
        /// <param name="source"> The GameObject reference that will get sent with this message </param>
        public GameEventMessage(GameObject source)
        {
            EventName = NO_GAME_EVENT;
            Source = source;
            CustomObject = null;
        }

        /// <summary> Initializes a new instance of the class with a game event string and a source GameObject reference </summary>
        /// <param name="gameEvent"> The game event string that will get sent with this message </param>
        /// <param name="source"> The game object reference that will get sent with this message </param>
        public GameEventMessage(string gameEvent, GameObject source)
        {
            EventName = gameEvent;
            Source = source;
            CustomObject = null;
        }

        /// <summary> Initializes a new instance of the class with a source GameObject reference and a custom Object reference </summary>
        /// <param name="source"> The GameObject reference that will get sent with this message </param>
        /// <param name="customObject"> A custom Object reference that will get sent with this message </param>
        public GameEventMessage(GameObject source, Object customObject)
        {
            EventName = NO_GAME_EVENT;
            Source = source;
            CustomObject = customObject;
        }

        /// <summary> Initializes a new instance of the class with a game event string and a custom Object reference </summary>
        /// <param name="gameEvent"> The game event string that will get sent with this message </param>
        /// <param name="customObject"> A custom Object reference that will get sent with this message </param>
        public GameEventMessage(string gameEvent, Object customObject)
        {
            EventName = gameEvent;
            Source = null;
            CustomObject = customObject;
        }

        /// <summary> Initializes a new instance of the class with a game event string, a source GameObject reference and a custom Object reference </summary>
        /// <param name="gameEvent"> The game event string that will get sent with this message </param>
        /// <param name="source"> The GameObject reference that will get sent with this message </param>
        /// <param name="customObject"> A custom Object reference that will get sent with this message </param>
        public GameEventMessage(string gameEvent, GameObject source, Object customObject)
        {
            EventName = gameEvent;
            Source = source;
            CustomObject = customObject;
        }

        #endregion

        #region Static Methods

        /// <summary> Send a message with the passed game event string </summary>
        /// <param name="gameEvent"> The game event string sent with this message </param>
        public static void SendEvent(string gameEvent) { SendEvent(new GameEventMessage(gameEvent)); }

        /// <summary> Send a message with the passed source GameObject reference </summary>
        /// <param name="source"> The source game object reference that sent this message </param>
        public static void SendEvent(GameObject source) { SendEvent(new GameEventMessage(source)); }

        /// <summary> Send a message with a given game event string and a source GameObject reference </summary>
        /// <param name="gameEvent"> The game event string sent with this message </param>
        /// <param name="source"> The source game object reference that sent this message </param>
        public static void SendEvent(string gameEvent, GameObject source) { SendEvent(new GameEventMessage(gameEvent, source)); }

        /// <summary> Send a message with a given game event string and a custom Object reference </summary>
        /// <param name="gameEvent"> The game event string sent with this message </param>
        /// <param name="customObject"> A custom Object reference sent with this message </param>
        public static void SendEvent(string gameEvent, Object customObject) { SendEvent(new GameEventMessage(gameEvent, customObject)); }

        /// <summary> Send a message with a given source GameObject reference and a custom Object reference </summary>
        /// <param name="source"> The source game object reference that sent this message </param>
        /// <param name="customObject"> A custom Object reference sent with this message </param>
        public static void SendEvent(GameObject source, Object customObject) { SendEvent(new GameEventMessage(source, customObject)); }

        /// <summary> Send a message with a given game event string, a source GameObject reference and a custom Object reference </summary>
        /// <param name="gameEvent"> The game event string sent with this message </param>
        /// <param name="source"> The source game object reference that sent this message </param>
        /// <param name="customObject"> A custom Object reference sent with this message </param>
        public static void SendEvent(string gameEvent, GameObject source, Object customObject) { SendEvent(new GameEventMessage(gameEvent, source, customObject)); }

        /// <summary> Send a list of messages, each message with the given event string, a source GameObject reference and a custom Object reference </summary>
        /// <param name="gameEvents"> The game event strings sent with these messages </param>
        /// <param name="source"> The source game object reference that sent these messages </param>
        /// <param name="customObject"> A custom Object reference sent with these messages </param>
        public static void SendEvents(List<string> gameEvents, GameObject source = null, Object customObject = null)
        {
            if (gameEvents == null || gameEvents.Count == 0) return;
            foreach (string gameEvent in gameEvents)
                SendEvent(gameEvent, source, customObject);
        }

        private static void SendEvent(GameEventMessage gameEventMessage)
        {
            Send(gameEventMessage.EventName, gameEventMessage);
        }

        #endregion
    }
}
