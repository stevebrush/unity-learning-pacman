using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public int GhostMultiplier { get; private set; }
    public int Score { get; private set; }
    public int Lives { get; private set; }

    public void GhostEaten(Ghost ghost)
    {
        SetScore(this.Score + (ghost.points * this.GhostMultiplier));
        this.GhostMultiplier++;
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);

        SetLives(this.Lives - 1);

        if (this.Lives > 0)
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
        SetScore(this.Score + pellet.points);

        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        foreach (var ghost in this.ghosts)
        {
            ghost.BehaviorScared.EnableForDuration(pellet.effectDuration);
        }

        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.effectDuration);
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (this.Lives <= 0 && Input.anyKeyDown)
        {
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
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        foreach (var ghost in this.ghosts)
        {
            ghost.ResetState();
        }

        this.pacman.ResetState();
        ResetGhostMultiplier();
    }

    private void GameOver()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(false);
        }

        foreach (var ghost in this.ghosts)
        {
            ghost.gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.Score = score;
    }

    private void SetLives(int lives)
    {
        this.Lives = lives;
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.GhostMultiplier = 1;
    }
}
