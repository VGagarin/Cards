using System;
using System.Collections.Generic;
using UI.Views;
using UnityEngine;

namespace UI.Management
{
    internal class CardHolder
    {
        private const float CentralAngleInDegrees = 40f;
        private const float DesiredAngleBetweenCardsInDegrees = 10f;
        private const float CardVerticalOffset = 250f;

        private CardViewMover _cardViewMover;
        private List<CardView> _cards = new List<CardView>();
    
        private Vector3 _centralPoint;
        private float _radius;

        public CardHolder(Vector3 centralPoint)
        {
            _centralPoint = centralPoint;
            _radius = -centralPoint.y + CardVerticalOffset;
        
            _cardViewMover = new CardViewMover();
        }

        public void Update(float deltaTime)
        {
            _cardViewMover.Update(deltaTime);
        }

        public void AddCard(CardView cardView)
        {
            _cards.Add(cardView);
        
            UpdatePositions();
        }

        public void RemoveCard(CardView cardView)
        {
            if (!_cards.Contains(cardView))
            {
                throw new Exception("Card holder doesn't contain card");
            }

            _cards.Remove(cardView);
            _cardViewMover.StopMove(cardView.transform);
        
            UpdatePositions();
        }
    
        public void UpdatePositions()
        {
            float startingAngle;
            float angleBetweenCards;
        
            if (_cards.Count * DesiredAngleBetweenCardsInDegrees > CentralAngleInDegrees)
            {
                angleBetweenCards = CentralAngleInDegrees / (_cards.Count - 1);
                startingAngle = 90f + CentralAngleInDegrees / 2f;
            }
            else
            {
                angleBetweenCards = DesiredAngleBetweenCardsInDegrees;
            
                float employedSector = (_cards.Count - 1)  * DesiredAngleBetweenCardsInDegrees;
                startingAngle = 90f + employedSector / 2f;
            }
        
            SetPositions(startingAngle, angleBetweenCards);
        }

        private void SetPositions(float startingAngelInDegrees, float angelBetweenCardsInDegrees)
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                float currentAngle = startingAngelInDegrees - i * angelBetweenCardsInDegrees;
            
                Vector3 offsetPosition = new Vector3
                {
                    x =  Mathf.Cos(currentAngle * Mathf.Deg2Rad) * _radius,
                    y =  Mathf.Sin(currentAngle * Mathf.Deg2Rad) * _radius
                };

                Vector3 targetPosition = _centralPoint + offsetPosition;
                Quaternion targetRotation = Quaternion.Euler(Vector3.forward * (currentAngle - 90f));

                _cardViewMover.MoveCard(_cards[i].transform, targetPosition, targetRotation);
            }
        }
    }
}
