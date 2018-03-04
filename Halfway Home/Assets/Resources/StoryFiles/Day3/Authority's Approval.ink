/******************************************************************************/
/*
@file   Authorty's Approval.ink
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
VAR week = 0
VAR current_room = "unset"


EXTERNAL GetValue(value)
EXTERNAL SetValue(name, values)

-> Start

=== Start ===
The player walks into the commons area.
Sees Max doing their own job.
->END