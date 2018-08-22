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
VAR possessive = "" 

EXTERNAL GetValue(value)
EXTERNAL SetValue(name, values)

# music_vol | -6

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
	 	~possessive = "her"
	 -player_gender == "M":
	 	~possessive = "him"
	 -else:
	 	~possessive = "them"
}


//~player_name = "Player"
//~week = 1
//play knocking sound effect
{ 
	-week == 1:
		A cheerful knock drags me out of my dreary haze.   #Background / YourRoom, eyeopen   # SFX : play_sfx_human_knock
		What do they want?
	- week == 2:
		I wake with a start. # Stress => 10  # Depression => 25  # Fatigue => 15 # Background / YourRoom, EyeOpen #Acheivement * ACH_WEEK
		I spend a few confused moments unsure of my reality before I grasp that I'm awake now.
		It was that same dream again, but this time I knew I was dreaming. That's never happened before.
		The events of the week prior feel so far away... lost to the vivid horror of the nightmare. I can barely remember any of it.
		I guess I've got to get ready to leave now, but I'm not really in a rush.
		I lay staring at the ceiling, as I've done so many times before, wondering what the next roof over my head will look like.
		A cheerful knock drags me out of my dreary haze.   # SFX : play_sfx_human_knock
		Must be Max, to take me away from this limbo.
	- GetValue("RRRFreakout"):
		I wake slowly, only dimly aware of the reality around me as I return to consciousness. # Depression => 25  # Fatigue => 15 # Stress => 10 # Background / YourRoom, EyeOpen
		That dream again... I can't quite remember...
		I stretch and turn over to face Timothy's bed. {My heart sinks to the floor.|I sigh lightly.}
		He's gone. {Why is he gone?!|It wasn't enough.}
		There's a knock on the door. # SFX : play_sfx_human_knock
		{-> Freakout|->NotSurprised}
	-else:
		I blearily open my eyes. # Depression => 25  # Fatigue => 15 # Stress => 10 # Background / YourRoom, EyeOpen
		I'm not holding out any hope that things are back to normal.
		There's a knock on the door. # SFX : play_sfx_human_knock
}
I slowly creak out of my bed, and slump and lumber towards the door.   # SFX : play_sfx_bed_creak
[{player_name}] "I'm coming, I'm coming..."   # Play : Stop_All
//turn off knocking. door opening sound?

{ 
	
	-week == 1:
		->Introductions
	- week == 2:
		->Again
	-else:
		->Repeat
}

===Introductions===
[Keyless>Max] "Hella yella, my {player_name} fella."   # Keyless = Calm   # Play : play_music_happy   # Ambience : Stop_All
What?
[{player_name}] "Huh?" #Skip
+[Hella yella?]
	"Hella yella? What does that even mean?"
	[Keyless>Max] "It's a thing people say. Don't look at me like that!"//"What, its just something I say sometimes."
	[{player_name}] "I have literally never heard anyone say anything remotely like that." // Formerly "heard you say"
	Max shrugs.//guess they're just being weird? whatever.
+[Morning...]
	"Ugh... @Morning, Max."
	[Keyless>Max] "Morning? It's almost noon! Don't tell me I was your alarm clock this morning!"
	[{player_name}] "N-no... I was already awake."
	[Keyless>Max] "Good. Pretty soon ya won't have the ol' Max alarm to rely on."
	[{player_name}] "I guess not."
-[Timothy>Scrawny looking kid] "..."   # Timothy = Calm   # Timothy = stage_Right
[{player_name}] "Who's that?"
[Keyless>Max] "This is Timothy Miyuri. He's the new resident I told you about, remember?"
[{player_name}] "Uh... yeah."
[Keyless>Max] "You don't remember, do you?" #Skip
+[I remember (Lie)]
	[{player_name}] "Uh, y-yeah, I, uh, remember that."
+[No (Honest)]
	[{player_name}] "...No."
