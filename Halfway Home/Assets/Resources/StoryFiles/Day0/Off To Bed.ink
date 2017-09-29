
VAR player_name = "tbd"
VAR player_gender = "tbd"
VAR grace = 0
VAR expression = 0
VAR awareness = 0
VAR fatigue = 0
VAR stress = 0
VAR delusion = 0
VAR doubt = 0
VAR Week = 1

EXTERNAL PlayMusic(trackName)
EXTERNAL CharEnter(nameString, poseString)
EXTERNAL CharExit(nameString)
EXTERNAL GetValue(value)
EXTERNAL SetValue(name, values)


-> Start

=== Start ===
I Walk into my room, to go to bed. 
I hope into the shower real quick, and by the time I come out, Timothy is already curled up in his bed.
//if first week
{ 
	- Week == 1:
		->ContemplateWeek1
	- else:
		->ContemplateWeek1

}

=IntoBed
I plant myself in my own bed, and sqwirm under the sheets.
I am beat.
I sit there, wavering between consiousness, and unconsiousness, when I hear a voice.
[Timothy] "hey,<delay=2> {player_name}?"
[{player_name}] "yeah?"
{
	- GetValue("Tutorial") == true:
		-> TalkWeek1
	- else:
		-> Start.Question
		
}
=Question
[Timothy] "D-d-does it ever get easier?"
"being here, I-I mean."
[{player_name}] "..."
*Yes -> Comfort
*No -> Cold


=== ContemplateWeek1 ===
man, its going to be a bit weird, having a roommate
eh, its only for one week
-> Start.IntoBed

=== TalkWeek1 ==
[Timothy] "th-th-thank you...<delay=2> for showing me around"
[{player_name}] "oh"
[{player_name}] "no problem."
->Start.Question

=== Comfort ===
[{player_name}] "yeah."
"Yeah.<delay=5> it does."
[Timothy] "th-thats good..."
->Sleep

=== Cold ===
[{player_name}] "No.<delay=2> Not really."
[Timothy] "oh... <delay=1> okay."
->Sleep

=== Sleep ===
[Timothy] "Good night {player_name}"
[{player_name}] "Good night Timothy"
{
	- expression > 10:
		"It was nice meeting you."
}
I start to drift off from there. My thoughts slow and hazy.
My dreams, if I had any, escape my memory, and the next thing I know, the suns in my eyes.
I toss and turn, pained to be awake.
I look over at my spare bed, and Timothy's still there.
I guess its his bed now.
Slumberland becons me back to it, but I should be getting up about now.
*Get Up 
	 I sigh, getting out of my cloth cocoon, and quickly get ready for the day. In no time, I'm ready to head out.
*Stay in Bed
	 I curl even deeper into lethargic bliss, and welcome the sweet unconcuiousness again.
	 {SetValue("SleptIn", true)}

- -> END