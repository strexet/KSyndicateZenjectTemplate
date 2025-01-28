using Cysharp.Threading.Tasks;

namespace Game.Infrastructure.SceneManagement
{
	public interface ISceneLoader
	{
		UniTask Load(string nextScene);
	}
}