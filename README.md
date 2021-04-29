# Car Parking Agent

## Goal
The focus of this project is to develop an agent that is adept in obstacle avoidance and seamless navigation. The project makes use of a multi-level car navigation game in Unity where a car must navigate through fixed and moving obstacles and park at the highlighted spot. 

## Game Overview
We found an open source 3D game developed in Unity and made modifications to it according to our use case. The game environment can be described as follows:

### Level 1
Level 1 of the game consists of a bounded arena with a car starting at an arbitrary position and a parking spot appearing at another random position. The goal location or parking spot is highlighted in red color. The car has to first identify the highlighted spot and then navigate towards it through three obstacles that are placed in the center of the arena.
![ezgif com-video-to-gif](https://user-images.githubusercontent.com/37352722/85719927-95d30280-b6f8-11ea-8be9-9c27e29541e7.gif)

### Level 2
Level 2 unlike level 1 is more complex. It consists of a bounded arena similar to level 1 but with moving obstacles and portals to navigate to different storeys. The car will start at an arbitrary position and a parking spot will appear on the same storey or a path will be highlighted to another storey. The car has to identify the highlighted parking spot or storey entrance and navigate through moving obstacles in the arena to reach the parking spot. The objects in the yellow color in the Fig2 are obstacles which move parallel to the walls of the arena and the object is green color is the portal that can help navigate to different storeys.
![ezgif com-video-to-gif (1)](https://user-images.githubusercontent.com/37352722/85720532-3295a000-b6f9-11ea-93ac-7227a5be51c1.gif)

## Game modifications

Some new features have been added to the game and some existing features have been modified to
ease training and make the game compatible to be trained with the ml-agents package.

### Scoreboard

While an AI agent is training, it is important to keep track of the performance of the agent. For
the purposes of debugging and performance analysis, following metrics were added to the game.
* Parking Score: Number of times agent parked the car in the highlighted spot
* Wall Hit Score: Number of times agent hit the walls
* Obstacle Hit Score: Number of times agent hit the obstacles
* Cumulative reward: In each episode, we try to see if the agent is moving towards the goal
(positive rewards) or away from the goal (negative rewards). This helps in debugging different
scenarios and also to identify if the agent is making correct inferences.

### Navigation
The open source game from Unity had touch screen controls, to change the direction of the car.
The touch screen controls have been modified to keyboard controls. The right arrow or \d" button
on the keyboard is used to change the direction to right and the left arrow or \a" button is used to
change the direction to left. In level 2 of the game, more controls for driving the agent have been
added. Along with changing the direction of the car, the agent can also accelerate or brake and
stay in the current location. The forward arrow or \w" button is used to accelerate the agent and
when this key is released, the agent stops accelerating in the forward direction. Using keyboard
controls we can leverage the mlagents package for training the agent which cannot be done with
touch screen controls as there is no support for touch screen in ml-agents package currently.

### Fixed Starting Point
In the open source game, the car that should be parked can start at any random location. We
have fixed the starting point to a single location for level 1 and level 2 of the game to reduce the
training time. A random start location exponentially increases the training time and the hardware
of our machines does not support training for such longer times.

### Fixed but Multiple Goal Points
In the modified game, there are some predefined goal locations where the agent is expected to
park the vehicle. For each episode, a goal location is randomly selected from the set of the 3 goal
locations for level 1 and 1 or 2 goal locations for level 2 for the agent to park. The higher the
number of goal locations, the more is the training time for training the agent. Since, level 2 is
tremendously complex in terms of environment and moving obstacles, it was trained for a single
goal location for two storeys.

### Collision Objects
In the open source game, only the obstacles in the arena are treated as collision objects. The
game was modified to treat both the walls and obstacles as collision objects. Whenever the agents
collides with either the wall or an obstacle, a negative reward is assigned to the agent, the episode
ends and the agent is starting again at the start location to park at the same highlighted spot.

## Training and Results
For more information about the training and results of the agent, please refer to the 
documentation in the website [here](https://usc-csci527-spring2021.github.io/Park-It-Right-/index.html)

## Contact
If you want to contact me you can reach me at <km69564@usc.edu> or <krishnamanoj14@gmail.com>.
