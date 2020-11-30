using UI.Management;
using UnityEngine;

#pragma warning disable CS0649
namespace UI.Views
{
    internal sealed class GameTableView : MonoBehaviour
    {
        [SerializeField] private CardViewProcessor _cardViewProcessor;
        [SerializeField] private RandomizeCardView _randomizeCardView;
        [SerializeField] private CardDropArea _cardDropArea;

        public CardViewProcessor CardViewProcessor => _cardViewProcessor;
        public RandomizeCardView RandomizeCardView => _randomizeCardView;
        public CardDropArea CardDropArea => _cardDropArea;
    }
}