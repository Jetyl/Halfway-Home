/******************************************************************************/
/*
@file   KnockOnTheMorningDoor.ink
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
VAR delusion = 0
VAR week = 0
VAR current_room = "unset"
VAR pronouns = ""
VAR possesive = "" 

EXTERNAL GetValue(value)
EXTERNAL SetValue(name, values)

-> Start

=== Start ===
{
	-player_gender == "F": 
		~pronouns = "she/her"
 	-player_gender == "M":
 		~pronouns = "he/him" 
 	-else:
 		~pronouns = "they/them"
 }
 {
	-player_gender == "F":
	 	~possesive = "her"
	 -player_gender == "M":
	 	~possesive = "him"
	 -else:
	 	~possesive = "them"
}


//~player_name = "Player"
//~week = 1
//play knocking sound effect
A cheerful knock drags me out of my dreary haze. # Background / YourRoom
{ 
	-week == 1:
		What do they want?
	- week == 2:
		Must be Max, to take me away from this limbo.
	//- GetValue("FirstRRR"):
	//	-> RestartRequired
	-else:
		The week has begun again.
}
I slowly creak out of my bed, and slump and lumber towards the door.
[{player_name}] "I'm coming, I'm coming..."
//turn off knocking. door opening sound?

{ 
	
	-week == 1:
		->Introductions
	//- week == 2:
	//	->Again
	-else:
		->Again
}

===Introductions===
[Max] "Heya {player_name}. Don't tell me I woke ya up?" # Keyless = Calm
[{player_name}] "Nah, I was already awake."
[Max] "Hope you weren't planning on sleeping in. You know you should have your routine down by now."
[{player_name}] "Yeah, yeah, I know."
[Max] "Ya oughta. Pretty soon you won't have old Max to be there reminding ya."
[{player_name}] "Yeah."
[Scrawny looking kid] "..." # Timothy = Calm
[{player_name}] "Who's that?"
[Max] "This is Timothy Miyuri. He's the new resident I told you about, remember?"
[{player_name}] "Uh... yeah?"
[Max] "You don't remember, do you?"
[{player_name}] "... No."
Max looks dissapointed in me. It's a sadly common look. # Keyless = Sad
[Max] "Look, I know you'd really prefer not having a roommate, but we're tight on space right now."
"Besides, it'll only be for one week."
[Timothy] "Nice to meet you." # Timothy = Happy
[Max] "Anyways, Timothy, this is {player_name}, pronouns are {pronouns}. You'll be sharing this room with {possesive} for the next week." # Keyless = Calm
[Timothy] "Okay." # Timothy = Calm
Timothy and Max both invite themselves into my room carrying several bags, likely full of Timothy's belongings.
Once Max has got all the bags in, they pat themselves down in a brief panic, before realizing something is wrong.
[Max] "Oh crap." # Keyless = Surprised
[{player_name}] "What?"
[Max] "It would seem I have misplaced my keys."
[{player_name}] "Uh-oh."
[Max] "Yeah, that's not good."
Max's eyes start darting while they think of what their next action should be.
"{player_name}, Imma need you to do me a solid."
I don't like where this is going. //Oh, you don't like the sound of that.
"Well... It's Timothy's first day, so someone's gotta show him the ropes." # Keyless = Happy
"But I can't have my keys unaccounted for here."
"So could you be a pal and show him around?"
[{player_name}] "Uh..."
[Max] "Thanks buddy." # Keyless = Exit
And just like that, Max leaves you all alone with this new stranger.
Timothy sighs dejectedly. Maybe he's used to this?
[{player_name}] "Uh... hey."
-> TakingTimothy

===Again===
What? # Keyless = Calm 
[Max] "Heya {player_name}. Don't tell me I woke ya up?" #Timothy=Calm
What in the...?
"Hope you weren't planning on sleeping in. You know you should have your routine down by now."
What the HELL?!
"Ya oughta. Pretty soon you won't have old Max to be there reminding ya"
"uh, Yello? Earth to {player_name}. ya still there?"
this doesn't feel real. Besides Max, I see... #Timothy=Calm
[{player_name}] "Timothy!"
[Timothy] "Meep!" #Timothy=Surprised
[{player_name}] "what are you doing back so soon?" #Timothy=Calm
"We're you being sent back to Blackwell Hospital?" #Timothy=Afraid
[Max] "Uh, {player_name}, you still dreaming buddy?"
"Timothy Myuri is the roommate I told you about"
wait
"You two will be sharing a room for the week, since we're kinda tight on space around here."
wait
[Max] "Anyways, Timothy, this is {player_name}, pronouns are {pronouns}. You'll be sharing this room with {possesive} for the next week." # Keyless = Calm
[Timothy] "Okay." # Timothy = Calm
Timothy and Max both invite themselves into my room carrying several bags, likely full of Timothy's belongings.
Once Max has got all the bags in, they pat themselves down in a brief panic, before realizing something is wrong.
[Max] "Oh crap." # Keyless = Surprised
[{player_name}] "What?"
[Max] "It would seem I have misplaced my keys."
[{player_name}] "Again?"
[Max] "Ah! I'm not that forgetful!" #Keyless=Sad
[{player_name}] "N-no, but, you lost them last week too, remember? they we're stuck in the couch cushions?"
[Max] "Couch cushions? hm..." #Keyless=Calm
"One sec. I'mma be right back." #Keyless=Exit
And with that Max quickly runs off, leaving me and Timothy alone
[{player_name}] "Timothy, are getting really weird deja vu today too?"
Timothy is just kind of quietly staring at the corner where his bed is.
"Yo, Timothy, can you hear me?"
[Timothy] "<size=30%>I don't know you<size=100%>"
[{player_name}] "what?"
[Max] "okay, I'm back!" #Max=Calm
"Anyways, where we're we Timothy?"
[Timothy] "You we're showing me around I think..."
[Max] "Ah, yes."
"Well lets head off."
"See you at the welcome dinner {player_name}!" #Max=Exit 
wait #Timothy=Exit
"Thanks again for the tip on the keys!"
and, they're gone...
What is even going on?
I'm left in my room, looking around. all of Timothy's stuff has been packed in the bags they brought in. 
my bags, on the other hand, have been emptied, and put away, from where I left them last night...
Last night....
[{player_name}] "Aggh!"
my head hurts, as if for a second I brain formed a spliting migrine, but I continue to follow the strangeness around me.
Was last night... really last night?
the knock on the door.@ Max inroducing Timothy as if we never met.@ Timothey even still being here!
this can't all be just deja vu, can it?
I think.... I think I'm back on the first day Timothy arrived here.@ But how? why?
I sit there, alone with my thoughts, theroizing how or why this situation has come before me, before I realize the time.
[{player_name}] "I-I should go looking for someone."
author Note: That is the end of this build. Hope you enjoyed it! :3
-> END

===Repeat===
same stuff as previously. to be expanded later.
for now, do you tell max where their keys are, and thus take Timothy around the home?
+[Yes, tell max where the keys are] ->Day1Alone
+[No, withold that information] ->TakingTimothy

===RestartRequired===
Nonononononononono.
In a bolt, I'm out of bed, and sprint for the door.
This can't be happening. this can't be happening.
-> Repeat

===TakingTimothy===
{SetValue("Tutorial", true)}
[{player_name}] "So, um..."
"Well, this is my room. Although, I guess it'll be <i>our</i> room for the time being."
"You can come here if you want to just get away from it all and destress."
"Also, obviously, you can just knock out here if you're too fatigued to do anything else. Or just whenever, really"
"I try to limit myself to 8 hours a day."
Though I've been falling off the wagon more often as of late.
"So yeah."
Timothy looks... not disinterested, but guarded and... preoccupied? Jeez, my ability to read people has seriously atrophied.
I go over and grab my watch of the desk beside my bed. 8:54. Close enough for my needs. He'll probably think I'm super weird, but Timothy should know about my system.
"So, uh... I don't know what brought you here and you don't have to tell me or anything, but one of the things I suffered from before was Lost Time."
[Timothy] "You mean you'd lose track of time? Doesn't everybody do that?"
[{player_name}] "No, it's more like..."
"There would be entire hours where I just am not home, up here:"
"Hours, maybe days, that I couldn't account for. couldn't remember."
"It's a type of dissociation."
"When I was at Blackwell the doctors told me to keep track of my time. I think it was mostly for them, so they'd know how bad things were."
"But ever since I count and keep track of the hours of the day very closely."
"I regiment what I do in a very hourly basis."
I flash Timothy the time on my watch, almost 9.
"C'mon, I'll show you."
//show map.
I am really starting to get hungry, so first stop: I should show Timothy the Cafe area. # Timothy = Exit
~SetValue("Tutorial", true)
-> END

===Day1Alone===
{SetValue("Tutorial", false)}
I've been left alone on this morning.
I should actually go about my day...

->END