using CodeBase.Data;
using CodeBase.Services.PlayerProgressService;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.WalletService
{
	public class WalletService : IWalletService, IProgressReader, IProgressSaver, IInitializable
	{
		readonly Dictionary<int, long> wallets = new();

		List<int> walletTypes = new();

		/// <summary>
		///     Currency, Old value, New value
		/// </summary>
		public Action<CurrencyType, long, long> OnWalletUpdate { get; set; }

		public void Initialize() => ClearWallets();

		public void LoadProgress(PlayerProgress progress)
		{
			ClearWallets();

			var savedValues = progress.WalletsData.ToDictionary();

			foreach (var pair in savedValues)
			{
				var currencyTypeCode = pair.Key;
				var currencyAmount = pair.Value;

				if (!Enum.IsDefined(typeof(CurrencyType), currencyTypeCode))
				{
					Debug.LogError($"Unknown currency type code {currencyTypeCode} with amount {currencyAmount} on progress loading found! May be some old currency.");
					continue;
				}

				var currencyType = (CurrencyType)currencyTypeCode;

				if (currencyType == CurrencyType.None)
				{
					Debug.LogError("None currency type on progress loading found!");
					continue;
				}

				SetAmount(currencyType, currencyAmount);
			}
		}

		public void UpdateProgress(PlayerProgress progress) => progress.WalletsData = new WalletsData(wallets);

		public long GetAmount(CurrencyType currency)
		{
			long value = 0;
			wallets.TryGetValue((int)currency, out value);
			return value;
		}

		public void SetAmount(CurrencyType currency, long amount)
		{
			var oldValue = wallets[(int)currency];

			if (oldValue != amount)
			{
				wallets[(int)currency] = amount;
				OnWalletUpdate?.Invoke(currency, oldValue, amount);
			}
		}

		public void AddAmount(CurrencyType currency, long amount) => SetAmount(currency, GetAmount(currency) + amount);

		public bool TrySpend(CurrencyType currency, long price)
		{
			var amount = GetAmount(currency);

			if (amount < price)
			{
				return false;
			}

			amount -= price;
			SetAmount(currency, amount);
			return true;
		}

		void ClearWallets()
		{
			wallets.Clear();
			walletTypes = Enum.GetValues(typeof(CurrencyType)).Cast<int>().ToList();

			foreach (var type in walletTypes)
			{
				wallets.Add(type, 0);
			}
		}
	}
}