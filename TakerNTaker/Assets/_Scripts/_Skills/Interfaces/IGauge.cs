using Goldmetal.UndeadSurvivor;

public interface IGauge
{
    /// <summary>
    /// 캐스팅 시간
    /// </summary>
    float ChangingTime { get; set; }

    /// <summary>
    /// 움직이면서 캐스팅 가능 여부
    /// </summary>
    bool CanChangeWithMove {  get; set; }

    /// <summary>
    /// 움직이면서 발동 가능 여부
    /// </summary>
    bool CanActivateWithMove { get; set; }

    /// <summary>
    /// 캐스팅 완료시, 즉발 여부
    /// </summary>
    bool IsAsSoonAsCasted { get; set; }

    /// <summary>
    /// 피격 당할시, 캔슬 여부
    /// </summary>
    bool CanEndurable {  get; set; }
    void Charge();
    bool IsCharingDone();
}   
