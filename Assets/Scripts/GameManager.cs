using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public Text gameOverText;
    public Text scoreText;
    public Text livesText;
    public Button RePlayGame;
    public Text TryAgain;
    public int GhostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }

    private void Start()
    {
        NewGame();
    }
    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }
    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }
    private void NewRound()
    {
        gameOverText.enabled = false;
        RePlayGame.enabled = false;
        TryAgain.enabled = false;
 

        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }
    private void ResetState()
    {
        ResetGhostMultiplier();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }
        this.pacman.ResetState();
    }
    private void GameOver()
    {
        gameOverText.enabled = true;
        RePlayGame.enabled=true;
        TryAgain.enabled = true;

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }
        this.pacman.gameObject.SetActive(false);
    }
    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');
    }
    private void SetLives(int Lives)
    {
        this.lives = Lives;
        livesText.text = "x" + lives.ToString();
    }
    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * GhostMultiplier;
        SetScore(this.score + points);
        this.GhostMultiplier++;
    }
    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);
        SetLives(this.lives = this.lives - 1);
        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);
        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 0.3f);
        }
    }
    public void PowerPelletEaten(PowerPellet pellet)
    {
        for(int i=0; i<this.ghosts.Length; i++)
        {
            this.ghosts[i].frightend.Enable(pellet.duration);
        }
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);

    }
    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf) return true;
        }
        return false;
    }
    private void ResetGhostMultiplier()
    {
        this.GhostMultiplier = 1;
    }

}
