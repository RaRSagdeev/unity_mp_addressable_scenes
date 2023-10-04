### [RU version](README_ru.md)

## Demonstration of loading Addressable Scenes in multiplayer

### How to try

#### Server build

1. go to File -> Build, select Standalone, include three scenes in the build:

   1. SampleScene
   2. VeryEmptyScene
   3. AdditivelyLoadedAddressableScene

2. Build a program named "build_s"

#### Client build

1. Go to Window -> Assets -> Addressables -> Groups
2. Click Build -> Default build script. Addressable Scene "AdditivelyLoadedAddressableScene" is thrown off
3. Go to File -> Build, select Standalone, include two scenes in the build:
4. SampleScene
5. VeryEmptyScene
6. Build a program named "build_c"

#### Testing

1. `cd` to the project folder
2. a. to start the server, run

MacOS: `build_s.app/Contents/MacOS/networking\ tutorial -mode server -screen-fullscreen 0`

Windows: `<something similar>`

2. b. to start the client, run

MacOS: `build_c.app/Contents/MacOS/networking\ tutorial -mode client -screen-fullscreen 0`

Windows: `<something similar>`

3. Go to the server window. Make sure that the plane and cube are displayed - one object from each aditively loaded scene - and the capsule denoting the connected player
4. Go to the server window. Make sure that the capsule is displayed - this is the player who was spooked by the server - and the plane, a simple object on the stage
5. From the client window, click the "Go to Addressable scene" button. There will be a blackout, then a cube should appear - this is the AdditivelyLoadedAddressableScene scene object.
6. In the server window, click the "Move" button (you can do it several times). The player must move in both windows.
7. In the player's window, click "Go back to sample scene". There should be a transition to the SampleScene scene (the first scene).
