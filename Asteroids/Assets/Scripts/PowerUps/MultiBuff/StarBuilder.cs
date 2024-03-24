using UnityEngine;

public class StarBuilder: IStarBuilder {
    private Star star;

    public StarBuilder(){
        this.Reset();
    }

    public void Reset(){
        this.star = new Star();
    }

    public IStarBuilder SetScoreCount(){
        this.star.score = Random.Range(0, 501);
        return this;
    }

    public IStarBuilder SetLivesCount(){
        this.star.lives = Random.Range(0, 3);
        return this;
    }

    public IStarBuilder SetSpeedCount(){
        this.star.speed = Random.Range(0.0f, 1.0f);
        return this;
    }

    public Star GetStar(){
        Star star = new Star(this.star);
        this.Reset();
        return star;
    }
}