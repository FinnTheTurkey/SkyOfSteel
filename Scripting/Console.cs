using Godot;
using System;
using System.Collections.Generic;


public class Console : Node
{
	private static Node Window;
	private static LineEdit InputLine;
	private static RichTextLabel ConsoleLabel;
	private static RichTextLabel LogLabel;
	private static List<string> History = new List<string>();
	private static int HistLocal = 0;


	private static Console Self;
	private Console()
	{
		Self = this;
	}


	public override void _Ready()
	{
		Window = GetTree().GetRoot().GetNode("RuntimeRoot/ConsoleWindow");
		InputLine = Window.GetNode("LineEdit") as LineEdit;
		ConsoleLabel = Window.GetNode("HBox/Console") as RichTextLabel;
		LogLabel = Window.GetNode("HBox/Log") as RichTextLabel;
		Console.Print("");
		Console.Log("");
	}


	public override void _Input(InputEvent Event)
	{
		if(Event.IsAction("ui_up"))
		{
			GetTree().SetInputAsHandled();
			InputLine.GrabFocus();

			if(Input.IsActionJustPressed("ui_up") && HistLocal > 0)
			{
				HistLocal -= 1;
				InputLine.Text = History[HistLocal];

				InputLine.CaretPosition = InputLine.Text.Length;
			}
		}

		if(Event.IsAction("ui_down"))
		{
			GetTree().SetInputAsHandled();
			InputLine.GrabFocus();

			if(Input.IsActionJustPressed("ui_down") && HistLocal < History.Count)
			{
				HistLocal += 1;
				if(HistLocal == History.Count)
				{
					InputLine.Text = "";
				}
				else
				{
					InputLine.Text = History[HistLocal];
				}

				InputLine.CaretPosition = InputLine.Text.Length;
			}
		}
	}


	public static void Print(string ToPrint)
	{
		ConsoleLabel.Text += " " + ToPrint + "\n";
	}


	public static void Log(string ToLog)
	{
		LogLabel.Text += " " + ToLog + "\n";
	}


	public static void ThrowPrint(string ToThrow)
	{
		Print($"ERROR: {ToThrow}");
	}


	public static void ThrowLog(string ToThrow)
	{
		Log($"ERROR: {ToThrow}");
	}


	public static void Execute(string Command)
	{
		Console.Print("\n >>> " + Command);

		if(History.Count <= 0 || History[History.Count-1] != Command)
		{
			History.Add(Command);
		}
		HistLocal = History.Count;

		Scripting.RunConsoleLine(Command);
	}


	public static void ClearLog()
	{
		LogLabel.Text = "";
	}
}
