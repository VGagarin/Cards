using UI.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Management
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(CardView))]
    public class CardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Image _image;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _image.raycastTarget = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            GetComponent<CardView>().DragEnded();
            _image.raycastTarget = true;
        }
    }
}
