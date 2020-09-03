using UnityEngine;
using JokerGho5t.CustomEvents;

namespace JokerGho5t.MessageSystem
{
    public class GameEventListener : MonoBehaviour
    {
#region Public Variables

        /// <summary> UnityEvent executed when this listener has been triggered </summary>
        public StringEvent Event;

        /// <summary> Game event string value that will trigger this listener's callback </summary>
        public string GameEvent;

        /// <summary> Enables the execution of the Event callback every time a game event is received </summary>
        public bool ListenForAllGameEvents;

#endregion

#region Unity Methods

        private void Reset()
        {
            ListenForAllGameEvents = false;
            GameEvent = string.Empty;
            Event = new StringEvent();
        }

        private void Awake() { GameEvent = GameEvent.Trim(); }

        private void OnEnable() { RegisterListener(); }

        private void OnDisable() { UnregisterListener(); }

#endregion

#region Private Methods

        private void RegisterListener()
        {
            Message.AddListener<GameEventMessage>(OnMessage);
        }

        private void UnregisterListener()
        {
            Message.RemoveListener<GameEventMessage>(OnMessage);
        }

        private void OnMessage(GameEventMessage message)
        {
            if (ListenForAllGameEvents || GameEvent.Equals(message.EventName))
                InvokeEvent(message);
        }

        private void InvokeEvent(GameEventMessage message)
        {
            if (!message.HasGameEvent) return;
            if (Event == null) return;
            Event.Invoke(message.EventName);
        }

#endregion
    }
}
