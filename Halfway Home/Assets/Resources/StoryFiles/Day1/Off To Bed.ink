/****************************************************************************/
/*
@file   OffToBed.ink
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
VAR week = 1
VAR current_room = "unset"

EXTERNAL GetValue(value)
EXTERNAL SetValue(name, values)
EXTERNAL SetTimeBlock(int)
EXTERNAL CallSleep()

-> Start

=== Start ===
I walk into my room to go to bed. 
I hop into the shower real quick. By the time I come out Timothy is already curled up in his bed.
{ 
	- week == 1:
		->ContemplateWeek1
	- week == 2:
		->ContemplateWeek2
	- else:
		->ContemplateEndlessWeeks

}

=IntoBed
I plant myself in my own bed, and squirm under the sheets.
I am <i>beat</i>.
I sit there, wavering between consciousness and unconsciousness, when I hear a voice.
[Timothy] "Hey,<delay=2> {player_name}?"
[{player_name}] "Yeah?"
{
	-GetValue("Tutorial") == true:
		-> Thanks
	- else:
		-> Start.Question	
}
=Question
[Timothy] "D-d-does it ever get easier?"
"Being here, I-I mean."
[{player_name}] "..."
+[Yes] -> Comfort
+[No] -> Cold


=== ContemplateWeek1 ===
Man, it's going to be a bit weird, having a roommate.
Eh, its only for one week.
-> Start.IntoBed

=== ContemplateWeek2 ===
what is happening... how did my week repeat like this?
Is... Is this just going to happen again when this week is over?
Why?
I look over at Timothy.
Timothy's breakdown the last day.
Could... could that have been it?
Honestly... It feels like a weak connection, but I can't think of anything else.
{
	-depression >65:
	[Voices] "You are such an ignorant fool."
}
->Start.IntoBed

=== ContemplateEndlessWeeks ===
I sigh, the quiet mundanity of my enviroment digging into the endlessness I've somehow become trapped in.
Looping weeks on end. I try to wonder on the reason why.
{
	-GetValue("SavedTimothy"):
		I look over at Timothy.
		I've got to find some way to save Timothy. That's got to break this loop.
	-else:
		I... I saved Timothy, yet...		
		why are the weeks still repeating?
}
{
	-depression >65:
	[Voices] "You are such an ignorant fool."
}
->Start.IntoBed

=== Thanks ==
[Timothy] "Th-thank you...<delay=1> for showing me around."
[{player_name}] "Oh."
"No problem."
->Start.Question

=== Comfort ===
[{player_name}] "Yeah."
"Yeah.<delay=1> It does."
[Timothy] "Th-that's good..."
->Sleep

=== Cold ===
[{player_name}] "No.<delay=1> Not really."
[Timothy] "Oh... <delay=1> okay."
{
	- grace >= 1:
		[{player_name}] "But you'll get stronger." #grace ^ good
}
->Sleep

=== Sleep ===
[Timothy] "Good night, {player_name}."
[{player_name}] "Good night, Timothy."
{
	- expression >= 1:
		"It was nice meeting you." #expression ^ good
}
I start to drift off from there, my thoughts slow and hazy. #Background / Dream , eyeclose
My dreams, if I had any, escape my memory, and the next thing I know, the sun's in my eyes. {CallSleep()} #sleep%12
I toss and turn, pained to be awake. #Background / YourRoom, eyeopen
I look over at my spare bed. Timothy's still there, breathing softly.
I guess it's his bed now.
Slumberland beckons me back to it, but I should be getting up about now.{SetTimeBlock(0)}
+[Get Up]
	 I sigh, getting out of my cloth cocoon and quickly  readying myself for the day. 
	 In no time I'm ready to head out. I'm a little more tired than I'd have liked, though. #fatigue -= 40
+[Stay in Bed]
	 I curl even deeper into lethargic bliss and welcome sweet unconsciousness again. #Background / Dream, eyeclose
	 I guess I was really tired, because I'm out like a light again in an instant.{SetValue("Slept In Day 2", true)} 
	 That loud knocking noise wakes me up again... #fatigue -= 80
-> END