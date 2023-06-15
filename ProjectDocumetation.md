# Game Basic Information #

## Summary ##
K.K., the daughter of all Patron Saints and the earth, hears the call of the Deer Patron Saints. Pollution has ravaged the earth, spawning evil creatures. Exhausted and powerless, the other Patron Saints await K.K.'s swift action. Guided by the Deer Patron Saints, she needs to fight monsters, gunslingers, and the fanboy to collect unique items. Her mission: save the other Patron Saints and restore harmony. Time is running out as the earth's fate hangs in the balance.

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
  - The player can browse and look for Deer Patron Saint for more instructions. 
  - There are two game objects which trigger other scenes
- Scene1:
  - The player needs to adventure the map, find the Fanboy and kill him to get the secret item. Then the player needs to return to the start position to switch back to the main scene.
  - The Fanboy cannot be killed by using default weapon, player has to find the master weapon, active and use it to kill the Fanboy.
- Scene2:
  - The player starts from left and need to reach the right most point in the map to fight against Gunslinger. 
  - There are NPCs in the path and very easy to kill.
  - Gunslinger can only be killed by using master weapon.

# Main Roles #
## Producer - Jesse Liu - GitHub: Jesse1211

Since our game is implemented from scratch, I have to ensure everyone knows the game type, storyline, essential features, current development progress, and expectation of the achievement. I have to keep active and communicative; I provided 1-1 meetings with everyone to make sure they know what to do and how to do it; I used [whentomeet](https://www.when2meet.com/?19949103-hDTso) to find the perfect time for weekly meetings to make sure the communication between teammates. Also, I added guidance about using GitHub to ensure everyone contributes conveniently.
- GitHub: 
  - I decided to use GitHub because [Issue](https://docs.github.com/en/issues/tracking-your-work-with-issues/about-issues), [Branch & Merging](https://www.varonis.com/blog/git-branching), [Project](https://docs.github.com/en/issues/planning-and-tracking-with-projects/learning-about-projects/about-projects) are very robust features as an environment to keep track of progress and deployment. 
  - I pushed all important files into this Repo, such as `ProgressReport`, `Proposal`, and `README`. `README` is to provide teammates basic knowledge of Github. 
  - I created over 50 issues by dividing each goal into small tasks. And opened & merged 26 branches, branches are used if the task takes a long time to implement to ensure the progress works in multi-thread. 
  - I created a [Project](https://github.com/users/Jesse1211/projects/1) in Github, which connects to the current repository. It provides Overall tasks, tasks sorted by Assignee in Table layout, and progress status view in Board layout. Since the project connects to the repository, we could put `Todo`, `In Progress`, and `Completed` statuses on each issue to provide issue status visually and positive feedback when completed.
  - The tricky point is Merging because Unity scenes are not reading-friendly. I tried to assign each branch works on different scenes, but the problem was inevitable. Therefore, I need the assignees accompany when merging the branching to avoid losing anything important. If something is wrong, we will look commitment history and get the old version of the scene, and update the references by hand. 

- Progress:
  - We split the project into three stages for 5 weeks. 1: draw scene by using `Tilemap`, add some prefabs. 2: Add script to prefabs to make it responsive, and implement the bag feature; 3: connect scenes, and implement enemy AI feature. 
  - Since most of the team members have no experience in Unity. A leader needs to catch up on Unity before everyone does. I have to figure out how to deploy the ideas into programming logic to increase working efficiency before assigning tasks. Then I can teach them coding or use Unity.
  - I believe tasks are not set only for specific assignees; they are always first come, first served by anyone. Since I am using divide & counter strategy, tasks are a small part of the stage. Anyone can finish them within a couple of hours. I provide my personal Zoom meeting link to anyone. Since I have learned how to implement it, I helped them complete most tasks efficiently. If they were not in the zone or stressed, I helped them to calm down and sort the logic by writing pseudocode.

- Completed tasks:

  Before implementing any features, I recommend everyone do it most generically. Because this is a big project, obeying encapsulation is convenient for future modification and maintenance. 

  - Build Scenes: 
    - Helped XuanZhen Lao and Zuge Li build scenes using `TileMap`. Using `TileMap` is not difficult, but drawing requires innovation, and we need to set different categories according to the use of components. We set `Ground` as the plane player stands on, `Props` contains all prefabs with scripts, and `Background` has the tilemap renderer to display. We did not know the final version of the map, but our innovation pumps out when brushing on the background. 
  - Develop interactable prefabs: 
    - Helped Zuge Li to develop interactable prefabs, such as a movable boat and elevator, and death zone, which caused damage to the player. etc. Writing scripts seemed complicated, but we handled it pretty well.  for prefabs to automatically move, we build 2 empty game objects representing 2 endpoints where prefabs can move from one to another. For prefabs that deduct the player's HP value, we put a specific tag to the prefabs, which changes the player's HP when it collides.
  - Develop [Bag feature](https://github.com/Jesse1211/ECS-189L/tree/main/Final%20Project/Assets/Scripts/Bag): 

    The player collects items that have already been instantiated, so I decided not to destroy and re-instantiate it from map to bag because destroy and instantiate are expensive. Therefore, I modify the items' positions and hierarchy and store the prefab data in one of 2 lists: activated weapon list and collected item list. When the player uses bag items, we determine if the item should be destroyed or swapped to another list. Prefabs inside the activated weapon list will be launched by pressing `1`, `2`, `3`, `4` keys. 
    - Since this task may require more time and be challenging because I had to implement it most generically. Otherwise, it will be harder to deploy the feature into the project.
    - Implementation details:
        - `BagManager`: provides `useItem()` when the user clicks the item. `useItem()` determines to destroy or swap the item to another weapon list
      - `BagItemSlot`: tracks if the player right-clicks the item, it executes `BagManager.useItem()`. (Helped by LiangHang Wu)
      - `BagDataLoader`: store two lists and provides 4 APIs to `BagManager.useItem()`
      - The `PlayerCOntrollerData` has access to `BagManager` to store the collided item in the bag
  - Player Controller: 

    Since the player is able to interact with every object in unity, it is better to split the controller into data and animator, implementation details:
    - `PlayerControllerData` handles the logic when the player collides with some game objects, and provides `TakeDamange(int damage)` to modify HP and `blood` animation.
    - `PlayerControllerAnimator` inherits `PlayerControllerData` handles setting triggers in the animator, and the logic to attack by instantiating prefabs.
  
  - Taught teammates about making animation and animator controllers for characters and the API methods to switch scenes. Animation needs to be done rigorously. We modified some animations a couple of times later because we could see minor lagging in character.
  
  - Dialogue: 
    
    Developed the structure of dialogue with typing effect in UI. This is a tricky feature because the UI only triggers when the player collides with certain prefabs. Also, typing effect happens in multi-thread. If the player moves away and hits the prefabs again, the typing thread cannot restart. we want to keep the player's experience, so we decided to "freeze" player movement until finished reading it. This is not the generic way to solve; it is the future task. 

  - Used `Mask`, `Fill` to display the number of orbs collected. And `MeterScript` which sets the value of `Slider` with gradient color.

## User Interface - Xuanzhen Lao
- Build the first version of `BattleScene` using the TileMap design. The model provides the basic style. In the initial phase, construct the prior version of the BattleScene utilizing TileMap design techniques. The supplied model directly informs the foundational aesthetic. Leveraging TileMap's capabilities allows for the transformation of the basic look. It enhances the overall visual appeal of BattleScene's second iteration. Any concerns or issues arising during this process can be found documented here.

- Built the basic outlook style of `BattleScene` by using TileMap. [issue] (https://github.com/Jesse1211/ECS-189L/issues/17). In addition to the BattleScene, the project also involves the assembly of the 'MainScene.' This task is accomplished using models collected throughout the development process. The process was not without its hurdles; various issues encountered during the creation of the 'MainScene' are detailed here.

-  Built `MainScene` from the collected model. [issue](https://github.com/Jesse1211/ECS-189L/issues/10). The ongoing evolution and refinement of the BattleScene and 'MainScene' are key components of the project's progression. As the scenes continue to evolve, they are expected to deliver increasingly immersive and interactive experiences for users.

## Interface Designer - Zuge Li
- Enhanced the `BattleScene` by expanding the range of grid using `TileMap`: I increased the grid size of the `BattleScene` accommodate more complex and larger-scale battles. This was achieved by utilizing the TileMap, allowing for a flexible grid system that can adjust to various battle scenarios and gameplay requirements, including modifying various type of special interactive ground, like elevator. Besides, expanding battle scene increases playability in content and riches players' experience.

- Added multiple responsive prefabs with scripts for `Boat`, `Elevator`, `Death Zone`, `Dragonfly`, `Climable Wall`, `BloodTrees`, `Orb Plant`. issue: I incorporated a multitude of responsive prefabs including vehicles like `Boat` and `Elevator`, environmental hazards such as `Death Zone`, creatures like `Dragonfly`, physical obstacles like `Climable Wall`, and unique game elements such as `BloodTrees` and `Orb Plant`. Each of these prefabs was endowed with unique behavior and interaction scripts, thereby significantly enriching the game world's interaction dynamics. For example, the `Death Zone` will be harmful to player to interact with. On the other hand, `Elevator` will be good tool to explore the world. [issue] (https://github.com/Jesse1211/ECS-189L/issues/23).

- Added Dialog feature by using `Canvas`, `Image`, `Button`, `Text`, and `Scripts` which helps to achieve typing effect. I created a new Dialog feature to promote interactive storytelling in the game. This feature was implemented using a blend of Unity UI elements such as `Canvas`, `Image`, `Button`, and `Text`, along with custom scripts for dialogue control. By using the images generated by group mates, I made a good looking of dialog interface, with black green color and nice margin, which fit to the ambience of the game and enhance the experience of playing the game. The typing effect added to the dialogues gave them a more engaging and realistic feel, thus enhancing the player's immersion in the game narrative. I also customize the dialog speed by controling the speed of apppearance of each letter in a sentence. [issue](https://github.com/Jesse1211/ECS-189L/issues/36).

- Implemented animation states for most models including `elk`, `fox`, `crow`, `rats`, `Fanboy`, `Gunslinger`. I introduced distinctive animation states to a majority of the game models. These included a variety of animal characters like `elk`, `fox`, `crow`, and `rats`, as well as human characters such as `Fanboy` and `Gunslinger`. The animation states provide each model with unique movements and behaviors, adding depth to their characteristics and making the game world more believable. To make sure they appropriately show the same size in the game world, I adjusted `pixel per unit` everytime when I make them. So basically now those characters will not be shrinked or expanded. [issue](https://github.com/Jesse1211/ECS-189L/issues/26).

- Scene changing for some scene. Considering of the content of the floatingisland, it is better to place the scene changing spot at further at the right of the map because I think there should be more room for the player to fight with the enemy. If not, it would be too restricted to fight, which influence the experience of game playing.

- Bag item functioning. Bag should have a function that can pick up the weapon and take it as weapons to shoot. So, we made weapons that can be picked up and equipt on weapon slot. When press 1, 2, 3, 4, it will accordingly emit the weapons in each slots. Of course, it would cause some damage.

- Secondary drafted animator controller for main character: I took the lead in creating a second draft for the animator controller of our main character. This was a thorough revision that incorporated feedback from the first draft and improved character movements, transitions, and controls to achieve a more natural and responsive animation flow. In detail, the secondary we tried to use trigger to transit the state between animations. We did it; however, it is not efficient to use, and tried to modify it later.

- Enemy AI:
  - Designed enemy AI by using FSM which sets `Idle`, `Patrol`, `Chase`, `React`, `Attack`, `Death`, `IsHit`, `teleport` states that make enemy more responsive to player: I devised a state-driven enemy AI design using Finite State Machine (FSM). This system incorporated a diverse set of states including `Idle`, `Patrol`, `Chase`, `React`, `Attack`, `Death`, `IsHit`, and `teleport`. Each state defined unique behaviors, making the enemy AI responsive and adaptive to player actions and game events. In details, `Idle` serves for the standing without any actions, but it will transit to `Patrol` state. This is a enemy need to patrol over time. If he found the player, he will enter in `React` state. This is a state for transiting the state. The states include `Attack`, `Patrol`. The rest will be easy to understand, `Death` and `IsHit`. The interesting part is `teleport` because it is a mechanism for strenthening enemy, increasing the difficulty of playing this game.

  - To each state, I attached key methods such as `OnEnter()`, `OnUpdate()`, `OnExit()`, `GetHp()`, and `SetUp()` using an interface, which efficiently pass the information between each state. This approach streamlines transitions between states and provides real-time updates to the enemy's behaviors, significantly boosting the AI's functionality and responsiveness. 

## Movement/physics - LiangHang Wu
- Developed the `FloatingIslandScene` in the game. 
- Finalized Animator & Animation for main character, depolyed it to `FloatingIslandScene`. [issue](https://github.com/Jesse1211/ECS-189L/issues/18)
- Implemented animator `Fanboy`, `Gunslinger`. [issue](https://github.com/Jesse1211/ECS-189L/issues/43)
- Implemented animators and animations for various enemy types, including Demon, Skeleton, Goblin, Mushroom, and FlyEye. 
- 
  Implemented essential features for enemies within the `Enemy.cs` script:
  - Developed a patrol mechanism allowing enemies to navigate and walk between designated points on the map, ensuring they maintain movement within a specific area.
  - Implemented functionality for enemies to scan the area in front of them, detecting the presence of the main character. Once the main character is detected, enemies initiate the attack behavior.
  - Incorporated damage detection for enemies, enabling them to receive damage when attacked by the main character. Additionally, implemented blood particle effects to enhance the visual feedback of enemy damage.
- 
  Implemented key features for the main character within the `GirlController.cs` script:
  - Developed fundamental movement mechanics and corresponding animation reactions, including running, jumping, attacking, dodging, and other skills.
  - Implemented a ranged attack functionality for the main character, enabling her to shoot a blue bolt projectile during attacks.
  - Integrated damage detection for the main character, allowing her to receive damage when attacked by enemies. Additionally, incorporated blood particle effects to enhance the visual representation of the main character's injuries.
- Created the 'bolt' prefab in game. When the main character performs a ranged attack, the prefab is instantiated and remains active for a certain duration. It disappears either upon colliding with an object or reaching its lifetime limit.
- Collaborated with Jesse(Producer), to implement the user interface for the player's bag:
  - Utilized the `Canvas` and creating multiple game objects to serve as slots within the bag. 
  - Implemented interactive functionality to ensure responsive actions when interacting with the bag UI.

## Gameplay Programmer - Jianfeng Lin -Github: Kkyyy1115 
- Camera Follow:
  
	The implementation of the camera controller is similar to the Exercise2 `PositionFollowCamera.cs`. But it has a little bit different. The boundary of the camera can be set manually so that the player won’t see the unrelated scene in the game. This is simply implemented by using the Lerp function and Clamp function
  
- Death Scene:
	When the player succumbs in the game, they are transported to the death scene. By interacting with any part of this death scene, they can navigate back to the last scene. The mechanism is simply implemented by a script `BackToFight`, which is attached to the button. The UI and animations were originally made for from previous game. I have incorporated them into this game to enhance the level of human interaction.

- Health Bar
  
	The functionality of the player's health bars is facilitated through a UI slider, composed of two distinct images. One is called the background, which indicates the background color of the health bar. The second is called `Fill area`, which indicates the current HP of the player. They are attached under the canvas. It is controlled by the script `CharacterHP`. However, this script is only invoked when the player's character dies, prompting the transition to the death scene. The health bar updates by assessing the value from `PlayerControllerAnimator`. The value of the health bar changes under various conditions, such as when the player stumbles into traps or when the player is attacked by the enemy.

- Enemy Health bar:
  
  The Enemy health bar is implemented in the same way as the player health bar. However, these bars are linked to two game bosses, namely Fanboy and Gunslinger. Their health bars are displayed above their heads and have the capability to track the bosses' movements. The value of the health bar is updated by the script called `IdleState` and `GunSlinderIdleState`. In these two scripts, they have a common state, which is `IsHit`. It can be accessed when the Bosses are hit by the player. 

- ThunderBolt：
  
	Before the player can confront the Boss in the battle scene, they must first engage in conversation with the Non-Player Character (NPC). Once the dialogue with the NPC concludes, a thunder-created battle zone materializes. The player is restricted from leaving this zone until they have successfully defeated the Boss. This battle zone is implemented by the script called `thunderTrigger`. I attach the trigger script to the NPC, specifically the deer character.

- Enemy AI: 
  
	The Fanboy AI and Gunslinger AI are implemented by using the finite state machine. The scripts are under the file called EnemyAI. "Model" holds the fundamental data. `IdleState` includes eight states, namely `Idle`, `Patrol`, `Chase`, `React`, `Attack`, `IsHit`, `Death`, and `Teleport`.  They are able to transition between states under the condition. These states allow the AI to do some simple decisions. For instance, if the AI's HP falls below 30, it has the ability to teleport to a different location for HP recovery. Moreover, AI can chase and attack the player in a given area. `FSM` is an executor, which provides some helper functions such as TransitionState, Flip, and OnTriggerEnter2D to help transition the states. 
	


## Visuals - Hugo Lin
- Used `Canvas` and implemented an animation & animator for scene switching event with `ToNextScene`
	When the player have a collision on an object or certain conditions are met, the game will switch to a different scent using SceneManager
- Add more detail to the main scene

## Animation and Visuals
Assets: 
Collect meter: https://assetstore.unity.com/packages/2d/gui/icons/elemental-meters-173133
Bag items: https://assetstore.unity.com/packages/2d/gui/icons/pixel-art-icon-pack-rpg-158343
Main scene: https://assetstore.unity.com/packages/2d/environments/2d-enchanted-forest-tileset-pack-199589
Battle scene: https://assetstore.unity.com/packages/2d/characters/the-dark-series-animals-trees-221401, https://assetstore.unity.com/packages/2d/environments/sci-fi-platformer-starter-pack-tileset-3-enemies-216694

## Animation and Visuals - Xinhe Wang
- Implement character animations and create initial [PlayerControllerAnimator](https://github.com/Jesse1211/ECS-189L/blob/c1e7eccd371f3cd57e46d9f20a3301bb32541086/Final%20Project/Assets/Scripts/PlayerControllers/PlayerControllerAnimator.cs#L1) draft with specified button and trigger requirements, and basic logic for player character animations.
  - Button and Trigger Requirements for Character Animations: Left: `A`, `left arrow`; Right: `D`, `right arrow`; Jump: `Space`, `up arrow`; Dodge: `S`; Stab: `N`; Gunattack: `B`
  - Character Animation Descriptions:
    - Idle1: Walking animation in the main scene (attack disabled)
    - Fall: The character's y-direction movement decreases, the character is off the ground
    - Run: Activated when speed is greater than a constant number
    - Transition: Triggered when collecting items (clicking M)
    - Dodge: Detected using the trigger in the animator
    - Stab: Detected using the trigger in the animator
    - Gunattack: Detected using the trigger in the animator
    - Idle2: Walking animation in the battle scene
    - Hit: Activated when the enemy hits the player
    - Death: Activated when the player's HP drops to 0 or below
  - Future Add-ons:
    - BrawlFall: Distinguished from Fall animation
    - BackDodge: Distinguished from Dodge animation
    - SpecialJump: Distinguished from Jump animation (may be developed into ultimate ability)
Note: The specified button and trigger requirements are utilized to control and trigger the respective character animations.

- After downloading the character and background-related prefabs and assets from Unity Store, create illustrations using Procreate, Adobe Illustrator, and Adobe Photoshop which were later used in the game. Digital Illustrations include UI-related dialog box, Patron Saints, Player Character K.K., Game UI Prototype, and Game Trailer Illustration.
  - Dialog Box:
![WeChat Photo Editor_20230612211303](https://github.com/Jesse1211/ECS-189L/assets/115097655/67a2ff58-27af-4b57-9df3-4f297b84971e)

  - Deer Patron Saint:
![WeChat Photo Editor_20230612211508](https://github.com/Jesse1211/ECS-189L/assets/115097655/2d4c0e1a-14bf-4cd8-9be6-22ca3d7eff7e)

  - Ram Patron Saint:
![WeChat Photo Editor_20230612211644](https://github.com/Jesse1211/ECS-189L/assets/115097655/455b4825-d645-4c14-b902-e88b4e161e79)

  - Fox Patron Saint:
![WeChat Photo Editor_20230612211747](https://github.com/Jesse1211/ECS-189L/assets/115097655/b799d6bc-a758-4d78-97ff-492a1b1bdf63)

  - Player Character K.K.:
![WeChat Photo Editor_20230612211412](https://github.com/Jesse1211/ECS-189L/assets/115097655/3f00a607-09bd-40cd-8c1f-28fb1caa6a5b)

  - Game UI Prototype:
![WeChat Image_20230611225512](https://github.com/Jesse1211/ECS-189L/assets/115097655/d4eb3831-3c45-41d5-836f-16417ea3f78a)

  - Game Trailer Illustration 1.0:
![WeChat Image_20230611225850](https://github.com/Jesse1211/ECS-189L/assets/115097655/3c763e61-dc68-429f-ac41-2adde8ed2a73)

  - Game Trailer Illustration 2.0:
![WeChat Image_20230611225913](https://github.com/Jesse1211/ECS-189L/assets/115097655/802c8264-93e1-4bb7-9768-c4c02d2eb843)

  - Final Game Trailer Illustration 2.1:
![784467850292513190](https://github.com/Jesse1211/ECS-189L/assets/115097655/00dfffe7-2850-4b81-a83b-4441055cb1e6)

# Sub-Roles

## Audio
### Jianfeng Lin & Huge Lin
- Depoly audio into game. 
### Jianfeng Lin - GitHub: Kkyyy1115
- Adding the background music to each scene. The background music is produced by Xinhe Wang. The music can be found under the file called BGM. In addition, I added the death scene and implemented it for every scenes when player die. I also added the background music for the death scene. 
- In addition, I assist in debugging the game by verifying if there are any inconsistencies between layers and colliders. If such mismatches are detected, I make the necessary adjustments to ensure that our game's mechanics function properly.

## Cross Platform - Hugo Lin
- modified some codes in order to build the game for MacOS and WebGL

### Xinhe Wang
- Create [original music](https://github.com/Jesse1211/ECS-189L/blob/ae76b1a3c031e01f98945e5c46cf6965f82c1301/Final%20Project/Assets/BGM/%E6%88%91%E7%9A%84%E4%B9%90%E6%9B%B2%205.wav) with Garage Band. Music is used in the Main scene, Battle scene, and Floating island scene. The export format was m4a however it is unable to use in Unity. Finalized with wav format.  


## Gameplay Testing - Jesse Liu - GitHub: Jesse1211

- After teammates finish scripts, I need to deploy them to various prefabs to combine three scenes. I need to pay attention to layer & tag settings, such as the enemy cannot attack the player. Also, scenes do not have the same unit scale, I need to modify the main character's size & vertical jump & movement speed. 
- Conduct comprehensive testing: 
  - After making these modifications, I tested the combined scenes to ensure the functionality and adjustments worked as expected. I considered running the game and manually interacting with the main character and enemies to verify that the changes had been applied correctly. Use `Debug.Log()` statements strategically to output relevant information to the console and confirm the desired behavior.
  - Test the game thoroughly to identify any bugs, glitches, or issues that may arise during gameplay. Test all possible scenarios and edge cases to ensure that the game behaves as expected. I modified `TileMap` since the player should only climb in specific walls. 
  - Evaluate the user interface for usability, clarity, and responsiveness. I specify the frame rate at which Unity tries to render the game. And the size of the bag feature was a little too small, and the position is not in the middle of the screen. This is caused by inproper setting of the canvas' Rect Transfrom position. 
  - 

## Narrative Design
### Xinhe Wang
- After the group meeting with other team members, where we collectively decided to create a 2D RPG, I took the initiative to generate game story ideas utilizing our current accessible assets, prefabs, and resources. Additionally, I worked on creating compelling dialogs between non-playable characters (NPCs) and the main characters. Simultaneously, I set the requirements and triggers necessary to propel the game and story forward.
- Characters:
  - Player: K.K. the purple hair little girl
  - Main NPC: Deer Patron Saint
  - In-game NPC: Ram Patron Saint, Fox Patron Saint, Fanboy Boss, Gunglinger Boss
- Storyline:
  - In the game, the deer assumes the role of a messenger, conveying a critical message to the player regarding the peril faced by the earth and other Patron Saints due to human-caused pollution. The player is tasked with collecting five orbs, which are essential for opening a teleportation gate to a different location where they can rescue the endangered Patron Saints. Each scene presents the player with multiple pathways to reach the boss, who guards and blocks the Patron Saint in need of rescue. Upon successfully saving the Patron Saint and obtaining the final orb, the scene undergoes a transformative shift, becoming more challenging as additional monsters appear. To progress, the player must navigate back to the teleport gate or a designated tree to return to the Deer Patron Saint, completing the ongoing mission and receiving a new one.
- Idea and message behind the game:
  - I strongly believe that a good game goes beyond mere entertainment value or visual appeal; it should also strive to impart lessons or convey meaningful messages to its players or audience. In light of recent reports, such as CNN's article on [Global Warming: World on track to breach a critical warming threshold in the next five years](https://www.cnn.com/2023/05/17/world/global-warming-breach-wmo-climate-intl/index.html) , it is evident that pollution and global warming remain pressing issues that demand urgent attention. We aspire to leverage our game as a medium to effectively deliver this message to the players, raising awareness and fostering a sense of responsibility towards environmental conservation.
- Narrative Documents and materials
  - First narrative draft with basic storylines and beginning scene's dialog:
    - [Narrative #1 - Scene1 draft with edited dialog and simple storyline.](https://github.com/Jesse1211/ECS-189L/files/11730698/Narrative.1.-.Scene1.draft.with.edited.pdf) 
  - Final narrative with the storyline, all scene's dialog, how to push the game forward, and the need for NPC and Assets:
    - [Final Narrative](https://github.com/Jesse1211/ECS-189L/files/11730702/ecef02a2f6c10abae31a05652bd4753e.pdf)  

## Game Feel
### LiangHang Wu
- Implemented visual effects in the game to enhance the combat experience. Added blood particles that appear when the main character or enemy is attacked, creating a realistic depiction of the impact. Additionally, incorporated dynamic animations to provide reactive movements when characters are hit, further immersing players in the game's action.
- Developed various enemy types. These additions enhanced the visual appeal and brought the enemies to life within the game.
