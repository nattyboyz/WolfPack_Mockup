using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.StateMachine
{
    public class StateMachine
    {
        private State _currentState;
        private StateTransition? _stateTransition;
        private readonly Dictionary<Type, State> _states = new Dictionary<Type, State>();

        public State CurrentState => _currentState;
        
        /// <summary>
        /// Update the state machine.
        /// </summary>
        /// <param name="deltaTime">Current delta time.</param>
        public void Update(float deltaTime)
        {
            // Check if there's a pending state transition.
            if (HasPendingTransition())
            {
                // Get the transition data.
                Type stateType = _stateTransition.Value.GetStateType();
                object args = _stateTransition.Value.GetArgs();
                _stateTransition = null;

                // Assign the next state as current state.
                SetCurrentState(stateType, args);
            }
            
            // Update current state.
            _currentState?.OnUpdate(deltaTime);
        }

        public void FixedUpdate()
        {
            _currentState?.OnFixedUpdate();
        }

        public void LateUpdate()
        {
            _currentState?.OnLateUpdate();
        }

        /// <summary>
        /// Set current active state.
        /// </summary>
        /// <param name="stateType">The state type.</param>
        /// <param name="args">Optional arguments.</param>
        private void SetCurrentState(Type stateType, object args = null)
        {
            // Exit previous state.
            _currentState?.OnExit();
            
            // Get the next state.
            _states.TryGetValue(stateType, out State state);
            if (state == null) throw new ArgumentNullException();
            
            // Enter the next state.
            state.OnEnter(args);
            
            // Set the next state as current state.
            _currentState = state;
        }
        
        /// <summary>
        /// Add a new state to the state machine.
        /// </summary>
        /// <param name="state">The state to add.</param>
        public void AddState(State state)
        {
            state.SetStateMachine(this);
            _states.Add(state.GetType(), state);
        }
        
        /// <summary>
        /// Transition to a new state.
        /// </summary>
        /// <param name="stateType">The type of the state to transition to.</param>
        /// <param name="args">Optional arguments.</param>
        public void TransitionToState(Type stateType, object args = null)
        {
            if (_stateTransition.HasValue)
            {
                Debug.LogWarning("Can't add perform state transition. A pending state transition already exists.");
                return;
            }
            
            _stateTransition = new StateTransition(stateType, args);
        }

        /// <summary>
        /// Check if there is a pending transition.
        /// </summary>
        /// <returns>True if there's a pending transition.</returns>
        public bool HasPendingTransition()
        {
            return _stateTransition.HasValue;
        }
    }
}