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
EXTERNAL SetTimeBlock(int)

-> Start

=== Start ===
After a brief scare of today's events, and everyone's shock, I head into my room to rest.
I saved Timothy, and The Time loops should be broken!
//add some details based on how the player "saved" timothy
Now, All I've got to do is wait, til tomorrow comes...
Tomorrow...
Its a little weird to even imagine tomorrow coming at all.
I look at my bags, barely packed, because, well why bother. Time Loop and whatnot. but now...
Now, I should probably be getting ready for tomorrow.

->FirstRRR


==FirstRRR==
I'm leaving tomorrow...
I'm leaving tomorrow, and I don't know what to do.
I start shaking slightly, as tears bubble in my eyes.
I'm leaving tomorrow, and I don't know what to do.
I don't want to leave. I've been here too long. I don't know what I'll do when I'm gone.
I don't want to stay here, in this dead man's room. I don't want to waste away.
I don't want to leave.@ I don't want to stay.@ I don't want anything but to make this pain go away.
->RRR

==RepeatRRR==
I failed... again.
I let Timothy down.
I let everybody down.

->RRR

==RRR==
But its fine.
I broke the loop.
I saved Timothy.
Everything will be turn out okay.
I tell myself that, over and over, untill I fall asleep.
.....
.......
................
//have this in UI color
<color=111111>Progress Status... @ Primary Objective: ... FAILURE @ Secondary Objective: ... SUCCESS
<color=111111>Determining Following Action: ... COMPLETE
<color=111111>Reinintializing.... Recontructing....
<color=111111><b>R</b>ecovery <b>R</b>estart <b>R</b>equired
<color=111111>Awaiting Input....
{SetTimeBlock(0)}
~week += 1
->END