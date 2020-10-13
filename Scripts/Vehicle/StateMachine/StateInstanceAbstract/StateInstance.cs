using Godot;
using System;

namespace Godot.StateMachine.Instance
{
	public abstract class StateInstance
	{
		public virtual void OnEnter( float delta )
		{
		}
		public virtual void _Ready()
		{
			
		}
		
		public virtual void _OnEnter()
		{
			
		}
		
		public virtual void _OnExit()
		{
		}
		
		public virtual void _Process( float delta )
		{
		}
		
		public virtual void _PhysicsProcess( float delta )
		{
			
		}
	}
}
