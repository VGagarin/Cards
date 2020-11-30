using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class DragEffect : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Graphic _graphic;

        private void Awake()
        {
            _graphic.enabled = false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _graphic.enabled = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _graphic.enabled = false;
        }
    }
}