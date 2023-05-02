using UnityEngine;

public class PowerCheese : Cheese
{
    public float duration = 8f;

    protected override void Eat()
    {
        FindObjectOfType<GameManager>().PowerCheeseEaten(this);
    }

}