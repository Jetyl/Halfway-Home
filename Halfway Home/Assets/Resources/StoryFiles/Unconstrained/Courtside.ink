/******************************************************************************/
/*
@file   Courside.ink
@author John Myres
@par    email: john.myres@digipen.edu
All content © 2018 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
VAR player_name = "tbd"
VAR player_gender = "tbd"
VAR grace = 0
VAR expression = 0
VAR awareness = 0
VAR fatigue = 0
VAR stress = 0
VAR delusion = 0
VAR doubt = 0
VAR week = 0
VAR current_room = "unset"
VAR seenBefore = false

EXTERNAL SetValue(name, values)
EXTERNAL GetValue(name)

-> Start

=== Start ===
Sam arrives at the garden to find Timothy absent-mindedly playing with the grass.
The two chat, expression dependent.
Max arrives, throws a basketball at Sam.
Suggests the two play some b-ball.
+[Accept] -> Ballin
+[Refuse] -> Refuse

=== Ballin ===
Sam and Tim head to the court.
Description of court.
Timothy has never played, asks Sam how to play.
Sam hasn't played in a long time, but once upon a time was forced to play as a kid and kind of enjoyed it.
Remembers playing 'horse.' Explains the rules. Decides to shorten it to three letters, let player choose (type in letters?) // type letter support could also be used for hangman game at earlier point
Player given choice as to what to do.
Build a little chance-based ink minigame for horse variant
Timothy gets tired when either he or the player reaches two letters.
+[Finish the game]
	Keep playing, with lower odds for Timothy. Afterwords -> Ignored
+[Stop the game] -> Break

=== Refuse ===
Sam refuses to play. Max doesn't push them, but is disappointed.
Sam returns to their room, increase in depression.
-> END

=== Ignored ===
If Timothy won, he is shocked.
If player won, Timothy is sad. 
Either way, thanks Sam for playing and heads off on his own to get some water.
-> END

=== Break ===
Sam stops the game.
Timothy thanks the player for not caring about the game. Also admits he thinks you are cool for always being so chill about everything. He wishes he could do that.
People always pressuring him.
-> Rest

=== Rest ===
Sam and Timothy sit in the shade for a while, not saying anything. Comfortable silence. Eventually, Timothy and the player both part ways.
-> END