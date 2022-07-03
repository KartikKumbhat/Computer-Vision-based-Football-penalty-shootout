# Computer Vision-based Football penalty shootout game
Football penalty shootout developed on Unity 3D. <br>
The kicker has the option to take shots in six total directions. <br>
The goalkeeper is controlled by the CPU. <br>
If the shot direction =/= goalkeeper's save direction, the kicker scores a goal. <br>
Imported and worked on Adobe <a href="https://www.mixamo.com/" target="_blank">Mixamo</a>'s 'Shannon' character. <br>
Built on Unity version 2019.4.21f1 <br>

There are two versions of this game: <br>
One has been built and exported and takes input through numpad keys between 1 and 6. It is present in the folder named 'Game'.<br>
The shot directions are as follows: top left corner (numpad 1), bottom left (2), top right (3), bottom right (4), top center (5) and center (6).
<br>
The other version runs in Unity engine that takes hand signs as input through a python script. <br>
The python script is based on a VGG-16 CNN-based model developed by data scientist <a href="https://medium.com/@brenner.heintz" target="_blank">Brenner Heintz</a>.<br>
The input from python script is passed to the 'AnimationStateControl.cs' C# script through socket programming.<br>
C# scripts and python program for hand sign detection have been uploaded in the 'Scripts' folder for reference.<br>
Check out my <a href="https://www.youtube.com/watch?v=DbZv878UbNM" target="_blank">video</a> for the demonstration of the project.<br>
[![CV-based Football penalty shootout](https://i.imgur.com/9DFqwa6.png)](https://www.youtube.com/watch?v=DbZv878UbNM "Demonstration")
