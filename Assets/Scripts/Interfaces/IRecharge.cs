interface IRecharge
{
    public float UseTime { get; }
    public float RemainingTime { get; }
    public void UpdateRechargeTimer();
}