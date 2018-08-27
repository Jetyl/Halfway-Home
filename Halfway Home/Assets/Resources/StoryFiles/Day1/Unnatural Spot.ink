﻿/******************************************************************************/
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
VAR ConstantGuess = 0
VAR VowelGuess = 0

VAR Guess_A = false
VAR Guess_E = false
VAR Guess_I = false
VAR Guess_O = false
VAR Guess_U = false
VAR Guess_S = false
VAR Guess_T = false
VAR Guess_R = false
VAR Guess_N = false
VAR Guess_H = false
VAR Guess_P = false
VAR Guess_M = false
VAR Guess_L = false

EXTERNAL PlayMusic(trackName)
EXTERNAL CharEnter(nameString, poseString)
EXTERNAL CharExit(nameString)
EXTERNAL GetPlayerData()
EXTERNAL GetStringValue(value)
EXTERNAL GetValue(value)
EXTERNAL SetValue(ValueName, newValue)
EXTERNAL GetIntValue(value)
EXTERNAL SetIntValue(ValueName, newValue)

-> UnnaturalSpot

=== UnnaturalSpot ===
~GameFails = 0
~CorrectGuesses = 0
~ConstantGuess = 0
~VowelGuess = 0
~Guess_A = false
~Guess_E = false
~Guess_I = false
~Guess_O = false
~Guess_U = false
~Guess_S = false
~Guess_T = false
~Guess_R = false
~Guess_N = false
~Guess_H = false
~Guess_P = false
~Guess_M = false
~Guess_L = false
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
	- week == 2:
		Its the exact same hangman game that he was scrawling in the dirt last week, another of today's multitude of deja vu's.
		I wonder if it has the same answer too...
	- else:
		[{player_name}] "Hangman?"
		[Timothy] "Hm? Oh. Yeah..."
		[{player_name}] "Cool. Cool."
		It's the same scrawled hangman setup as before... with the same 4 letter word in it.
}
[Timothy] "W-Want to play?"
[{player_name}] "Uh, sure."
[Timothy] "Guess."
[{player_name}] "Hm? Oh, uh, okay."
"Let's see..." #Skip
+[Common consonants] -> Constant
+[Can I buy a vowel?] -> Vowel
+{GetValue("SolvedHangman")}[It's Hope] -> AlreadyKnow


=== HangMan ===
+{ConstantGuess < 5}[Common consonants] -> Constant
+{VowelGuess < 5}[Vowels] -> Vowel
+{CorrectGuesses == 3}[Guess at the answer] -> Guess

=== Vowel ===
{VowelGuess < 1:
	[{player_name}] "Can I buy a vowel?"
	[Timothy] "Hehehe... yeah."
	[{player_name}] "Okay, then..." #Skip
-else:
	Let's see, vowels I haven't guessed yet... #Skip
}
+{Guess_A == false}[A] 
	[{player_name}] "How about 'A'?"
	~Guess_A = true
	~ VowelGuess += 1
	->Wrong
+{Guess_E == false}[E] 
	[{player_name}] "How about 'E'?"
	~Guess_E = true
	~ VowelGuess += 1
	[Timothy] "There is an 'E'" #Hangman / E
	-> Correct 
+{Guess_I == false}[I] 
	[{player_name}] "How about 'I'?"
	~Guess_I = true
	~ VowelGuess += 1
	-> Wrong
+{Guess_O == false}[O]
	[{player_name}] "How about 'O'?"
	~Guess_O = true
	~ VowelGuess += 1
	[Timothy] "There is an 'O'" #Hangman / O
	-> Correct
+{Guess_U == false}[U]
	[{player_name}] "How about 'U'?"
	~Guess_U = true
	~ VowelGuess += 1
	-> Wrong
+{VowelGuess >= 1}[Back]
	[{player_name}] "Hm..." #Skip
	->HangMan

=== Constant ===
Okay, some common consonants are... #Skip
+{Guess_S == false}[S]
	[{player_name}] "How about 'S'?"
	~Guess_S = true
	~ ConstantGuess += 1
	-> Wrong
+{Guess_T == false}[T]
	[{player_name}] "How about 'T'?"
	~Guess_T = true
	~ ConstantGuess += 1
	-> Wrong
+{Guess_R == false}[R]
	[{player_name}] "How about 'R'?"
	~Guess_R = true
	~ ConstantGuess += 1
	-> Wrong
+{Guess_N == false}[N]
	[{player_name}] "How about 'N'?"
	~Guess_N = true
	~ ConstantGuess += 1
	-> Wrong
+{Guess_H == false}[H]
	[{player_name}] "How about 'H'?"
	~Guess_H = true
	~ ConstantGuess += 1
	[Timothy] "There is an 'H'." #Hangman / H
	-> Correct 
+[Back]
	[{player_name}] "Hm..." #Skip
	->HangMan

=== Wrong ===
~GameFails += 1
[Timothy] "Nope!"
//scrawling noise
{
	- GameFails == 6:
		->YouLose
	- GameFails == 5:
		"Oh, no! Only one more shot!" #Hangman / 5
		"Next guess?" #Skip
		->HangMan
	- GameFails == 4:
		"Next guess?" #Skip #Hangman / 4
		->HangMan
	- GameFails == 3:
		"Next guess?" #Skip #Hangman / 3
		->HangMan
	- GameFails == 2:
		"Next guess?" #Skip #Hangman / 2
		->HangMan
	- else:
		"Next guess?" #Skip #Hangman / 1
		->HangMan
}

=== Correct ===
[Timothy] "Good Job!"
~CorrectGuesses += 1
{
	- CorrectGuesses == 3:
		"Care to guess the word?"
		Hmm....
		->Guess
	- else:
		"Next guess?" #Skip
		->HangMan
}

===Guess===
[{player_name}] "Is it..." #Skip
+{Guess_L == false}[Hole] 
	[{player_name}] "Is it `hole`?"
	~Guess_L = true
	->Wrong
+{Guess_P == false}[Hope] 
	[{player_name}] "Is it `hope`?" 
	~Guess_P = true
	->YouWin 
+{Guess_M == false}[Home] 
	[{player_name}] "Is it `home`?"
	~Guess_M = true
	->Wrong
+[...nevermind]
	[{player_name}] "Hm..." #Skip
	->HangMan


===AlreadyKnow===
[{player_name}] "It's `hope`."
[Timothy] "What?! No Way!" #Timothy = Surprised
"How'd you guess is so quickly?" #Timothy=Sad
{
	-week == 2:
		Crap, it was the same! Yet another deja vu to the pile I suppose.
}
[{player_name}] "Uh, lucky guess?"
{
	-week > 2:
		You know, that and weird time travel shenanigans.
}
[Timothy] "..."
Timothy looks a little sad that I immediately got the answer.
But eventually he shrugs and smiles. #Timothy = Happy
->GameOver

=== YouLose ===
[Timothy] "You lose." #Hangman / 6
[{player_name}] "Aww, dang."
"Hey Timothy, what was the answer anyways?"
"Oh, nothing important..."
->GameOver

=== YouWin ===
[Timothy] "Yep!" #Hangman / P
"It's Hope." #Acheivment $ ACH_HANGMAN
~SetValue("SolvedHangman", true)
->GameOver


=== GameOver ===
[Timothy] "Hehehe.." #Background / Garden, crossfade, NoDefaults #Timothy = Happy
[{player_name}] "What's so funny?" 
[Timothy] "Oh, nothing... it's just..."
"Hangman is a good game..." 
"It teaches you that just by saying a few wrong things, you can end someone’s life..." #Timothy = Sad
{
	-week == 1:
		Wow. That was surprisingly dark joke from this guy.
		Should... Should I question him about that? #Skip
	-week == 2:
		Wow. And the exact same dark joke from Timothy. This must be a record of coincedences, or...
		A-anyways, should I bother questioning Timothy on that joke? #Skip
	-else"
		And somehow, even after looping time, such a dark joke from Timothy continues to throw me off.
		I guess I could question him on that, but should I? #Skip
}
+[Question Timothy <(expression > 2)>]
	[{player_name}] "You okay, Timothy?" # expression ^ good
	[Timothy] "Huh? Yeah. Why wouldn't I be?" #Timothy = Surprised
	[{player_name}] "Oh! its nothing. That joke was just kind of dark is all. I didn't expect that from you."
	[Timothy] "Oh, yeah. Sorry..." #Timothy = sad
	[{player_name}] "I mean, you don't need to apologize. I just want to make sure everything's okay with you."
	[Timothy] "Oh! uh... thanks?" #Timothy = Surprised
	"I dunno. I just like dark jokes." #Timothy = Angry
	"I-is that okay?" #Timothy = sad
	Timothy shrivels up. Is he really expecting me to reprimand him for having a sense of humor?
	[{player_name}] "Dude, of course it's okay." 
	"I don't mean to make you feel self-conscious or anything!" #Timothy = Surprised
	"You just be you, okay?" #Timothy = Happy 
	~SetIntValue("TimothyPoints", GetIntValue("TimothyPoints") + 1)
	Timothy seems a lot happier than I've seen him all day. I think I might of pressed one of his buttons, but like, in a good way?
	->WrapUp
+[Let it Slide]
	Eh, I'll let it slide. It's probably nothing.
	->WrapUp

===WrapUp===
//make this tutorial check instead
{
	-GetValue("Tutorial") == true:
		"Oh! Uh... S-Sorry f-for interrupting the t-tour.." #Timothy = Surprised
		[{player_name}] "It's okay."
}
[{player_name}] "So, what are you doing over here?"
[Timothy] "I'm not sure..." #Timothy = Calm
"These gardens are really pretty, and..."
"I don't know, I guess I'm really bad at putting feelings like that into words?" #Timothy=Sad
"Games make sense to me. @So I, uh, made my feelings a game?"
"I'm probably not making any sense."
[{player_name}] "No, no, I think I get it?"
{
	-GameFails < 6:
		"So, why Hope?"
		[Timothy] "Oh, uh, um, well..." #Timothy = Happy
		"It's my first day here. I guess maybe the sunshine made me a little optimistic." // OLD LINE: I guess the gardens made me think about word 'Hope'."
	-else:
		"So, what was the word?"
		[Timothy] "It's a secret."  #Timothy = Angry
		[{player_name}] "Aww, come on."
		[Timothy] "Nope. You didn't win, so them's the breaks."
}
[Timothy] "..." #Timothy = Afraid
"I'm sorry. I'm acting weird."
[{player_name}] "No, no, you're fine."
[Timothy] "T-Thanks." #Timothy = Happy
"That really m-means a lot."
Timothy gives me a cute, crooked smile as he gets up from the dirt patch. {SetIntValue("TimothyPoints", GetIntValue("TimothyPoints") + 1)} // +1 TP
{
	-GetValue("Tutorial"):
		Our little game finished, we head off to the next stop on the tour. #Timothy = Exit
		<>@The brief stop to reflect has <color=color_awareness>improved <b>Awareness</b> faintly.</i></color> # Awareness+
	-else:
		I spend the remainder of the hour showing Timothy around the gardens, thinking about how hectic things are probably going to get soon. #Timothy = Exit
		<>@Reflection has <color=color_awareness>improved <b>Awareness</b> faintly.</i></color> # Awareness+
}
-> END