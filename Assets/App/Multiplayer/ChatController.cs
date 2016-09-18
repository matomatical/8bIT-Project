using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace _8bITProject.cooperace.multiplayer {
    public class ChatController : MonoBehaviour {
        
        // the update manager which should be told about any updates
        public IUpdateManager updateManager;

        private Button chatButton;
        

        public List<String> chatHistory = new List<string>();
        private String currMessage = string.Empty;


        public void Something() {
            //UIHelper.GoTo("MainMenu");
            TouchScreenKeyboardType keyboard = TouchScreenKeyboardType.Default;
            bool autocorrection = true;
            bool multiline = false;
            bool secure = false;
            bool alert = false;
            string textPlaceholder = "";

            TouchScreenKeyboard.Open(currMessage, keyboard, autocorrection,  multiline, secure, alert, textPlaceholder);
        }

        // Use this for initialization
        void Start() {
             chatButton = GameObject.Find("btnChat").GetComponent<Button>();
        }

        // Update is called once per frame
        void Update() {

        }

        public void Deserialize(List<byte> message) {
            throw new NotImplementedException();
        }

        public void Notify(List<byte> message) {
            throw new NotImplementedException();
        }

        public List<byte> Serialize(String meesage) {
            throw new NotImplementedException();
        }

        public void Subscribe(IListener<List<byte>> o) {
            throw new NotImplementedException();
        }

    }
}