/******************************************************************************/
/*
@file   RF-Commons.ink
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
VAR doubt = 0
VAR week = 0
VAR current_room = "unset"

EXTERNAL PlayMusic(trackName)
EXTERNAL CharEnter(nameString, poseString)
EXTERNAL CharExit(nameString)
EXTERNAL GetPlayerData()
EXTERNAL GetStringValue(value)
EXTERNAL GetValue(value)

-> CommonsFunction

=== CommonsFunction ===
[{player_name}] "This is the Commons!" #Timothy = Calm
"You probably already came through here to get to our room."
[Timothy] "Yeah..."
[{player_name}] "Anyway, this is sort of a big hub area."
"The front desk is nearby, so if you need anything there or are expecting any packages, there's where you go."
"The Commons is also obviously a big social area."
Which is why it stresses me out.
[Timothy] "It's pretty empty right now..."
Huh. Yeah, it is. That's rare.
The only other person in sight is... #Keyless = Calm
Max, frantically looking for their keys.
{
	-week >= 2:
		It doesn't look like Max has found them yet.
		But, I know where they are. should I...
		*[Grab them] ->FindersKeepers
		*[Leave Max to find them] ->Misgender
	-else:
		->Misgender
}

=== FindersKeepers ===
I sneak away from Timothy for a brief moment to the couch Max had mentioned lossing the keys in. #Timothy = Exit #Keyless = Exit
I fish the keys out of the couch cushion and pocket them. Finders keepers after all.
I casually walk back to where Timothy is standing.
Timothy seems to be preoccupied watching Max. #Timothy = Calm
->Misgender

=== Misgender ===
[Timothy] "I hope he finds his keys." #Keyless = Exit
Oh no...
I cringe at the potential minefield before me as I reflexively correct Timothy.
[{player_name}] "Their keys."
[Timothy] "Huh?"
[{player_name}] "Max is not a guy. Their pronouns are they/them."
[Timothy] "Oh! Uh I-I..." #Timothy = Afraid
"S-s-sorry, I-I-I just..."
[{player_name}] "Timothy, just relax. It's fine."
"If you make a mistake like that all you've got to do is apologize and not make a big deal out of it."
"Trust me, the last thing you want to do is excessively draw attention to having misgendered someone by lengthily apologizing and explaining yourself."
"Just say your sorry and move on."
[Timothy] "O-okay..."
Ugh... now I feel terrible.
[{player_name}] "Sorry, for spewing an improptu rant at you."
"It's just... I care a lot about that kinda stuff, y'know?"
A lot more than Max sometimes. Max is super chill about that stuff, as long as it's a genuine mistake.
[Timothy] "It's okay. I messed up." #Timothy = Calm
{
	-player_gender == "N":
		"oh, a-a-and {player_name}?"
		[{player_name}] "Yeah?"
		[Timothy] "I-I-I.." #Timothy = Happy
		Timothy starts shaking, looking away from me in embaressment, before giving me a woobly thumbs up.
		[Timothy] "I-I Th-Think y-you're v-val-valid."
		"A-and I-I-I'm s-sorry if I-I sc-screw up, an-and pl-please correct m-me if I do."
		"S-SORRY!" #Timothy = Scared
		I hold back a slight chuckle, and simply pat Timothy on the head.
		[{player_name}] "thanks, I guess?" # Stress -= 10
		[Timothy] "I'm sorry..." #Timothy = Sad
}
Okay, this conversation has reached peak akwardness. Time to move on!
I shuffle Timothy off to another part of the building to continue the tour. #Timothy = Exit
-> END