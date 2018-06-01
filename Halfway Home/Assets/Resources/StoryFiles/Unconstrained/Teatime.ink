/******************************************************************************/
/*
@file   Teatime.ink
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
VAR doubt = 0
VAR week = 0
VAR current_room = "unset"

-> Start

=== Start ===
Making note of the time, I head to Charlotte's room. I would feel bad if I was late.
Who knows what she would think of me?
-> KickedOut

=== KickedOut ===
You bump into Trissa on her way out of the room. She seems to be a bit grumpy at having been "kicked out of her own room", but upon seeing you realizes Charlotte must have done so for your sake.
-> SmallTalk

=== SmallTalk ===
Charlotte admits you into her room and invites you to take a seat by the window.
She has already prepared the tea.
She has a different line if you were invited here from Exile or Lessons.
She comments about the time of day. She loves the scenery around Sunflower House and always tries to take tea at this time. It relaxes her.
She asks you how you're doing.
Choose an answer, but Charlotte will know if you're lying based on your stats.
If you answer her dishonestly, she will be disappointed and won't open up. (courtesy)
{awareness>2} -> Understanding

=== Understanding ===
You try to explain to Charlotte that she's been blind to her own progress. She is ready to leave this place, not go to Blackwell as she fears.
You praise her wisdom at having realized the nature of empathy without possessing it. She has attained enlightenment already. There is nothing left for her to gain at Sunflower House.
You convince her that she is ready to leave.

=== Courtesy ===
If you don't have 3+ awareness, you are cordial and follow Charlotte's teachings well. She is pleased for your company, but laments that her disability prevents her from truly appreciating it.
She also laments that you, her student, are certain to surpass her due to her disadvantage. She asks that you remember what she gave you when you're out charming the pants off of everyone.