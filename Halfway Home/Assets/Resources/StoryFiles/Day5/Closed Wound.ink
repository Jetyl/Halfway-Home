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
+[Go get Max] ->GetMax

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
{firsttime == true:
	Getting Max would probably just force Timothy to pretend to be more okay than he actually is.
-chickenedOut:
	I'm not making the same mistake as last time. Even if I don't think I can, I have to help him.
	Not Max. Me.
-else:
	I've got to break through to him. Everything is in place.
}

// you get Timothy's attention
// Timothy is startled, looks ghostly pale when he looks at you.
// Has obviously been crying, but isn't now.
->END

=GetMax
If I tried to help him, I'd probably just screw things up. It would be better to get Max.
At least they're paid to deal with this stuff, not to mention trained.
->END