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

EXTERNAL GetStringValue(value)
EXTERNAL GetValue(value)
EXTERNAL GetIntValue(value)
EXTERNAL SetValue(ValueName, newValue)
EXTERNAL SetTimeBlock(int)

-> Start

=== Start ===
I head to my room, nervously thumbing the key I took from Max's key ring days ago. # Background / Commons
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
I step inside, gently nudging the door closed behind me. # Background / Commons
Timothy is curled up on his bed {firsttime:.|, just like before.}
~SetTimeBlock(0)
-> END

===ClosedWound===
~SetValue("FoundWound", true)
You follow Timothy, who heads back to your bedroom # Background / YourRoom
He's curled up on his bed, the letter he got, opened up on his room
He looks extremely distrught.
{
	-awareness > 3:
		should you try to comfort him, or go get someone more capable?
		+[Comfort Timothy on Your Own]
			->ComfortTimothy
		+[Go Get Max]
			->PoorStitches
}
->PoorStitches

===PoorStitches===
You do not feel comfortable handling this on your own. best to get Max. Their at least paid for this.
->END

===ComfortTimothy===
Comfort Misha! :P
->END