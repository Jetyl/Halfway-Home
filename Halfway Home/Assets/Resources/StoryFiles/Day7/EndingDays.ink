/******************************************************************************/
/*
@file   EndingDays.ink
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
VAR TalkC = true
VAR TalkE = true
VAR TalkI = true
VAR TalkT = true

EXTERNAL SetPlayerGender(gender)
EXTERNAL GetPlayerName()
EXTERNAL GetStringValue(name)
EXTERNAL SetStringValue(name, string)
EXTERNAL SetIntValue(name, int)

EXTERNAL GetIntValue(value)
EXTERNAL GetValue(value)
EXTERNAL SetValue(name, values)

-> Start

=== Start ===
I am woken up by an all too familar knock. # SFX : play_sfx_human_knock #Background / YourRoom, eyeopen
I let out a small resigned sigh, as I get out of bed and answer the door.
but when I open the door, Max is alone. #Max = Happy
[Max] "Hella Yella Morning Fella!"
[{player_name}] "hey Max. Where's Timothy?"
Max stares at me like I'm a weirdo. #Max = Sad
{
	-GetValue("Saved Timothy"):
		[Max] "Uh, he's right there, in bed?" #Max = Happy
		Timothy gets out of bed.
	-else:
		[Max] "He, um, left, remember?"

}
->Ready

===Ready===
[Max] "Anyways, you ready to leave?"
you say yes
you head out the commons #Background / commons
There, you see some of your friends all hanging out.
->Mingling

=Mingling
I should talk to...
+{TalkC}[Charlotte]
	->CharlotteEnd
+{TalkE}[Eduardo]
	->EduardoEnd
+{TalkI}[Isaac]
	->IsaacEnd
+{TalkT}[Trissa]
	->TrissaEnd
+[I am ready to leave]
	->OutsideWorld

===CharlotteEnd===
~TalkC = false
I talk to Charlotte. One of three basic conversations occur.
first, if you told Charlotte what your favorite kind of book you like, she will give one to you.
then, one of 3 outcomes.

=Polite
Charoltte is the same as ever, and you didn't really get close to her.
->Ready.Mingling

=Friendly
Charoltte and you are friendly, but Charlotte is staying here.
->Ready.Mingling

=Leaving
Charoltte informs you that she is leaving too.
->Ready.Mingling

===EduardoEnd===
~TalkE = false
I talk to Eduardo. he has 2 basic states, based on if Isaac broke up with him.

=BrokeUp
Eduardo is basically super depressed that Isaac broke up with him.
->Ready.Mingling

=Patches
he is kinda down because of the talk they had the other day, but happy that you helped him.
Additionally, Isaac may come over, having opened up to you.
also, if you had told Eduardo Isaac's secret, it gets revealed here.
->Ready.Mingling

=Psychic
helped eduardo, but didn't get Isaac to open up. Isaac comes over, and is suprised you knew so much about him.
->Ready.Mingling

===TrissaEnd===
~TalkT = false
I chat with Trissa. small variations, based on Charlotte's state.
She's basiaclly always the same. her life is going fine, and you have little control over it to screw it up.
she does have more to say about Charlotte if you told Trissa she was jealous of her.
in she will also have some varience to if charlotte is leaving or not.
regardless, you too say your goodbyes, and walk away.
->Ready.Mingling

===IsaacEnd===
~TalkI = false
I chat with Isaac. Mostly, this is based on if you got him to open up with you.

=StoneWall
I didn't get Isaac to open up, and they broke up.
->Ready.Mingling

=Outcome
Isaac opened up to you, but they still broke up.
->Ready.Mingling

=Thanks
Isaac opened up and the relationship survived.
->Ready.Mingling

===OutsideWorld===
You head outside, and begin putting the bags into the car. 
Max asks you some things about what your plans are after this.
multiple player choices are here.
then, roll credits
->END