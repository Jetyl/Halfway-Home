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
I step inside, gently nudging the door closed behind me. # Background / YourRoom
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
{grace>1:
	[{player_name}] "Uh, hey. There." # grace ^ poor
	Wow, great start.
-else:
	[{player_name}] "Sorry for intruding, but you look like you could use some company." # grace ^ good
}
Timothy shoots upright and looks at me, surprised. # Timothy = Afraid
His red-eyed face is ghostly pale.
[Timothy] "O-oh! Hey, {player_name}." # Timothy = Surprised
"<size=80%>Y-you startled me..." # Timothy = Calm
[{player_name}] "<size=100%>I wasn't trying too... I did knock!"
"Wait, no I didn't. <size = 60%>That was another time..."
"<size=100%>Apologies. Guess I'm all turned around today."
[Timothy] "Heh. I can relate." # Timothy = Happy


// Timothy is startled, looks ghostly pale when he looks at you.
// Has obviously been crying, but isn't now.
// If you have enough Timothy points, Timothy will mention the letter, otherwise he will hide it
// If you have enough expression and has ever Timothy mentioned the letter, you can convince him to talk about it
// Doing so will net you a special awareness star as you relate your life to Timothy's
// Otherwise, you talk about something random to take his mind off things
// If you have 5 awareness stars, you open up completely to Timothy
// This is also when the player gets to know who Sam really is, what the voices are, etc.
// Sam tells Timothy the story of how he created the voices and when the nightmare  began.
// Timothy is in awe of your self awareness and finally feels like he has a friend he can trust
// Timothy begins to believe in himself, success -> Timothy is ready for Dye Job
->END

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
	He's pretending to be okay because he feels pressured. grace ^ good
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
->END