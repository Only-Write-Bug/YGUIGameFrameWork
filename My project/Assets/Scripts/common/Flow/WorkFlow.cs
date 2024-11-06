using common.Enum;

namespace common.Flow
{
    public class WorkFlow : BaseFlow
    {
        public EFinishResult Start(dynamic[] args)
        {
            if (steps.Count <= 0)
                return EFinishResult.WARN;
            var lastStepResult = args;
            foreach (var step in steps)
            {
                var result = step.Invoke(lastStepResult);
                if(result.finishState > EFinishResult.FAILED)
                {
                    lastStepResult = result.result;
                    continue;
                }
                Logger.Error($"Ready FLow work Error: {step.Method.Name}");
                return EFinishResult.FAILED;
            }

            return EFinishResult.SUCCEED;
        }
    }
}