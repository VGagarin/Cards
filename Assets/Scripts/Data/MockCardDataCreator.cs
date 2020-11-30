using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    internal static class MockCardDataCreator
    {
        public static List<CardData> CreateListOfCardData(int count)
        {
            List<CardData> result = new List<CardData>(count);

            for (int i = 0; i < count; i++)
            {
                result.Add(CreateCardData());
            }

            return result;
        }

        private static CardData CreateCardData()
        {
            string name = $"Name: {Random.Range(0, 100)}";
            string description = $"Description: {Random.Range(0, 100)}";
            int health = Random.Range(1, 10);
            int mana = Random.Range(0, 10);
            int attack = Random.Range(0, 10);
            
            return new CardData(name, description, health, mana, attack);
        }
        
        
    }
}