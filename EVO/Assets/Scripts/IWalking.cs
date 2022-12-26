using System;

public interface IWalking
{
    public float BaseSpeed { get; set; }
    public float SpeedMultiplier { get; set; }
    
    public float Speed => BaseSpeed * SpeedMultiplier;
    
    public void ChangeSpeedMultiplier(float coef)
    {
        SpeedMultiplier *= coef;
    }
}