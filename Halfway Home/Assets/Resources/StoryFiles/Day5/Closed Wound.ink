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

// you get Timothy's attention
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
If I tried to help him, I'd probably just screw things up. It would be better to get Max.
At least they're paid to deal with this stuff, not to mention trained.

// You leave the room and go find Max in the commons
// You tell him that Timothy isn't doing so well
// You go back to Timothy with Max in tow
// Max tries to cheer up Timothy
// Timothy pretends to be okay
// Max leaves
// Timothy shares an awkward silence with you before leaving for the garden
// End of the scene, failure

->END