-Max looks disappointed in me. It's a sadly common look.   # Keyless = Sad
[Keyless>Max] "Look, I know you'd prefer not having a roommate foisted on you your last week, but we're tight on space right now." // Added reference to it being last week here
"Besides, I think it'll be good for both of you." // OLD LINE: It'll only be for one week
[Timothy] "<jitter>N-Nice to meet you.</jitter>" # Timothy = Happy
[Keyless>Max] "Anyways, Timothy, this is {player_name}, pronouns are {pronouns}. You'll be sharing this room with {possessive} for the next week."   # Keyless = Calm
[Timothy] "Okay."   # Timothy = Calm
Timothy and Max both invite themselves into my room carrying several bags, likely full of Timothy's belongings.
Max dumps the last of the bags on the floor and puts their hands on their hips.
[Keyless>Max] "Oh crap."   # Keyless = Surprised
Suddenly confused, they begin patting themselves down. Confusion turns to panic.
[{player_name}] "What?"
[Keyless>Max] "I can't find my keys! I must have left 'em somewhere..." # Keyless = Afraid
Boy do I know that feeling. I haven't been able to find my own key for months.
Max's eyes start darting while they think of what their next action should be.
"{player_name}, I'm gonna need you to do me a solid."
I get the feeling I'm about to be dragged into something.
"Well... It's Timothy's first day, so someone's gotta show him the ropes." # Keyless = Happy
"But I can't have my keys unaccounted for here."
Called it.
"So could you be a pal and show him around?" #Skip
+[Sure]
	[{player_name}] "Sure."
+[Um... no]
	[{player_name}] "Uh..."
-[Keyless>Max] "Thanks, buddy!"
And just like that, Max leaves me all alone with this new stranger.   # Keyless = Exit   # SFX : play_sfx_human_footsteps_approaching
//Timothy sighs dejectedly. Maybe he's used to this? // Could remove line
[{player_name}] "Uh... hey."
-> TakingTimothy

