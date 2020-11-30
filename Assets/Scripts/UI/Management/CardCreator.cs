using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UI.Views;
using UnityEngine;
using ViewModels;
using Object = UnityEngine.Object;

namespace UI.Management
{
    internal class CardCreator
    {
        private CardView _cardViewPrefab;
        private Transform _parent;
    
        private Dictionary<CardData, CardView> _cardViews = new Dictionary<CardData, CardView>();

        public CardView GetCardView(CardData cardData) => _cardViews[cardData];

        public CardData GetCardData(CardView cardView)
        {
            foreach (var keyValuePair in _cardViews.Where(keyValuePair => keyValuePair.Value == cardView))
            {
                return keyValuePair.Key;
            }
        
            throw new Exception("Not contain card data for card view");
        }
    
        public CardCreator(CardView cardViewPrefab, Transform parent)
        {
            _cardViewPrefab = cardViewPrefab;
            _parent = parent;
        }
    
        public CardView CreateCard(CardData cardData, CardsViewModel viewModel)
        {
            CardView cardView = Object.Instantiate(_cardViewPrefab, _parent);
            cardView.InitializeViewModel(viewModel);
            cardView.SetInitialData(cardData);

            _cardViews[cardData] = cardView;
        
            return cardView;
        }

        public void DestroyCard(CardData cardData)
        {
            CardView cardView = GetCardView(cardData);

            _cardViews.Remove(cardData);
        
            Object.Destroy(cardView.gameObject);
        }
    }
}
