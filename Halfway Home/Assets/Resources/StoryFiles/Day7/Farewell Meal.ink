/******************************************************************************/
/*
@file   RF-Library.ink
@author Jesse Lozano & John Myres
@par    email: jesse.lozano@digipen.edu, john.myres@digipen.edu
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

# Play : Stop_All

-> Start

=== Start ===
I'm called to the kitchen for my farewell meal. # Ambience : play_ambience_fireplace # ambience_vol ! 0
I walk in seeing everyone chatting and having a good time.
Acting like everything is okay. // Should only happen if Timothy not helped
Trissa is chatting up Jesse and John at the center table.
I see Charlotte assisting in the preparations of the farewell meal. // TODO: Support good outcome: I see Charlotte, over in her usual chair, relaxing of all things.
Eduardo and Isaac are surprisingly not sitting nearby each other, but on near opposite ends of the room. // TODO: Support good outcome: Eduardo and Isaac are in their usual corner, giggling.
[Max] "We've got your throne all ready, sire." # Max = Happy
They're doing a good job of disguising it, but you can tell Max is only pretending to be happy.
I take my seat at the center table, remembering how, less than a week ago, Timothy had done the same.
[Max] "I'll grab you some food!"
[{player_name}] "Thanks." #Max = Exit
Eduardo is the first to come up. #Eduardo = Sad
[Eduardo] "Hey, {player_name}. It was great hangin'."
[Eduardo] "With your chill, I'm sure you'll do great out there."
Eduardo turns to leave.
+[Ask if he's okay.]
	[{player_name}] "You seem pretty down, man..."
	[Eduardo] "Sim, amigo. Just at the bottom today. Usually I've got Isaac to help around this time, but..."
	[Eduardo] "I messed up, {player_name}. I said some things I really shouldn't have and now I don't know where we even stand any more." # Eduardo = Angry
	[Eduardo] "But this isn't your problem, eh? You've got the whole world waiting for you. Come back and visit, okay?." # Eduardo = Sad
+[Let him be.]
	I decide he's probably just in his depressive state and needs some time to himself.
- Next to approach is Charlotte, who seems to have taken a break from her duties. # Eduardo = Exit
[Charlotte] "How are you finding your last day? Pleasant, I hope!" # Charlotte = Happy
+[Yeah.]
	[{player_name}] "It's fine, I guess."
	To be honest, I'm not really okay after earlier. But I figure it'd be kinder to put on a good face.
	[Charlotte] "Well, that's not quite the enthusiasm I have come to expect from outgoing residents." # Charlotte = Calm
	[Charlotte] "Perhaps I am simply too used to Trissa's unwavering excitement. She's leaving two weeks from now and it seems to be her favorite subject of conversation."
	Charlotte seems to be carrying on without any concern for Timothy's absence.
+[Not really.]
	[{player_name}] "You're kidding, right? Did you not hear about Timothy?"
	[Charlotte] "Ah, you refer to the events of earlier today involving Mr. Miyuri." # Charlotte = Calm
	[Charlotte] "These things happen from time to time. The hospital is the best place for him, I'm sure."
- How is she so unfazed?
[Charlotte] "I should return to my post, now. The line is starting to grow to an uncomfortable size. We'll speak later, I'm sure."
I watch Charlotte stride gracefully toward the serving line as Max emerges with a steaming plate of food. # Charlotte = Exit
[Max] "Sorry that took so long! Turns out the cook wasn't really ready yet." # Max = Calm
[{player_name}] "I don't mind at all."
[Max] "Thanks for being so cool about... well, everything." # Max = Happy
[Max] "Timothy might not have had the chance to thank you today, but I know he appreciated what you did for him." # Max = Sad
[Max] "Anyway, time for the toast." # Max Calm
[Max] "Ladies, gentlemen, and everyone in between, may I have your attention?"
The cafeteria's ambient babble slowly subsides into silence. Max looks over an index card before shoving it in their pocket.
[Max] "It's been a rough week for many of us, I know."
[Max] "Today we said goodbye to our newest resident, Timothy Miyuri, who I have received word is safe and comfortable at Blackwell. It wasn't what we all hoped for, but I'm sure we'll see him again when he's ready." # Max = Sad
[Max] "But today we also say farewell to one of our more established residents, {player_name}, who is finally ready to go back out into the real world!" # Max = Happy
[Max] "This toast is to both of you. May you find happiness and good fortune as you face the challenges ahead."
Max raises their cup and the room follows suit. A small applause follows the toast and the murmur of conversation returns.
A few more residents come up to say goodbye, but they're not really people I knew that well.
Today has really tired me out. I end up leaving the cafeteria early. #All = Exit
-> END