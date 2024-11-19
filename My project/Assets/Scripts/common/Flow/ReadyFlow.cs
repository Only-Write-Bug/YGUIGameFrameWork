using System;
using common.Enum;

namespace common.Flow
{
    /// <summary>
    /// 准备流：不会进行参数/结果传递的工作流，只会依赖工作步骤返回的结果，当结果为Failed时结束工作
    /// Workflows that do not pass parameters/results will only rely on the results returned by the work step, and end the work when the result is Failed
    /// </summary>
    public class ReadyFlow : BaseFlow
    {
        public new EFinishResult Start(dynamic[] args = null)
        {
            if (steps.Count <= 0)
                return EFinishResult.WARN;

            foreach (var step in steps)
            {
                var result = step.Invoke(null);
                if (result.finishState > EFinishResult.FAILED) 
                    continue;
                Logger.Error($"Ready FLow work Error: {step.Method.Name}");
                return EFinishResult.FAILED;
            }

            return EFinishResult.SUCCEED;
        }
    }
}