# Game Basic Information #

## Summary ##

A little girl and her parents are trapped in a unknown island. To find and rescue her parents, she needs to follow Ms. Deer's instruction to collect unique items by fighting against monsters, gunslinger, and fanboy. She has to move soon, otherwise her parents will be eaten by the monsters.

## Gameplay Explanation ##

- Move left by pressing `A` or `left arrow` key; move right with `D` or `right arrow` key; jump using either `W` or `space bar`; dodge by pressing `L`.
- Click `K` for launch the default weapon. 
- Click `J` to hit the NPC without a weapon. 
- Press `B` or `Left-click` to open & close the bag and check the items / activated weapons you have.
  - Player will find other weapons while adventuring, they will be collected in the bag by touching them. 
  - Player can collect fruits to bump the HP
  - Open the bag, left panel shows the activated weapon. To active them, right click the collected weapon in the right panel. To launch the weapon, click `1`, `2`, `3`, `4`.
  
There are three scenes in this game:
- Main scene: 
  - The player can browse and look for Ms. Deer for more instructions. 
  - There are two game objects which trigger other scenes
- Scene1:
  - The player needs to adventure the map, find the Fanboy and kill him to get the secret item. Then the player needs to return to the start position to switch back to the main scene.
  - The Fanboy cannot be killed by using default weapon, player has to find the master weapon, active and use it to kill the Fanboy.
- Scene2:
  - The player starts from left and need to reach the right most point in the map to fight against Gunslinger. 
  - There are NPCs in the path and very easy to kill.
  - Gunslinger can only be killed by using master weapon.

# Main Roles #
## Producer - Jesse Liu - github: Jesse1211

