using System;
using System.Collections.Generic;
using Basis;
using Data;
using Random = UnityEngine.Random;

namespace Models
{
    internal class CardsModel : IModel
    {
        public event Action<List<CardData>> CardsCreated;
        public event Action<CardData> CardRemovedFromHand;
        public event Action<CardData> CardDestroyed;
        public event Action<CardData> CardUpdated;
    
        public List<CardData> InHandCards { get; } = new List<CardData>();

        private int _activeCardIndex;

        public void CreateCards(List<CardData> cards)
        {
            InHandCards.AddRange(cards);
        
            CardsCreated?.Invoke(cards);
        }

        public void RemoveCardFromHand(CardData card)
        {
            _activeCardIndex = InHandCards.IndexOf(card) >= _activeCardIndex ? _activeCardIndex : _activeCardIndex - 1;
            
            InHandCards.Remove(card);
        
            CardRemovedFromHand?.Invoke(card);
        }

        public void RandomizeNextCard()
        {
            if (_activeCardIndex >= InHandCards.Count)
            {
                _activeCardIndex = 0;
            }

            CardData updatedCard = InHandCards[_activeCardIndex];
            Randomize(updatedCard);

            if (updatedCard.Health <= 0)
            {
                InHandCards.Remove(updatedCard);
                CardDestroyed?.Invoke(updatedCard);
            }
            else
            {
                CardUpdated?.Invoke(updatedCard);
                _activeCardIndex++;
            }
        }

        private void Randomize(CardData cardData)
        {
            Action<int>[] valueChangers = {cardData.SetHealth, cardData.SetAttackDamage, cardData.SetManaCosth};

            int index = Random.Range(0, valueChangers.Length);
        
            valueChangers[index].Invoke(GetRandomValue());
        }

        private int GetRandomValue()
        {
            const int lowerBound = -2;
            const int upperBound = 10;
        
            return Random.Range(lowerBound, upperBound);
        }
    }
}