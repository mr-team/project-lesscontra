using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class DialogueWindow
{
	[SerializeField]
	string dialogueText;
	[SerializeField]
	bool isQuestion;
	[SerializeField]
	int windowNum;
	[SerializeField]
	bool lastWindow;

	public string DialogueText {
		get
		{
			return dialogueText;
		}
		set
		{
			dialogueText = value;
		}
	}

	public bool IsQuestion {
		get
		{
			return isQuestion;
		}
		set
		{
			isQuestion = value;
		}
	}

	public int WindowNum {
		get
		{
			return windowNum;
		}
		set
		{
			windowNum = value;
		}
	}

	public bool LastWindow {
		get
		{
			return lastWindow;
		}
		set
		{
			lastWindow = value;
		}
	}

	public DialogueWindow (string _dialogueText, int _windowNum, bool  _isQuestion = false, bool _lastWindow = false)
	{
		dialogueText = _dialogueText;
		windowNum = _windowNum;
		isQuestion = _isQuestion;
		lastWindow = _lastWindow;
	}
}
