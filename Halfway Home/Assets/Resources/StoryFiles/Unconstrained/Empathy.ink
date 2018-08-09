/****************************************************************************/
/*
@file   Empathy.ink
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
VAR seenBefore = false

EXTERNAL SetValue(name, values)
EXTERNAL SetStringValue(name, string)
EXTERNAL GetValue(name)

# Load @ story_charlotte   # Play : play_music_charlotte_2

-> Start

=== Start ===
When I arrive at the library, {I discover that I am not alone|Charlotte is in the same place as {before|always}}.
{Charlotte is here, too|This must be a regular thing for her|It's a very familiar scene at this point}. She's seated on the sofa, reading to herself.
+[Join Her] -> Sofa
+[Browse] -> Browsing

=== Sofa ===
I decide to take this opportunity to spend some time with Charlotte. # Charlotte = Calm
{
	-grace==0:
		[{player_name}] "Uh... hi, Charlotte. Could I, um, sit there? I mean next to you. I mean, you don't have to move or anything." # grace ^ poor
		Jeez that was awkward. Why am I so bad at this?
		Charlotte looks up from her book and smiles.
		[Charlotte] "I would be pleased if you would, {player_name}." # Charlotte = Happy

	-grace>2: "Begging your pardon, Charlotte. Would you mind if I joined you on the sofa?" # grace ^ good
		Charlotte looks up from her book and smiles.
		[Charlotte] "Ah, {player_name}. I would be honored if you would join me." # Charlotte = Happy

	-else:
		[{player_name}] "Hey, Charlotte. Would you mind if I sat here?"
		Charlotte looks up from her book and smiles.
		[Charlotte] "I wouldn't mind at all, {player_name}. In fact, I would be glad for your company." # Charlotte = Happy
}
I take a seat on the couch beside her. # Charlotte = close
[Charlotte] "It's nice to see you in the library, {player_name}. I don't recall you coming in here much before." # Charlotte = Calm
{week==2:So she really doesn't remember anything either, huh?}
[{player_name}] "Yeah, I didn't venture out of my room very often before this week."
{week>1:Assuming you count {this weird repeat|all the repeats} as the same week.}
"Seeing as I'm leaving soon I figured I'd best try and get out more."
[Charlotte] "Well, I'm glad to see you here. Perhaps you can make use of my recommendations."
She nods toward the notes affixed to the shelves.
[{player_name}] "{Wait, you write those?|Maybe so. Thanks for taking the time to write those, by the way.}"
[Charlotte] "{Most of them.|Oh, thank you! I do enjoy it, but it would be nice to have some help from time to time.}"
->AskToSkip

=== Browsing ===
{week==1:I feel a bit awkward approaching her. We don't actually know each other that well.|{week==2:Maybe I shouldn't approach her. What if I slip up and say something that makes me sound crazy?|I'll let Charlotte take the lead.}}
I idly browse the shelves for a book, {week==1:like|trying to look} normal.
[Charlotte] "I would be pleased to make you a recommendation, if you're having trouble making up your mind." # Charlotte = Calm
{
-week==1:
	[{player_name}] "Wha- Oh, uh... it's fine. There's already recommendations, anyway."
	[Charlotte] "True, but when I write those recommendations I am unsure as to who the audience is."
	[{player_name}] "Wait, you write those?"
-week==2:
	I guess I should have figured she would notice me.
	[{player_name}] "Oh. No. That's okay."
	[Charlotte] "Are you certain? I so seldom get the opportunity to make a personal recommendation."
	[{player_name}] "Do you spend a lot of time writing those recommendations?"
	[Charlotte] "Not more than I enjoy doing so, dear, but..."
-else:
	"Oh, I wouldn't want to burden you. Don't you write all these yourself already?"
}
[Charlotte] "Usually, yes. Why don't you join me?"
*[Okay]
	[{player_name}] "Okay. I just didn't want to disturb you."
	[Charlotte] "That is very considerate of you, {player_name}, but you needn't worry about that. If I wished not to be disturbed, I would not be out in public."
	Her wording strikes me as odd, but I guess this is pretty much what constitutes 'in public' for us residents.
	[Charlotte] "Anyway..."
	-> AskToSkip
*[No, thanks]
	[{player_name}] "Uh... no, thanks. I just came here to read. Sorry."
	[Charlotte] "Of course. Apologies. Do enjoy yourself!" #Charlotte = Sad
	-> JustRead

=== AskToSkip ===
~ temp repeating = Trissa>0 && History>0
{repeating:
	<color=color_descriptor>I already know how this plays out. Skip ahead?
	+[Skip]
		{grace>2:->Invitation|->Instruction}
	+[Continue] -> Reading
-else:
	->Reading
}

=== Reading ===
[Charlotte] "Sometimes I can get another resident to recommend a favorite, but such occurrences are rarer than I would like." {SetValue("SeenEmpathy", true)} # Charlotte = Sad, close
"Perhaps you would be willing to make one yourself?" #Charlotte = Calm
[{player_name}] "Oh, uh... I don't really read enough to feel like I could."
[Charlotte] "I see." # Charlotte = Sad
"Well, if you come across a work you like before you leave and change your mind, just let me know." # Charlotte = Calm
[{player_name}] "Sure. I <i>have</i> been trying to read more."
[Charlotte] "That's nice to hear. As I said before, I would be pleased to offer you a recommendation."
"Do you generally prefer fiction or nonfiction?"
+[Fiction]
	[{player_name}] "Fiction, I guess."
	[Charlotte] "And how about genre? Fantasy? Science Fiction? Horror?"
	Charlotte gains a mischievous look. # Charlotte = Happy
	[Charlotte] "Or perhaps you prefer Romance?"
	++[Fantasy]
	[{player_name}] "I like fantasy worlds a lot, so that probably." {SetStringValue("BookGenre", "fantasy")}
	[Charlotte] "Quite a lot goes into creating such wondrous places. It's a hard genre to dislike, and an even harder genre to disrespect."
	++[Science Fiction]
	[{player_name}] "I'm kind of a sci-fi {player_gender == "M":guy|{player_gender == "F":girl|person}}." {SetStringValue("BookGenre", "sci-fi")}
	[Charlotte] "A challenging genre, to be sure. One which asks us questions we are often afraid to ask ourselves."
	++[Horror]
	[{player_name}] "I like horror books, actually." {SetStringValue("BookGenre", "horror")}
	[Charlotte] "The most human genre of all, in my humble opinion. A wonderful choice."
	++[Romance]
	[{player_name}] "So what if I do like romance novels?" {SetStringValue("BookGenre", "romance")}
	[Charlotte] "Nothing to be ashamed of! Romance is outside my typical choice of material, but I'm sure I can think of something palatable..."
+[Nonfiction]
	[{player_name}] "Nonfiction, I think."
	[Charlotte] "An uncommon preference, but one I think I share."
	"What sorts of topics do you enjoy reading about? Or perhaps you simply read a variety to learn new things?"
	++[History]
	[{player_name}] "I know most people are bored by it, but I find history really interesting." {SetStringValue("BookGenre", "history")}
	[Charlotte] "You surprise me, {player_name}."
	++[Natural Science]
	[{player_name}] "I really like nature, so I guess I like books on space and animals and stuff." {SetStringValue("BookGenre", "natural science")}
	[Charlotte] "My father loves the natural sciences as well."
	++[Creative Nonfiction]
	[{player_name}] "I like reading stories about things that really happened." {SetStringValue("BookGenre", "creative nonfiction")}
	[Charlotte] "A grounded choice. You know I think you're the first person I've encountered with such a preference."
	++[I prefer variety]
	[{player_name}] "I don't really have a favorite topic, so variety I guess. I do like learning new things, but I never thought about it that way." {SetStringValue("BookGenre", "variety")}
	[Charlotte] "{player_gender == "M":A man|{player_gender == "F" :A woman|Someone}} after my own heart." # Charlotte = Happy
-[Charlotte] "I know just the thing."
She stands up and walks over to a far shelf. # Charlotte = Exit, StageLeft
She returns holding a thick, nondescript book. # Charlotte = Happy, close
"Somewhere along the line this fellow lost his jacket, but, I assure you, you will be no less absorbed."
I take the book from her and look it over. {It has a very humble appearance, but I am excited to start on it nonetheless|I never got around to reading it last time. Maybe that will change}.
-> Confessions

=== Confessions ===
{Confessions>1:I decide it's best to play along and try not to out myself as an involuntary time traveller.}
[{player_name}] "So you must read a lot to have all these recommendations for people."
[Charlotte] "Oh, yes. I've been reading since I was quite young. I had quite a sheltered childhood. Books were my only companions until I was older." # Charlotte = Sad
"I have been a reader my whole life. Although... may I confide something in you, {player_name}?" # Charlotte = Calm
[{player_name}] "Uh, sure."
[Charlotte] "I only began reading in public when I realized it would let me... <i>observe</i> people without arousing suspicion."
[{player_name}] "You... <i>observe</i> people?"
[Charlotte] "Indeed. Well, I more study them, really. I find people fascinating."
[{player_name}] "{Uh|Hmm}..."
{That's kinda weird.|Charlotte is a very odd person, but I kinda like that.}
I try to hide the {uncertainty|look} on my face, but Charlotte appears to have picked up on it.
[Charlotte] "I ought to explain myself. You see, I have ASPD." # Charlotte = Surprised
[{player_name}] "Antisocial Personality Disorder, right?"
[Charlotte] "Yes. Though I do try so very hard to be 'pro-social', dear." # Charlotte = Calm
"The crux of it is that I have absolutely no sense of empathy at all."
[{player_name}] "Really? I mean, you seem nice enough. Certainly not like the psychos you hear about on the news."
[Charlotte] "I am very happy to hear that. I have gone to great lengths to construct a believable facade." # Charlotte = Happy
[{player_name}] "Well you had me fooled. In fact, I thought you worked here for my first few months."
[Charlotte] "Ah... Max is always telling me I do too much around here. Maybe they have a point..." # Charlotte = Surprised
She shrugs.
[Charlotte] "When I was a young girl I simply couldn't understand people." # Charlotte = Sad
"And, as is human nature, I was <delay=0.3>afraid <delay=0.3>of what I did not understand. That fear became hate, motivating deeds too heinous to recount in civil conversation."
"I fear I was quite a horrible, loathsome child. But that's a story for another time."
That reminds me of how I was- before the Voices.
"When I matured, I forced myself to care."
"I swore to myself and my family that I would never again act without regard for others."
"Having been robbed of an intuitive understanding of emotion, I resolved to train myself to recognize the <i>physical</i> signs of emotion instead." # Charlotte = Angry
"I was prepared to check myself into Blackwell, but my family insisted I come here first. I know it's only a matter of time before I end up there, but I had to honor their wishes." # Charlotte = Sad
"But I digress. The point of it all is that I took to watching people in order to better understand them."
"I strove to memorize every action: to commit to memory every micro-expression, gesticulation, and change in posture." # Charlotte = Angry
"And that is why I read in public. It's a kind of practice." # Charlotte = Calm
Charlotte's eyes dart around my face before she breathes a gentle sigh of relief.
[Charlotte] "My apologies for the monologue. You withstood it with aplomb, I must say." # Charlotte = Happy
"You are an excellent listener, do you know that?" 
[{player_name}] "Thanks for sharing. But, I mean, it was no trouble. I just sat here, really."
[Charlotte] "Nonsense. I know quite well the struggle of strapping in while an oblivious associate starts over-sharing."
-> Questions

=== Questions ===
// You get one, you have to loop if you want the other
[Charlotte] "Perhaps you have some questions for me after all that?" # Charlotte = Calm
Charlotte looks at me expectantly.
+[Ask her about Trissa] -> Trissa
+[Ask her about her Family] -> History
+[Not really] -> Fail

=== Trissa ===
[{player_name}] "So I noticed things seem, uh... <i>tense</i> with you and your roommate."
[Charlotte] "What? Trissa? No. Certainly not. She's a charming girl. Absolutely delightful to everyone." # Charlotte = Angry
"All the time, really. She makes it look effortless. Haha. Which is, of course, spectacular."
"Why would I have a problem with that?"
[{player_name}] "Yeah... you know, for someone who is apparently good at reading people, you're not that hard to read yourself."
Charlotte sighs. # Charlotte = Sad
[Charlotte] "My mother once told me the same thing, you know."
"Heavens, it's embarrassing to admit the truth: I've been... less than courteous towards her... at times."
Charlotte recoils as if stung by her own words.
"To make matters worse, she has never said an unkind word to me... she has never reciprocated the unjust spite I have levied against her."
"I'm jealous of her, {player_name}. She may have a different sort of grace, but hers is so fluid and natural. She is far better at any of this than I ever will be, no matter how hard I try." {SetValue("Know Charlotte Jealous", true)}
"She doesn't understand how valuable her gift is! To be so caring. To be so empathetic. She has never had to work for such qualities as I have!" # Charlotte = Angry
{grace>2:
[{player_name}] "Have you spoken with her about this?" # Grace ^ Good
-else: [{player_name}] "Wow. Uh... maybe try talking to her or something?" # Grace ^ Poor
}
[Charlotte] "Psh. Oh, I'm certain she would understand. She would be downright <i>sympathetic</i>, I expect."
"It would only exacerbate things, my friend. I couldn't bear the thought of her pitying me like that." # Charlotte = Sad
"No, {player_name}. It is, unfortunately, better this way. At least by distancing myself from her I can spare us both that pain."
-> OutOfTime


=== History ===
[{player_name}] "You mentioned your family before. Could you tell me about them?"
[Charlotte] "I would be happy to."
"The Blackwells are an old and wealthy family, though our wealth isn't old by some standards. My great grandfather, Winston Blackwell, made his fortune overseas."
"When he returned home, he married and had a few children. One such child, my great aunt Viola, suffered from what we now understand to be schizophrenia."
"My great grandfather opened Blackwell Asylum to care for her and any others from the community who similarly suffered."
"Regrettably, my great grandfather gave his life in the war a few years later. Blackwell Asylum atrophied without his guidance."
"It became a cruel place for the discarded. I fear it was not longer a place of medicine and hospice, but something far darker."
"When my great aunt Viola died, it was a wake-up call for my grandparents. They shut down Blackwell and vowed only to reopen it when they were prepared to run it properly and the necessary restitutions had been made."
"Blackwell did indeed reopen a decade later as Blackwell Psychiatric Hospital. It is, thankfully, a very different sort of place, where care for its patients is paramount."
I might not like the place, but Blackwell never abused anyone that I saw.
{-awareness>2:Looking back on it, I think what I really hated about Blackwell was who I was while I was there.}
"My family is very touchy about this legacy. When I confronted them about my own condition, they refused to allow me to go to Blackwell.
They worried it would 'reopen old wounds'." # Charlotte = Angry
"So they sent me here to Sunflower House. If you ask me, they are simply delaying the inevitable. But I have honored their wishes thus far."
"Thanks for taking such an interest, {player_name}!." # Charlotte = Happy
{grace>2:
	->Invitation
-else: ->Instruction
}

=== OutOfTime ===
Charlotte glances as the wall clock above the door and exclaims. # Charlotte = Surprised #time % 1
[Charlotte] "Heavens, how time has passed!"
"I have a routine to maintain. It has been a pleasure, {player_name}." # Charlotte = Calm
Charlotte rises and takes a step toward the door before turning back to me. # Charlotte = center
{
	-grace>3: ->Invitation
	-grace<3: 
		<color=color_descriptor><i>Spending time with Charlotte has <color=color_grace>improved <b>Grace<b> faintly<color=color_descriptor>.</color></i> # Grace+
		->Instruction
	-else: ->Instruction
}

=== Invitation ===
[Charlotte] "You've impressed me today, {player_name}."
"Would you be interested in joining me for tea tomorrow?"
+[Yes]
	[{player_name}] "I'd be happy to. What time?"
	[Charlotte] "I usually take tea at three in the afternoon, if that isn't a bother."
	[{player_name}] "Three p.m. Got it." {SetValue("ReadyForTea", 2)} # 9 & InProgress
+[No]
	[{player_name}] "Sorry, while ordinarily I would never turn down an invitation for free food, I've got other obligations."
	[Charlotte] "That's a shame, but I understand. Thank you for your company today, {player_name}."
-Charlotte smiles, curtsies, and strides out of the room. # Charlotte = Exit
I should probably head out as well. 
-> END

=== Instruction ===
[Charlotte] "I just had a thought. Seeing as you're leaving soon, what say I give you some pointers on social protocol on the outside?"
"I would be happy to tutor you in etiquette, if that would be something you would be interested in..."
+[Yes]
	[{player_name}] "Sure. What time?"
	[Charlotte] "How about here in the library tomorrow at two o'clock?"
	"I usually take tea at three, but I shall make an exception so that we have some uninterrupted time."
	[{player_name}] "Two p.m. tomorrow. Got it." {SetValue("ReadyForInstruction", 2)} # 8 & InProgress
+[No]
	[{player_name}] "Uh... sorry, I don't really feel comfortable committing to that right now."
	[Charlotte] "That's a shame, but I understand. Thank you for your company today, {player_name}."
-Charlotte smiles, curtsies, and strides out of the room. # Charlotte = Exit
Man, Charlotte is seriously living in a different century from anyone I've ever met.
I should probably head out as well.
-> END

=== Fail ===
[Charlotte] "Ah. Perhaps I have overstepped."
Charlotte glances as the wall clock above the door and exclaims. # Charlotte = Surprised #time % 1
[Charlotte] "Heavens, how time has passed!" 
Charlotte rises to her feet. # Charlotte = center
"Until next we meet, {player_name}."
She curtsies and strides out of the room. # Charlotte = Exit
Man, Charlotte is seriously living in a different century from anyone I've ever met.
She seemed embarrassed. Maybe asking her a question would have been better...
Oh, well. I guess I'd better head out, too.
-> END

=== JustRead ===
I peruse the shelves until a title catches my eye. # Charlotte = Exit
I pull out {~a thin|a small|a heavy|an old| a brand new| an ornate| a worn} book {~on {~archeology| world cultures| astronomy| botany| mythology}| about {~ the adventures of a wandering knight| a fearsome band of pirates| an ancient empire of dragons| the life of a loving pet| a fishing boat lost at sea| a boy who loses his mom to cancer}}.
The book is {~beautifully written and I learn a lot just from the prose.| rather dry, but well constructed and informative.| poorly written, but I learn a few things from its failures.}
<color=color_descriptor><i>Focusing on the text has taken a toll on your concentration, <color=color_wellbeing_penalty>increasing <b>Fatigue<b> slightly<color=color_descriptor>.</color> # Fatigue += 10
{
	- grace > 2:
		<color=color_descriptor>Competency with <color=color_grace><b>Grace</b><color=color_descriptor> has resulted in this activity <color=color_wellbeing_relief>relieving <b>Stress</b><color=color_descriptor>!</color> # Stress -= 20 # grace ^ good, nocolor
	- grace > 1:
		<color=color_descriptor>Experience with <color=color_grace><b>Grace</b><color=color_descriptor> has removed the <b>Stress<b> from this activity<color=color_descriptor>!</color> # grace ^ good, nocolor
		<color=color_descriptor>New knowledge has <color=color_grace>improved <b>Grace</b> faintly<color=color_descriptor>.</i></color> # Grace+
	- else:
		<color=color_descriptor>Engaging with the material was enlightening, but <color=color_wellbeing_penalty>increased <b>Stress</b> slightly<color=color_descriptor>.</color> # Stress += 10 # grace ^ poor, nocolor
		<color=color_descriptor>New knowledge has <color=color_grace>improved <b>Grace</b> faintly<color=color_descriptor>.</i></color> # Grace+
}
-> END