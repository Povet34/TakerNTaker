using Goldmetal.UndeadSurvivor;

public interface IGauge
{
    /// <summary>
    /// ĳ���� �ð�
    /// </summary>
    float ChangingTime { get; set; }

    /// <summary>
    /// �����̸鼭 ĳ���� ���� ����
    /// </summary>
    bool CanChangeWithMove {  get; set; }

    /// <summary>
    /// �����̸鼭 �ߵ� ���� ����
    /// </summary>
    bool CanActivateWithMove { get; set; }

    /// <summary>
    /// ĳ���� �Ϸ��, ��� ����
    /// </summary>
    bool IsAsSoonAsCasted { get; set; }

    /// <summary>
    /// �ǰ� ���ҽ�, ĵ�� ����
    /// </summary>
    bool CanEndurable {  get; set; }
    void Charge();
    bool IsCharingDone();
}   
