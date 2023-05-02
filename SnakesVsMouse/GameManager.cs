using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Snake[] snakes;
    public Mouse mouse;
    public Transform cheeses;

    public Text gameOverText;
    public Text scoreText;
    public Text livesText;

    public int snakeMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown) {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        gameOverText.enabled = false;

        foreach (Transform cheese in cheeses) {
            cheese.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < snakes.Length; i++) {
            snakes[i].ResetState();
        }

        mouse.ResetState();
    }

    private void GameOver()
    {
        gameOverText.enabled = true;

        for (int i = 0; i < snakes.Length; i++) {
            snakes[i].gameObject.SetActive(false);
        }

        mouse.gameObject.SetActive(false);
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');
    }

    public void MouseEaten()
    {
        mouse.DeathSequence();

        SetLives(lives - 1);

        if (lives > 0) {
            Invoke(nameof(ResetState), 3f);
        } else {
            GameOver();
        }
    }

    public void SnakesEaten(Snake snake)
    {
        int points = snake.points * snakeMultiplier;
        SetScore(score + points);

        snakeMultiplier++;
    }

    public void CheeseEaten(Cheese cheese)
    {
        cheese.gameObject.SetActive(false);

        SetScore(score + cheese.points);

        if (!HasRemainingCheeses())
        {
            mouse.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    public void PowerCheeseEaten(PowerCheese cheese)
    {
        for (int i = 0; i < snakes.Length; i++) {
            snakes[i].frightened.Enable(cheese.duration);
        }

        CheeseEaten(cheese);
        CancelInvoke(nameof(ResetSnakeMultiplier));
        Invoke(nameof(ResetSnakeMultiplier), cheese.duration);
    }

    private bool HasRemainingCheeses()
    {
        foreach (Transform cheese in cheeses)
        {
            if (Cheese.gameObject.activeSelf) {
                return true;
            }
        }

        return false;
    }

    private void ResetSnakeMultiplier()
    {
        snakeMultiplier = 1;
    }

}