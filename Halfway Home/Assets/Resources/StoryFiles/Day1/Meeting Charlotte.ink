/******************************************************************************/
/*
@file   MeetingCharlotte.ink
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

# Load @ story_meeting_charlotte   # Play : play_music_charlotte_elegant

-> Start

=== Start ===
[{player_name}] "{Oh. Hi, Charlotte.|Hello, Charlotte.}" # Charlotte = Calm, left
{week==2:
	I don't believe it... Things are playing out exactly the same way they did last time.
}
[Charlotte] "Ah, yes. {player_name}. How do you do?"
+[Great (genuine)]
	[{player_name}] "Great. I've been feeling better recently."
	[Charlotte]	"I am glad to hear it. I’ve heard it rumored that you are to seek your fortunes outside the House soon." # Charlotte = Happy
	[{player_name}] "Yeah, end of the week. I think I'm ready."
	[Charlotte] "Yes, you’ll do very well out there. Society loves a smile, my dear."
	{grace>2:->Formal_Introduce_Timothy}
	Charlotte glances at Timothy.
	"Where are my manners? You must properly introduce this new face." # Charlotte = Surprised
+[Great (sarcastic)]
	[{player_name}] "Great. I'm still in one piece, at least."
	[Charlotte] "I see. Perhaps the rumors were a fabrication."
	"I heard that you are due to be released at week’s end… and yet your language lacks the tone of aplomb I have come to expect from those moving on to greener pastures."
	[{player_name}] "No, it's true. I am super 'due'. Guess I'm just still coming to terms with it."
	[Charlotte] "Not to worry, {player_name}. I am certain you will find your confidence by the time you leave us."
	{grace>2:->Formal_Introduce_Timothy}
	Charlotte glances at Timothy.
	"Where are my manners? You must properly introduce this new face." # Charlotte = Surprised
+{grace<2}[Great (rude)]
	[{player_name}] "Just great. Nothing beats wasting my life in a box with a bunch of fellow crazies." # grace ^ poor
	[Charlotte] "Mind your manners! I’ve heard it rumored that you are to rejoin polite society soon. Such churlish elocution will do you no favors there."" # Charlotte = Angry
	[{player_name}] "Sorry, Charlotte. I'm just frustrated is all.""
	Charlotte glances at Timothy.
	[Charlotte] "How hypocritical of me. Here I am lecturing you on manners, meanwhile failing to request an introduction to your associate." # Charlotte = Surprised
+{grace > 2}[Well, thanks (formal)<grace>]
	[{player_name}] "I am doing well, thank you. And how are you? As well as you look, I hope." # Charlotte = Surprised # grace ^ good
	[Charlotte] "When did you acquire such charm? I must say it is a special joy to find a kindred spirit with whom I may hold a proper conversation!" # Charlotte=happy {CharEnter("Charlotte", "Happy")}
	"Oh dear, that sounded terribly callous, didn’t it? Your eloquence may have caught me off-guard, {player_gender=="M":sir|{player_gender=="F":madam|friend}}." # Charlotte = Afraid
	"Please do not think I hold those without formal dialectical training in any sort of contempt."
	[{player_name}] "Of course not. # Charlotte = Calm
	->Formal_Introduce_Timothy
- -> Introduce_Timothy

=== Introduce_Timothy ===
[{player_name}] "What? Oh! Yeah, sorry."
"Charlotte, this is Timothy Miyuri, my new roommate. Timothy, this is Charlotte Blackwell. She's... uh, she lives here, too." # grace ^ poor
-> Trissa

== Formal_Introduce_Timothy ===
[{player_name}] "Before we continue, let me introduce my roommate Timothy Miyuri." 
[Charlotte] "Oh! Yes, indeed." # Charlotte = Surprised
[{player_name}] "Charlotte, this is my new roommate Timothy Miyuri."
Timothy peeks out from his hiding place in the shadows behind me. # Timothy = Calm, close, Stage_Left
[{player_name}] "Timothy, this is my fellow resident Charlotte Blackwell. She's a top-class orator. Always knows the right thing to say." # grace ^ good
[Charlotte] "You flatterer! You seem to have dipped your tongue in silver since we last spoke, {player_name}." # Charlotte = Happy
{
-week==2:
	Wait, but... she's the one who taught me this stuff.
	If this hasn't happened before, how do I know all this? But if it has, why does no one remember?!
-week>2:	
	It's weird to think that the last time she spoke with me is different from the last time I spoke with her.
}
-> Trissa

=== Trissa ===
[Timothy] "Uh... hi." # Timothy = Afraid, Stage_Left, Right
[Charlotte] "It is a pleasure to meet you, Timothy. Welcome to our home." # Charlotte = Happy
[Timothy] "Th-<delay=1>thanks." # timothy=happy # Timothy = Happy
[Charlotte] "My roommate is here, too. Doubtless she'll be overjoyed to make your acquaintance." # Charlotte = Angry
I get an uncharacteristic hint of annoyance from Charlotte as she beckons to her.
"Trissa, dear, come and meet the new arrival!" # Charlotte = Calm, Right
[Trissa] "Hiya! What's goin on, Lotty? You say somethin' about a new arrival?" # Trissa = Calm, Stage_Right, Left
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
{I hate that shirt.|I don't even really like polar bears.}
[Trissa] "Then you must be the new guy! You can call me Trissa!" # Trissa = Happy, Stage_Center # Charlotte = Stage_Right
[Timothy] "Hi. I'm... Timothy." # Timothy = Afraid
[Trissa] "No need to act so scared, my man. I don't bite! Not often, anyway..."
Timothy looks like he might not believe her.
[Trissa] "I'm joking, of course! You and me are gonna be tight compadres, wait and see."
"Just take it easy your first week. You'll find your rhythm."
[Timothy] "Ok." # Timothy = Happy
Trissa turns to look at me and lowers her voice to speak privately. # Trissa = close
[Trissa] "You take care of this one, alright? Let's make sure to give Timothy a good home."
"Anyways, I gotta run. Take care y'all!" # Trissa = center
[Charlotte] "It would be wise for me to take my leave as well." # Trissa = Exit
[Charlotte] "I am very pleased to have made your acquaintance, Timothy. It was nice to see you as well, {player_name}."
[Timothy] "Uh... where to next?" # Charlotte = Exit # Timothy = Exit
-> END