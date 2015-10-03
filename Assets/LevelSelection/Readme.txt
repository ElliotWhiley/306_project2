Contact me at:
name: Nechifor Andrei Marian
email: neomarian@gmail.com
email: neom_2005@yahoo.com
facebook: https://www.facebook.com/andreimarian.nechifor

Plugin Name: LevelSelection
Version: 1.0
Release Date: 23 August 2013

1.	Description
This package is designed to ease your work when you create a level based game. Is fully customizable (you can change main background, buttons, lines, button click effect, buttons position, rotation and scale) and you can use on any game you want. Also you can set stars to be displayed for every level. This package is fully optimized for Android, Web Player, Windows, Mac OS, Linux, Google Native Client and iOS platforms.

2.	Package Content
- Demo scene (Scenes/TestScene)
- Buttons, stars, lines and plane materials, textures and prefabs
- One custom shader (for grayscale)

3.	How to Use
First you need to import package in your project. Then you can open `TestScene` scene from `Scenes` folder to see an examples about how to use this package.
On your scene you can easely drag `SelectLevel` prefab from `Prefabs` folder and after that you can customize that Select Level object (Level Selection component options). You can customize levels buttons (position, rotation, scale and state = enabled/disabled), lines between levels buttons, stars from buttons and what method to call when a button is pressed (method name, object which contains a script with that method, delay time = after how much time to call that method and if to send level selected, as parameter, to that method or not). You can see that I had set on SelectLevel prefab to call `CallAfterSelectLevelExample` method from `SelectLevel` object after `0.5` seconds and to send level as parameter. That method `CallAfterSelectLevelExample` can be found on `CallAfterSelectLevel` component from `SelectLevel` object.
About buttons effects, you can select to don't have any effect, a random effect, or a default effect from effects created by me. These effects can be found on `ShakeAndMove` script from `Scripts/Effects` folder.
If you want to add new click effect to buttons just create new effect script (or add your effect on `ShakeAndMove` script) and add new effect method name (case-sensitive) as option for `HitAnim` enum from `LevelSelection` and then select new effect on `ButtonHitAnim` option from `LevelSelection` component.

4. Level Selection Component properties:
- Level Buttons Parent: buttons parent (which object from scene to be parent for buttons); if null then buttons will not have a parent;
- Levels: an array with buttons elements; every button element contains:
	- Prefab: button prefab to be instantiated;
	- Position: instantiated button position;
	- Is Local Position: if instantiated button position is local (relative to the parent) or global;
	- Euler Angles: instantiated button euler angles (vector3 rotation from unity3d editor);
	- Is Local Euler Angles: if instantiated button euler angles is local (relative to the parent) or global;
	- Scale: instantiated button scale;
	- State: instantiated button state; this is used for stars - how much stars to be colored (0, 1, 2 or 3) and for button - if state is < 0 then button is grey and can not be pressed and if state is >= 0 then button will be colored and can be pressed
- Draw Lines Between Levels:  if to draw lines between levels or not;
- Line Prefab: between levels line prefab;
- Line Prefab Grey: between levels grey line prefab (used for grey/unlocked buttons which have state < 0);
- Reset Line Scale: if to reset lines local scale after setting parent;
- Greyscale Shader: shader used to greyscale materials (for buttons);
- Stars Prefabs: an array with two prefabs used for stars (first for greyscale star and second for colored star);
- Display State Stars: if stars to be displayed on button or not;
- Stars Local Positions: an array with every star position;
- Stars Local Scale: an array with every star scale (size);
- Add Stars Relative To Button Position: if to set stars position relative to button or not;
- Button Hit Anim:
	- what animation to play when button is hit;
	- there are 3 animation: Shake, Move and ShakeAndMove but None or Random can be selected too;
	- if you want to add animations for buttons just create a component with an animation, add that component to all buttons prefabs and add animation function name from that component to HitAnim enum from LevelSelection;
- Call Method After Select Level: method to call after selecting a button; contains:
	- Call: if to call method after pressing a button or not;
	- Call Delay: after how much time to call method after button is pressed (if button have an animation which take 0.5 seconds then this attribute can be set to 0.7 seconds or something similar);
	- Object Name: object name which contains component with method which need to be called;
	- Method Name: method (function) name which need to be called;
	- Send Level Nr: if to send level nr as int parameter to called method or not;
- Destroy All Objs Created On Destroy: if is set then all buttons will be distroyed, array cleared and Garbage Collector called manually;
- Play Sound On Hit: if to play sound when button is hit or not; for this should be set a sound on Audio Source Component from same game object;