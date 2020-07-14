using UnityEngine;

namespace Plugins.StateMachine
{
    public abstract class State
    {
        private StateMachine _stateMachine;
  
        public virtual void OnEnter(object args)
        {
            
        }
    
        public virtual void OnExit()
        {
            
        }
    
        public virtual void OnUpdate(float deltaTime)
        {
            
        }

        public virtual void OnFixedUpdate()
        {

        }

        public virtual void OnLateUpdate()
        {

        }

        public void SetStateMachine(StateMachine stateMachine)
        {
            if (_stateMachine != null)
            {
                Debug.LogError("Changing already assigned StateMachine is not supported.");
            }
            
            _stateMachine = stateMachine;
        }
        
        public StateMachine GetStateMachine()
        {
            return _stateMachine;
        }
    }
}