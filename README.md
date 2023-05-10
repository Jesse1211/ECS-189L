ECS 189L Group Project:

#### Information:
[Gameplay Programming Project Groups Spreadsheet](https://docs.google.com/spreadsheets/d/1OUz9atsn2HAFm9Wa97dMX5Q5Pg5Whuir-C6jLmuWleM/edit#gid=0).

[Game Project Initial Plan](https://github.com/Jesse1211/ECS-189L/blob/main/InitPlan.md).

[Game Project Progress report](https://github.com/Jesse1211/ECS-189L/blob/main/ProgressReport.md).

[Game Trailer and Press Kit](https://docs.google.com/document/d/1eE_IGRv6Os1QEapPc-oXWKw6kwcKxbdPbvuBu-scxC8/edit).

### 



## GitHub

The template is stored in GitHub which requires basic konwledge with GitHub

### Generate (regenerate) a new GitHub personal access token

- Login to GitHub in the browser
- In the upper-right corner of any page, click your profile photo, then click Settings.
- In the left sidebar, click Developer settings.
- In the left sidebar, click Personal access tokens.
- Click Generate new token.
- Give your token a descriptive name. e.g. read pkg pat
- To give your token an expiration, select the Expiration drop-down menu, then click a default or use the calendar picker.
- Select the scopes you'd like to grant this token. To use your token to access repositories from the command line, select `repo`. To Upload and download packages to GitHub Package Registry, select ``write:packages``. 
- Click Generate token.
- Please note that the token will not be found unless you save it. 

### Clone template by command line

- For Mac: Press `Command-Space` and enter `Terminal` to open the Terminal
- For Windows: 
- Go to the desired directory for the project you want to store and enter
```
    git clone https://github.com/SchedGo/sample-college-integration-dotnet-csharp.git
```
- Please see the instruction (Jump to github personal access token) below if the clone requires GitHub personal access token (or the token expired).
- If it's the first time use Github, the Terminal will require you to enter a username and password, which are the username in Github and personal access token. 
- Now the template is cloned into your computer

## GitHub Access by Visual Studio

Visual Studio [Installation](https://visualstudio.microsoft.com/downloads/)

### For windows
#### Clone a GitHub repo and sign in
There is a comprehensive instruction from [Microsoft learning](
https://learn.microsoft.com/en-us/visualstudio/version-control/git-clone-repository?view=vs-2022).

### For Mac
#### Clone a GitHub repo and sign in
- Open Visual Studio
- Click ``Git`` and ``Clone Repository...`` from menu bar
- Copy `https://github.com/SchedGo/sample-college-integration-dotnet-csharp.git` into the URL input
- It will prompt a window for entering a GitHub username and password. The password must be identical to the generated access token saved. 

#### Update GitHub token
- Press ``Command-Space``, enter ``Keychain Access`` and press Enter
- Search ``GitHub``keyword and update the password in the corresponding result

