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
VAR GameFails = 0
VAR CorrectGuesses = 0
VAR FirstVowel = true

EXTERNAL PlayMusic(trackName)
EXTERNAL CharEnter(nameString, poseString)
EXTERNAL CharExit(nameString)
EXTERNAL GetPlayerData()
EXTERNAL GetStringValue(value)
EXTERNAL GetValue(value)
EXTERNAL SetValue(ValueName, newValue)

-> UnnatrualSpot

=== UnnatrualSpot ===
I wander around the gardens for a bit, until I spot Timothy, in an unusual opening of shrubry by the building.
//CG mayhaps later
He's crocuhed over, looking down at a small patch of dirt in the grass, with a stick in hand.
[{player_name}] "Hey, there you are."
I get closer, and see that he's scribling something in the dirt.
{
	- week == 1:
		"Whatch'a doing?"
		I lean in, seeing a bunch of crude lines.
		[Timothy] "Hangman..."
		[{player_name}] "Oh!"
		Now that he mentions it, yeah, I can see the Hangman set up. It seems to be looking for a 4 letter word.
	- else:
		"Hangman game?"
		[Timothy] "hm? oh..."
		"Yeah..."
		[{player_name}] "cool. cool."
		its the same scrawled hangman setup, with the same 4 letter word in it.
}
[Timothy] "Guess."
[{player_name}] "Hm? oh, uh, okay."
"lets see, lets start with..."
+[Common consants] -> Constant
+[Can I buy a vowel?] -> Vowel
+{GetValue("SolvedHangman")}[It's Hope] -> AlreadyKnow


=== HangMan ===
+[Common consants] -> Constant
+[Vowels] -> Vowel
+{CorrectGuesses == 3}[Guess at the answer] -> Guess

=== Vowel ===
{FirstVowel:
	[{player_name}] "Can I buy a vowel?"
	[Timothy] "Hehehe... yeah."
	[{player_name}] "Okay, then..."
	~ FirstVowel = false
-else:
	[Timothy] "Go for it."
}
*A -> Wrong
*E -> Correct
*I -> Wrong
*O -> Correct
*U -> Wrong

=== Constant ===
okay, some common consants are...
*S-> Wrong
*T-> Wrong
*R-> Wrong
*N-> Wrong
*H-> Correct

=== Wrong ===
[Timothy] "Nope!"
~GameFails += 1
//scrawling noise
{
	- GameFails == 6:
		->YouLose
	- else:
		"Next guess?"
		->HangMan
}

=== Correct ===
[Timothy] "Correct."
~CorrectGuesses += 1
{
	- CorrectGuesses == 3:
		"Any Guesses?"
		hmmm....
		->Guess
	- else:
		"Next guess?"
		->HangMan
}

===Guess===
[{player_name}] "Is it..."
*[Hole] ->Wrong
*[Hope] ->YouWin
*[Home] ->Wrong


===AlreadyKnow===
[{player_name}] "It's hope."
[Timothy] "What?! No Way!" #Timothy = Suprised
"How'd you guess is so quickly?" #Timothy=Sad
[{player_name}] "Lucky guess?"
You know, that and weird time travel shenagians.
->GameOver

=== YouLose ===
[Timothy] "You lose."
[{player_name}] "aw dang."
"Hey Timothy, what was the answer anyways?"
"oh, nothing important..."
->GameOver

=== YouWin ===
[Timothy] "Yep!"
"It's Hope." ~SetValue("SolvedHangman", true)
->GameOver


=== GameOver ===
[Timothy] "hehehe.."
[{player_name}] "what's so funny?"
[Timothy] "oh, nothing... its just..."
"Hangman is a good game..."
"It teaches you that just by saying a few wrong things, you can end someone’s life..."
//make this tutorial check instead
{
	-week == 1:
		"OH! uh... S-Sorry f-for interupt-t-t-ting the t-tour.."
		[{player_name}] "its okay."
}
{
	-week >= 2:
		hhhhhhhhrrrgghh
		not this week timothy. noth this week.
}
[{player_name}] "so, what are you doing over here?"
[Timothy] "I'm not sure..."
"have you ever felt like a patch of earth is just unnatrual?"
"That, you hold a connection to an otherwise pointless peice of land?"
[{player_name}] "Do you like this spot?"
[Timothy] "No."
"I don't like thise spot at all."
[{player_name}] "Well then, uh, lets leave then."
[Timothy] "I'm not sure I can."
[{player_name}] "what?"
[Timothy] "oh, uh, n-nothing. Its just a strange feeling I'm having."
"I've been getting those a lot... lately..."
[{player_name}] "Well okay, if you say so."
{
	-week >= 2:
		I spend the remainder of the hour showing timothy around the gardens, finding more relaxing, more natural feeling places.
		my awareness of my surroundings goes up. #awareness += minor
}
-> END