using UnityEngine;
using UnityEngine.UI;
using ServiceLocator.Main;

namespace ServiceLocator.UI
{
    public class MapButton : MonoBehaviour
    {
        [SerializeField] private int MapId;
        private Events.EventService eventService;

        public void Init(Events.EventService eventService)
        {
            this.eventService = eventService;
            GetComponent<Button>().onClick.AddListener(OnMapButtonClicked);
        }
        // To Learn more about Events and Observer Pattern, check out the course list here: https://outscal.com/courses
        private void OnMapButtonClicked() =>  eventService.OnMapSelected.InvokeEvent(MapId);
    }
}