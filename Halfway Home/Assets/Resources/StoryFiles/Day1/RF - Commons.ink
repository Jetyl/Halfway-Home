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
EXTERNAL SetValue(name, values)

-> CommonsFunction

=== CommonsFunction ===
[{player_name}] "This is the Commons!"   #Timothy = Calm
"You probably already came through here to get to our room."
[Timothy] "Yeah..."
[{player_name}] "Anyway, this is sort of a big hub area."
"The front desk is nearby, so if you need anything there or are expecting any packages, that's where you go."
"The Commons is also obviously a big social area."
Which is why I tend to avoid it...
[Timothy] "It's pretty empty right now..."
Huh. Yeah, it is. That's rare.
The only other person in sight is...   #Keyless = Calm
Max, frantically looking for their keys.
{
	-week >= 2:
		It doesn't look like Max has found them yet.
		But I know where they are. Should I... #Skip
		+[Grab them] ->FindersKeepers->RepeatedMistakes
		+[Leave Max to find them] ->RepeatedMistakes
	-else:
		->Misgender
}

=== FindersKeepers ===
I sneak away from Timothy for a brief moment to the couch Max had mentioned losing the keys in.   #Timothy = Exit   #Keyless = Exit
I fish the keys out of the couch cushion. I remove the spare copy for my room from the ring and slip it into my pocket. Finders keepers.  #5 & Success
~SetValue("RoomKey", true)
I stuff the remaining keys back where I found them and casually walk back to Timothy.
He seems to be preoccupied watching Max.   #Timothy = Calm
->->

=== Misgender ===
[Timothy] "I hope he finds his keys." #Skip #Keyless = Exit
+[<i>Their</i> Keys]
	[{player_name}] "Their keys."  #Timothy= Surprised
