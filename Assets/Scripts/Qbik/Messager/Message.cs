using System;
using System.Collections.Generic;

namespace JokerGho5t.MessageSystem
{
    public class Message
    {
        protected Message() { }

        #region Static Properties

        /// <summary> The handlers database </summary>
        private static readonly Dictionary<string, List<Delegate>> Handlers = new Dictionary<string, List<Delegate>>();

        #endregion

        #region Static Methods

        /// <summary> Adds a listener that triggers the given callback when the message with the given name is received </summary>
        /// <param name="messageName"> The message name that will be listened to </param>
        /// <param name="callback"> The callback that will be triggered when the message is received </param>
        public static void AddListener(string messageName, Action callback) { RegisterListener(messageName, callback); }

        /// <summary> Adds a listener that triggers the given callback when a message of the given type is received </summary>
        /// <typeparam name="T"> The message type that will be listened to </typeparam>
        /// <param name="callback"> The callback that will be triggered when the message is received </param>
        public static void AddListener<T>(Action<T> callback) where T : Message { RegisterListener(typeof(T).ToString(), callback); }

        /// <summary> Adds a listener that triggers the given callback when a message of the given type and name is received </summary>
        /// <typeparam name="T"> The message type that will be listened to </typeparam>
        /// <param name="messageName"> The message name that will be listened to </param>
        /// <param name="callback"> The callback that is triggered when the message is received </param>
        public static void AddListener<T>(string messageName, Action<T> callback) where T : Message { RegisterListener(typeof(T) + messageName, callback); }

        /// <summary> Removes a listener that would trigger the given callback when a message with the given name is received </summary>
        /// <param name="messageName"> The message name that is being listened to </param>
        /// <param name="callback"> The callback that is triggered when the message is received </param>
        public static void RemoveListener(string messageName, Action callback) { UnregisterListener(messageName, callback); }

        /// <summary> Removes a listener that would trigger the given callback when a message of the given type is received </summary>
        /// <typeparam name="T"> The message type that is being listened to </typeparam>
        /// <param name="callback"> The callback that is triggered when the message is received </param>
        public static void RemoveListener<T>(Action<T> callback) where T : Message { UnregisterListener(typeof(T).ToString(), callback); }

        /// <summary> Removes a listener that would trigger the given callback when a message of the given type is received </summary>
        /// <typeparam name="T"> The message type that is being listened to </typeparam>
        /// <param name="messageName"> The message name that is being listened to </param>
        /// <param name="callback"> The callback that is triggered when the message is received </param>
        public static void RemoveListener<T>(string messageName, Action<T> callback) where T : Message { UnregisterListener(typeof(T) + messageName, callback); }

        /// <summary> Sends a message of the given name </summary>
        /// <param name="messageName"> The name of the message </param>
        public static void Send(string messageName) { SendMessage<Message>(messageName, null); }

        /// <summary> Sends a message of the given type </summary>
        /// <typeparam name="T"> The type of the message </typeparam>
        /// <param name="message"> The instance of the message </param>
        public static void Send<T>(T message) where T : Message { SendMessage(typeof(T).ToString(), message); }

        /// <summary> Sends a message of the given name and type </summary>
        /// <typeparam name="T"> The type of the message </typeparam>
        /// <param name="messageName"> The name of the message </param>
        /// <param name="message"> The instance of the message </param>
        public static void Send<T>(string messageName, T message) where T : Message { SendMessage(typeof(T) + messageName, message); }

        private static void RegisterListener(string messageName, Delegate callback)
        {
            if (callback == null) //check that the passed callback is not null
            {
                return; //stop here
            }

            if (!Handlers.ContainsKey(messageName)) //check that this messageName has not been added to the handlers database
                Handlers.Add(messageName, new List<Delegate>()); //create a new entry in the handlers database with the given messageName
            List<Delegate> messageHandlers = Handlers[messageName]; //create a new list of Delegates so that we can add the callback
            messageHandlers.Add(callback); //add the callback
        }

        private static void UnregisterListener(string messageName, Delegate callback)
        {
            if (!Handlers.ContainsKey(messageName)) return;
            List<Delegate> messageHandlers = Handlers[messageName]; //create a list of delegates in order to be able to search through it
            Delegate messageHandler = messageHandlers.Find(x => x.Method == callback.Method && x.Target == callback.Target); //look for the callback
            if (messageHandler == null) return;
            messageHandlers.Remove(messageHandler); //remove the callback
        }

        private static void SendMessage<T>(string messageName, T e) where T : Message
        {
            if (!Handlers.ContainsKey(messageName)) return;

            List<Delegate> messageHandlers = Handlers[messageName];
            Delegate[] handlers = messageHandlers.ToArray();

            foreach (Delegate messageHandler in handlers)
            {
                if (messageHandler.GetType() != typeof(Action<T>) && messageHandler.GetType() != typeof(Action)) continue;

                if (typeof(T) == typeof(Message))
                {
                    var action = (Action)messageHandler;
                    action();
                }
                else
                {
                    var action = (Action<T>)messageHandler;
                    action(e);
                }
            }
        }

        #endregion
    }
}
