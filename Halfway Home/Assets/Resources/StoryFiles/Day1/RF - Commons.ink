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
[{player_name}] "Their keys."
I instictively correct Timothy on Max's pronouns and move on, showing Timothy around the Commons area. #Timothy= Surprised
"If I'm having a bad day, I sometimes come here. Social interaction helps with my uh... <color=A5C5E3FF><i>depression</i></color>, but it can be kinda <color=A5C5E3FF><i>stressful</i></color>." #Timothy=Exit
"I dunno exactly what your needs are, but the Commons are a good place to go if you want to be around people."
"Ya got that, Timothy?"
"Huh?" #Timothy =Afraid
Timothy has frozen where I had last looked, and is shaking slightly.
"Uh, Timothy? you okay?"
//[{player_name}] "Max is not a guy. Their pronouns are they/them."
[Timothy] "Oh! Uh I-I..."
Timothy starts stammering to say, something.
 "S-s-sorry, I-I-I just..."
[{player_name}] "Sorry? what for?"
The kid looks like I caught him stealing cookies from a cookie jar, or something.
[Timothy] "I-I-I'm s-sorry I-I-"
"I-I'm sorry I insulted Max and misgendered th-them!"
Oh! <delay=1>That's what this is about?
[{player_name}] "Uh, dude, it's okay."
[Timothy] "Huh?" #Timothy = Surprised
"Y-you mean you're n-not mad at me?" #Timothy = Afraid
[{player_name}] "...Of course not."
"You said you're sorry, and you didn't mean to be disrespectful or anything. It's no harm, no foul dude."
Honestly, what kind of life did this kid have where he thinks people would get mad over something like that?
I mean, yeah, some people will get mad if he refused to correct himself, or make a big deal out of it or something.
And Max is the chillist person I know, and has tolerated <i><b>much</i></b> worse crap than that.
{
	-player_gender == "N":
		As for me? I'm usually too apathetic to care about slight mishaps like that.
		Again, assuming their not being a jerk about it.
}
[Timothy] "O-okay..."
{
	-week == 1:
		Man, I am going to have to walk on egg shells around this kid, aren't I?
	-else:
		Man, if he's having a freakout over this, it's no wonder he breaks down by the end of the week.
}
I give Timothy a minute to collect himself before continuing the tour.
[{player_name}] "You okay now?"
He nods #Timothy = Calm
[Timothy] "Y-yeah. I-I'm okay."
[{player_name}] "Cool. then lets keep going."
{
	-player_gender == "N":
		[Timothy]"Oh, {player_name}?"
		[{player_name}] "Yeah?"
		[Timothy] "I..." #Timothy = Happy
		Timothy starts shaking, looking away from me in embaressment, before giving me a woobly thumbs up.
		[Timothy] "I-I th-think y-you're v-val-valid."
		"A-and I-I-I'm s-sorry if I-I sc-screw up, an-and pl-please correct m-me if I do."
		[{player_name}] "Uh..."
		"S-SORRY!" #Timothy = Afraid
		I hold back a slight chuckle.
		[{player_name}] "Thanks, I guess?"
		It's reaking peak awkwardness now, but it's kinda sweet and I appeciate the gesture, as unnecessary as it is.
		It does relax me a little, so thats nice. @<color=A5C5E3FF><i>(Stress Reduced!)</i></color> # Stress -= 10
		[Timothy] "I'm sorry..." #Timothy = Calm
}
Leaving that awkard conversation behind us, its time to move on!
I shuffle Timothy off to another part of the building to continue the tour. #Timothy = Exit
-> END