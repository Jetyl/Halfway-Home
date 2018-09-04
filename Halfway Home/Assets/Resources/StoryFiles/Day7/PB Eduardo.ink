/******************************************************************************/
/*
@file   PB Eduardo.ink
@author Jesse Lozano
@par    email: jesse.lozano@digipen.edu
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

EXTERNAL GetStringValue(name)
EXTERNAL SetValue(name, values)
EXTERNAL GetValue(name)
EXTERNAL SetTimeBlock(int)
EXTERNAL GetTrueSocial(name)
EXTERNAL CallSleep()
EXTERNAL GetHour()

-> Start

=== Start ===
Eduardo comes in. he looks like he's been crying.
You ask if he's okay. He says he's fine, super really.
you comment how affected he was by timothy's breakdown.
he laughs. sure, thats it. I'm just sooo empathetic.
Sorry, {player_name}, You shouldn't have to deal with two people's breakdowns in one day.
Plus, I just want to be left alone.
You say okay, and you leave. #expression+
->END