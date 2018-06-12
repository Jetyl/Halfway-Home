/******************************************************************************/
/*
@file   Lessons.ink
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
VAR topicsDiscussed = 0

EXTERNAL SetValue(name, values)
EXTERNAL GetValue(name)

-> Start

=== Start ===
I arrive at the library on time to meet with Charlotte, which feels like a miracle.
She's already here, seated at the same sofa as yesterday.
Splayed out on the table in front of her are a handful of books, some opened to specific chapters and others closed and stacked neatly.
[Charlotte] "Punctuality is a good start. Thank you for meeting me here." # Charlotte = Happy
[{player_name}] "Yeah, sure."
[Charlotte] "The polite response would be to thank me for offering my time, {player_name}." # Charlotte = Calm
[{player_name}] "Shit, right. Thanks for helping me out." //is shit pg13?
[Charlotte] "You are very welcome. It's nice to have the chance to share my knowledge." # Charlotte = Happy
"Now, let's begin."
"I have distilled the many lessons I have learned about etiquette over the years into three core ideas: Principles, Protocol, and Behavior." # Charlotte = Calm
"I tried thinking of another word that starts with `P`, but, alas, not everything fits into the creative molds that we would prefer." # Charlotte = Sad
"I would like you to decide which to begin with." # Charlotte = Happy
-> Topic

=== Behavior ===
~topicsDiscussed += 1
{
	-topicsDiscussed == 1:
		[{player_name}] "Let's do Behavior first, I guess."
	-else:
		[{player_name}] "Let's do Behavior next."
}
[Charlotte] "Very well, then." # Charlotte = Calm
"We humans are perplexing creatures and it can be very difficult for us to understand one another."
"However, while we may like to think of our minds as complex and mysterious, our bodies are quite the opposite: simple and honest."
"You could spend all day talking to someone over the phone and not understand how they truly feel, and yet from seeing a single photograph you could probably guess their state with relative accuracy."
She pulls a book off the table and flips to a diagram of different facial expressions.
"Expressions, micro-expressions, their subtle counterparts, and body language can all give extremely useful information as to how someone feels and what they might be thinking."
"For someone like you, this will all come naturally. You see a man with slumped shoulders and downcast eyes and you can feel his sadness as your own."
"I have no such empathy and have instead been forced to memorize what physical responses correspond to which emotions. In so doing I have gained a far more... `academic` perspective."
"This practice has enabled me to perceive details that many others might miss."
"With training, you, too, can learn to recognize the physical language of emotion."
"Paired with your natural gift, everyone would be as an open book to you."
[{player_name}] "Cool. Sign me up."
[Charlotte] "It's so nice to have an interested pupil!" # Charlotte = Happy
"But there's more to behavior than emotion. Motivation and goals are just as important."
She pulls two more books from the stack and lays them out on the table.
"Psychology and anthropology are a must for anyone who wants to understand others."
"Learning more about people's tendencies will help you anticipate their motivations."
[{player_name}] "Have you really read all this stuff?"
[Charlotte] "Of course!"
"Though I confess my knowledge still proves inadequate more often than I would like." # Charlotte = Sad
"But with your natural talent, I'm sure you will surpass my own abilities in no time." # Charlotte = Happy
I don't know if I would consider myself `talented`, but I guess everyone must look that way to Charlotte.
We spend an hour going over the basics of psychology, anthropology, and body language. # Time % 1
<color=color_descriptor>Charlotte's tutelage has <color=color_grace>improved <b>Grace</b> moderately<color=color_descriptor>.</i></color> # Grace++
{
	-topicsDiscussed == 3:
		[Charlotte] "That's all the time I allocated for today. Do you have any final questions?"
		
	-else:
		[Charlotte] "I think we've spent enough time on this for now. How about we move on to another topic?"
}
{
	-topicsDiscussed==2:->Trissa
	-else:->Topic
}

=== Principles ===
~topicsDiscussed += 1
{
	-topicsDiscussed == 1:
		[{player_name}] "It makes the most sense to talk about the principles first, I think."
		[Charlotte] "I would very much agree."
	-else:
		[{player_name}] "How about we talk about the principles next?"
		[Charlotte] "As you wish."
}
[Charlotte] "Etiquette, at its heart, is empathy. For someone like you, that part should be simple enough." # Charlotte = Calm
"You were likely taught the `Golden Rule` as a child, yes?"
[{player_name}] "Uh, yeah. `Do unto others as you would have them do unto you.`"
Usually when I heard that it meant I had just screwed up.
[Charlotte] "Indeed. That rule, in some form, is present in cultures all over the world."
[{player_name}] "I think I remember reading that somewhere."
[Charlotte] "All courtesy stems from this consideration for others. We create the rules of etiquette to serve this core purpose."
"We have built social systems to reward those who act with virtue and punish those who do not."
"Learning the exact rules is useful and you should strive to do so, but not nearly as important as understanding why they exist."
[{player_name}] "That makes sense."
She retrieves several smaller books from the pile on the table and sets them within easy reach.
[Charlotte] "The books suggest working to consider things from the point of view of another."
"I struggle a great deal with that part, but I find it to be an enormously helpful practice." # Charlotte = Sad
"Why don't we go over some best practices?" # Charlotte = Calm
We spend the next hour talking about different tips and tricks for changing your perspective. # Time % 1
<color=color_descriptor>Charlotte's tutelage has <color=color_grace>improved <b>Grace</b> moderately<color=color_descriptor>.</i></color> # Grace++
{
	-topicsDiscussed == 3:
		"That's all the time I allocated for today. Do you have any final questions?"
		
	-else:
		[Charlotte] "Let's move on."
}
{
	-topicsDiscussed==2:->Trissa
	-else:->Topic
}

=== Protocol ===
~topicsDiscussed += 1
{
	-topicsDiscussed == 1:
		[{player_name}] "How about we talk about protocol first."
		[Charlotte] "Straight into the meat of it, I see."
	-else:
		[{player_name}] "Tell me about protocol."
		[Charlotte] "A large topic, but I will do my best to cover it efficiently."
}
[Charlotte] "When I refer to protocol, I refer to the set of social rules by which a given group operates." # Charlotte = Calm
[{player_name}] "Like `Don't chew with your mouth open` and stuff?"
My mom used to nail me for that one when I was little.
[Charlotte] "Exactly. I imagine most groups share that particular rule."
"The most significant `rookie mistake` is to treat etiquette as a monolith, when in actuality protocol shifts between different environments."
"In the early years of my own etiquette training, I would chastise others for disregarding rules that I believed to be protocol."
"In reality, I was trying to enforce the protocol of a different group and it was <i>I</i> who was behaving improperly." # Charlotte = Happy
Charlotte laughs a little too hard at that.
"You could spend a lifetime learning all the details of a particular group's protocol, but still fail because you did not learn to <i>adapt</i>." # Charlotte = Calm
"Today I would like to spend time discussing how to identify when social rules change as well as methods for learning rules quickly and politely."
I had no idea Charlotte knew so much about this stuff. I just thought she read a lot and didn't get out much.
We spend an hour talking about different tactics to adapting to new social rules and whatnot. # Time % 1
<color=color_descriptor>Charlotte's tutelage has <color=color_grace>improved <b>Grace</b> moderately<color=color_descriptor>.</i></color> # Grace++
{
	-topicsDiscussed == 3:
		[Charlotte] "That's all the time I allocated for today. Do you have any final questions?"
		
	-else:
		[Charlotte] "I think you grasp the important concepts. Shall we continue on to the next topic?"
}
{
	-topicsDiscussed==2:->Trissa
	-else:->Topic
}

=== Topic ===
*[Principles] -> Principles
*[Protocol] -> Protocol
*[Behavior] -> Behavior
*{topicsDiscussed>0}[Ask Why She Cares] -> Motivation
*{topicsDiscussed == 3}[Thank Her For Her Time] -> Farewell

=== Trissa ===
Before Charlotte can continue, the door to the library swings open with a cheerful creak. # Charlotte = Calm, stage_Right
In strides Trissa, looking as laid back as ever. # Trissa = Calm, stage_Left, Right
[Trissa] "Yo, Lotty. I'm about to head to the store if you-"
"Oh, hey {player_name}! Are you two having a party in here?" # Trissa = Happy
"And you didn't think to invite your bestie Trissa?" # Trissa = Angry
[Charlotte] "It wasn't anything of the sort. We were simply-" # Charlotte = Surprised
[Trissa] "I'm just messin' whichu!" # Trissa = Happy # Charlotte = Calm
[Charlotte] "I was just sharing some of what I've learned with {player_name} here."
[Trissa] "That's so nice! Sorry to interrupt you guys. I just wanted to see if you needed anything from the store."
[Charlotte] "I am flattered by your thoughtfulness." # Charlotte = Angry
[Trissa] "Oh, it's no big deal. Just lookin' out for my roomie."
[Charlotte] "I couldn't possibly ask you to burden yourself on my account."
[Trissa] "What? It's not like I'm offering to roll a boulder up a hill for you, girl." # Trissa = Surprised
"You were just tellin' me you were running outta tea." # Trissa = Calm
[Charlotte] "Just the Earl Grey."
[Trissa] "See? Let me grab you some a' that!" # Trissa = Happy
[Charlotte] "If you insist, I suppose it would be rude to object further."
[Trissa] "You? Rude? Impossible."
"One fat stack of Earl Grey coming right up."
[Charlotte] "Thank you, Trissa. You are truly a blessing."
[Trissa] "A blessing, huh? You oughtta try telling my mom that."
"Alright, I'll just leave it in the room for you."
"Good seeing you again, {player_name}. Keep it real."
Trissa strides confidently out of the room, throwing a thumbs up behind her as she exits. # Trissa = Exit
[Charlotte] "She's just... too... perfect!"
Charlotte sighs and composes herself. # Charlotte = Calm, stage_Center
"Sorry about that. Now where were we?"
"Ah, yes. You were about to choose another topic..."
->Topic

=== Motivation ===
[{player_name}] "Hey, Charlotte. Don't take this the wrong way or anything, but why do you care so much? About all this I mean."
[Charlotte] "Oh, you don't want to hear that story."
[{player_name}] "Of course I do. You've helped me a lot today and I'd like to get to know you a little better."
[Charlotte] "That's very sweet of you. It's a rather long story and one that I have some difficulty retelling, so please be patient with me."
"I understand why you might be confused. From what I understand I am rather... unique among those with my disadvantage." # Charlotte = Happy
"Unique principally in that I understand it to be a disadvantage, where many like me are content torturing small animals... or worse." # Charlotte = Sad
"It may surprise you to learn that I was not always so committed to the wellbeing of others."
"It pains me to admit it, but I was a cruel child. Arrogant, abusive, and impulsive. And my wealth served only to exacerbate my vile temperament."
"I didn't care at all what anyone thought of me. I demanded the finest things and treated with contempt all those I thought of as beneath me."
"Looking back on it, I believe I didn't care because I was unable to tell what others felt... with one exception. I developed a knack for recognizing pain."
"I was ruthless. I loved the power I felt dispensing such abuses on others. I loved being able to know that I affected them."
Wow. I had no idea Charlotte had changed so much.
"I refused to eat for a week just to see the pain and worry on my parents' faces. They only got me to stop by bribing me with my Great Aunt Viola's jewelry box."
"Goodness, how petty I was back then." # Charlotte = Angry
"After that I became somewhat obsessed with her. She had the most beautiful old things with such <i>history</i> to them." # Charlotte = Calm
"I eventually found my way into her old things up in the attic. Among them was her diary, written in her own neat hand."
"What I read in that diary changed me, {player_name}. She was like me."
"Not exactly, mind you, but she struggled with the same feeling that I did. She felt the same isolation."
"It was the first time I ever truly identified with anyone. It was utterly eye-opening."
"I read at a fevered pace."
"I read on as the only person I ever connected with cataloged her own descent into madness."
"I read on as she wrote of the abuses she suffered at the hands of the orderlies of Blackwell Asylum."
"My family's asylum." # Charlotte = Angry
"It was then that I finally understood the price of my actions. I understood how the world sees people like me." # Charlotte = Sad
"I became terrified of ending up like her. I committed to changing myself."
"What began out of fear continued out of newfound understanding, as the more I learned the clearer it became that my previous actions were wrong."
"I discovered that the best way to get what I want was to help others in turn." # Charlotte = Calm
"Even though I still feel nothing directly, I know that every positive action makes life better for all."
"The `Golden Rule` was never common sense to me, but to this day I hold it sacrosanct." # Charlotte = Happy
"To bring a very long explanation to a close, that is why I care, {player_name}."
[{player_name}] "Wow. You uh... really opened up."
"That's really amazing, Charlotte. It seems like you've grown a lot."
{
	-topicsDiscussed==3:->Farewell
	-else:
	[Charlotte] "Thank you. Now where were we?"
	->Topic
}

=== Farewell ===
[{player_name}] "Thanks for taking the time to show me all this stuff, Charlotte."
{-depression>40:[Voices] "You should have told her not to waste her time in the first place."}
[Charlotte] "It was my pleasure, {player_name}. I hope this information serves you well in the trials to come." # Charlotte = Happy
{
	-grace>2:
		[{player_name}] "I assure you, the pleasure was all mine. If nothing else, I am glad for your company."
		[Charlotte] "My you <i>have</i> learned quickly, haven't you? You seem like an entirely different person this week."
		"I daresay I'm quite proud."
		"Would you do me the kindness of joining me for tea tomorrow? It would be an excellent opportunity to test your skills."
		+[Yes] 
			[{player_name}] "I'd love to. Where and when?"
			[Charlotte] "Three in the afternoon tomorrow in my room. I'll have everything ready. Simply knock when you arrive." {SetValue("ReadyForTea", 2)}
			[{player_name}] "I look forward to it."
		+[No]
			[{player_name}] "I'd love to, but I can't."
			[Charlotte] "I see." # Charlotte = Sad
	-else:
		I stand awkwardly for a few moments before Charlotte breaks the silence.
		[Charlotte] "Make sure to keep studying, {player_name}. I'm sure all of this will be second-nature to you soon enough."
}
"Now then, I should return to my routine. Have a wonderful evening." # Charlotte = Calm
Charlotte gracefully exits the room. # Charlotte = Exit
I look over the books on the table one more time before heading out myself.
-> END