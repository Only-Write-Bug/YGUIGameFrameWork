using System;
using System.Collections.Generic;
using util;

namespace common.StateMachine.BaseStateMachine
{
    public class BaseStateMachine
    {
        public string name { get; private set; }
        public (string stateKey, EMachineState workState) curState { get; private set; }

        private Dictionary<string, Dictionary<EMachineState, Action>> stateCallbacks = new();

        public BaseStateMachine(string name)
        {
            this.name = name;
            curState = (string.Empty, EMachineState.Empty);
        }

        public void SetStateCallbackByStateKey(string stateKey, EMachineState workState, Action callback)
        {
            if (!stateCallbacks.ContainsKey(stateKey))
            {
                stateCallbacks[stateKey] = new Dictionary<EMachineState, Action>();
            }
            stateCallbacks[stateKey][workState] = callback;
        }

        public void TransitionTo(string targetState, EMachineState targetWorkState)
        {
            if (curState.stateKey == targetState)
                return;

            ExitCurState();
            curState = (targetState, EMachineState.Empty);
            
        }

        private void ExitCurState()
        {
            if (string.IsNullOrEmpty(curState.stateKey))
                return;

            var subsequentState = EnumUtil.GetEnumValuesLargerThan(curState.workState);
            while (subsequentState.Count > 0)
            {
                var nextWorkState = subsequentState.Dequeue();
                if (stateCallbacks[curState.stateKey].TryGetValue((EMachineState)nextWorkState, out var callback))
                {
                    Logger.Log($"{curState.stateKey} transition to {nextWorkState}");
                    callback?.Invoke();
                    curState = (curState.stateKey, (EMachineState)nextWorkState);
                }
            }
            
            Logger.Log($"{curState.stateKey} exit over");
            curState = (string.Empty, EMachineState.Empty);
        }
    }
}