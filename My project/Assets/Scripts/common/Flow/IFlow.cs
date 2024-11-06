using System;
using common.Enum;
using common.Flow;

public interface IFlow
{
    public EFinishResult Start(dynamic[] args);
    public void Clear();
    public BaseFlow Add(Func<dynamic[], StepResult> callback);
}