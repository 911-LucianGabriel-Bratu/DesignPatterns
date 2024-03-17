public interface IScoreObserver
{
    void OnScoreThresholdReached();

    void OnScoreReset();
}