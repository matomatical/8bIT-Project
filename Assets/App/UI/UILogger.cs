using UnityEngine;
using System.Collections.Generic;
using System;

namespace xyz._8bITProject.cooperace {
	public class UILogger : MonoBehaviour {

		public int maxMessages = 10;
		public int fontSize = 40;

		LinkedList<string> messages;

		GUIStyle style;

		void Awake() {
			messages = new LinkedList<string>();
		}

		void OnGUI() {
			if (style == null) {
				style = new GUIStyle(GUI.skin.GetStyle("label"));
				style.normal.textColor = Color.black;
				style.fontSize = fontSize;
			}
			foreach (string msg in messages) {
				GUILayout.Label(msg, style);
			}
		}

		void AddMsg(string msg) {
			messages.AddFirst(msg);
			while (messages.Count > maxMessages) {
				messages.RemoveLast();
			}
		}

		static UILogger instance;
		static UILogger getInstance() {
			if (instance == null) {
				instance = (UILogger)GameObject.FindObjectOfType(typeof(UILogger));
				if (instance == null) {
					GameObject go = new GameObject("UILogger");
					instance = go.AddComponent<UILogger>();
				}
			}
			return instance;
		}

		private static string[] ConvertToStringArray(object[] objs) {
			return Array.ConvertAll(objs, o => o == null ? "null" : o.ToString());
		}

		private static void LogMsg(string msg) {
			Debug.Log(msg);
			getInstance().AddMsg(msg);
		}

		public static void Log(params object[] objs) {
			LogMsg(string.Join(" ", ConvertToStringArray(objs)));
		}

		public static void Logf(string format, params object[] objs) {
			LogMsg(string.Format(format, ConvertToStringArray(objs)));
		}

	}
}