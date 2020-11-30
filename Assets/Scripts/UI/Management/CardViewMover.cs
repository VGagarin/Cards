using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Management
{
    internal class CardViewMover
    {
        private class MoveState
        {
            private readonly Transform _transform;
        
            private readonly Vector3 _fromPosition;
            private readonly Vector3 _toPosition;
        
            private readonly Quaternion _fromRotation;
            private readonly Quaternion _toRotation;
        
            private readonly float _duration;
            private float _time;
		
            public bool IsFinished => _time / _duration >= 1f;
            public Transform Transform => _transform;

            private float RelativePosition => EaseInOutCubic(_time / _duration);
		
            public MoveState(Transform transform, Vector3 toPosition, Quaternion toRotation, float duration)
            {
                _transform = transform;
                _fromPosition = transform.position;
                _toPosition = toPosition;
            
                _fromRotation = transform.rotation;
                _toRotation = toRotation;
            
                _duration = duration;
                _time = 0f;
            }

            public void IncreaseTime(float deltaTime)
            {
                _time += deltaTime;
			
                _transform.position = Vector3.Lerp(_fromPosition, _toPosition, RelativePosition);
                _transform.rotation = Quaternion.Lerp(_fromRotation, _toRotation, RelativePosition);
            }
		
            private float EaseInOutCubic(float x)
            {
                return x < 0.5f ? 4f * Mathf.Pow(x, 3f) : 1f - Mathf.Pow(-2f * x + 2f, 3f) / 2f;
            }
        }

        private const float Duration = 0.3f; 
	
        private List<MoveState> _inMove = new List<MoveState>();

        public void Update(float deltaTime)
        {
            for (int i = 0; i < _inMove.Count; i++)
            {
                MoveState moveState = _inMove[i];
			
                moveState.IncreaseTime(deltaTime);

                if (moveState.IsFinished)
                {
                    _inMove.RemoveAt(i--);
                }
            }
        }
	
        public void MoveCard(Transform cardTransform, Vector3 targetPosition, Quaternion targetRotation)
        {
            MoveState moveState = new MoveState(cardTransform, targetPosition, targetRotation, Duration);

            _inMove.Add(moveState);
        }

        public void StopMove(Transform cardTransform)
        {
            MoveState move = _inMove.FirstOrDefault(moveData => moveData.Transform == cardTransform);

            if (move != default)
            {
                _inMove.Remove(move);
            }
        }
    }
}