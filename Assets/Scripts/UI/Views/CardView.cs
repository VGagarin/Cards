using Data;
using TMPro;
using UnityEngine;
using ViewModels;

namespace UI.Views
{
    internal sealed class CardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _health;
        [SerializeField] private TextMeshProUGUI _manaCost;
        [SerializeField] private TextMeshProUGUI _attackDamage;

        private CardsViewModel _viewModel;
        
        public void InitializeViewModel(CardsViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void DragEnded()
        {
            _viewModel.CardDragEnded();
        }
        
        public void SetInitialData(CardData data)
        {
            _name.text = data.Name;
            _description.text = data.Description;
            _health.text = $"{data.Health}";
            _manaCost.text = $"{data.ManaCost}";
            _attackDamage.text = $"{data.AttackDamage}";
        }
        
        public void UpdateValues(CardData cardData)
        {
            OnHealthChanged(cardData.Health);
            OnManaCostChanged(cardData.ManaCost);
            OnAttackDamageChanged(cardData.AttackDamage);
        }

        private void OnHealthChanged(int healthValue)
        {
            _health.text = $"{healthValue}";
        }

        private void OnManaCostChanged(int manaCostValue)
        {
            _manaCost.text = $"{manaCostValue}";
        }

        private void OnAttackDamageChanged(int attackDamageValue)
        {
            _attackDamage.text = $"{attackDamageValue}";
        }
    }
}