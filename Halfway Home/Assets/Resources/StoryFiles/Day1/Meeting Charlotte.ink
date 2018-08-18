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

EXTERNAL GetValue(name)

# Load @ story_meeting_charlotte   # Play : play_music_charlotte_elegant

-> Start

=== Start ===
[{player_name}] "{Oh. Hi, Charlotte.|Hello, Charlotte.}" # Charlotte = Calm, left
{week>1:
	{I don't believe it... Things are playing out exactly the same way they did last time.|Yep. Exactly the same.}
}
[Charlotte] "Ah, yes. {player_name}. How do you do?" #Skip
+[Great (genuine)]
	[{player_name}] "Great. I've been feeling better recently."
	[Charlotte]	"I am glad to hear it. I’ve heard it rumored that you are to seek your fortunes outside the House soon." # Charlotte = Happy
	[{player_name}] "Yeah, end of the week. I think I'm ready."
	[Charlotte] "Yes, you’ll do very well out there. Society loves a smile, my dear."
	{GetValue("Tutorial")==false:->NoTimothy}
	{grace>2:->Formal_Introduce_Timothy}
	Charlotte glances at Timothy.
	"Where are my manners? You must properly introduce this new face." # Charlotte = Surprised
+[Great (sarcastic)]
	[{player_name}] "Great. I'm still in one piece, at least."
	[Charlotte] "I see. Perhaps the rumors were a fabrication."
	"I heard that you are due to be released at week’s end… and yet your language lacks the tone of aplomb I have come to expect from those moving on to greener pastures."
	[{player_name}] "No, it's true. I am super 'due'. Guess I'm just still coming to terms with it."
	[Charlotte] "Not to worry, {player_name}. I am certain you will find your confidence by the time you leave us."
	{GetValue("Tutorial")==false:->NoTimothy}
	{grace>2:->Formal_Introduce_Timothy}
	Charlotte glances at Timothy.
	"Where are my manners? You must properly introduce this new face." # Charlotte = Surprised
+[Great (rude) <(grace<2)>]
	[{player_name}] "Just great. Nothing beats wasting my life in a box with a bunch of fellow crazies." # grace ^ poor
	[Charlotte] "Mind your manners! I’ve heard it rumored that you are to rejoin polite society soon. Such churlish elocution will do you no favors there." # Charlotte = Angry
	[{player_name}] "Sorry, Charlotte. I'm just frustrated is all."
	{GetValue("Tutorial")==false:->NoTimothy}
	Charlotte glances at Timothy.
	[Charlotte] "How hypocritical of me. Here I am lecturing you on manners, meanwhile failing to request an introduction to your associate." # Charlotte = Surprised
+[Well, thanks (formal)<(grace>2)>]
	[{player_name}] "I am doing well, thank you. And how are you? As well as you look, I hope." # Charlotte = Surprised # grace ^ good
	[Charlotte] "When did you acquire such charm? I must say it is a special joy to find a kindred spirit with whom I may hold a proper conversation!" # Charlotte=happy 
	"Oh dear, that sounded terribly callous, didn’t it? Your eloquence may have caught me off-guard, {player_gender=="M":sir|{player_gender=="F":madam|friend}}." # Charlotte = Afraid
	"Please do not think I hold those without formal dialectical training in any sort of contempt."
	[{player_name}] "Of course not." # Charlotte = Calm
	{GetValue("Tutorial")==false:->NoTimothy|->Formal_Introduce_Timothy}
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
[Timothy] "Uh... hi." # Timothy = Afraid, Stage_Left, Right, Close
[Charlotte] "It is a pleasure to meet you, Timothy. Welcome to our home." # Charlotte = Happy
[Timothy] "Th-<delay=1>thanks." # Timothy = Happy
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
[Trissa] "Then you must be the new guy! You can call me Trissa!" # Trissa = Happy, Stage_Center # Charlotte = Stage_Right, left
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
I can't help but be impressed by Charlotte's etiquette. I need to get better at that. # Charlotte = Exit
<color=color_descriptor>Charlotte's example has <color=grace>increased grace faintly<color=color_descriptor>.</color># grace+
[Timothy] "Uh... where to next?" # Timothy = Exit
-> END

=== NoTimothy ===
[{player_name}] "Oh yeah, have you met my new roommate Timothy yet?"
[Charlotte] "Yes, indeed I have. Not long ago, in fact, in the company of our charming resident assistant." # Charlotte = Calm
"He seems like quite a timid fellow, your roommate. You will take care of him, won't you?"
[{player_name}] "I'll do my best."
[Charlotte] "Good. Anyway, I'm always happy to show off the library to newcomers!" # Charlotte = Happy
The way she talks, you'd think she was the librarian. {GetValue("SeenEmpathy")==true:I guess she kind of is, if what I remember is real.|Wait, is she?}
"Only, they spent very little time here. I barely had time to explain the notes to Timothy before Max whisked him away."
Trissa chimes in from the corner of the room, having apparently been listening in to our conversation.
[Trissa] "Hey, I'm sure you'll have plenty of time to explain everything to him once he's settled." # Trissa = Calm, Stage_Right # Charlotte, right
"Max probably just didn't want you to stress him out with all your rules!" # Trissa = Happy
Trissa stands up straight and puts on her best impression of Max.
"<i>Welcome to Sunflower House! This is the library.</i>"
"<i>Make sure not to leave a book open face down or the spine will crack and blondie here will kill you in your sleep!</i>"
"You know, first impressions and all!"
[Charlotte] "I would never!" # Charlotte = Surprised
"Although... I can't say I haven't been upset about how <i>some</i> residents care for their books. Or rather <i>do not</i>." # Charlotte = Angry
[Trissa] "Take it easy, I'm just messin' wichu."
[Charlotte] "Ah. Of course." # Charlotte = Calm
[Trissa] "Uh-oh, {player_name}, I think I'm next on her list! I'd better get outta here."
[Charlotte] "What? Don't be ridiculous, I-" #Skip # Charlotte = Angry
Trissa strides out of the room with her arms up in a mock sprint, laughing the whole way. # Trissa = Exit
"Oh, that girl."
Charlotte exhales gently and her demeanor relaxes considerably. # Charlotte = Calm
[Charlotte] "I think I had best find something else to do. Farewell, {player_name}."
"It's nice to see you out and about for a change!" # Charlotte = Happy
She really doesn't remember, huh?
[{player_name}] "Yeah. Nice seeing you, too. Bye!"
Charlotte exits the room gracefully. # Charlotte = Exit
I don't have any reason to be in here either. I wait a minute so it doesn't seem like I'm following her and then walk out of the room.
-> END