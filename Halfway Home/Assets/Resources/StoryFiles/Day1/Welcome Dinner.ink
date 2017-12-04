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

VAR ByTimothy = false

EXTERNAL GetValue(value)

-> Start

=== Start ===
I shuffle into the kitchen, which is much more packed than usual, with everybody showing up for the celebatory Welcome Dinner.
The Halfway Home has these big get together dinners whenever we get someone new, or someone leaves.
It's supposed to be a big bonding moment. Or, something like that.
I actually barely remember my welcome dinner. I think there were beans? That's about all I got.
Timothy, the star of this show, gets the center seat, so he can meet everybody. Currently, he seems to be talking with Trissa.
Eduardo and Isaac are off in their own little corner, giggling to themselves. 
Charlotte and Max are helping serve food at the front of the line.
I decide to...
*[Sit near Timothy] ->NearTimothy
*[Sit near Eduardo] ->NearEduardo
*[Offer help to Charlotte] ->NearCharlotte


===NearTimothy===
~ByTimothy = true
I guess I must be a bit late since the line for food isn't all that long. I grab a plate and head for the center table.
I pull up a chair next to Timothy and take a seat. Trissa gives me a welcoming smile. # Trissa = Happy # Timothy = Calm
[Timothy] "Hi, {player_name}."
[Trissa] "Come to see the man of hour, huh?"
[{player_name}] "Sure did."
{delusion > 40: [Voices] "As if he wants to see you."}
[Trissa] "Like I was saying. You've got nothing to worry about! Everybody here is excited to meet you!"
[Timothy] "That's kind of a lotta pressure."
*[Reassure Him]
	{
		-awareness > 1: 
			[{player_name}] "You're a nice guy, Timothy. Just be yourself and if anybody has a problem with that their opinion shouldn't matter to you."}
			// Add 2 Timothy Points
		-else : 
			[{player_name}] "I remember feeling the same way at my welcome dinner. It's okay be to nervous. It'll pass soon."
			// Add 1 Timothy Point
*[Leave it to Trissa]
	[Trissa] "I get it. Meeting new people can be hard, but we're all nice!"
	[Trissa] "You're talking to me, aren't ya?"
	[Timothy] "Uh, yeah?"
	[Trissa] "If you can handle [i]my[/i] energy, you can handle anything!"
	[Timothy] "Thanks, Trissa."
-Timothy looks grateful. He is about to reply when Max shows up with a plate of food. # Max = Calm
[Max] "Hey, gang!"
I give them a nod of acknowledgement.
[Trissa] "Hi, Max!"
[Timothy] "Hey."
[Max] "I can't wait to talk about your first day, Timothy, but first we've gotta deal with the formalities. One sec."
Max stands up and clears his throat.
->Toast

===NearEduardo===
I guess I must be a bit late since the line for food isn't all that long. I grab a plate and head for Eduardo and Isaac's booth.

->Toast

===NearCharlotte===


->Toast

===Toast===
#Max=Calm
[Max] "Can I get everyone's attention!"
the whole cafe quites a little, as Max speaks up.
[Max] "Thanks eveybody for showing yerselfs"
[Max] "Today we're welcoming our newest family member, Timothy Miyrui."
[Max] "Now, Some of you have already talked with him, but for those, that haven't, he's dis guy right here."
[Max] "He just got out Blackwell Hospital, for those who need the context for that,"
[Jesse] "Thanks Fam!"
[Max] "And he said he's not really up to this kinda group speaking, which we all can understand."
[Max] "But we all want to wish him the best of luck"
[Max] "And to welcome him to our home."
[Everyone] "Welcome Timothy!"
#Max=Calm Max sits back down near Timothy, and the usual chatter of the room resumes.
{
	- ByTimothy == false:
		I get up and move closer to them and Timothy, given Trissa seems to have up and moved.
}
[Max] "Eeeh, {player_name}, just the <>
{
	-player_gender == "M": 
		man 
	-player_gender == "F": 
		gal
	-else: 
		person
} 
<>I was lookin' fer."
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
scene end. #Trissa=Exit #Eduardo=Exit #Charlotte=Exit #Isaac=Exit #Max=Exit #Timothy=Exit


-> END