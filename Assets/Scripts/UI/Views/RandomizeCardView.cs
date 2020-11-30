using UnityEngine;
using UnityEngine.UI;
using ViewModels;

namespace UI.Views
{
    internal class RandomizeCardView : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public void ViewModelInitialized(CardsViewModel cardsViewModel)
        {
            _button.onClick.AddListener(cardsViewModel.RandomizeNextCard);
        }
    }
}