===Again===
What?   # Keyless = Calm   #Timothy=Calm   # Play : play_music_dejavu
[Keyless>Max] "Hella Yella, my {player_name} fella."
[{player_name}] "Huh?"
D-didn't Max say that before?
"Uh?... Morning Max." 
[Keyless>Max] "Morning? It's almost noon! Don't tell me, I was your alarm clock this morning?"
[{player_name}] "W-What? N-no... @I, uh, was already awake."
Okay, I'm getting the strangest case of 'deja vu' right now...
[Keyless>Max] "That's good. Pretty soon ya won't have the ol' Max alarm to rely on."
Okay, now its getting weird.
-"Uh, hello? Earth to {player_name}. Ya still there?"
This doesn't feel real. Besides Max, I see...   #Timothy=Calm
[{player_name}] "Timothy!"
[Timothy] "Meep!" #Timothy=Surprised
[{player_name}] "What are you doing back so soon?" #Timothy=Calm
"Weren't you being sent back to Blackwell?" #Timothy=Afraid
[Keyless>Max] "Uh, {player_name}, you still dreaming buddy?"
"Timothy Miyuri is the roommate I told you about."
Wait...
"You two will be sharing a room for the week, since we're kinda tight on space around here."
Wait...
[Keyless>Max] "Anyways, Timothy, this is {player_name}, pronouns are {pronouns}. You'll be sharing this room with {possessive} for the next week."   # Keyless = Calm
[Timothy] "Okay."   # Timothy = Calm
Timothy and Max both invite themselves into my room carrying several bags, likely full of Timothy's belongings.
I just stand there uselessly, trying to rationalize the mundane madness around me.
Max dumps the last of the bags on the floor and puts their hands on their hips.
[Keyless>Max] "Oh crap."   # Keyless = Surprised
Suddenly confused, they begin patting themselves down. Confusion turns to panic.
[{player_name}] "What?"
[Keyless>Max] "It would seem I have misplaced my keys." # Keyless = Afraid
[{player_name}] "Again?"
[Keyless>Max] "Ah! I'm not that forgetful!" #Keyless=Sad
[{player_name}] "N-no, but, you lost them last week too, remember? They were stuck in the couch cushions in the commons."
[Keyless>Max] "I don't recall losing my keys last week, but... Couch cushions? Hmm..." #Keyless=Calm
"One sec. I'mma be right back." #Keyless=Exit
And with that Max quickly runs off, leaving me and Timothy alone
[{player_name}] "Timothy, are you getting really weird 'deja vu' today, too?"
Timothy is just kind of quietly staring at the corner where his bed is.
"Yo, Timothy, can you hear me?"
[Timothy] "<jitter><size=50%>I don't know you...<size=100%></jitter>"
[{player_name}] "What?"
Before I can question further, Max returns jangling their keys. The world feels like it's on fast forward. // OLD LINE: Before I could question further, Max returned like they had never left. Everything feels too fast.
[Max] "Okay, I'm back! Nice call on those couch cushions, {player_name}!" #Max=Calm
"Anyways, where were we Timothy?"
[Timothy] "The... The tour?"
[Max] "Ah, yes."
"Let's head off!"
"See you at the welcome dinner, {player_name}!" #Max=Exit #Timothy=Exit
What the heck is going on?!
Now alone, I look around the room, trying to come to grips with the situation.
In place of the bags I packed last night, Timothy's meager bags now occupy the floorspace. 
Last night...
[{player_name}] "Agh!"
A flash of pain slices through my brain. My hands jump to my temples, but the sensation dissipates before they reach their destination. //OLD LINE: My head hurts, as if for a second I brain formed a splinting migraine, but I continue to follow the strangeness around me.
Was last night... really last night?
The knock on the door.@ Max introducing Timothy as if we'd never met.@ Timothy... here...!
This can't all be just 'deja vu', can it?
I think... I think I'm back on the first day Timothy arrived here.@ But how? Why?
I flip through my notebook, but all the notes I had taken over the week are gone.
I'm a week in the past!
"W-why is this happening? @<i>How</i> did this happen?!"
I rack my brain, desperately searching for any explanation.
Was it all a dream? // OLD LINE: Maybe the whole last week didn't happen? Maybe it was a dream?
It felt so real... but, then again, dreams often do. // OLD LINE: It didn't feel like a dream, but then again, dreams never do when you're in them.
//And, I don't remember anything particularly strange or dreamlike about last week... @remember...
I have to know. I write everything I can remember down in my notebook before I have a chance to forget it. // OLD LINE: Dream or no, I decide to refill my notebook of everything I had done last week I could remember.
There are some gaps and it's a bit rough, but I manage to record a decent amount. Time to see if I'm crazy or not. # time % 1 // OLD LINE: It takes some time, and its probably not perfect, but its there. It'll help ground me. #time % 1
I take a deep breath, shaking off some of the existential dread still washing over me. // OLD LINE: Even after having been up for quite some time, the feeling of dread and surrealness hadn't left, although I was calmer now.
[{player_name}] "I should go looking for someone."
-> END

===Repeat===
I open the door to Max's beaming face. A scared Timothy hides behind them.   #Keyless = Calm   #Timothy = Afraid
{GetValue("RRRFreakout")==true:
	{I was wrong. Looks like I'm in for another repeat week.|Yep. That's about right.}
	{I try to hide my disappointment.|One more time from the top.}
	~SetValue("RRRFreakout", false)
}
[Keyless>Max] "Hella yella, my {player_name} fella." #Skip
+[Morning Max]
	[{player_name}] "Ugh... Morning Max."
	[Keyless>Max] "Morning? It's almost noon! Don't tell me I was your alarm clock this morning!"
	[{player_name}] "What? N-no... @I, uh, was already awake."
	[Keyless>Max] "That's good. Pretty soon ya won't have the ol' Max alarm to rely on."
	[{player_name}] "Yeah. yeah."
+[Afternoon Max]
	[{player_name}] "Ugh... Mor- I mean, good afternoon Max."
	[Keyless>Max] "It ain't quite afternoon yet, buddy. But good to see ya too."
	I roll my eyes at Max's pedantry. You can never win with them sometimes.
