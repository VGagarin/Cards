using System.Collections.Generic;
using Basis;
using Data;
using Models;
using UI.Management;
using UI.Views;

namespace ViewModels
{
    internal class CardsViewModel
    {
        private CardViewProcessor _cardViewProcessor;
        private ModelRepository _modelRepository;

        public void Initialize(ModelRepository modelRepository, CardViewProcessor cardViewProcessor)
        {
            _modelRepository = modelRepository;
            _cardViewProcessor = cardViewProcessor;

            CardsModel cardsModel = GetModel<CardsModel>();

            cardsModel.CardsCreated += CreateCards;
            cardsModel.CardRemovedFromHand += RemoveCard;
            cardsModel.CardDestroyed += DestroyCard;
            cardsModel.CardUpdated += UpdateCard;
        }

        public void RandomizeNextCard()
        {
            CardsModel cardsModel = GetModel<CardsModel>();

            if (cardsModel.InHandCards.Count > 0)
            {
                cardsModel.RandomizeNextCard();
            }
        }
        
        public void CardDroppedFromHand(CardView cardView)
        {
            CardData cardData = _cardViewProcessor.GetCardData(cardView);

            GetModel<CardsModel>().RemoveCardFromHand(cardData);
        }
        
        public void CardDragEnded()
        {
            _cardViewProcessor.UpdateCardsPositions();
        }

        private void CreateCards(List<CardData> listOfCardData)
        {
            _cardViewProcessor.CreateCards(listOfCardData, this);
        }
        
        private void RemoveCard(CardData cardData)
        {
            _cardViewProcessor.RemoveCardFromHand(cardData);
        }
        
        private void DestroyCard(CardData cardData)
        {
            _cardViewProcessor.DestroyCard(cardData);
        }
        
        private void UpdateCard(CardData cardData)
        {
            _cardViewProcessor.UpdateCard(cardData);
        }

        private T GetModel<T>() where T : IModel
        {
            return _modelRepository.GetModel<T>();
        }

        ~CardsViewModel()
        {
            CardsModel cardsModel = GetModel<CardsModel>();

            cardsModel.CardsCreated -= CreateCards;
            cardsModel.CardRemovedFromHand -= RemoveCard;
            cardsModel.CardDestroyed -= DestroyCard;
            cardsModel.CardUpdated -= UpdateCard;
        }
    }
}