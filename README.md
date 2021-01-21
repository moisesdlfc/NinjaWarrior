# NinjaWarrior
 2D platform game where a ninja must overcome all the obstacles in his way, trying to travel as far as possible to get the best score.

<b>NinjaWarrior v1.0</b>

Features:
- The scene uses procedural generation to create platforms.
- Player automatically moves forward.
- Player can jump.
- Player start with 3 health points.
- Player can be damaged by enemies and restore health with potions.
- Ground is lava and kill the player when touched.

Scenes:
- MainMenu
	Main menu with buttons to Start Game or Exit Game.

- Game
	In game scene.

Main scripts:
- GameManager.cs
	It controls the main functionalities of the game and is responsible for organizing and synchronizing the rest of the components.

- PlayerController.cs
	Stores and manages player data and behaviors.

- CameraFollow.cs
	Manages the data and movements made by the camera following the player.

- LevelGenerator.cs
	Manages the procedural generation of the scene. Stores the data and behaviors of the platform componentes.

- LevelBlock.cs
	Stores the start points and exit points of individual blocks.

- LeaveZone.cs
	Manages the destruction of old blocks and create new blocks.

- Collectable.cs
	Manages the execution times and the behavior of the collectables based on its type.

- KillTrigger.cs
	Control player damage and death behavior.

- Parallax.cs
	Background movement control.

Enemies:
- Cannon
	Shoot cannon balls with static rate of fire.
	Damage: 1
	Individual scripts:
		Cannon: Manages rate of fire and instantiate cannon balls.
		CannonBall: Manages cannon ball movement and own destruction.

- Spikes
	Static spikes above ground.
	Damage: 2

- ChainSaw
	Saw that rides on the platform it is on.
	Damage: 3
	Individual scripts:
		Saw: Manages the movement of the saw on the platform.
		SawRotation: Manages the saw rotation.

Items:
- Feather
	Increase the player’s jumping power temporally.

- Death Coin
	Make the player invulnerable temporally.

- Potion
	Recover 1 health point.

Future features:
- MaxPotion - Recover initial health if currentHealth < initialHealth
- ExtraHealth - Earn extra health if currentHealth == initialHealth

Bugs:
- Background parallax doesn’t contemplate the player reset. (New method Reset() in Parallax.cs called by PlayerController.cs ? )