-[Timothy] "..." #Skip
+[Who's That?]
	I decide it's best if I just play along.
	[{player_name}] "Who's that?"
	[Keyless>Max] "This is Timothy Miyuri. He's the new resident I told you about, remember?"
	[{player_name}] "Uh... yeah."
	[Keyless>Max] "You don't remember, do you?"
	It hurts to lie to Max, but it's better than the alternative.
	[{player_name}] "... No."
	Max looks disappointed in me, but I'd prefer that to them suspecting I'd gone off the deep end.   # Keyless = Sad
+[Hi, Timothy <(expression > 0)>]
	[{player_name}] "Hi, Timothy".
	[Timothy] "Meep!"   #Timothy=Surprised
	[Keyless>Max] "Ah, good, you remembered. And here I was worried you forgot."
	[{player_name}] "Psh. Me? Forget? No chance." // OLD LINE: "Oh, Max, please. You wound me." This kinda seems a bit too snarky for Sam
	[Keyless>Max] "Again, {player_name}, thanks for being reasonable about this."
-[Keyless>Max] "I know you'd really prefer not having a roommate for your last week, but we're tight on space right now."
"Anyways, Timothy, this is {player_name}, pronouns are {pronouns}. You'll be sharing this room with {possessive} for the next week."   # Keyless = Calm
[Timothy] "Nice to meet you."   # Timothy = Happy
Timothy and Max both invite themselves into my room carrying several bags full of Timothy's belongings.
Max dumps the last of the bags on the floor and puts their hands on their hips.
Suddenly confused, they begin patting themselves down. Confusion turns to panic.
[Keyless>Max] "Oh crap."   # Keyless = Surprised
Max must be realizing they don't have their keys.
I could tell Max where there keys are themselves and have the day to myself...
Or I could show Timothy around myself and snag the keys from the cushions before they can. #Skip
+[Tell Max where the keys are] 
	[{player_name}] "What?"
	[Keyless>Max] "It would seem I have misplaced my keys." # Keyless = Afraid
	->Day1Alone
+[Withhold that information <(expression > 1)>] 
	[{player_name}] "What?"
	[Keyless>Max] "I can't find my keys! I must have left 'em somewhere..." # Keyless = Afraid
	"{player_name}, I'm gonna need you to do me a solid."
	"Well... It's Timothy's first day, so someone's gotta show him the ropes."   # Keyless = Happy
	"But I can't have my keys unaccounted for here."
	"So could you be a pal and show him around?"
	++[Sure]
		[{player_name}] "Sure." #Grace ^ Good
		[Keyless>Max] "Thanks, buddy!"
		And just like before, Max leaves me with Timothy.   # Keyless = Exit   # SFX : play_sfx_human_footsteps_approaching
		He's no stranger to me any more, but I am to him... weird to think about.
		->TakingTimothy
	++[Actually, I remember where you lost your keys]
		[{player_name}] "Actually, I think I remember where you lost your keys!" #expression ^ Good
		[Keyless>Max] "Wait, really? where?"
		->Day1Alone
	

===Freakout===
No no no no!
I bolt out of bed and sprint for the door.   # SFX : play_sfx_human_footsteps_approaching
This can't be happening.
-> Repeat

===NotSurprised===
I drag myself out of bed and take a deep breath. # SFX : play_sfx_bed_creak
Here we go again.
->Repeat

===TakingTimothy===
{SetValue("Tutorial", true)}
[{player_name}] "So, um..." #Timothy = Stage_center
{
	-week == 1:
	Oh, man. I did not wake up expecting to be a tour guide today. C'est la vie.
}
I whip out my planner and jot down a reminder to myself, {a habit my old therapist encouraged me to follow.|as well as fill in some things from the previous week.} # 0 & InProgress
"So... welcome to Sunflower House. You probably already know all of this, but..."
"It's a Halfway House for young adults with mental issues. Like me. And, I assume, you."
"It's supposed to be `halfway` between needing serious care and being able to capably live with your condition in the real world."
"You can technically leave any time, but usually a therapist will recommend a date for you."
"Sunflower House is a bit weird compared to a lot of other places, from what I hear."
"For example, people tend to stay here for a lot longer and are more self-directed."
The skeptical part of me thinks this is so the administrators can save money on oversight, but I don't mention that to the new guy.
I go over and grab my watch off the desk beside my bed. Max was right, it's noon on the dot.  # Time * Show
"This is my room. Although, I guess it'll be <i>our</i> room for the time being."
"You can come here if you want to just get away from it all and de-<color=color_wellbeing_penalty>stress</color>."   # Stress * Show
"Also, obviously, you can just knock out here if you're too <color=color_wellbeing_penalty>fatigued</color> to do anything else. Or just whenever, really."   # Fatigue * Show
"Max'll get mad if you spend too much time loafing around, though, since we're supposed to be `active agents of self-recovery` or whatever."
"Oh, and one more thing. I kinda lost my room key a while ago, so if you lock the door I don't really have a way in unless Max is around."
[Timothy] "Oh, sure. <jitter>W-wait,</jitter> couldn't you just ask Max for a new key?"   # Timothy = Surprised
[{player_name}] "Well, ya see... by the time I finally asked Max last week, they said it would take longer to get me a new key than I was even around for."
"There's three copies of every room key. One for each roommate and one for the floor R.A. in case of emergencies."
"I lost mine, Max can't give theirs away, and since you were coming Max wouldn't give me yours."
"To be honest, it hasn't been a big deal since I've been by myself for a while."
[Timothy] "Oh."   # Timothy = Sad
[{player_name}] "So, uh... I don't know what brought you here and you don't have to tell me or anything, but I learned my lesson after my last roommate so I wanna let you in on my situation a bit."
[Timothy] "Oh, okay. @@<size=50%>Last roommate?<size=100%>"   # Timothy = Surprised
[{player_name}] "I was in Blackwell for a few years before coming here."
[Timothy] "Me, too."   # Timothy = Happy
[{player_name}] "It's a pretty common story around here, really. We all know what it's like. At least, better than anyone out there."
"Anyway, I struggled a lot with depression and lost time. I still do sometimes."   # Timothy = Calm
Maybe more than sometimes, but he doesn't need to know that.
[Timothy] "Oh, man. Depression sucks. I've never heard of `lost time`, though."   # Timothy = Sad
"You mean you lose track of time? Doesn't everybody do that?"   # Timothy = Surprised
[{player_name}] "No, it's more like..."
"There are times when I'm not home... up here. It was way worse before, of course."
That much is true, but it's only gotten a little better, if I'm being honest.
"There were hours, sometimes days that I couldn't account for and couldn't remember. As if the time was lost."
"The actual term is dissociative amnesia."
"It tends to flair up when I'm more <color=color_wellbeing_penalty><i>depressed</i></color>."   # Depression * Show
If I get too <color=color_wellbeing_penalty><i>depressed</i></color>, the amount of time it takes to do a task can <color=color_descriptor><i>double</i></color>, or even <color=color_descriptor><i>triple</i></color>.
"So if I'm ever late for anything, it's not because I don't care or something..."
[Timothy] "Right, okay."
I am really starting to get hungry, so my first move should be to show Timothy the cafe area.   # Timothy = Exit   # 0.0 & InProgress   // # Load @ story_rf_kitchen   # Load @ room_cafe
~SetValue("Tutorial", true)
-> END

===Day1Alone===
{SetValue("Tutorial", false)}
[{player_name}] "They probably just fell into the couch cushions in the commons."
[Keyless>Max] "Oh, don't be silly {player_name}, I-"
"Actually, that's entirely plausible. Don't move, I'll be right back!"
Max turns and rushes out of the room before either of us get a chance to speak.   #Keyless = Exit   # SFX : play_sfx_human_footsteps_approaching
[Timothy] "What was that-"   #Skip
[{player_name}] "Give 'em a second."
Moments later, Max bursts in through the door, jangling their Keys in celebration.   #Max = Calm
[Max] "Good call, {player_name}! You psychic or somethin' now?"
[{player_name}] "Just a lucky guess."
[Max] "Anyways, where were we Timothy?"
[Timothy] "The... The tour?"
[Max] "Ah, yes."
"Let's head off."
"See you at the welcome dinner, {player_name}!"   #Max=Exit   #Timothy=Exit   # SFX : play_sfx_human_footsteps_approaching
[{player_name}] "See ya!"
I quickly scribble some of my notes from last week into my journal.
Looks like I managed to steal some alone time.
Better make use of it...
->END
