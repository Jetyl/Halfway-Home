/******************************************************************************/
/*
@file   RoomDefaults.ink
@author Jesse Lozano & John Myres
@par    email: jesse.lozano@digipen.edu, john.myres@digipen.edu
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

VAR ByTimothy = false
VAR HeardIsaacsStory = false

EXTERNAL SetValue(ValueName, newValue)
EXTERNAL GetValue(value)

# Load @ story_welcome_dinner   # Ambience : play_ambience_crowd

-> Start

=== Start ===
I shuffle into the kitchen, which is much more packed than usual, with everybody showing up for the celebratory Welcome Dinner.
The Halfway Home has these big get together dinners whenever we get someone new, or someone leaves.
It's supposed to be a big bonding moment. Or something like that.
I actually barely remember my own welcome dinner. I think there were beans? That's about all I got.
Timothy, the star of this show, gets the center seat, so he can meet everybody. Currently, he seems to be talking with Trissa.
Eduardo and Isaac are off in their own little corner, giggling to themselves. 
Charlotte and Max are helping serve food at the front of the line.
I decide to... #Skip
*[Sit near Timothy] ->NearTimothy
*[Sit near Eduardo] ->NearEduardo
*[Offer help to Charlotte] ->NearCharlotte


===NearTimothy===
~ByTimothy = true
I guess I must be a bit late since the line for food isn't all that long. I grab a plate and head for the center table. 
I pull up a chair next to Timothy and take a seat. Trissa gives me a welcoming smile. # Trissa = Happy, Stage_Right, Left # Timothy = Happy, Right
[Timothy] "Hi, {player_name}."
[Trissa] "Come to see the man of the hour, huh?"
[{player_name}] "Sure did."
{depression > 40: [Voices] "As if he wants to see you."}
[Trissa] "Like I was saying. You've got nothing to worry about! Everybody here is excited to meet you!"
[Timothy] "That's kind of a 'lotta pressure." # Timothy = Afraid #Skip
*[Reassure Him]
	{
		-awareness > 1: 
			[{player_name}] "You're a nice guy, Timothy. Just be yourself and if anybody has a problem with that their opinion shouldn't matter to you."
			~SetValue("TimothyPoints", GetValue("TimothyPoints") + 2)
		-else : 
			[{player_name}] "I remember feeling the same way at my welcome dinner. It's okay be to nervous. It'll pass soon."
			~SetValue("TimothyPoints", GetValue("TimothyPoints") + 1)
	}
*[Leave it to Trissa]
	[Trissa] "I get it. Meeting new people can be hard, but we're all nice!"
	[Trissa] "You're talking to me, aren't ya?"
	[Timothy] "Uh, yeah?"
	[Trissa] "If you can handle <i>my</i> energy, you can handle anything!"
	[Timothy] "Thanks, Trissa."
-Timothy looks grateful. # Timothy = Happy
Was I any different from him when I first got here?
On that note, am I any better now?
<color=color_descriptor><i>Reflection has <color=color_awareness>improved <b>Awareness</b> faintly<color=color_descriptor>.</i></color> # Awareness+
I am driven from my thoughts when Max shows up with a plate of food. # Max = Calm, Stage_Left, Right
[Max] "Hey, gang!"
I give them a nod of acknowledgment.
[Trissa] "Hi, Max!"
[Timothy] "Hey."
[Max] "I can't wait to talk about your first day, Timothy, but first we've gotta deal with the formalities. One sec."
Max stands up and clears his throat. # Trissa = Exit # Timothy = Exit # Max = Stage_Center
->Toast

===NearEduardo===
I guess I must be a bit late since the line for food isn't all that long. I grab a plate and head for Eduardo and Isaac's booth.
As I approach, the two are giggling about something. # Eduardo = Happy, Stage_Left, Right # Isaac = Happy, Stage_Right, Left
[{player_name}] "Mind if I join you?"
{depression > 40: [Voices] "Of course they mind. Why would anyone want <i>you</i> around?"}
Isaac is too busy cracking up to respond, but Eduardo is able to compose himself. Briefly.
[Eduardo] "Nah, {player_gender == "F": girl| man}. Get in here!"
[Eduardo] "I was just telling meu bem Isaac here the story about <i>my</i> first day. I could have sworn I already told it to him, though." // meu bem means `my love`. "My `meu bem` would be "my my love"
[Isaac] "You did. Wanted to hear it again." # Eduardo = Surprised
[Eduardo] "You sneaky devil, you. Taking advantage of my poor memory, will you? TWO can play at that game."
Eduardo leans over to me, the most devilish smirk widening across his face. #Eduardo = close, Calm
[Eduardo] "Hey, {player_name}, you ever heard the story of me and Isaac's first day rooming together?"
[Isaac] "Hrm!" # Isaac = Surprised
[Eduardo] "It was only a couple of weeks before you arrived..."
I spend a few minutes listening to Eduardo tell the tale. #Eduardo = center
Eduardo really is a masterful storyteller. His raw charisma is inspiring.
<color=color_descriptor><i>Inspiration has <color=color_expression>increased <b>Expression</b> faintly<color=color_descriptor>.</i></color> # Expression+
[Eduardo] "And you know what the most embarrassing part was?"
Isaac looks deathly pale. # Isaac = Afraid
As if on cue, Max makes a loud sound from the center of the room and rises to their feet.
Isaac breathes a heavy sigh of relief. #Isaac = Calm
[Eduardo] "Oh, you lucky man."
"Next time {player_name}, I'll tell ya the rest..." # Eduardo = Exit # Isaac = Exit
->Toast

===NearCharlotte===
I duck past the line to where Charlotte stands behind the serving area, busily refilling several trays at once.
[{player_name}] "Hey, Charlotte. Is there uh... something I can help with maybe?" # Charlotte = Calm
[Charlotte] "I'm sure I could find a use for you."
{depression > 40: [Voices] "She'd have to be a miracle worker to do <i>that</i>."}
[Charlotte] "Heavens, that was curt of me. Allow me to try that again." # Charlotte = Sad
[Charlotte] "That is very generous of you. I can't think of anything at the moment, but..."
[Charlotte] "On second thought, there actually is a task for which I could benefit from your assistance. Or, more accurately, Max could."
[Max] "Say what now? Did I hear my name?" # Max = Calm
[Charlotte] "It is my understanding that you're about to make the preparations for Timothy's toast. Is that correct?"
[Max] "Heh. Yeah, that's right."
[Charlotte] "Would {player_name} make for a suitable second to perform your duties while you are thusly occupied?"
[Max] "Uh. Yeah, {player_name} could totally cover for me. I'd actually really appreciate that!"
[Charlotte] "It's settled, then."
[Max] "Thanks a bunch, {player_name}, I owe you one!"
Max hustles toward the kitchen and out of sight. # Max = Exit
I spend the next few minutes dishing up food and replacing trays. It's nice to see how happy people look to get a good meal.
Charlotte serves each portion with a smile. Her graceful demeanor is inspiring.
<color=color_descriptor><i>Inspiration has <color=color_grace>improved <b>Grace</b> faintly<color=color_descriptor>.</i></color> # Grace+
Max returns with a plate of their own. # Max = Calm
[Max] "Everything's set. Hit me up with some grub, {player_name}! Oh, and you're good to eat with us as soon as I've done the toast."
The moment I finish dishing them up a sizable portion, Max takes off towards the center table. # Charlotte = Exit
->Toast

===Toast===
[Max] "Can I get everyone's attention!" #Max=happy
The whole cafe quiets a little, as Max speaks up.   # Play : Stop_All   # Ambience : play_ambience_fireplace_far_birds_day
[Max] "Thanks everybody for showing yourselves!"
[Max] "Today we're welcoming our newest family member, Timothy Miyuri."
[Max] "Now, some of you have already talked with him, but for those that haven't, he's dis guy right here."
[Max] "He just got out of Blackwell Hospital."
[Max] "And he said he's not really up to this kind of group speaking, which we all can understand."
[Max] "But we all want to wish him the best of luck."
[Max] "And to welcome him to our home."
[Everyone] "Welcome Timothy!"
Max sits back down near Timothy, and the usual chatter of the room resumes.   #Max=Calm   # Play : play_music_cafejazz   # Ambience : play_ambience_crowd
{
	- ByTimothy == false:
		I get up and move closer to them and Timothy, given Trissa seems to have up and moved.
}
[Max] "Eeeh, {player_name}, just the <>
{
	-player_gender == "M": 
		man 
	-player_gender == "F": 
		gal
	-else: 
		person
} 
<> I was lookin' for."
{
	//-GetValue("Tutorial"):
	-week == 1:
		->TutorialTalk
	-else:
		->GeneralTalk
}

===TutorialTalk===
[Max] "I wanna thank ya for showing Timothy the ropes for me."
[Max] "I'm real sorry about that."
//make a choice here maybe?
[{player_name}] "No problem."
{
	-week > 1:
		After all, technically, I chose this for myself this week. which reminds me, despite already know the answer, I ask-
}
[{player_name}] "Did you ever find your keys?"
[Max] "Oh! yeah... I did."
[Max] "The little sucker fell between the couch cushions in the commons. Must've been loose on my chain or somethin'."
{
	-GetValue("RoomKey"):
		I briefly check my pocket again, and feel the room key I picked off the key ring. This should be helpful later in the week.
}
->TalkingToTimothy

===GeneralTalk===
[Max] "I wanna again, apologies for the whole roommate debacle."
[Max] "I know you don't want another thing on your plate for this week."
You have no idea.
[Max] "Also, thanks with the save with the keys."
[{player_name}] "no prob."
[Max] "How'd you even know they were there?" #Skip
+[Lucky Guess]
	[{player_name}] "lucky guess."
	[Max]"Well that's some fine luck ya got." 
+[I saw it in a dream]
	[{player_name}] "I was having one of those 'deja vu' things? you know, where you feel you saw all this before in a dream."
	[Max] "Well, that was some real accurate dreaming you got there."
->TalkingToTimothy

===TalkingToTimothy===
[{player_name}] "Hey, Timothy. How's the dinner?" #Timothy = Calm
[Timothy] "It's nice. Uh, sorry again for taking the other bed. I'm sure you were enjoying the extra space."
{
		-awareness > 1: 
			[{player_name}] "Actually, I think it'll be good for me to have some company during my last week."
			[Timothy] "Oh. Cool." # Timothy = Surprised
		-else : 
			[{player_name}] "Oh, please. I feel worse for you. You've gotta share a room with <i>me</i>."
			Timothy chuckles softly. # Timothy = Happy
	}
We spend the next hour or so talking about our interests and such.
It's mostly small talk, but Timothy relaxes more the longer we chat.
Some others join in and in no time a few hours have passed. # Time % 3
When it's finally time to head to bed, he seems almost comfortable. #All=Exit
-> END