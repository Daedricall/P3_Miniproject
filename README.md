# P3-Miniproject
 \chapter{Report}
\textbf{Introduction}\newline
This mini project was inspired by "Only Up!" A game in which your objective is to maneuver through a 3D world, jumping higher and higher until you finally reach the goal. There are no save points, so if you jump and miss a platform, you will fall down and lose a bunch, if not all, of your progress. This type of game has been replicated quite a lot after Only Up!. Other iterations of this gameplay-loop are seen in game like Chained Together which is, in essence, a multiplayer version of Only Up! with the twist of being chained with a group of friends. My twist, is to make the game a first-person game, and to have the world completely dark. However, you have a flashlight, functioning as eyes. This means that you can see a certain distance, and the game is set up so that everything you can see, you can jump to. Encouraging memorization and exploration of the platforms on which you land.\newline
\newline
\newline
\textbf{These are the main aspects of the game:}

\begin{itemize}
    \item The player\label{Player} is controlled with WASD, with a jump function assigned to spacebar and a sprint function assigned to Left Shift.
    \item The camera is controlled with mouse movement from a first-person perspective, and the player has a "flashlight" attached to their head which allows them to see in the dark.
    \item Platforms are scattered in the dark, encouraging the player to explore the platforms before progressing.
    \item Lights are scattered across the first couple of platforms in the dark, acting as a helping hand to start off.
    \item Boosts are scattered across some platforms, some revealing hidden pathways if you do not immidiately jump to the obvious platform. These boosts are bright colored yellow rectangles in the floor.
    \item UI. When the game starts, the player sees some controls which fade out, and a timer that ticks to indicate their completion time when they reach the finishline. After the player has reached the finish line, UI prompting the player to either replay or quit will pop up, along with a list of their top three fastest completion times.
\end{itemize}
\textbf{Gameplay}\newline
The player sees the controls in the UI, then explores the first platform and looks around. Soon enough, they notice a light on a nearby platform that looks reachable, they jump up the platform, then notice another platform directly in front of them which is not lit up, but they can faintly see it with their "flashlight-eyes". They progress through the game and notice a bright yellow portion of a platform, investigate and find out that it makes them jump very high. With the knowledge gathered so far the player knows to explore and look for further platforms to progress onto. At the end of the road, they see a bright white platform they can jump to. With no other platform to access from that point, they jump onto the white platform and the game ends, offering two options; retry or quit.
\newline
\newline
\textbf{Scripts Controlling the Game}

\begin{itemize}
    \item Finish contains one function that displays the UI element for having finished the game. This also displays your three top scores.
    \item MoveCam keeps the camera anchored on the character by having a "transform.position = cameraPosition.position;" line and a public cameraPosition variable to store the position of the camera. This is filled out by an empty GameObject child of Player(\ref{Player})
    \item PlayerCam is the script that moves the camera in accordance with your mouse input, like a usual 3D first-person game would, having downward / upward angles clamped at 90 so you are unable to look up or down more than 90 degrees.
    \item PlayerMovement this script handles movement and boosts, I figured I would rather have less scripts and call the boost aspect movement-related, despite it being an event occurring because you stepped on a certain object.
    \item Popups handles the "tutorial UI" displaying the controls as UI, fading out over the span of 10 seconds. On top of this, the functionality for the UI of the buttons on the finish screen and the displayed highscores, are also handled in here.
    \item ResetPosition handles what happens, should you fall below the starting platform. If you fall too far, you respawn at the initial location where you start the game.
    \item TimerPopup handles the timer UI displayed on the player's screen, and defines a function to hide this timer whenever the finish UI pops up.
\end{itemize}
\pagebreak
\textbf{Running the Game}\newline
In order to run the game, the user needs:\newline
1 - A PC with a keyboard and mouse/touchpad.\newline
2 - Unity version 6000.0.25f1.\newline
3 - Clone / Download the project folders.\





\begin{center}
\textbf{Time-Table}
\begin{tabular}{ |c|c| } 
 \hline
 Task & Time Spent In Hours, Roughly \\ 
 \hline
 Researching which concept to use for the mini-project & 1 \\
 \hline
 Setting up Github & 0.5 \\ 
 \hline
 Setting up Unity. Layers, buttons, lighting, materials & 1.5 \\
 \hline
 Smaller Scripts + researching (MoveCam, Finish, TimerPopup.) & 1.5 \\
 \hline
 Main Scripts + researching (PlayerMovement, PlayerCam, Popups) & 4 \\
 \hline
 Making the scripts work together properly & 1 \\
 \hline
 Bugfixing and testing features & 3 \\
 \hline
 Level design & 0.5 \\
 \hline
 Making UI & 0.5 \\
 \hline
 Making README & 0.5 \\
 \hline
 Total time spent & 14 \\
 \hline
\end{tabular}
\end{center}

\begin{center}
\textbf{References}
\begin{tabular}{|c|c|}
\hline
    First & "FIRST PERSON MOVEMENT in 10 MINUTES - Unity Tutorial" \cite{1st} \\
\hline
    Second & "SLOPE MOVEMENT, SPRINTING \& CROUCHING - Unity Tutorial" \cite{2nd} \\
\hline
    Third & "\#1 FPS Movement\: Let's Make a First Person Game in Unity!" \cite{3rd} \\
\hline
\end{tabular}
\end{center}
