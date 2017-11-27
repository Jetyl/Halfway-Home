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

-> Start

=== Start ===
I'm called to the kitchen for my farewell meal.
I walk in seeing everyone chatting, and having a good time.
Acting like everything is okay.
Trissa is chatting up Jesse and John at the center table.




===CharlottePositive===
I see Charlotte, over in her usual chair,<delay=2> relaxing, of all things.

->Toast

===CharlotteNagative===
I see Charlotte assisting in the preperations of the farewell meal.

->Toast

===EduardoPositive===
Eduardo and Isaac are in their usual corner, giggling

->Toast

===EduardoNagative===
Eduardo and Isaac are suprisingly not sitting nearby each other, but on near oppostie ends of the room.

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
#Max=Exit Max sits back down near Timothy, and the usual chatter of the room resumes.
[Max] "Eeeh, {player_name}, just the <>
{
	-player_gender == "M": 
		man 
	-player_gender == "F": 
		gal
	-else: 
		person
} 
<> I was lookin' fer."
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
scene end. #Trissa=Exit #Eduardo=Exit #Charlotte=Exit #Isaac=Exit #Max=Exit


-> END