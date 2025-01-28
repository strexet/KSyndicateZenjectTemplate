using Zenject;

namespace Game.Infrastructure.States
{
	public class StatesFactory
	{
		readonly IInstantiator instantiator;

		public StatesFactory(IInstantiator instantiator) => this.instantiator = instantiator;

		public TState Create<TState>() where TState : IExitableState => instantiator.Instantiate<TState>();
	}
}