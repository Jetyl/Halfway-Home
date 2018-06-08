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
VAR depression = 0
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
{ 
	-week == 1:
		A cheerful knock drags me out of my dreary haze. # SFX : play_sfx_human_knock #Background / YourRoom, eyeopen
		What do they want?
	- week == 2:
		I wake with a start. # Depression => 25 # Play : Stop_All # Ambience : Stop_All # Background / YourRoom, EyeOpen
		I spend a few confused moments unsure of my reality before I grasp that I'm awake now.
		It was that same dream again, but this time I knew I was dreaming. That's never happened before.
		The events of the week prior feel so far away... lost to the vivid horror of the nightmare. I can barely remember any of it.
		I guess I've got to get ready to leave now, but I'm not really in a rush.
		I lay staring at the ceiling, as I've done so many times before, wondering what the next roof over my head will look like.
		A cheerful knock drags me out of my dreary haze. # SFX : play_sfx_human_knock
		Must be Max, to take me away from this limbo.
	- GetValue("FirstRRR"):
		-> RestartRequired
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
[Keyless>Max] "Hella yella, my {player_name} fella." # Keyless = Calm
What?
[{player_name}] "Huh?"
"Ugh... @Morning, Max."
[Keyless>Max] "Morning? It's almost noon! Don't tell me I was your alarm clock this morning!"
[{player_name}] "N-no... @I was already awake."
[Keyless>Max] "That's good. Pretty soon ya won't have the ol' Max alarm to rely on."
[{player_name}] "Yeah."
[Timothy>Scrawny looking kid] "..." # Timothy = Calm #Timothy = stage_Right
[{player_name}] "Who's that?"
[Keyless>Max] "This is Timothy Miyuri. He's the new resident I told you about, remember?"
[{player_name}] "Uh... yeah."
[Keyless>Max] "You don't remember, do you?"
[{player_name}] "... @No."
Max looks disappointed in me. It's a sadly common look. # Keyless = Sad
[Keyless>Max] "Look, I know you'd really prefer not having a roommate, but we're tight on space right now."
"Besides, it'll only be for one week."
[Timothy] "Nice to meet you." # Timothy = Happy
[Keyless>Max] "Anyways, Timothy, this is {player_name}, pronouns are {pronouns}. You'll be sharing this room with {possesive} for the next week." # Keyless = Calm
[Timothy] "Okay." # Timothy = Calm
Timothy and Max both invite themselves into my room carrying several bags, likely full of Timothy's belongings.
Once Max has got all the bags in, they pat themselves down in a brief panic, before realizing something is wrong. // Could remove line
[Keyless>Max] "Oh crap." # Keyless = Surprised
[{player_name}] "What?"
[Keyless>Max] "I can't find my keys! I must have left 'em somewhere..."
Max's eyes start darting while they think of what their next action should be.
"{player_name}, I'm gonna need you to do me a solid."
Oh, you don't like the sound of that.
"Well... It's Timothy's first day, so someone's gotta show him the ropes." # Keyless = Happy
"But I can't have my keys unaccounted for here."
"So could you be a pal and show him around?"
[{player_name}] "Uh..."
[Keyless>Max] "Thanks, buddy!"
And just like that, Max leaves me all alone with this new stranger. # Keyless = Exit
Timothy sighs dejectedly. Maybe he's used to this? // Could remove line
[{player_name}] "Uh... hey."
-> TakingTimothy

===Again===
What? # Keyless = Calm #Timothy=Calm
[Keyless>Max] "Hella Yella, my {player_name} fella."
[{player_name}] "huh?"
D-didn't Max say that before?
"uh?... Morning Max." 
[Keyless>Max] "Morning? It's almost noon! Don't tell me, I was your alarm clock this morning?"
[{player_name}] "W-What? N-no... @I, uh, was already awake."
Okay, I'm getting the strangest case of deja vu right now...
[Keyless>Max] "Das good. Das good. Y'all can't be relying on a the Max alarm pretty soon."
Okay, now its getting weird.
"Uh, hello? Earth to {player_name}. Ya still there?"
This doesn't feel real. Besides Max, I see... #Timothy=Calm
[{player_name}] "Timothy!"
[Timothy] "Meep!" #Timothy=Surprised
[{player_name}] "what are you doing back so soon?" #Timothy=Calm
"We're you being sent back to Blackwell Hospital?" #Timothy=Afraid
[Keyless>Max] "Uh, {player_name}, you still dreaming buddy?"
"Timothy Miyuri is the roommate I told you about."
Wait...
"You two will be sharing a room for the week, since we're kinda tight on space around here."
Wait...
[Keyless>Max] "Anyways, Timothy, this is {player_name}, pronouns are {pronouns}. You'll be sharing this room with {possesive} for the next week." # Keyless = Calm
[Timothy] "Okay." # Timothy = Calm
Timothy and Max both invite themselves into my room carrying several bags, likely full of Timothy's belongings.
Once Max has got all the bags in, they pat themselves down in a brief panic, before realizing something is wrong.
[Keyless>Max] "Oh crap." # Keyless = Surprised
[{player_name}] "What?"
[Keyless>Max] "It would seem I have misplaced my keys."
[{player_name}] "Again?"
[Keyless>Max] "Ah! I'm not that forgetful!" #Keyless=Sad
[{player_name}] "N-no, but, you lost them last week too, remember? they we're stuck in the couch cushions?"
[Keyless>Max] "Couch cushions? hm..." #Keyless=Calm
"One sec. I'mma be right back." #Keyless=Exit
And with that Max quickly runs off, leaving me and Timothy alone
[{player_name}] "Timothy, are getting really weird deja vu today too?"
Timothy is just kind of quietly staring at the corner where his bed is.
"Yo, Timothy, can you hear me?"
[Timothy] "<size=50%>I don't know you<size=100%>"
[{player_name}] "what?"
[Keyless>Max] "okay, I'm back!" #Max=Calm
"Anyways, where we're we Timothy?"
[Timothy] "You we're showing me around I think..."
[Keyless>Max] "Ah, yes."
"Well lets head off."
"See you at the welcome dinner {player_name}!" #Max=Exit #Timothy=Exit
Wait...
"Thanks again for the tip on the keys!"
And, they're gone...
What is even going on?
I'm left in my room, looking around. all of Timothy's stuff has been packed in the bags they brought in. 
My bags, on the other hand, have been emptied, and put away, from where I left them last night...
Last night....
[{player_name}] "Aggh!"
My head hurts, as if for a second I brain formed a splinting migraine, but I continue to follow the strangeness around me.
Was last night... really last night?
the knock on the door.@ Max introducing Timothy as if we never met.@ Timothy even still being here!
this can't all be just deja vu, can it?
I think.... I think I'm back on the first day Timothy arrived here.@ But how? why?
I sit there, alone with my thoughts, theorizing how or why this situation has come before me, before I realize the time.
[{player_name}] "I-I should go looking for someone."
<color=A5C5E3FF><i>author Note: That is the end of this build. Hope you enjoyed it! :3</i></color>
-> END

===Repeat===
same stuff as previously. to be expanded later.
for now, do you tell max where their keys are, and thus take Timothy around the home?
+[Yes, tell max where the keys are] ->Day1Alone
+[No, withhold that information] ->TakingTimothy

===RestartRequired===
Nonononono!
In a bolt, I'm out of bed, and sprint for the door.
This can't be happening. this can't be happening.
-> Repeat

===TakingTimothy===
{SetValue("Tutorial", true)}
[{player_name}] "So, um..." #Timothy = Stage_center
Oh, man. I did not wake up expecting to be a tour guide today. Ce la vie.
I whip out my planner and jot down a reminder to myself, a habit my old therapist encouraged me to follow. # 0 & InProgress
"Well, this is my room. Although, I guess it'll be <i>our</i> room for the time being."
"You can come here if you want to just get away from it all and de-stress." # Stress * Show
"Also, obviously, you can just knock out here if you're too <color=color_wellbeing_penalty>fatigued</color> to do anything else. Or just whenever, really." # Fatigue * Show
"Max'll get mad if you spend too much time loafing around, though, since we're supposed to be `active agents of self-recovery` or whatever."
I go over and grab my watch off the desk beside my bed. Max was right, it is almost noon. // Show time here?
"So, uh... I don't know what brought you here and you don't have to tell me or anything, but I learned my lesson after my last roommate so I wanna let you in on my situation a bit."
[Timothy] "Oh, okay. <size=50%>Last roommate?<size=100%>" # Timothy = Surprised
"I was in Blackwell for a few years before coming here."
[Timothy] "Me, too." # Timothy = Happy
[{player_name}] "It's a pretty common story around here, really. We all know what it's like. At least, better than anyone out there."
"Anyway, I struggled a lot with depression and Lost Time. I still do sometimes." # Timothy = Calm
Maybe more than sometimes, but he doesn't need to know that.
[Timothy] "Oh, man. Depression sucks. I've never heard of Lost Time, though." # Timothy = Sad
"You mean you lose track of time? Doesn't everybody do that?" # Timothy = Calm
[{player_name}] "No, it's more like..."
"There are times when I'm not home... up here. It was way worse before, of course."
That much is true, but it's only gotten a little better, if I'm being honest.
"There were hours, sometimes days that I couldn't account for and couldn't remember. As if the time was lost."
"The actual term is dissociative amnesia."
"It tends to flair up when I'm more <color=color_wellbeing_penalty><i>depressed</i></color>." # Depression * Show
If I get too <color=color_wellbeing_penalty><i>depressed</i></color>, the amount of time it takes to do a task can <color=color_descriptor><i>double</i></color>, or even <color=color_descriptor><i>triple</i></color>.
"So if I'm ever late for anything, it's not because I don't care or something..."
[Timothy] "Right, okay."
I am really starting to get hungry, so my first move should be to show Timothy the Cafe area. # Timothy = Exit # 0.0 & InProgress
~SetValue("Tutorial", true)
-> END

===Day1Alone===
{SetValue("Tutorial", false)}
I've been left alone on this morning.
I should actually go about my day...

->END
