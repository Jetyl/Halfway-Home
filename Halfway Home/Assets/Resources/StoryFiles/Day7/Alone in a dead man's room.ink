/******************************************************************************/
/*
@file   AloneInADeadMansRoom.ink
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
VAR week = 0
VAR current_room = "unset"


EXTERNAL PlayMusic(trackName)
EXTERNAL GetValue(value)
EXTERNAL SetTimeBlock(int)

# Load @ story_alone_in_a_dead_mans_room   # Play : play_music_sad

-> Start

=== Start ===
After hours of solemn talking and worrying, I slip away to my room.
Well, I suppose it's not my room anymore. not after today.
It's Timothy's Room now. But...
I'm not sure he'll ever return to claim it.
I look at my bags, half packed with the crap I call my own.
With a heavy sigh, I look away, and toss myself into my sheets. one of the last things I can call my own in this space.
I'm alone in a dead man's room, and nothing is fine.
{
	-week == 1: 
		->WeekOne 
	-else: 
		->Repeat
}


==WeekOne==
I'm leaving tomorrow...
I'm leaving tomorrow, and I don't know what to do.
I start shaking slightly, as tears bubble in my eyes.
I'm leaving tomorrow, and I don't know what to do.
I don't want to leave. I've been here too long. I don't know what I'll do when I'm gone.
I don't want to stay here, in this dead man's room. I don't want to waste away.
I don't want to leave.@ I don't want to stay.@ I don't want anything but to make this pain go away.
->Wish

==Repeat==
I failed... again.
I let Timothy down.
I let everybody down.

->Wish

==Wish==
{SetTimeBlock(0)}
~week += 1

->END