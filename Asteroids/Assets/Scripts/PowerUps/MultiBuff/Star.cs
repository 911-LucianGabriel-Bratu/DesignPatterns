public class Star{
    public int score {get; set;}
    public int lives {get; set;}
    public float speed {get; set;}

    public Star(){
        this.score = 0;
        this.lives = 0;
        this.speed = 0.0f;
    }

    public Star(Star star){
        this.score = star.score;
        this.lives = star.lives;
        this.speed = star.speed;
    }
}