using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using common.Enum;

namespace common.Flow
{
    public class Flow
    {
        public string name { private set; get; }
        private EExecutionState _executionState = EExecutionState.Ready;
        private Queue<Step> _steps = new Queue<Step>();
        private Queue<Step> _trash = new Queue<Step>();

        public bool isRunning => _executionState == EExecutionState.Running;

        public Flow(string name)
        {
            this.name = name;
        }
        
        private class Step
        {
            public bool isAllow => Condition.Invoke();
            public bool isAsync = false;
            public Action Work { get; set; } = () => { };
            public Func<bool> Condition { get; set; } = () => true;
        }
        
        public void AddAsyncCall(Action action)
        {
            _steps.Enqueue(new Step()
            {
                isAsync = true,
                Work = action
            });
        }

        public void AddSyncCall(Action action)
        {
            _steps.Enqueue(new Step()
            {
                Work = action
            });
        }

        public void AddAsyncStep(IStep step)
        {
            _steps.Enqueue(new Step()
            {
                isAsync = true,
                Work = step.StepWork
            });
        }

        public void AddSyncStep(IStep step)
        {
            _steps.Enqueue(new Step()
            {
                Work = step.StepWork
            });
        }

        public void AddCondition(Func<bool> condition)
        {
            _steps.Enqueue(new Step()
            {
                Condition = condition
            });
        }

        public void Start()
        {
            _executionState = EExecutionState.Running;

            while (_steps.Count > 0)
            {
                var step = _steps.Peek();
                if (!step.isAllow) 
                    continue;
                _trash.Enqueue(_steps.Dequeue());
                try
                {
                    if (step.isAsync)
                    {
                        Task.Run(step.Work);
                    }
                    else
                    {
                        step.Work();
                    }
                }
                catch (Exception e)
                {
                    Logger.Error($"flow : {name}, work has error, error message : {e.Message}");
                }
            }
        }

        public void Clear()
        {
            _steps.Clear();
            _trash.Clear();
        }

        public void Stop()
        {
            Reset();
        }

        public void Reset()
        {
            _executionState = EExecutionState.Stop;
            while (_steps.Count > 0)
            {
                _trash.Enqueue(_steps.Dequeue());
            }

            _steps = _trash;
            _trash = new Queue<Step>();
        }

        public void Pause()
        {
            _executionState = EExecutionState.Pause;
        }
    }
}