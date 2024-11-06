using UnityEngine;

public class PowerPellet : Pellet
{
    public float effectDuration = 8.0f;

    protected override void Eat()
    {
        FindFirstObjectByType<GameManager>().PowerPelletEaten(this);
    }
}
