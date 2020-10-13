using Godot;
using Godot.StateMachine.Instance;
using System;

public class StateMachine : VehicleBody
{
	enum States { Drive, Reverse, Park, MAX_STATE };
	private States _state;
	private StateInstance[] states = new StateInstance[(int)States.MAX_STATE];
	public float _steeringSensitivity = .025f;
	private MeshInstance _steeringWheel;
	public Label _gear, _speed;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_steeringWheel = this.GetNode( "JeepSteeringWheel" ) as MeshInstance;
		_gear = this.GetNode( "Camera/Panel/Gear" ) as Label;
		_speed = this.GetNode( "Camera/Panel2/Speed" ) as Label;
		_gear.Text = "Park";
		
		_state = States.Park;
		states[(int)States.Drive] = new StateDrive( this );
		states[(int)States.Reverse] = new StateReverse( this);
		states[(int)States.Park] = new StatePark( this);
		Input.SetMouseMode(Input.MouseMode.Captured);
	}
	
	private void ChangeState( States newState )
	{
		if( newState != _state )
		{
			states[(int)_state]._OnExit();
			_state = newState;
			states[(int)_state]._OnEnter();
		}
	}
	
	public override void _Process( float delta )
	{
	
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			if (Input.GetMouseMode() == Input.MouseMode.Visible)
 				Input.SetMouseMode(Input.MouseMode.Captured);
			else
				Input.SetMouseMode(Input.MouseMode.Visible);
		}
		
		if( Input.IsKeyPressed( 'r' ) || Input.IsKeyPressed( 'R' ) )
		{
				ChangeState( States.Reverse );
		}
		else if( Input.IsKeyPressed( 'd' ) || Input.IsKeyPressed( 'D' ) )
		{
			ChangeState( States.Drive );
		}
		else if( Input.IsKeyPressed( 'x' ) || Input.IsKeyPressed( 'X' ) )
		{
			ChangeState( States.Park );
		}
		
		states[(int)_state]._Process( delta );
	}
	
	public override void _Input( InputEvent @event )
	{
		if( @event is InputEventMouseMotion && Input.GetMouseMode() == Input.MouseMode.Captured )
		{
			InputEventMouseMotion m = @event as InputEventMouseMotion;
			if( m.Relative.Normalized().x > 0 && this.Steering > -0.8f )
			{
				this.Steering -= 1f * _steeringSensitivity;
				this._steeringWheel.RotateObjectLocal( Vector3.Up, -2f*_steeringSensitivity );
			}
			else if( m.Relative.Normalized().x < 0 && this.Steering < 0.8f )
			{
				this.Steering += 1f * _steeringSensitivity;
				this._steeringWheel.RotateObjectLocal( Vector3.Up, 2f*_steeringSensitivity );
			}
		}
	}
	
	private void updateSpeed()
	{
		float speed = this.LinearVelocity.Length();
		speed *= 2.23f; //Convert to miles per hour.
		_speed.Text = $"{Math.Round((decimal)speed, 0, MidpointRounding.AwayFromZero)} MPH";
	}
	
	public override void _PhysicsProcess( float delta )
	{
		states[(int)_state]._PhysicsProcess( delta );
		updateSpeed();
	}
}
