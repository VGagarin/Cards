using System.Collections.Generic;
using Basis;
using Data;
using Models;
using UI.Views;
using UnityEngine;
using UnityEngine.Serialization;
using ViewModels;

#pragma warning disable CS0649
namespace Main
{
    internal sealed class GameStarter : MonoBehaviour
    {
        private const int CardsCount = 6;
        
        [FormerlySerializedAs("_gameTablePrefab")] [SerializeField] private GameTableView _gameTableViewPrefab;

        private ModelRepository _modelRepository;
        
        private void Awake()
        {
            _modelRepository = new ModelRepository();
            
            CreateViews();
            
            FillModels();
        }

        private void CreateViews()
        {
            GameTableView gameTableView = Instantiate(_gameTableViewPrefab);
            
            CardsViewModel cardsViewModel = new CardsViewModel();
            cardsViewModel.Initialize(_modelRepository, gameTableView.CardViewProcessor);

            gameTableView.CardDropArea.ViewModelInitialized(cardsViewModel);
            gameTableView.RandomizeCardView.ViewModelInitialized(cardsViewModel);
        }

        private void FillModels()
        {
            List<CardData> listOfCardData = MockCardDataCreator.CreateListOfCardData(CardsCount);
            
            _modelRepository.GetModel<CardsModel>().CreateCards(listOfCardData);
        }
    }
}