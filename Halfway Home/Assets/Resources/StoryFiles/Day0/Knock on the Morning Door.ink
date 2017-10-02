
VAR player_name = "tbd"
VAR player_gender = "tbd"
VAR grace = 0
VAR expression = 0
VAR awareness = 0
VAR fatigue = 0
VAR stress = 0
VAR delusion = 0
VAR week = 1

VAR pronouns = ""
{
	-player_gender == "F": 
		pronouns = she/her 
 	-player_gender == "M":
 		pronouns = he/him  
 	-else:
 		pronouns = they/them
 }
VAR possesive = "" 
{
	-player_gender == "F":
	 possesive = her
	 -player_gender == "M":
	 possesive = him
	 -else:
	  possesive = them
}

EXTERNAL PlayMusic(trackName)
EXTERNAL CharEnter(nameString, poseString)
EXTERNAL CharExit(nameString)
EXTERNAL GetValue(value)
EXTERNAL SetValue(name, values)

-> Start

=== Start ===
//play knocking sound effect
[{player_name}] "uuugh..."
the persistant knocking drags me out of my dreary haze
{ 
	- week == 1:
		what do they want?
	- week == 2:
		must be Max, to take me away from this limbo.
	- GetValue("FirstRRR"):
		-> RestartRequired
	-else:
		The week has begun again.
}
I slowly creak out of my bed, and slump and lumber towards the door.
[{player_name}] "I'm coming, I'm coming..."
//turn off knocking. door opening sound?
{CharEnter("Timothy", "Test")}
{CharEnter("Max", "Test")}
{ 
	- week == 1:
		->Introductions
	- week == 2:
		->Again
	-else:
		->Repeat
}

===Introductions===
[Max] "Heya {player_name}. Don't tell me I woke ya up?"
[{player_name}] "Yeah."
[Max] "Hope you weren't planning on sleeping in. You know you should have your routine down by now."
[{player_name}] "yeah, yeah, I know."
[Max] "Ya oughta. Pretty soon you won't have old Max to be there reminding ya"
[{player_name}] "Yeah."
[Scrawny looking kid] "..."
[{player_name}] "who'se that?"
[Max] "Oh, this is Timothy Miyuri. He's the new resident I told you about, remember."
[{player_name}] "uh... yeah?"
[Max] "You don't remember, do you?"
[{player_name}] "...no..."
Max looks dissapointed in you. Something you feel is sadly common.
[Max] "Look, I know you'd really perfer no having a roommate, but we're tight on space right now."
"besides, it'll only be for one week."
[Timothy] "Nice to meet you."
[Max] "Anyways, Timothy, this is {player_name}, pronouns are {pronouns}. You'll be sharing this room with {possesive} for the next week."
[Timothy] "okay"
Timothy and Max both invite themselves into your room, carrying several bags and such, likely full of Timothy's belongings.
Once Max's got all the bags in your room, they pat themselves down in a brief panic, before realizing something is wrong.
[Max] "Oh crap."
[{player_name}] "What?"
[Max] "It would seem I have misplaced my keys."
[{player_name}] "uh-oh."
[Max] "Yeah that's not good."
Max's eyes start darting while they think of what their next action should be.
"{player_name}, I'mma need you to do me a solid."
oh, you don't like the sound of that.
"So, It's Timothy's first day, so someone's gotta show him the ropes."
"But I can't have my keys unaccounted for here."
"So could you be a pal and show him around?"
[{player_name}] "uuh..."
[Max] "Thanks buddy." CharExit("Max")
and just like that, Max leaves you all alone with this new stranger.
The kid, Timothy, sighs in a very dejected manner. maybe he's used to this?
[{player_name}] "uh... hey."
-> TakingTimothy

===Again===
What.
[Max] "Heya {player_name}. Don't tell me I woke ya up?"
What in the...
"Hope you weren't planning on sleeping in. You know you should have your routine down by now."
What the HELL
"Ya oughta. Pretty soon you won't have old Max to be there reminding ya"
"uh, Yello? Earth to {player_name}. ya still there?"
//show just timothy
-> Day1Alone

===Repeat===

->END

===RestartRequired===
Nonononononononono.
In a bolt, I'm out of bed, and sprint for the door.
This can't be happening. this can't be happening.
-> END

===TakingTimothy===
{SetValue("Tutorial", true)}
[{player_name}] "So, um..."
"Well, this is my room. although, I guess it'll be our room for the time being."
"you can come here, if you want to just get away from it all, and Destress."
"Also, obviously, you can just knock out here if your too fatigued to do anything else. or just whenever, really"
"I try to limit myself to 8 hours a day."
though I've been falling of the wagon on that one more often, as of late.
"So yeah."
Timothy looks... not disinterested, but very guarded, and preoccupied at the same time.
I go over and grab my watch of the desk beside my bed. 8:54. close enough for my needs.
"So, um. I don't know what brought you here, and you don't have to tell me or anything, but for me."
"One of the things I suffered from really bad before was Lost Time"
[Timothy] "you mean you'd lose track of time?"
[{player_name}] "no, more..."
hm, how to explain this...
"more, there would be entire hours where I just am not home, up here"
"hours, maybe days, that I couldn't account for. couldn't remember."
"it's a type of dissasiociation."
"becuase of that, one of the stratagies is I count and keep track of the hours of the day very closely."
"I regiment what I do in a very hourly basis."
I flash Timothy the time on my watch, showing its almost 9.
"c'mon, I'll show you."
//show map.
anyways, I am hungry. so first stop, I should show Timothy the Cafe area.
-> END

===Day1Alone===

->END