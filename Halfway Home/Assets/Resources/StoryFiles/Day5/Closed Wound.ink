/******************************************************************************/
/*
@file   Closed Wound.ink
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
VAR HoursSpent = 0
VAR firsttime = false
VAR readLetter = false
VAR chickenedOut = false

EXTERNAL GetStringValue(value)
EXTERNAL GetValue(value)
EXTERNAL GetIntValue(value)
EXTERNAL SetValue(ValueName, newValue)
EXTERNAL SetTimeBlock(int)

# Play : Play_music_placeholder_main_fadein # music_vol ! -11

-> Start

=== Start ===
I head to my door, nervously thumbing the key I took from Max's key ring days ago. # Background / Commons
{
	-TURNS_SINCE(-> Unlocked)==-1: // TURNS_SINCE tells how many knots have been diverted since going to a particular knot, -1 means you've never been there
		~firsttime = true
		->Unlocked.First
	-else: 
		~firsttime = false
		-> Unlocked.Again
}

=== Unlocked ===
= First
I stand before the door.
I don't know why Timothy locked himself in, but I am certain that it's the reason behind his future breakdown.
And maybe if I can help stop that...
I can finally escape this dream and leave this place.
->Unlocked.Enter

= Again
That damn letter is the root of so much pain.
{awareness==5:
	I will help him as many times as he needs...
	To see that he is loved.
	To see that he is strong.
-else:
	I wasn't able to help you last time...
	But maybe...
}
-> Unlocked.Enter

= Enter
I slide the key into the lock and the door swings open.
I step inside, gently nudging the door closed behind me. # Background / YourRoom, blackwipe
-> ClosedWound

===ClosedWound===
Timothy is curled up on his bed {firsttime:facing away from the door.|, just like before.}
An open letter rests on the pillow beside a torn envelope.
Timothy is trembling slightly, but doesn't otherwise react to my entrance.
~temp lookLetter = false
->InitialChoice
=InitialChoice
+{not LookAtLetter}[Look at the letter]
    ->LookAtLetter
+[Get Timothy's attention]->GetAttention
+[Go get Max] ->GetMax // TODO: Add section in prior Timothy scene explaining why this is not a good idea

=LookAtLetter
{readLetter == false:
	From this distance I can't make out what it says, but it looks hand-written.
	Several dark splotches litter the paper and its sides appear slightly crumpled, as if once tightly clenched.
	There's no doubt whatever's in that letter is responsible for Timothy's current state.
-else:
	It's the letter Timothy's parents sent him.
	I'm sure they thought of it as a nice gesture, but all it's really done is mess everything up.
	There are splotches of tears on it and crumpled edges from when Timothy was reading it.
}
->InitialChoice

=GetAttention
{
-firsttime == true:
	Getting Max would probably just force Timothy to pretend to be more okay than he actually is.
-chickenedOut:
	I'm not making the same mistake as last time. Even if I don't think I can, I have to help him.
	Not Max. Me.
-else:
	Everything is in place. I've got to break through to him.
}
{grace<2:
	[{player_name}] "Uh, hey. There." # grace ^ poor
	Wow, great start.
-else:
	[{player_name}] "Sorry for intruding, but you look like you could use some company." # grace ^ good
}
Timothy shoots upright and looks at me, surprised. # Timothy = Afraid
His red-eyed face is ghostly pale.
[Timothy] "O-oh! Hey, {player_name}." # Timothy = Surprised
"<size=80%>Y-you startled me...<size=100%>" # Timothy = Calm
[{player_name}] "I wasn't trying too... I did knock!"
"Wait, no I didn't. <size=60%>That was another time...<size=100%>"
"Apologies. Guess I'm all turned around today."
[Timothy] "Heh. I can relate." # Timothy = Happy
[{player_name}] "So... you doin' okay in here?"
[Timothy] "Oh. Yeah, I..." # Timothy = Surprised
{GetIntValue("TimothyPoints")>5:
	Timothy sighs. # Timothy = Sad
	"I'm trying not to think about it."
	"It's just this letter... I- I was doing so well!" # Timothy = Angry
	I straighten my back and summon all the confidence I can muster.
	{expression>2:
		[{player_name}] "Timothy, you need to be honest with me. What was in that letter?" # expression ^ good
		Timothy nervously scoops the letter up from his pillow. # Timothy = Afraid
		[Timothy] "But...!"
		Timothy sighs again. # Timothy = Sad
		"You've been nothing but nice to me this whole time."
		"But I can't tell you."
		I really thought I would be able to break through to him.
		Slowly, Timothy lowers his head and holds up the letter in his outstretched hand.
		"So... It'd be better if you just read it for yourself."
		I try to hide my surprise as I casually take the letter from Timothy.
		->TheLetter
	-else:
		[{player_name}] "Timothy, I- You should... Don't you think you should talk to someone about this?" # expression ^ poor
		[Timothy] "I can't. I really can't." # Timothy = Afraid
		"I don't even want to <i>think</i> about it."
		"I'm sorry, {player_name}. I'm just a huge disappointment to everyone. Even you." # Timothy = Sad
	    ->Tangents
	}
-else:
	(You have {GetIntValue("TimothyPoints")} Timothy Points)
	"I'm fine. You don't need to worry about me." # Timothy = Calm
	->Tangents
}

=TheLetter
The letter is penned in a neat and steady hand, in contrast to the recently disheveled state of the paper.
<color=FF8A2D><i>Dear Timothy,</i></color> # override.1
<color=FF8A2D><i>It's so cute that Sunflower House has a no-email policy.</i></color>
<color=FF8A2D><i>We have been informed that you are adjusting well and we are happy to hear it. Don't get too comfortable, though!</i></color>
<color=FF8A2D><i>Your father and I are doing all we can to pull strings like we did at Blackwell.</i></color>
<color=FF8A2D><i>We spoke with one of administrators and she said that, assuming all goes well, you could be back home with us within the month!</i></color>
<color=FF8A2D><i>I'm sure we would all love nothing more than to put this dark chapter behind us. </i></color>
<color=FF8A2D><i>After all, college application deadlines are closing in and your father still thinks it would be good for you to get a summer job.</i></color>
<color=FF8A2D><i>Love, Mom & Dad</i></color>
Back to normal # override.0

// This gives you insight and a special awareness star as you relate your life to Timothy's
{awareness==5:->Healing|->NotYet}

=Tangents
Boy does <i>that</i> tone sound familiar.
I gotta do something to take his mind off of all this.
Quick, {player_name}, change the subject!
+[Talk about the other residents]
+[Talk about games]
+[Talk about the weather]
// talk about something random to take his mind off things
{awareness==5:->Healing|->NotYet}

=Healing
// You open up completely to Timothy
// This is also when the player gets to know who Sam really is, what the voices are, etc.
// Sam tells Timothy the story of how he created the voices and when the nightmare  began.
// Timothy is in awe of your self awareness and finally feels like he has a friend he can trust
// Timothy begins to believe in himself, success -> Timothy is ready for Dye Job
-> END // 1 of 3 (GOOD)

=NotYet
// You don't have enough awareness to open up to Timothy completely
// He doesn't fully trust you or believe in himself, and will break down on the last day
->END // 2 of 3 (BAD)

=GetMax
~chickenedOut = true
If I tried to help him, I'd probably just screw things up. It would be better to get Max.
They're paid to deal with this stuff, not to mention trained.
I head back out into the commons. # Background / Commons
Max is one of the easier people to spot in a crowd.
They're over in the corner chatting with one of the second floor R.A.s # Max = Happy
Immediately upon seeing me making my way over to them, they nod to the other R.A. and approach me.
Max really is a good R.A., despite how in-your-face they can be.
[Max] "What's up, {player_name}? There somethin' I can help with?"
[{player_name}] "It's Timothy. He's not doing so well. I think maybe the letter he got upset him."
[Max] "Oh no!" # Max = Afraid
"Hmmm..." # Max = Sad
"I'm sure if we work together we can cheer him up! Come on!" # Max = Happy
Max tears through the crowd and I follow through the slipstream. # Max = exit
When we arrive, Timothy is no longer curled up, but sitting hunched over at the edge of his bed. # Background / YourRoom
[Max] "Knock knock!" # Max = Calm, right
Timothy stands and turns around, his face looking somewhat pale. # Timothy = Surprised, left
[Timothy] "Oh, hi guys."
His voice cracks a little as he speaks. He clears his throat softly.
[Max] "{player_name} said you might be a little down."
[Timothy] "{player_gender=="M":He|{player_gender=="F":She|They}} did?" # Timothy = Afraid
Oh man, outed immediately.
[Max] "I thought I'd come and try out some of my new puns." # Max = Happy
[Timothy] "W-wait, puns? # Timothy = Afraid
Oh god, this just went from bad to worse.
Max clears their throat. # Timothy = Calm
"I wasn't planning on being an R.A. after leaving Sunflower House, ya know."
"I wanted to be a doctor, but I didn't have the <i>patients</i> for it."
Timothy visibly winces. Max really can't read a room, huh?
"Then I tried to be a baker, but I couldn't raise the <i>dough</i>."
Please. Bring me death.
"Oh! And then I switched to juggling, but I didn't have the-" # Skip
"Actually, that one might be a bit too risque..." # Max = Surprised
I swear I've heard these somewhere. Pretty sure these aren't original. # Timothy = Angry
[Timothy] "R-really, Max, I'm fine." # Timothy = Happy
<size=60%>"Please stop."
Timothy still doesn't look fine.
{grace>2:
	He's pretending to be okay because he feels pressured. # grace ^ good
-else:
	But maybe bad puns were just the distraction he needed... Maybe. # grace ^ poor
}
[Max] "Okay, okay!" # Max = Happy
"Look, nobody falls in love with this place in their first week." # Max = Calm
"Well, okay, nobody but Trissa Waters." # Max = Happy
"This all takes getting used to." # Max = Calm
"The other residents and I will be here for you until you do, okay?"
[Timothy] "Yeah. Thanks."
Nobody has a reponse. An awkward silence fills the void.
[Max] "Well, I guess I should give you some space, huh?" # Max = Happy
"Catch you two later!"
I get the distinct feeling I messed up bringing Max into this. # Max = Exit
Timothy seems to have withdrawn even further. # Timothy = Sad
At least I'm pretty sure I'll have another chance.
->END // 3 of 3 (BAD)