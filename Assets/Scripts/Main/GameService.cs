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
    public class GameService : MonoBehaviour
    {
        // Services:
        private Events.EventService eventService;
        private MapService mapService;
        private WaveService waveService;
        private SoundService soundService;
        private PlayerService playerService;

        [SerializeField] private UIService uIService;
        public UIService UIService => uIService;


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
            eventService = new Events.EventService();
            mapService = new MapService(mapScriptableObject);
            waveService = new WaveService(waveScriptableObject);
            soundService = new SoundService(soundScriptableObject, SFXSource, BGSource);
            playerService = new PlayerService(playerScriptableObject);
        }

        private void InjectDependencies()
        {
            playerService.Init(soundService, UIService, mapService);
            waveService.Init(eventService, mapService, UIService, soundService, playerService);
            mapService.Init(eventService);
            UIService.Init(eventService, waveService, playerService);
        }

        private void Update()
        {
            playerService.Update();
        }
    }
}