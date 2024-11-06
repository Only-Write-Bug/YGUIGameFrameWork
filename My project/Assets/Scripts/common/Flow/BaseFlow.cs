using System;
using System.Collections.Generic;
using common.Enum;

namespace common.Flow
{
    public class StepResult
    {
        public EFinishResult finishState;
        public dynamic[] result;
    }
    
    public class BaseFlow : IFlow
    {
        protected List<Func<dynamic[], StepResult>> steps;
        
        public EFinishResult Start(dynamic[] args)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            steps.Clear();
        }

        public BaseFlow Add(Func<dynamic[], StepResult> callback)
        {
            steps.Add(callback);
            return this;
        }
    }
}