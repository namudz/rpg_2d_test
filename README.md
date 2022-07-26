# RPG 2D Test
![demo](/Demo/rpg_2d_demo.gif)

## How to balance
Balance of the game can be achieved through ScriptableObjects. 
Are located on Assets > Scriptable Objects.
	- GridConfig: handle the grid size
	- GameInitialStateConfig: how many units are spawned, which units and where.
	- Units folder: stats of the Units.	

## How to play
- Select any of your units (displayed with alpha at 50%).
    - Unselect the unit by clicking on it again.

- Click on any cell within range:
    - Movement range: displayed with green color.
        - If the cell is occupied and outside the attack range, the unit will not move.
    - Attack range: displayed with red color.    
        - If an enemy is over the cell, the unit selected will apply damage to the enemy.

- Once you have moved all your units or skipped your turn, it's time to the CPU to move!

Enjoy!

## Class Diagrams
<details>
	<summary>Show diagrams</summary>
	
![diagram_1](/Diagrams/Diagram_1-Game.png)

--- 

![diagram_2](/Diagrams/Diagram_2-Grid.png)

---

![diagram_3](/Diagrams/Diagram_3-GameInitialStateConfig.png)

---

![diagram_4](/Diagrams/Diagram_4-TurnDealer.png)

---

![diagram_5](/Diagrams/Diagram_5-AUnitController.png)
</details>

## Releases
[Latest version](https://github.com/namudz/rpg_2d_test/releases/latest)

## Unity Version
2019.2.21f1

## Credits
    - Sprites: 0x72 (https://0x72.itch.io/dungeontileset-ii)
        
    
