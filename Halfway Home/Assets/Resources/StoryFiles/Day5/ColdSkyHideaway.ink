/******************************************************************************/
/*
@file   ColdSkyHideaway.ink
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

EXTERNAL PlayMusic(trackName)
EXTERNAL CharEnter(nameString, poseString)
EXTERNAL CharExit(nameString)
EXTERNAL GetPlayerData()
EXTERNAL GetStringValue(value)
EXTERNAL GetValue(value)

-> Start

=== Start ===
I step out into the gardens. its rather cold out tonight.
There isn't a single star in sky tonight.
{ 
	
	-GetValue("IssacOpenedUp") == true:
		->Recap
	- GetValue("ColdTalkBefore") == true:
		->TryAgain
	-else:
		->StoneGiant
}

=== StoneGiant ===
I don't really have a reason to be strolling out here at this time, but I feel it.
Walking around the beaten path, I spot an unusual sight.
It's Issac, sitting in a little hidden patch of grass, staring up at the sky. #Issac = Sad
->Quiet

=== TryAgain ===
I go to where I know Issac is hiding, to talk to him about what is bothering him.
->Quiet


=== Quiet ===
[{player_name}] "Hey, Issac. What's up?" #Issac = Sad
[Issac] "...."
No response. not surprising
[{player_name}] "so... um, what'cha looking at?"
[Issac] "..."
"...Hm?" #Issac = Left
[{player_name}] "I don't usually see you out here."
[Issac] "..."
"I'm hiding"
+[Want to be left alone?]
	[{player_name}] "Oh! um, want to be left alone then?"
	[Issac] "..."
	"Yeah."
	[{player_name}] "Uh, okay. See you later then."
	->GivenUp
+[Want to talk about it?]
	[{player_name}] "Do you.. want to talk about it?"
	{
		-GetValue("LongNightHangoutComplete") == true:
			->PullingTeeth
		-else
			[Issac] "..."
			"Not really."
			[{player_name}] "...Okay... I'll see you around, then."
			[Issac] "..."
			"Thanks."
			->GivenUp
	}

=== PullingTeeth ===
//show Issac CG here
[Issac] "..." 
“...”
“...”
“...You...”
“...You ready...? To leave?”
+[Yeah]
	[{player_name}] "Y-yeah. I am."
	[Issac] "Hm."
	->PoorGrip
+[....]
	[{player_name}] "..."
	[Issac] "Hm."
	->PoorGrip
+{grace>2}[Whether I am or not, I’m still leaving]
	[{player_name}] "Whether I am or not, I’m still leaving."
	[Issac] "Hm... That's true. Hm..."
	->OpenAnswer
+{expression>2} [No, not really]
	[{player_name}] "No, not really"
	[Issac] "Are you scared? To leave?"
	[{player_name}] "...Yeah."
	->OpenAnswer


=PoorGrip 
[Issac] "..."
"..."
//continue here
->HelpfulListener

=OpenAnswer
[Issac] "..."
"...Does...?"
"...Does your life feel like its moving too fast for you?"
[{player_name}] "A bit, yeah."
"Do, uh, do you feel that way too?"
[Issac] "..."
After a few quiet seconds, Issac nods, slightly.
"How...?"
"How long... do you...?"
"...Think...I...I...?"
"...I've...been...here...?"
How long he's been here?
//continue here
->HelpfulListener

=== GivenUp ===
I head off, leaving Issac to stew in his hideaway.
I couldn't get him to open up.
->END

=== HelpfulListener ===
I help Issac up off the ground. //player has finished helping his issues
->END


=== Recap ===
I go to Issac's little hideaway, and talk him through his issues like last time.
->END