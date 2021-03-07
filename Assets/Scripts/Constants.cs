using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public static float CLOSE_TO_WALL_REWARD = -0.005f;
    public static float CLOSE_TO_OBSTACLE_REWARD = -0.005f;
    public static float CLOSEST_TO_TARGET_REWARD = 0.00003f;
    public static float BEST_DISTANCE_TO_TARGET_REWARD = 0.00002f;
    public static float MOVING_TO_TARGET_REWARD = 0.00001f;
    public static float MOVING_AWAY_TARGET_REWARD = -0.00002f;
}
