﻿/******************************************************************************/
/*
@file   RoomDefaults.ink
@author John Myres
@par    email: john.myres@digipen.edu
All content © 2018 DigiPen (USA) Corporation, all rights reserved.
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
VAR week = 1
VAR current_room = "unset"
VAR currentHour = 0

-> Start

=== Start ===
I try distract myself from what just happened, but I can't keep myself from replaying the event in my mind.
{Start==1:
	->First
-else:
	->Repeated
}
=First
I close my eyes and I can still see his panicked expression. # Background / Dream, EyeClose
I can feel his grip tight on my arm.
I can hear his short, heaving breaths.
{
	- current_room == "Garden":
		Was there nothing I could have done? # Background / Garden, EyeOpen
	- current_room == "Library":
		Was there nothing I could have done? # Background / Library, EyeOpen
	- current_room == "ArtRoom":
		Was there nothing I could have done? # Background / ArtRoom, EyeOpen
}
I've heard about panick attacks, but this is the first one I've actually witnessed.
It really came out of nowhere... Did I miss something that caused this?
More importantly, is Timothy going to be okay?
->END

=Repeated
It happened again. Timothy's panick attack.
There has to be something I can do to help!
I feel so powerless... but I can't give up!
->END