using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.States
{
	public abstract class StateMachine : IStateMachine
	{
		readonly Dictionary<Type, IExitableState> registeredStates = new();
		IExitableState currentState;

		public async UniTask Enter<TState>() where TState : class, IState
		{
			var newState = await ChangeState<TState>();
			await newState.Enter();
		}

		public async UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPaylodedState<TPayload>
		{
			var newState = await ChangeState<TState>();
			await newState.Enter(payload);
		}

		public void RegisterState<TState>(TState state) where TState : IExitableState => registeredStates.Add(typeof(TState), state);

		async UniTask<TState> ChangeState<TState>() where TState : class, IExitableState
		{
			if (currentState != null)
			{
				await currentState.Exit();
			}

			var state = GetState<TState>();
			currentState = state;

			return state;
		}

		TState GetState<TState>() where TState : class, IExitableState => registeredStates[typeof(TState)] as TState;
	}
}