+[I'm sure they will]
	[{player_name}] "I'm sure they'll find them." #Timothy= Surprised
-"<speed=50%>Anyways,"
"If I'm having a bad day, I sometimes come here. Social interaction helps with my uh... <color=color_wellbeing_relief><i>depression</i></color>, but it can be kinda <color=color_wellbeing_penalty><i>stressful</i></color>."   #Timothy=Exit
"I dunno exactly what your needs are, but the Commons are a good place to go if you want to be around people."
"Ya got that, Timothy?"
"Huh?"   #Timothy =Afraid
Timothy has frozen where I had last looked and is shaking slightly.
"Uh, Timothy? You okay?"
[Timothy] "Oh! Uh I-I..."
"<jitter>S-s-sorry, I-I-I just...</jitter>"
[{player_name}] "Sorry? What for?"
The poor guy looks like a kid caught stealing cookies from the cookie jar.
[Timothy] "<jitter>I-I'm sorry I-I-</jitter>"
"<jitter>I-I'm sorry I insulted Max and mis-gendered th-them!</jitter>"
Oh! <delay=0.5>That's what this is about?
[{player_name}] "Uh, dude, it's okay."
[Timothy] "Huh?" #Timothy = Surprised
"<jitter>Y</jitter>-you mean you're <jitter>n</jitter>-not mad at me?"   #Timothy = Afraid
[{player_name}] "...Of course not."
"You said you're sorry and you didn't mean to be disrespectful or anything. It's no harm, no foul dude."
Honestly, what kind of life did this kid have where he thinks people would get mad over something like that?
I mean, yeah, some people would get mad if he refused to correct himself or made a big deal out of it or something.
But Max is the most chill person I know. They've tolerated <i><b>much</i></b> worse crap than that.
{
	-player_gender == "N":
		As for me? I'm usually too apathetic to care about slight mishaps like that.
		Again, assuming they're not being a jerk about it.
}
[Timothy] "<jitter>O-okay...</jitter>"
{
	-week == 1:
		Man, I am going to have to walk on egg shells around this kid, aren't I? #stress += 5
	-else:
		Man, if he's having a freak-out over this, it's no wonder he breaks down by the end of the week. #stress +=5
}
I give Timothy a minute to collect himself before continuing the tour.
[{player_name}] "You okay now?"
He nods.   #Timothy = Calm
[Timothy] "Y-yeah. I-I'm okay."
[{player_name}] "Cool. then lets keep going."
{
	-player_gender == "N":
		->NonBinaryBonus->Awkward
}
->Awkward
=Awkward
Leaving that awkward conversation behind us, it's time to move on!
I shuffle Timothy off to another part of the building to continue the tour.   #Timothy = Exit   #0.1 & Success
-> END

=== RepeatedMistakes ===
[Timothy] "I hope he finds his keys." #Skip #Keyless = Exit
+[Correct Timothy <(expression)>]
	[{player_name}] "Their keys." #expression+ #expression ^ good
	I instinctively correct Timothy on Max's pronouns out of habit.   #Timothy= Surprised
	As Timothy begins to retreat inward I quickly add... #Timothy = Afraid
+[Let it Slide <(grace)>]
	[{player_name}] "..." #grace+ #grace ^ good
	I decide not to correct Timothy, mostly because I don't want to set him off.
	Unfortunately, Timothy freezes up anyway, seemingly realizing the mistake on his own. #Timothy = Afraid
-"Don't worry if you make an honest mistake like that. Max'll understand."
Timothy looks at me as if I've read his mind.
[Timothy] "Y-you sure?"
I chuckle a little.
[{player_name}] "Yeah."
Honestly, I'm pretty sure Max'd be more worried as to why Timothy is having a near breakdown over this mistake than the fact they got mis-gendered.
"Timothy, I don't really know what kind of life you've had up 'til now, but you're safe here."
"It's okay to make mistakes."
[Timothy] "R-really?"
He asks as if he genuinely hasn't considered that before. @I chuckle again.
[{player_name}] "Yes."
[Timothy] "B-but what if they get mad?"
Max being genuinely angry at someone seems about as realistic as an M.C. Escher painting.
[{player_name}] "People don't get mad at slight slips of the tongue. They get made at being disrespected and invalidated."
"Words can be used to do that, but it's the intent that really matters. You, Timothy, are fine."
[Timothy] "Okay..." #Timothy = Sad
I try to word it as confidently as possible to assuage Timothy's fears. I don't know if I succeeded.
[{player_name}] "Hey, Timothy, are you okay?"
[Timothy] "...S-sorry... I'm such a mess."
[{player_name}] "Hey, don't worry about it. You're here to heal remember? You've got nothing to apologize for."
[Timothy] "T-thanks..." #Timothy = Calm
{
	-player_gender == "N":
		->NonBinaryBonus->Calming
}
->Calming
=Calming
Leaving that awkward conversation behind us, it's time to move on!
I shuffle Timothy off to another part of the building to continue the tour.   #Timothy = Exit   #0.1 & Success
-> END


===NonBinaryBonus===
[Timothy] "Oh, {player_name}?"
[{player_name}] "Yeah?"
[Timothy] "I..."   #Timothy = Happy
Timothy starts shaking, looking away from me in embarrassment, before giving me a wobbly thumbs up.
[Timothy] "<jitter>I-I th-think y-you're v-val-valid.</jitter>"
"<jitter>A-and I-I'm s-sorry if I sc-screw up, and pl-please correct me if I do.</jitter>"
[{player_name}] "Uh..."
[Timothy] "<jitter><size=120%>S-SORRY!</size></jitter>"   #Timothy = Afraid
I hold back a slight chuckle.
[{player_name}] "Thanks, I guess?"
It's reaching peak awkwardness now, but it's kinda sweet and I appreciate the gesture, as unnecessary as it is.
It does relax me a little, so that's nice. @<color=color_wellbeing_relief><i>Stress decreased slightly!</i></color> # Stress -= 10
[Timothy] "I'm sorry..."   #Timothy = Calm
->->