using System;
using System.Collections.Generic;
using UI.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using ViewModels;

#pragma warning disable CS0649
namespace UI.Management
{
    internal sealed class CardDropArea : MonoBehaviour, IDropHandler
    {
        public event Action<CardView> CardDropped; 
        
        [SerializeField] private float _maxDistance;

        private readonly List<Transform> _cardTransforms = new List<Transform>();
        
        private CardViewMover _cardViewMover = new CardViewMover();
        private Vector3 _origin;
        private float _areaWidth;

        private void Awake()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            _origin = rectTransform.position;
            _areaWidth = rectTransform.rect.width;
        }

        private void Update()
        {
            _cardViewMover.Update(Time.deltaTime);
        }

        public void ViewModelInitialized(CardsViewModel cardsViewModel)
        {
            CardDropped += cardsViewModel.CardDroppedFromHand;
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            GameObject cardGameObject = eventData.pointerDrag;
            
            PlaceNewCard(cardGameObject.transform);
            cardGameObject.GetComponent<CardDrag>().enabled = false;
            
            CardDropped?.Invoke(cardGameObject.GetComponent<CardView>());
        }

        private void PlaceNewCard(Transform cardTransform)
        {
            var index = GetIndexForNewCard(cardTransform.position);
            
            if (index == _cardTransforms.Count)
            {
                _cardTransforms.Add(cardTransform);
            }
            else
            {
                _cardTransforms.Insert(index, cardTransform);
            }
            
            SendCardsToPositions();
        }

        private int GetIndexForNewCard(Vector3 currentCardPosition)
        {
            float minDistance = Mathf.Infinity;
            int resultIndex = -1;
            
            for (int i = 0; i < _cardTransforms.Count; i++)
            {
                if (_cardTransforms[i].position.x > currentCardPosition.x)
                {
                    float distance = _cardTransforms[i].position.x - currentCardPosition.x;
                    
                    if (minDistance > distance)
                    {
                        minDistance = distance;
                        resultIndex = i;
                    }
                }
            }

            return resultIndex == -1 ? _cardTransforms.Count : resultIndex;
        }

        private void SendCardsToPositions()
        {
            for (int i = 0; i < _cardTransforms.Count; i++)
            {
                _cardViewMover.MoveCard(_cardTransforms[i], SamplePosition(i), Quaternion.identity);
            }
        }

        private Vector3 SamplePosition(int i)
        {
            float distance = Mathf.Min(_areaWidth / _cardTransforms.Count, _maxDistance);

            return _origin + distance * _cardTransforms.Count / 2 * Vector3.left + (i + 0.5f) * distance * Vector3.right;
        }
    }
}