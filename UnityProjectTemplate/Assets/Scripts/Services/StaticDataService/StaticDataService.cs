using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.LogService;
using CodeBase.Services.ServerConnectionService;
using CodeBase.UI.PopUps.PolicyAcceptPopup;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace CodeBase.Services.StaticDataService
{
	// This service incapsulate logic of uploading configs and give convenient API
	// for all consumers to receive necessary configs
	public class StaticDataService : IStaticDataService
	{

		readonly ILogService log;
		readonly IAssetProvider assetProvider;
		Dictionary<int, PolicyAcceptPopupConfig> policyAcceptConfigs;

		public StaticDataService(ILogService log, IAssetProvider assetProvider)
		{
			this.log = log;
			this.assetProvider = assetProvider;
		}

		public ServerConnectionConfig ServerConnectionConfig { get; private set; }

		public async UniTask InitializeAsync()
		{
			// load your configs here
			var tasks = new List<UniTask>();
			tasks.Add(LoadServerConfigs());
			tasks.Add(LoadPolicyAcceptConfigs());

			await UniTask.WhenAll(tasks);
			log.Log("Static data loaded");
		}

		public PolicyAcceptPopupConfig GetPolicyAcceptPopupConfig(PolicyAcceptPopupTypes type) => policyAcceptConfigs[(int)type];

		async UniTask LoadPolicyAcceptConfigs()
		{
			var configs = await GetConfigs<PolicyAcceptPopupConfig>();
			policyAcceptConfigs = configs.ToDictionary(config => (int)config.Type, config => config);
		}

		async UniTask LoadServerConfigs()
		{
			var configs = await GetConfigs<ServerConnectionConfig>();

			if (configs.Length > 0)
			{
				ServerConnectionConfig = configs.First();
			}
			else
			{
				log.LogError("There are no server connection config founded!");
			}
		}

		async UniTask<List<string>> GetConfigKeys<TConfig>() => await assetProvider.GetAssetsListByLabel<TConfig>(AssetLabels.Configs);

		async UniTask<TConfig[]> GetConfigs<TConfig>() where TConfig : class
		{
			var keys = await GetConfigKeys<TConfig>();
			return await assetProvider.LoadAll<TConfig>(keys);
		}
	}
}