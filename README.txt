-------------------------
- Vancouver Film School -
-------------------------
--- Game Design ---------
--- Unity I -------------
--- Assigment 3 ---------
- Guilherme Toda - GD53 -
-------------------------
Smash Brawl

This is a 2 player game that the first player to collect 10 gems wins the match, 
during the battle, the players can attack each other and when the player dies he loses all his collected gems.

Controls
Player1 - Adventurer from Platform2D
A -> Move Left
D -> Move Right
Space -> Jump
Left Control -> Attack

Player2 - The Bandit who looks like Jesus
Arrow Key Left -> Move Left
Arrow Key Right -> Move Right
Arrow Key Up -> Jump
/ -> Attack

---> I got most of the movement mechanics from the Platform that we made in class and I added some codes to create a different gameplay

Coded Features
Melee Attack (Player.cs and Character.cs) - I tried some things to do this and the best approach was by using a Overlap Box and every time that the 
player "isAttacking" I got all overlapped game objects and if they have Health and if the object isn't the Player whos attacking, i take some damage from them.

Gem Spawn (ItemSpawn.cs) - The Gems of the level are only spawned on Platforms that have the "Ground" tag
and after 3s I randomly get a new ground and I spawn the gem in a random position of the platform

EnergyBall (EnergyBallSpawner.cs and Projectile.cs) - At every three seconds a Energy ball is spawned in the right or left side of the screen. 

PlayerRespawn.cs - At every frame I check if all the players are alive, if there is one player dead I respawn that player
 
GameScore.cs - My static values are all there, in this script I can control the points of the players
