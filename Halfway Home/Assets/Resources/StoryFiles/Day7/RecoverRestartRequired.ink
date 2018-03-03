/******************************************************************************/
/*
@file   RecoverRestartRequired.ink
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


EXTERNAL PlayMusic(trackName)
EXTERNAL GetValue(value)
EXTERNAL SetTimeBlock(int)
EXTERNAL SetValue(name, values)
EXTERNAL SetIntValue(name, string)
EXTERNAL CallSleep()

-> Start

=== Start ===
Once the Dinner has wound down, I make my leave, and head into my room to rest.
{
	-GetValue("Saved Self"):
		->PrimarySuccess
	-GetValue("Saved Timothy"):
		->SecondarySuccess
	-week == 1:
		->FirstRRR
	-else:
		->RepeatRRR //debug issue here later (weeks do not update properly)
}


===PrimarySuccess===
//player has broken timeloop, and is going to end the game
Success. fill out later
->END

===SecondarySuccess===
//player has saved timothy, but not themselves
I saved Timothy, and The time loops should be broken!
//add some details based on how the player "saved" timothy
Now, All I've got to do is wait, til tomorrow comes...
Tomorrow...
Its a little weird to even imagine tomorrow coming at all.
I look at my bags, barely packed, because, well why bother. Time Loop and whatnot. but now...
Now, I should probably be getting ready for tomorrow.

->FirstRRR


==FirstRRR==
Well, I supose its not my room anymore. not after today.
It's Timothy's Room now. but...
I'm not sure he'll ever return to claim it.
I look at my bags, half packed with the crap I call my own.
with a heavy sigh, I look away, and toss myself into my sheets. @One of the last things I can call my own in this space.
I'm alone in a room ment for no one, and nothing is fine.
I'm leaving tomorrow...
I'm leaving tomorrow, and I don't know what to do.
I start shaking slightly, as tears bubble in my eyes.
I'm leaving tomorrow, and I don't know what to do.
I don't want to leave. I've been here too long. I don't know what I'll do when I'm gone.
I don't want to stay here, in this vacant room. I don't want to waste away.
I don't want to leave.@ I don't want to stay.@ I don't want anything but to make this pain go away.
I just want the pain to go away...
I silently wish to myself, this over<delay=2> and over, until nothing else remains. #Background / Dream, eyeclose
I want the pain, to go away. {SetIntValue("week", 2)}
....{CallSleep()}
.......{SetValue("Depression Time Dilation", true)}
................{SetTimeBlock(0)}
//have this in UI color
<color=A5C5E3FF>Objectives.... Detected. @Initiating Protocol R-3 Instance 00001224671
<color=A5C5E3FF>Detecting Parameters: ... COMPLETE @Hypothizing End State: ... COMPLETE @Localizing Probablity Space: ... COMPLETE
<color=A5C5E3FF>Reinintializing.... @Recontructing.... @Resetting....
<color=A5C5E3FF><b>R</b>ecovery <b>R</b>estart <b>R</b>equired
<color=A5C5E3FF>Operation Active in: 3... @2... @1
//~week += 1
->END

==RepeatRRR==
I failed... again.
I let Timothy down.
I let everybody down.
there's an evil that is plotting
faith in man is quickly falling
cannot stop this painful clotting
It's not over. They are mocking
As my soul is slowly rotting
->RRR

==RRR==
But its fine.
I broke the loop.
I saved Timothy.
Everything will be turn out okay.
I tell myself that, over and over, untill I fall asleep.
.....
.......
................
//have this in UI color
<color=A5C5E3FF>Progress Status: ... DETECTING @ Primary Objective: ... FAILURE @ Secondary Objective: ... SUCCESS </color>
<color=A5C5E3FF>Determining Following Action: ... COMPLETE</color>
<color=A5C5E3FF>Reinintializing.... @Recontructing.... @Resetting....</color>
<color=A5C5E3FF><b>R</b>ecovery <b>R</b>estart <b>R</b>equired</color>
<color=A5C5E3FF>Awaiting Further Input....</color>
{SetTimeBlock(0)}
//~week += 1
->END