### Galaxy Wars Game Overview

**Game Title**: Galaxy Wars

**Game Genre**: Multiplayer, Turn-based, Console-based Strategy Game

**Game Description**:
Galaxy Wars is a strategy game where players start on randomly selected planets in a galaxy. Players collect resources on their planets, which they can sell or use to build ships. The goal is to build powerful fleets and dominate the galaxy.

**Game Mechanics**:

1. **Planet and Resource Management**:
   - Each planet has a 2D map filled with logically distributed resources.
   - Players can collect these resources, sell them, or use them to build ships.
   - Each planet is displayed in the color of the owning player.

2. **Ship and Fleet Configuration**:
   - Players can create fleets from various types of ships (Scout, Fighter, Frigate, Destroyer, Cruiser, Capital Ship, Freighter).
   - Each ship type has attributes such as name, maximum speed, fuel capacity, cargo capacity, firepower, shield strength, and cost.
   - When creating fleets, players can add any available ships to their fleets.

3. **Turn-based Strategy**:
   - The game is turn-based, and each player's turn involves making strategic decisions and managing fleets.
   - Players can perform various actions (buying ships, creating fleets, moving fleets, attacking, upgrading planet defenses, etc.) when it's their turn.

4. **Multiplayer Dynamics**:
   - Players compete to dominate the galaxy by efficiently using resources and making strategic moves.
   - Players can attack each other to destroy fleets or capture planets.

**Development Details**:

- The game will be developed in .NET and played through a console-based interface.
- The code will be written in C#, with each component of the game planned out in detail with classes and methods.

**Main Components of the Game**:

1. **Player Class**:
   - Stores the player's name, health, gold, fleets, ships, and resources.
   - Players can buy ships, create fleets, and upgrade planet defenses.

2. **Planet Class**:
   - Stores the planet's name, resources, position, and defense capacity.
   - Produces a certain amount of resources each turn and transfers them to the owning player.
   - When captured by a player, the planet's color changes to the player's color.

3. **Fleet Class**:
   - Stores the ships in a fleet and the fleet's location.
   - Fleets can move and attack other fleets.

4. **SpaceShip Class**:
   - Stores each ship's name, type, speed, fuel capacity, cargo capacity, firepower, shield strength, and cost.

5. **Game Class**:
   - Manages the main flow of the game.
   - Sets up players and planets.
   - Runs the main game loop and allows players to take actions each turn.

6. **GameDisplay Class**:
   - Displays the game board and the status of players on the console.
   - Shows the status of fleets and planets on the screen.

**How to Play**:

- At the start of the game, players are placed at random locations.
- Players take turns and can perform the following actions:
  1. **Buy Ship**: Players can buy ships.
  2. **Create Fleet**: Players can create new fleets and add their ships to these fleets.
  3. **Move Fleet**: Players can move their fleets to different planets.
  4. **Attack**: Players can attack other players with their fleets.
  5. **Upgrade Planet Defense**: Players can increase the defense capacity of their planets using gold.
  6. **View Occupied Planets and Fleets**: Players can view their own planets and fleets, as well as the planets occupied by other players.

Galaxy Wars is an exciting game that allows you to use your strategy and management skills to dominate the galaxy. This game offers a fun and challenging experience where players must efficiently use their resources and make strategic moves to conquer the galaxy.