Since our game is implemented from scratch, I have to ensure everyone knows the game type, storyline, essential features, current development progress, and expectation of the achievement. I have to keep active and communicative; I provided 1-1 meetings with everyone to make sure they know what to do and how to do it; I used [whentomeet](https://www.when2meet.com/?19949103-hDTso) to find the perfect time for weekly meetings to make sure the communication between teammates. Also, I added guidance about using GitHub to ensure everyone contributes conveniently.
- GitHub: 
  - I decided to use GitHub because [Issue](https://docs.github.com/en/issues/tracking-your-work-with-issues/about-issues), [Branch & Merging](https://www.varonis.com/blog/git-branching), [Project](https://docs.github.com/en/issues/planning-and-tracking-with-projects/learning-about-projects/about-projects) are very robust features as an environment to keep track of progress and deployment. 
  - I pushed all important files into this Repo, such as `ProgressReport`, `Proposal`, and `README`. `README` is to provide teammates basic knowledge of Github. 
  - I created over 50 issues by dividing each goal into small tasks. I open branches if the task takes a long time to implement to ensure the progress works in multi-thread. 
  - I created a [Project](https://github.com/users/Jesse1211/projects/1) in Github, which connects to the current repository. It provides Overall tasks, tasks sorted by Assignee in Table layout, and progress status view in Board layout. Since the project connects to the repository, we could put `Todo`, `In Progress`, and `Completed` statuses on each issue to provide issue status visually and positive feedback when completed.
  - The tricky point is Merging because Unity scenes are not reading-friendly. I tried to assign each branch works on different scenes, but the problem was inevitable. Therefore, I need the assignees accompany when merging the branching to avoid losing anything important. If something is wrong, we will look commitment history and get the old version of the scene, and update the references by hand. 

- Progress:
  - We split the project into three stages for 5 weeks. 1: draw scene by using `Tilemap`, add some prefabs. 2: Add script to prefabs to make it responsive, and implement the bag feature; 3: connect scenes, and implement enemy AI feature. 
  - Since most of the team members have no experience in Unity. A leader needs to catch up on Unity before everyone does. I have to figure out how to deploy the ideas into programming logic to increase working efficiency before assigning tasks. Then I can teach them coding or use Unity.
  - I believe tasks are not set only for specific assignees; they are always first come, first served by anyone. Since I am using divide & counter strategy, tasks are a small part of the stage. Anyone can finish them within a couple of hours. I provide my personal Zoom meeting link to anyone. Since I have learned how to implement it, I helped them complete most tasks efficiently. If they were not in the zone or stressed, I helped them to calm down and sort the logic by writing pseudocode.
    - Helped XuanZhen Lao and Zuge Li build scenes using `TileMap`. Using `TileMap` is not difficult, but drawing requires innovation. We did not know the final version of the map, but our innovation pumps out when brushing on the background. I set the different categories in the map because of component differences. Also, encapsulation is very convenient for future modification and maintenance. 
    - Helped Zuge Li to develop interactable prefabs, such as movable boat and elevator, and death zone which makes damage to player. etc.
    - Since [Bag feature](https://github.com/Jesse1211/ECS-189L/tree/main/Final%20Project/Assets/Scripts/Bag) is too difficult, I designed and implemented it. This was challenging because I had to implement it most generically. Otherwise, it will be harder to deploy the feature into the project.
      - Implementation details:
        - `BagManager`: provides `useItem()` when the user clicks the item. `useItem()` determines to destroy or swap the item to another weapon list
        - `BagItemSlot`: tracks if the player right-clicks the item, it executes `BagManager.useItem()`
        - `BagDataLoader`: store two lists and provides 4 APIs to `BagManager.useItem()`
        - The `PlayerCOntrollerData` has access to `BagManager` to store the collided item in the bag
    - Since the player is able to interact with every object in unity, it is better to split the controller into data and animator, implementation details:
      - `PlayerControllerData` handles the logic when the player collides with some game objects, and provides `TakeDamange(int damage)` to modify HP and `blood` animation.
      - `PlayerControllerAnimator` inherits `PlayerControllerData` handles setting triggers in the animator, and the logic to attack by instantiating prefabs
    - Taught teammates about making animation and animator controllers for characters and the API methods to switch scenes. Animation needs to be done rigorously. We modified some animations a couple of times later because we could see minor lagging in character.
    - Developed the structure of dialogue with typing effect in UI. This is a tricky feature because the UI only triggers when the player collides with certain prefabs. Also, typing effect happens in multi-thread. If the player moves away and hits the prefabs again, the typing thread cannot restart. we want to keep the player's experience, so we decided to "freeze" player movement until finished reading it. This is not the generic way to solve; it is the future task. 
    - Used `Mask`, `Fill` to display the number of orbs collected. And `MeterScript` which sets the value of `Slider` with gradient color


## User Interface - Xuanzhen Lao
- Built the basic outlook style of `BattleScene` by using TileMap. [issue] (https://github.com/Jesse1211/ECS-189L/issues/17).
- Also built `MainScene` from collected model. [issue](https://github.com/Jesse1211/ECS-189L/issues/10)

## Interface Designer - Zuge Li
- Enhenced the `BattleScene` by expanding the range of grid using `TileMap`
- Added multiple responsive prefabs with scripts for `Boat`, `Elevator`, `Death Zone`, `Dragonfly`, `Climable Wall`, `BloodTrees`, `Orb Plant`. [issue] (https://github.com/Jesse1211/ECS-189L/issues/23).
- Added `Dialog` feature by using `Canvas`, `Image`, `Button`, `Text` and Scripts which helps to achieve typing effect. [issue](https://github.com/Jesse1211/ECS-189L/issues/36).
- Implemented animation states for most models including `elk`, `fox`, `crow`, `rats`, `Fanboy`, `Gunslinger`. [issue](https://github.com/Jesse1211/ECS-189L/issues/26).
- Secondary drafted animator controller for main character
- Enemy AI:
  - Designed enemy AI by using FSM which sets `Idle`, `Patrol`, `Chase`, `React`, `Attack`, `Death`, `IsHit`, `teleport` states that make enemy more responsive to player. 
  - Use interface to provide `OnEnter()`, `OnUpdate()`, `OnExit()`, `GetHp()`, `SetUp()` to each state.

## Movement/physics - LiangHang Wu
- Designed & Implemented `FloatingIslandScene` with prefabs which made by `TileMap`
- Worked with Jesse (Producer):
    - Implemented UI for bag with responsive actions by using `Canvas`, many game objects for slots inside the bag
    - Used many game objects as slots of `active weapon` and `collected items`.
- Finalized Animator & Animation for main character, depolyed it to `FloatingIslandScene`. [issue](https://github.com/Jesse1211/ECS-189L/issues/18)
- Implemented animator `Fanboy`, `Gunslinger`. [issue](https://github.com/Jesse1211/ECS-189L/issues/43)
- Simple Enemy NPC:
  - Impletement NPCs' attack, run animation from `Enemy`

## Gameplay Programmer - Jianfeng Lin
- Camera Follow:
	The implementation of the camera controller is similar to the Exercise2 PositionFollowCamera.cs. But it has a little bit different. The boundary of the camera can be set manually so that the player won’t see the unrelated scene in the game. This is simply implemented by using the Lerp function and Clamp function
  
- Death Scene:
	When the player succumbs in the game, they are transported to the death scene. By interacting with any part of this death scene, they can navigate back to the last scene. The mechanism is simply implemented by a script “BackToFight”, which is attached to the button.

- Health Bar
	The functionality of the player's health bars is facilitated through a UI slider, composed of two distinct images. One is called the background, which indicates the background color of the health bar. The second is called “Fill area”, which indicates the current HP of the player. They are attached under the canvas. It is controlled by the script “CharacterHP.” However, this script is only invoked when the player's character dies, prompting the transition to the death scene. The health bar updates by assessing the value from “PlayerControllerAnimator.” The value of the health bar changes under various conditions, such as when the player stumbles into traps or when the player is attacked by the enemy.

- Enemy Health bar:
The Enemy health bar is implemented in the same way as the player health bar. However, these bars are linked to two game bosses, namely Fanboy and Gunslinger. Their health bars are displayed above their heads and have the capability to track the bosses' movements. The value of the health bar is updated by the script called “IdleState” and “GunSlinderIdleState”. In these two scripts, they have a common state, which is “IsHit.” It can be accessed when the Bosses are hit by the player. 

- ThunderBolt：
	Before the player can confront the Boss in the battle scene, they must first engage in conversation with the Non-Player Character (NPC). Once the dialogue with the NPC concludes, a thunder-created battle zone materializes. The player is restricted from leaving this zone until they have successfully defeated the Boss. This battle zone is implemented by the script called “thunderTrigger.” After 

- Enemy AI: 
	The Fanboy AI and Gunslinger AI are implemented by using the finite state machine. The scripts are under the file called EnemyAI. "Model" holds the fundamental data. "IdleState" includes eight states, namely Idle, Patrol, Chase, React, Attack, IsHit, Death, and Teleport.  They are able to transition between states under the condition. These states allow the AI to do some simple decisions. For instance, if the AI's HP falls below 30, it has the ability to teleport to a different location for HP recovery. Moreover, AI can chase and attack the player in a given area. “FSM” is an executor, which provides some helper functions such as TransitionState, Flip, and OnTriggerEnter2D to help transition the states. 
	


## Visuals/Input - Hugo Lin
- Used `Canvas` and implemented an animation & animator for scene switching event with `ToNextScene`

## Animation and Visuals
Assets: 
Collect meter: https://assetstore.unity.com/packages/2d/gui/icons/elemental-meters-173133
Bag items: https://assetstore.unity.com/packages/2d/gui/icons/pixel-art-icon-pack-rpg-158343
Main scene: https://assetstore.unity.com/packages/2d/environments/2d-enchanted-forest-tileset-pack-199589
Battle scene: https://assetstore.unity.com/packages/2d/characters/the-dark-series-animals-trees-221401, https://assetstore.unity.com/packages/2d/environments/sci-fi-platformer-starter-pack-tileset-3-enemies-216694
## Animation and Visuals - Xinhe Wang & Jesse Liu
- First Drafted animator controller for main character
## Animation and Visuals - Xinhe Wang
- Illustration

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
