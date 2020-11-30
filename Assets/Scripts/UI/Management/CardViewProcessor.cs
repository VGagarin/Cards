using System.Collections.Generic;
using Data;
using UI.Views;
using UnityEngine;
using ViewModels;

#pragma warning disable CS0649
namespace UI.Management
{
    internal sealed class CardViewProcessor : MonoBehaviour
    {
        [SerializeField] private Transform _centralPoint;
        [SerializeField] private CardView _cardPrefab;

        private CardHolder _cardHolder;
        private CardCreator _cardCreator;

        private void Awake()
        {
            _cardHolder = new CardHolder(_centralPoint.position);
            _cardCreator = new CardCreator(_cardPrefab, transform);
        }

        private void Update()
        {
            _cardHolder.Update(Time.deltaTime);
        }

        public void CreateCards(IEnumerable<CardData> cardDataCollection, CardsViewModel viewModel)
        {
            foreach (CardData cardData in cardDataCollection)
            {
                CardView cardView = _cardCreator.CreateCard(cardData, viewModel);
                _cardHolder.AddCard(cardView);
            }
        }

        public CardData GetCardData(CardView cardView)
        {
            return _cardCreator.GetCardData(cardView);
        }

        public void RemoveCardFromHand(CardData cardData)
        {
            CardView cardView = _cardCreator.GetCardView(cardData);
            
            _cardHolder.RemoveCard(cardView);
        }
        
        public void DestroyCard(CardData cardData)
        {
            RemoveCardFromHand(cardData);

            _cardCreator.DestroyCard(cardData);
        }

        public void UpdateCard(CardData cardData)
        {
            CardView cardView = _cardCreator.GetCardView(cardData);

            cardView.UpdateValues(cardData);
        }

        public void UpdateCardsPositions()
        {
            _cardHolder.UpdatePositions();
        }
    }
}