/******************************************************************************/
/*
@file   Exile.ink
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
VAR delusion = 0
VAR doubt = 0
VAR week = 0
VAR current_room = "unset"

-> Start

=== Start ===
Charlotte is reading to herself in the library, sitting on the sofa.
*[Join Her] -> Sofa
*[Browse] -> Browsing

=== Sofa ===
low grace: Awkward
med grace: Polite
high grace: Pleasant
- ->Reading
=== Browsing ===
Charlotte observes you and invites you to sit down.
-> Reading

=== Reading ===
Charlotte discusses reading with you, talks preferences.
You learn that she is the one who maintains the recommendations and usually does them herself.
-> Confessions

=== Confessions ===
Charlotte admits that, while she is an avid reader, when she reads in public she does so so that she can watch people without looking suspicious.

=== Trissa ===
You can discuss her relationship with her roommate.

=== History ===
You can discuss her past.