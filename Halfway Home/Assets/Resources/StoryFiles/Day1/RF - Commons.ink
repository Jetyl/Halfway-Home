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

EXTERNAL PlayMusic(trackName)
EXTERNAL CharEnter(nameString, poseString)
EXTERNAL CharExit(nameString)
EXTERNAL GetPlayerData()
EXTERNAL GetStringValue(value)
EXTERNAL GetValue(value)

-> CommonsFunction

=== CommonsFunction ===
[{player_name}] "And here's the Commons room" #Timothy = Calm
"You probably already came thru here to get tom my room."
[Timothy] "yeah..."
[{player_name}] "but yeah, this is sort of a bit hub area."
"the front desk is nearby, so if you need anything there, or are expecting any packages, there's where you go."
"the Commons is also obviously a big social hub"
which is why it stresses me out
[Timothy] "Its pretty empty right now..."
huh, yeah, it is. thats rare.
the only other person in sight is... #Keyless = Calm
Max, frantically looking for their keys.
{
	-week >= 2:
		It doesn't look like Max has found them yet.
		But, I know where they are. should I...
		*[Grab them before Max find's them] ->FindersKeepers
		*[leave Max to find them] ->Misgender
	-else:
		->Misgender
}

=== FindersKeepers ===
I sneak away from Timothy for a breif second to the couch Max had mentioned lossing the keys in #Timothy = Exit #Keyless = Exit
I fish the keys out of the couch cushion, and pocket them. Finder's Keeper's after all.
I casually walk back to where Timothy is standing.
Timothy seems to be more preoccupied watching Max. #Timothy = Calm
->Misgender

=== Misgender ===
[Timothy] "I hope he finds his keys." #Keyless = Exit
oh no...
I cringe at the potential minefield before me, as I reflexively correct Timothy
[{player_name}] "Their keys."
[Timothy] "huh?"
[{player_name}] "Max is not a guy. Their pronouns are they/them."
[Timothy] "Oh! uh I-I..." #Timothy = Scared
"S-s-sorry, I-I-I just..."
I sigh internally
[{player_name}] "Timothy, just relax. It's fine." 
"if you make a mistake like that, all you've got to do is apologize, and not make a big deal out of it."
"trust me, the last thing you want to do is excessively draw attention to having misgendering someone by lengthily apologiezing and explaining yourself."
"Just say your sorry and move on."
[Timothy] "o-okay..."
uuugh... now I feel terrible.
[{player_name}] "Sorry, for spewing an improptu rant at you."
"It's just... I care a lot about that kinda stuff, y'know?"
a lot more than Max sometimes. Max is super chill about that stuff, as long as its a genuine mistake.
[Timothy] "it's okay." #Timothy = Calm
{
	-player_gender == "N":
		"oh, a-a-and {player_name}?"
		[{player_name}] "Yeah?"
		[Timothy] "I-I-I.." #Timothy = Happy
		Timothy starts shaking, looking away from me in embaressment, before giving me a woobly thumbs up.
		[Timothy] "I-I Th-Think y-you're v-val-valid."
		"A-and I-I-I'm s-sorry if I-I sc-screw up, an-and pl-please correct m-me if I do."
		"S-SORRY!" #Timothy = Scared
		I hold back a slight chuckle, and simply pat Timothy on the head.
		[{player_name}] "thanks, I guess?" # Stress -= 10
		[Timothy] "I'm sorry..." #Timothy = Sad
}
okay, this conversation has reached peak akwardness. Time to move on!
I shuffle Timothy off to another part of the building to continue the tour. #Timothy = Exit
-> END