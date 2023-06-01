Initial Plan ([Template](https://docs.google.com/document/d/1qwWCpMwKJGOLQ-rRJt8G8zisCa2XHFhv6zSWars0eWM/edit#heading=h.i3tv2mxf7h7z))

## Core Concept

### Provide a synopsis for your game.

We are building a 2D RPG game emphasizing the storyline and user experience of one of the main characters. The protagonist is a girl, Brenna, who travels to an island with her parents. Her parents are attracted by greediness, temptation, and desires from the island, and they will be killed because of their greediness eventually. Brenna is innocent; she feels disgusted by these attractions. Therefore, Brenna is bounded by the city, and she meets some essential characters who help her improve her ability by saving her parent's journey. Also, the player can switch their storyline to one of the characters she meets. 

The game uses side-on scenes only. There is a main scene for player browsing and searching for information on the island. When the player is battling, it switches to another scene. 


### Describe the core gameplay system of your game. 


For the design part, we have one sub-team focusing on designing models such as characters, collectible items, the background of scenes, and some user interfaces. 
For the coding part of the gameplay system. 
- As mentioned, the collectible item is a part of the compound feature. The player has a bag to keep the items for future merging new items. This feature requires UI.
- When the player decides to fight, it will be switched to another scene. 
- The player uses the keyboard S, B, N, and M to trigger different attack animations and the left, right, and top arrows for movements. If the player wins, there will be collectible items dropped from energy. Also, the results of the battle will be displayed via UI.
- The energy's animations will be implemented by Unity.AI.


### Explain your implementation plan to develop your core gameplay system?


Designing: 
- Since most of the characters are collected from the internet, the design team will draw a detailed outlook of each character for chatting dialog. 
- Collect assets from the internet for the health bar and bag display.
- Scenes: 
  - Use Tilemap for the scene, and we will put multiple models for NPCs, characters, and collectible objects. 
  - We will use Unity Animator Controller to create each behavior’s animation.

Implementing: 
- NPCs, Characters, Collectible objects: 
  - They are Game Objects with RigidBody2D. Some of them will be set as inactive by default; when the player approaches a certain distance, it will appear; when the player collides with the objects, it triggers some events, such as conversation, or saves items in the bag.
- Scene Switching: we save the scene into a specific order and use SceneManager.LoadScene to implement this feature. SceneManager.LoadScene will be invoked when a specific condition is triggered.
- Bag feature: We will have a bag system to store the items we get in the game map, and the items can be generated for new materials.
- Building system: The player can use the collected items to make some unique equipment to access the dungeons.
- Battle feature: we will useUnityEngine.AIModule to deploy how energy fights against the player.
- Player movement in keyboard: Input.GetAxisRaw() and Input.GetKeyDown()

### Why do you want to make this game? 


First, we decided to build a 2D game because most members had no Unity experience. 
Second, our team assumes the storyline is as vital as the game system. We brainstormed this storyline about how people are attracted by their dark desires. Also, there are multiple characters along the storyline, and the position of the game is an island with a unique type of fictional metal that can be built into anything. It gives a lot of potential design ideas that we can choose in the future. Such as, the character can choose to stay on the ground or build a wetsuit and dive under the island for a new journey. We decide to develop everything on the land to reduce the implementation pressure. 

For the characters, we will first build a complete storyline with the protagonist. But other characters' storyline is an option for future development.

### Explain your game and the major systems it is composed of in terms of other games and genres.

Our game has similar systems as The Legend of Zelda: A Link to the Past. We have two perspectives of the game: horizontal and top-down. In the top-down view, we plan to make an open-world map where our main character can freely explore and enhance her skills. In addition, we will incorporate (NPCs) into the game to motivate our main character to delve deeper into the storyline and encourage exploration. We will construct various in-game objects, such as stones and metals, that our main character can interact with and utilize within the game world.

Furthermore, our main character can utilize the materials she collects to unlock dungeons, which will be presented in the horizontal view of the game. To approach this, we will design a bag system and a building system to help our character build unique items to access the dungeons. These two perspectives of games will significantly enhance our players' overall enjoyment of the gameplay experience. In our dungeons, we will introduce AI enemies for our main character to confront and engage with during gameplay. After the main character wins the battle, the player can exit, gain some new material, and explore more of our game story. 

### List your non-core gameplay systems and features.

The non-core feature is we need an algorithm for player HP during battle.

As you can tell, the features described above sections require a lot of implementation. Because of time restrictions, we will not dive into many non-core gameplay features until we complete the current system. If the progress goes faster than expected, we will design an electrical system affecting the city power supply.


## Your Team
#### Each team member should provide:
- Basic contact information (e.g. your name and your @ucdavis.edu email address)
- Your chosen role and subrole.
- How confident you are on a scale of 1 (not confident at all) to 3 (neutral) 5 (supremely confident) for both the role and subrole.
- Explain why you chose your role and subrole.
- Describe what you hope to learn or accomplish as part of the team.
- Explain your anticipated challenges when performing as a member of your team.
  
### Member: [Jesse Liu, lzhliu@ucdavis.edu]
- Producer
- Gameplay testing
  
### Member: [Xuanzhen Lao, xzlao@ucdavis.edu]
- User Interface
- Animation and Visuals
  
### Member: [Xinhe Wang, xhewang@ucdavis.edu]
- Animation and Visuals
- Narrative Design

### Member: [Lianghang Wu, acewu@ucdavis.edu]
- Movement/physics
- Game feel

### Member: [Jianfeng Lin, jfglin@ucdavis.edu]
- Gameplay programmer
- Audio

### Member: [Hugo Lin, mhlin@ucdavis.edu]
- Visuals/Input
- Cross-Platform

### Member: [Zuge Li, zgli@ucdavis.edu]
- Interface Designer
- Cross-Platform


## Scheduling

### How would your game be if everything went as planned?

We opened a GitHub repo with all tasks as Issues and displayed them in the project from GitHub to keep everything organized. Each task is assigned to the members. 

To avoid time conflicts for meetings, there are two weekly meetings to provide progress updates. 

### What would the results be if you lost significant time (e.g. you lost two weeks due to unforeseen circumstances)?

We will reduce the time-consuming and only focus on the main tasks: scene and character designing, player controlling, and game fighting features.

If the Back-end or Front-end's progress is behind schedule, we will look for open resources and deploy them into our game.

### If your progress is faster than expected, how would your game change?

We will dive into more features; as we mentioned, this storyline has a lot of developing potential. We will create more scenes that have underwater as background.
