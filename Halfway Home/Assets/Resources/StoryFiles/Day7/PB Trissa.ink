/******************************************************************************/
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
VAR timesReflected = 0
VAR BreakdownReflections = 0

EXTERNAL GetStringValue(name)
EXTERNAL SetValue(name, values)
EXTERNAL GetValue(name)
EXTERNAL SetTimeBlock(int)
EXTERNAL GetTrueSocial(name)
EXTERNAL CallSleep()
EXTERNAL GetHour()

-> Start

=== Start ===

->END