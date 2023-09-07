﻿
    using CodeBase.Data;
    using CodeBase.Services.PlayerProgressService;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] protected Button CloseButton;
    
        protected IPlayerProgressService ProgressService;
        protected PlayerProgress Progress => ProgressService.Progress;

        [Inject]
        public void Construct(IPlayerProgressService progressService) => 
            ProgressService = progressService;

        private void Awake() => 
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy() => 
            Cleanup();

        protected virtual void OnAwake() => 
            CloseButton?.onClick.AddListener(()=> Destroy(gameObject));

        protected virtual void Initialize(){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void Cleanup(){}
    }
