using Zenject;

namespace Game.Infrastructure.States
{
	public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<StatesFactory>()
					 .AsSingle();

			Container.Bind<GameStateMachine>()
					 .AsSingle();
		}
	}
}