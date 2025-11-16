using UnityEngine;
using UnityEngine.UIElements;

public class RotatingPowerup : Powerup
{


    void Start()
    {
    }

    protected override void onPickUp()
    {
        
    }

    protected override void onUpdate()
    {
        RotatePowerUp();
    }
}
