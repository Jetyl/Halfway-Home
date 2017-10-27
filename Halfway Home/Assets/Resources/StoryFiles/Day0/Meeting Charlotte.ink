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

EXTERNAL GetPlayerData()

-> Start

=== Start ===
[{player_name}] "Oh, hi Charlotte." # Charlotte = Calm
[Charlotte] "Ah, yes. {player_name}. How do you do?"
*[Great (genuine)]
	[{player_name}] "Great. I've been feeling better recently."
	[Charlotte]	"I am glad to hear it. I’ve heard it rumored that you are to seek your fortunes outside the House soon." # Charlotte = Happy
	[{player_name}] "Yeah, end of the week. I think I'm ready."
	[Charlotte] "Yes, you’ll do very well out there. Society loves a smile, my dear."
	Charlotte glances at Timothy.
	"Where are my manners? You must properly introduce this new face." # Charlotte = Surprised
*[Great (sarcastic)]
	[{player_name}] "Great. I'm still in one piece, at least."
	[Charlotte] "I see. Perhaps the rumors were a fabrication."
	"I heard that you are due to be released at week’s end… and yet your language lacks the tone of aplomb I have come to expect from those moving on to greener pastures."
	[{player_name}] "No, it's true. I am super 'due'. Guess I'm just still coming to terms with it."
	[Charlotte] "Not to worry, {player_name}. I am certain you will find your confidence by the time you leave us."
	Charlotte glances at Timothy.
	"Where are my manners? You must properly introduce this new face." # Charlotte = Surprised
{
	- grace < 3:
		*[Great (rude)]
		[{player_name}] Just great. Nothing beats wasting my life in a box with a bunch of fellow crazies.
		[Charlotte] Mind your manners! I’ve heard it rumored that you are to rejoin polite society soon. Such churlish elocution will do you no favors there. # Charlotte = Angry
		[{player_name}] Sorry, Charlotte. I'm just frustrated is all.
		Charlotte glances at Timothy.
		[Charlotte] "How hypocritical of me. Here I am lecturing you on manners, meanwhile failing to request an introduction to your associate." # Charlotte = Surprised
	- grace > 3:
		*[Well, thanks (formal)]
		[{player_name}] "I am doing well, thank you. And how are you? As well as you look, I hope." # Charlotte = Surprised
		[Charlotte] "When did you acquire such charm? I must say it is a special joy to find a kindred spirit with whom I may hold a proper conversation!" # charlotte=happy {CharEnter("Charlotte", "Happy")}
		"Oh dear, that sounded terribly callous, didn’t it? Your eloquence may have caught me off-guard, { -player_gender == "M": sir-player_gender == "F": madam-else: friend}." # Charlotte = Afraid
		"Please do not think I hold those without formal dialectical training in any sort of contempt."
		[{player_name}] "Of course not. # Charlotte = Calm
}
- -> Introduce_Timothy

=== Introduce_Timothy ===
[{player_name}] "What? Oh! Yeah, sorry."
"Charlotte, this is Timothy, my new roommate. Timothy, this is Charlotte. She's... uh, she lives here, too."
[Timothy] "Uh... hi." # Timothy = Afraid
[Charlotte] "It is a pleasure to meet you, Timothy. Welcome to our home." # Charlotte = Happy
[Timothy] "Th-<delay=1>thanks." # timothy=happy # Timothy = Happy
[Charlotte] "My roommate is here, too. Trissa, dear, come and meet the new arrival!"
[Trissa] "Hiya! What's goin on, Lotty? You say something about a new arrival?" # Trissa = Calm
[Charlotte] "I did indeed. This is Timothy, {player_name}'s new roommate." # Charlotte = Calm
[Trissa] "Who?"
[{player_name}] "Me. {player_name}?"
[Trissa] "Oh, right! I remember now. You're the <>
{
	-player_gender == "M": 
		guy 
	-player_gender == "F": 
		girl
	-else: 
		one
} 
<> who's leaving this week." # Trissa = Happy
"When Max introduced you, you were wearing that shirt with the polar bear on it."
I hate that shirt.
[Trissa] "Then you must be the new guy! My name's Trissa!"
[Timothy] "Hi. I'm... Timothy." # Charlotte = Exit # Timothy = Exit # Trissa = Exit
// WIP
-> END

== Formal_Introduce_Timothy ===
[{player_name}] "Of course not. But before we continue, let me introduce my roommate Timothy." 
[Charlotte] "Oh! Yes, indeed." # charlotte=surprised # Charlotte = Surprised
Timothy peeks out from his hiding place in the shadows behind me. # Timothy = Calm
[{player_name}] "Timothy, this is my fellow resident Charlotte. She's a top-class orator. Always knows the right thing to say. # Charlotte = Happy
// more to follow, not important atm
-> END