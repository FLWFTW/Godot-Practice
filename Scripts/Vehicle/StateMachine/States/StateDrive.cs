using Godot;
using Godot.StateMachine.Instance;
using System;

public class StateDrive : StateInstance
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private StateMachine _v;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	public StateDrive( StateMachine v )
	{
		this._v = v;
	}
	
	public override void _OnEnter()
	{
		_v._gear.Text = "Drive";
	}
	
	public override void _OnExit()
	{
	}
	
	public override void _Process( float delta )
	{
	}
	
	public override void _PhysicsProcess( float delta )
	{
		if( Input.IsKeyPressed( ' ' ) )
		{
			_v.EngineForce = 100f;
		}
		else if( Input.IsKeyPressed( 'b' ) || Input.IsKeyPressed( 'B' ) )
		{
			_v.Brake = 50f;
		}
		else
		{
			_v.EngineForce = 0f;
			_v.Brake = 0f;
		}
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
