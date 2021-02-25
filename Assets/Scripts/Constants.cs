using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    private static float MAX_STEPS = 50000;
    public static float CLOSE_TO_WALL_REWARD = -0.001f; 
    public static float CLOSEST_TO_TARGET_REWARD = 1f/MAX_STEPS;
    public static float BEST_DISTANCE_TO_TARGET_REWARD = -1f/MAX_STEPS;
    public static float MOVING_TO_TARGET_REWARD = -2f/MAX_STEPS;
    public static float MOVING_AWAY_TARGET_REWARD = -4f/MAX_STEPS;
}
