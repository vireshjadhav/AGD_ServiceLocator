using ServiceLocator.Events;
using ServiceLocator.Map;
using ServiceLocator.Player;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Utilities;
using ServiceLocator.Wave;
using System;
using UnityEditor.MPE;
using UnityEngine;

namespace ServiceLocator.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        // Services:
        public Events.EventService EventService { get; private set; }
        public MapService MapService { get; private set; }
        public WaveService WaveService { get; private set; }
        public SoundService SoundService { get; private set; }
        public PlayerService PlayerService { get; private set; }

        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;


        // Scriptable Objects:
        [SerializeField] private MapScriptableObject mapScriptableObject;
        [SerializeField] private WaveScriptableObject waveScriptableObject;
        [SerializeField] private SoundScriptableObject soundScriptableObject;
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        // Scene Referneces:
        [SerializeField] private AudioSource SFXSource;
        [SerializeField] private AudioSource BGSource;

        private void Start()
        {
            createServices();
            InjectDependencies();
        }

        private void createServices()
        {
            EventService = new Events.EventService();
            MapService = new MapService(mapScriptableObject);
            WaveService = new WaveService(waveScriptableObject);
            SoundService = new SoundService(soundScriptableObject, SFXSource, BGSource);
            PlayerService = new PlayerService(playerScriptableObject);
        }

        private void InjectDependencies()
        {
            PlayerService.Init(SoundService, UIService, MapService);
            WaveService.Init(EventService, MapService, UIService, SoundService, PlayerService);
            MapService.Init(EventService);
            UIService.Init(EventService, WaveService, PlayerService);
        }

        private void Update()
        {
            PlayerService.Update();
        }
    }
}