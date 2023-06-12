# Game Basic Information #

## Summary ##

**A paragraph-length pitch for your game.**

This is a 2D RPG game. 

## Gameplay Explanation ##

**In this section, explain how the game should be played. Treat this as a manual within a game. It is encouraged to explain the button mappings and the most optimal gameplay strategy.**


**If you did work that should be factored in to your grade that does not fit easily into the proscribed roles, add it here! Please include links to resources and descriptions of game-related material that does not fit into roles here.**

# Main Roles #
Your goal is to relate the work of your role and sub-role in terms of the content of the course. Please look at the role sections below for specific instructions for each role.

Below is a template for you to highlight items of your work. These provide the evidence needed for your work to be evaluated. Try to have at least 4 such descriptions. They will be assessed on the quality of the underlying system and how they are linked to course content. 

*Short Description* - Long description of your work item that includes how it is relevant to topics discussed in class. [link to evidence in your repository](https://github.com/dr-jam/ECS189L/edit/project-description/ProjectDocumentTemplate.md)

Here is an example:  
*Procedural Terrain* - The background of the game consists of procedurally-generated terrain that is produced with Perlin noise. This terrain can be modified by the game at run-time via a call to its script methods. The intent is to allow the player to modify the terrain. This system is based on the component design pattern and the procedural content generation portions of the course. [The PCG terrain generation script](https://github.com/dr-jam/CameraControlExercise/blob/513b927e87fc686fe627bf7d4ff6ff841cf34e9f/Obscura/Assets/Scripts/TerrainGenerator.cs#L6).

You should replay any **bold text** with your relevant information. Liberally use the template when necessary and appropriate.


## Producer 
### Jesse Liu
- Group Scheduling: 
  - Used [whentomeet](https://www.when2meet.com/?19949103-hDTso) to get avaliable time from everyone.
  - Scheduled zoom meetings twice a week for brief updates, assign tasks.
- Task organize:
  - Used the Github Issue feature to articulate each task detail and Project feature to have a macro vision of the current task progress. 
  - Set up 1-1 zoom meeting with teammates to ensure the understanding of each task, and provide helps for any Unity feature & script coding problems.
- Task:
  - Open & Merge branches for difficult tasks

## User Interface
### Xuanzhen Lao
- Built the basic outlook style of `BattleScene` by using TileMap. [issue] (https://github.com/Jesse1211/ECS-189L/issues/17).
- Also built `MainScene` from collected model. [issue](https://github.com/Jesse1211/ECS-189L/issues/10)

### Zuge Li & Jesse
- Enhenced the `BattleScene` by expanding the range of grid using `TileMap`
- Added multiple responsive prefabs with scripts for `Boat`, `Elevator`, `Death Zone`, `Dragonfly`, `Climable Wall`, `BloodTrees`, `Orb Plant`. [issue] (https://github.com/Jesse1211/ECS-189L/issues/23).
- Added `Dialog` feature by using `Canvas`, `Image`, `Button`, `Text` and Scripts which helps to achieve typing effect. [issue](https://github.com/Jesse1211/ECS-189L/issues/36).

### LiangHang Wu
- Designed & Implemented `FloatingIslandScene` with prefabs which made by `TileMap`
- Worked with Jesse (Producer):
    - Implemented UI for bag with responsive actions by using `Canvas`, many game objects for slots inside the bag
    - Used many game objects as slots of `active weapon` and `collected items`.

### Jesse Liu
- Used `Mask`, `Fill` to display the number of orbs collected. And `MeterScript` which sets the value of `Slider` with gradient color

### Jianfeng Lin
- Used `FillArea`, `Fill` to display the number of orbs collected. And `CharacterHP` which sets the value of `FillArea`
- Implemented `DeathScene` when player's HP drops lower than 1

### Hugo Lin
- Used `Canvas` and implemented an animation & animator for scene switching event with `ToNextScene`

## Animation and Visuals
### LiangHang Wu
- Finalized Animator & Animation for main character, depolyed it to `FloatingIslandScene`. [issue](https://github.com/Jesse1211/ECS-189L/issues/18)
- Implemented animator `Fanboy`, `Gunslinger`. [issue](https://github.com/Jesse1211/ECS-189L/issues/43)
### Zuge Li
- Implemented animation states for most models including `elk`, `fox`, `crow`, `rats`, `Fanboy`, `Gunslinger`. [issue](https://github.com/Jesse1211/ECS-189L/issues/26).
### Zuge Li & Jesse Liu
- Secondary drafted animator controller for main character
### Xinhe Wang & Jesse Liu
- First Drafted animator controller for main character
### Xinhe Wang
- Illustration

## Game Logic
### Jesse Liu
- Bag:
  - `BagManager`: provides `useItem()` when user clicks the item. `useItem()` determines to destory or swap the item to another weapon list
  - `BagItemSlot`: tracks if player right clicks the item, it execute `BagManager.useItem()`
  - `BagDataLoader`: store two lists and provides 4 APIs to `BagManager.useItem()`
  - The `PlayerCOntrollerData` has access to `BagManager` to store the collided item into bag
- PlayerController:
  - `PlayerControllerData` handles the logic when player collides some game objects, and provides `TakeDamange(int damage)` to modify HP and `blood` animation.
  - `PlayerControllerAnimator` inherits `PlayerControllerData` handles setting triggers in the animator, and the logic to attack by instantiate prefabs
### Jianfeng Lin & Zuge Li
- Enemy AI:
  - Designed enemy AI by using FSM which sets `Idle`, `Patrol`, `Chase`, `React`, `Attack`, `Death`, `IsHit`, `teleport` states that make enemy more responsive to player. 
  - Use interface to provide `OnEnter()`, `OnUpdate()`, `OnExit()`, `GetHp()`, `SetUp()` to each state.
### Lianghang Wu
- Simple Enemy NPC:
  - Impletement NPCs' attack, run animation from `Enemy`

# Sub-Roles

## Audio
### Jianfeng Lin & Huge Lin
- Depoly audio into game
- **Describe the implementation of your audio system.**
### Xinhe Wang
- Original audio developer
- **Document the sound style.** 

## Gameplay Testing
### Jesse Liu
- Only used `Debug.Log()` to make sure the functionality

## Narrative Design
### Xinhe Wang
- **Document how the narrative is present in the game via assets, gameplay systems, and gameplay.** 

## Press Kit and Trailer
### Xinhe Wang
**Include links to your presskit materials and trailer.**
**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**

## Game Feel
### LiangHang Wu
- **Document what you added to and how you tweaked your game to improve its game feel.**