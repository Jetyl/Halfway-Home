
VAR player_name = "tbd"
VAR player_gender = "tbd"
VAR grace = 0
VAR expression = 0
VAR awareness = 0
VAR fatigue = 0
VAR stress = 0
VAR delusion = 0
VAR week = 0

VAR ByTimothy = false

EXTERNAL PlayMusic(trackName)
EXTERNAL CharEnter(nameString, poseString)
EXTERNAL CharExit(nameString)
EXTERNAL GetValue(value)

-> Start

=== Start ===
{CharEnter("Timothy", "Calm")}{CharEnter("Trissa", "Calm")}{CharEnter("Eduardo", "Calm")}{CharEnter("Charlotte", "Calm")}{CharEnter("Isaac", "Calm")}{CharEnter("Max", "Calm")}
I shuffle into the kitchen, which is much more packed than usual, with everybody showing up for the celebatory Welcome Dinner.
The Halfway Home has these big get together dinners, whenever we get someone new, or someone leaves.
its supposed to be a big bonding moment. Or, somthing like that.
I actually barely remember my welcome dinner.
Eduardo and Isaac are off in their own little corner, giggling to themselves. Charlotte and Max are setting out more of the tableware and food.
Timothy, the star of this show, gets the center seat, so he can meet everybody. Currently, he seems to be talking with Trissa.

I decide to...
*[Sit near Timothy] ->NearTimothy
*[Sit near Eduardo] ->NearEduardo
*[Help Charlotte layout the dishes] ->NearCharlotte


===NearTimothy===
~ByTimothy = true
->Toast

===NearEduardo===


->Toast

===NearCharlotte===


->Toast

===Toast===
{CharEnter("Max", "Calm")}
[Max] "Can I get everyone's attention!"
the whole cafe quites a little, as Max speaks up.
"Thanks eveybody for showing yerselfs"
"Today we're welcoming our newest family member, Timothy Miyrui."
"Now, Some of you have already talked with him, but for those, that haven't, he's dis guy right here."
"He just got out Blackwell Hospital, for those who need the context for that,"
[Jesse] "Thanks Fam!"
"And he said he's not really up to this kinda group speaking, which we all can understand."
"But we all want to wish him the best of luck"
"And to welcome him to our home."
[Everyone] "Welcome Timothy!"
{CharExit("Max")} Max sits back down near Timothy, and the usual chatter of the room resumes.
{
	- ByTimothy == false:
		I get up and move closer to them and Timothy, given Trissa seems to have up and moved.
}
[Max] "Eeeh, {player_name}, just the {-player_gender == "M": man -player_gender == "F": gal -else: person }I was lookin' fer."
{
	-GetValue("Tutorial"):
		->TutorialTalk
	-else:
		->GeneralTalk
}

===TutorialTalk===
[Max] "I wanna thank ya for showing Timothy the ropes for me."
[Max] "I'm rul sorry about that."
//make a choice here maybe?
[{player_name}] "No problem"
[{player_name}] "Did you ever find your keys?"
[Max] "Oh! yeah... I did."
[Max] "the little sucker fell between the couch cushions in the common's. musta been loose on ma chain or somethin'."

->TalkingToTimothy

===GeneralTalk===
[Max] "I wanna again, apologies for the whole roommate debacle."
[Max] "I know you don't want another thing on your plate for this week."
You have no idea.
[Max] "Also, thanks with the save with the keys."
[{player_name}] "no prob."
[Max] "how'd you even know they were there?"
//choice here maybe.
[{player_name}] "lucky guess."
[Max]"Well that's some fine luck ya got." 
->TalkingToTimothy

===TalkingToTimothy===
[{player_name}] "Hey, Timothy. how's the dinner?"


-> END