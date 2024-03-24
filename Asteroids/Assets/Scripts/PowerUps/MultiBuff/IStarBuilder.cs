public interface IStarBuilder {
    public IStarBuilder SetScoreCount();
    public IStarBuilder SetSpeedCount();
    public IStarBuilder SetLivesCount();
    public Star GetStar();
}