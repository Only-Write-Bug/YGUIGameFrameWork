namespace common.StateMachine
{
    //枚举值顺序决定了状态机切换状态的顺序，新增顺序时请务必注意
    public enum EMachineState
    {
        Empty,
        OnEnter,
        OnRunning,
        OnExit,
    }
}