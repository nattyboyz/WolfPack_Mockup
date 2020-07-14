using System;

namespace Plugins.StateMachine
{
    public struct StateTransition
    {
        private readonly Type _stateType;
        private readonly object _args;
        
        public StateTransition(Type stateType, object args = null)
        {
            _stateType = stateType;
            _args = args;
        }

        public Type GetStateType()
        {
            return _stateType;
        }

        public object GetArgs()
        {
            return _args;
        }
    }
}