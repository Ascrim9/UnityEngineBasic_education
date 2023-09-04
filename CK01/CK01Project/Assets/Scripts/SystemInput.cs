using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SystemInput
{
	//TODO: Add Keyboard input
	
	//Keys
	const int VK_LBUTTON = 0x01; 
	const int VK_RBUTTON = 0x02; 
	const int VK_MBUTTON = 0x02; 
	const int SM_SWAPBUTTON = 23;

	//Key states
	const int BUTTONDOWNFRAME = -32767;
	const int BUTTONDOWN = -32768;
	const int BUTTONUP = 0; 
	
	//키보드 ?

	[DllImport("user32.dll", EntryPoint = "SetCursorPos")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool SetCursorPos(int x, int y);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetCursorPos(out Vector2Int lpMousePoint);	//Cursor coordinates start top-left, rather than Unity's bottom-left, so y axis will need to be modified

	[DllImport("user32.dll")]
	public static extern short GetAsyncKeyState(int virtualKeyCode);

	[DllImport("user32.dll")]
	public static extern short GetSystemMetrics(int metricsCode);


	static bool mouseButton0Down = false;
	static bool mouseButton1Down = false;
	static bool lastMouseButton0Down = false;
	static bool lastMouseButton1Down = false;
	static bool hasPressedButton0 = false;
	static bool hasPressedButton1 = false;


	public static bool GetMouseButton(int button = 0)
	{
		return (button == 0) ? hasPressedButton0 : hasPressedButton1;
	}


	public static bool GetMouseButtonDown(int button = 0)
	{
		return (button == 0) ? mouseButton0Down : mouseButton1Down;
	}


	public static bool GetMouseButtonUp(int button = 0)
	{
		return (button == 0) ? (!hasPressedButton0 && lastMouseButton0Down) : (!hasPressedButton1 && lastMouseButton1Down);
	}
	
	public static Vector2Int GetCursorPosition()
	{
		GetCursorPos(out var point);
		return point;
	}

	public static void SetCursorPosition(Vector2Int point)
	{
		SetCursorPos(point.x, point.y);
	}

	public static void Process()
	{
		CheckMouseButtons();
	}

	static void CheckMouseButtons()
	{
		lastMouseButton0Down = hasPressedButton0;
		lastMouseButton1Down = hasPressedButton1;
		mouseButton0Down = false;
		mouseButton1Down = false;

		var mbp0 = MouseButtonPressed(0);
		var mbp1 = MouseButtonPressed(1);

		switch (hasPressedButton0)
		{
			//Check MouseButton0
			case false when mbp0:
				hasPressedButton0 = true;
				mouseButton0Down = true;
				break;
			case true when !mbp0:
				hasPressedButton0 = false;
				break;
		}

		switch (hasPressedButton1)
		{
			//Check MouseButton1
			case false when mbp1:
				hasPressedButton1 = true;
				mouseButton1Down = true;
				break;
			case true when !mbp1:
				hasPressedButton1 = false;
				break;
		}
	}

	static bool MouseButtonPressed(int button)
	{
		bool state = false;
		bool swapped = GetSystemMetrics(SM_SWAPBUTTON) > 0;
		switch (button)
		{
			case 0:
				state = GetAsyncKeyState(swapped ? VK_RBUTTON : VK_LBUTTON) == BUTTONDOWN;
				break;
			case 1:
				state = GetAsyncKeyState(swapped ? VK_LBUTTON : VK_RBUTTON) == BUTTONDOWN;
				break;
			case 2:
				state = GetAsyncKeyState(VK_MBUTTON) == BUTTONDOWN;
				break;
			default:
				return false;
		}

		return state;
	}

	//TODO: Keyboard Input 
	public static bool GetKey(KeyCode key)
	{
		if (VK_KeyCodes.TryGetValue(key, out var value))
		{
			return GetAsyncKeyState(value) == BUTTONDOWN;
		}

		return false;
	}
	
	static bool KeyCodePressed(int value)
	{
		return GetAsyncKeyState(value) == BUTTONDOWN;
	}
	
	static Dictionary<KeyCode, int> VK_KeyCodes = new Dictionary<KeyCode, int>()
	{
		{KeyCode.Keypad8, 0x68},
		{KeyCode.Keypad4, 0x64},
		{KeyCode.Keypad6, 0x66},
		{KeyCode.Keypad2, 0x62},
	};
	
	static Dictionary<KeyCode, KeyState> KeyStates = new Dictionary<KeyCode, KeyState>();
	public struct KeyState
	{
		public KeyCode KeyCode;
		public KeyPressType KeyPressType;
	}

	public enum KeyPressType
	{
		None,
		Down,
		Hold,
		Up,
	}
}