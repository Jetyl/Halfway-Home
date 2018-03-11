/******************************************************************************/
/*
@file   UnnaturalSpot.ink
@author Jesse Lozano
@par    email: jesse.lozano@digipen.edu
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
VAR player_name = "tbd"
VAR player_gender = "tbd"
VAR grace = 0
VAR expression = 0
VAR awareness = 0
VAR fatigue = 0
VAR stress = 0
VAR depression = 0
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
I wander around the gardens for a bit until I spot Timothy in an unusual opening of shrubbery by the building. #Timothy=Happy, Stage_left
He's crouched over a small patch of dirt in the grass, with a stick in hand.
[{player_name}] "Hey, there you are."
{
	-GetValue("Tutorial") == true:
		"Why'd you run off?"
		[Timothy] "Oh! oh, sorry!" #Timothy = Surprised, Stage_center
		"I-I, um..."
		He nervously looks down at the patch of dirt he was looking over.
}
I get closer and see that he's writing something in the dirt. #Hangman / Open
{
	- week == 1:
		[{player_name}]"Whatch'ya doing?"
		I lean in to see a row of straight lines under a weird-looking symbol.
		<color=A5C5E3FF><i>Right Click the Mouse to Hide the UI.</i></color>
		[Timothy] "Hangman..."
		[{player_name}] "Oh! That's what that is. I thought you might be a wizard or something."
		Timothy chuckles. //#Timothy=Happy
		Now that he mentions it, yeah, I can see the Hangman set up. It seems to be looking for a 4 letter word.
	- else:
		[{player_name}] "Hangman?"
		[Timothy] "Hm? Oh. Yeah..."
		[{player_name}] "Cool. Cool."
		It's the same scrawled hangman setup as before... with the same 4 letter word in it.
}
[Timothy] "W-Want to play?"
[{player_name}] "uh, sure."
[Timothy] "Guess."
[{player_name}] "Hm? Oh, uh, okay."
"Let's see...""
+[Common consonants] -> Constant
+[Can I buy a vowel?] -> Vowel
+{GetValue("SolvedHangman")}[It's Hope] -> AlreadyKnow


=== HangMan ===
+[Common consonants] -> Constant
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
*[A] 
	[{player_name}] "how about 'A'?"
	->Wrong
*[E] 
	[{player_name}] "how about 'E'?"
	[Timothy] "There is an 'E'" #Hangman / E
	-> Correct 
*[I] 
	[{player_name}] "how about 'I'?"
	-> Wrong
*[O]
	[{player_name}] "how about 'O'?"
	[Timothy] "There is an 'E'" #Hangman / O
	-> Correct
*[U]
	[{player_name}] "how about 'U'?"
	-> Wrong

=== Constant ===
Okay, some common consonants are...
*[S]
	[{player_name}] "how about 'S'?"
	-> Wrong
*[T]
	[{player_name}] "how about 'T'?"
	-> Wrong
*[R]
	[{player_name}] "how about 'R'?"
	-> Wrong
*[N]
	[{player_name}] "how about 'N'?"
	-> Wrong
*[H]
	[{player_name}] "how about 'H'?"
	[Timothy] "There is an 'H'." #Hangman / H
	-> Correct 

=== Wrong ===
~GameFails += 1
[Timothy] "Nope!"
//scrawling noise
{
	- GameFails == 6:
		->YouLose
	- GameFails == 5:
		"oh no. one more shot!" #Hangman / 5
		"Next guess?" 
		->HangMan
	- GameFails == 4:
		"Next guess?" #Hangman / 4
		->HangMan
	- GameFails == 3:
		"Next guess?" #Hangman / 3
		->HangMan
	- GameFails == 2:
		"Next guess?" #Hangman / 2
		->HangMan
	- else:
		"Next guess?" #Hangman / 1
		->HangMan
}

=== Correct ===
[Timothy] "Good Job!"
~CorrectGuesses += 1
{
	- CorrectGuesses == 3:
		"care to guess the word?"
		Hmm....
		->Guess
	- else:
		"Next guess?"
		->HangMan
}

===Guess===
[{player_name}] "Is it..."
*[Hole] 
	[{player_name}] "Is it `Hole`?"
	->Wrong
*[Hope] 
	[{player_name}] "Is it `Hope`?" 
	->YouWin 
*[Home] 
	[{player_name}] "Is it `Home`?"
	->Wrong


===AlreadyKnow===
[{player_name}] "It's hope."
[Timothy] "What?! No Way!" #Timothy = Surprised
"How'd you guess is so quickly?" #Timothy=Sad
[{player_name}] "Lucky guess?"
You know, that and weird time travel shenanigans.
->GameOver

=== YouLose ===
[Timothy] "You lose." #Hangman / 6
[{player_name}] "aw dang."
"Hey Timothy, what was the answer anyways?"
"oh, nothing important..."
->GameOver

=== YouWin ===
[Timothy] "Yep!" #Hangman / P
"It's Hope." ~SetValue("SolvedHangman", true)
->GameOver


=== GameOver ===
[Timothy] "Hehehe.." #Background / Garden, crossfade #Timothy = Happy
[{player_name}] "what's so funny?" 
[Timothy] "Oh, nothing... its just..."
"Hangman is a good game..." 
"It teaches you that just by saying a few wrong things, you can end someone’s life..." #Timothy = Sad
Wow. that was surprisingly dark joke from Timothy.
->QuestionTimothy
=QuestionTimothy
Should... Should I question him about that?
+[Question Timothy]
	{
		-expression > 2:
			You question Timothy
		-else:
			<color=A5C5E3FF><i>Unfortunately, Your <b>expression</b> is not high enough to successfully ask Timothy this question.</i></color>
			->QuestionTimothy
	}
+[Let it Slide]
	Eh, I'll let it slide. It's probably nothing.
	->WrapUp

===WrapUp===
//make this tutorial check instead
{
	-GetValue("Tutorial") == true:
		"OH! uh... S-Sorry f-for interrupting the t-tour.." #Timothy = Surprised
		[{player_name}] "its okay."
}
[{player_name}] "so, what are you doing over here?"
[Timothy] "I'm not sure..." #Timothy = Calm
"These gardens are really pretty, and..."
"I don't know, I guess I'm really bad at putting feelings like that into words?" #Timothy=Sad
"Game's made sense... to me. @So I, uh, made my feelings a game?"
"I'm probably not making any sense."
[{player_name}] "No, no, I think I get it?"
{
	-GameFails < 6:
		"So, why Hope?"
		[Timothy] "Oh, uh, um, well..." #Timothy = Happy
		"It is my first day here. and, I guess, the gardens made me think about word 'Hope'."
	-else:
		"So, what was the word?"
		[Timothy] "It's a secret."  #Timothy = Angry
		"You didn't win, so thats the breaks."
}
[Timothy] "..." #Timothy = Afraid
"I'm sorry. I'm acting weird."
[{player_name}] "No, no, you're fine."
[Timothy] "T-Thanks." #Timothy = Happy
"That really m-m-means a lot."
Timothy gives me a cute crooked smile, as he gets up from the dirt patch.
{
	-GetValue("Tutorial"):
		that little game finished, we head off to the next stop on the tour. #Timothy = Exit
	-else:
		I spend the remainder of the hour showing timothy around the gardens, finding more relaxing relaxing little nooks and crannies of the garden. #Timothy = Exit
		my awareness of my surroundings goes up. #awareness += minor
}
-> END