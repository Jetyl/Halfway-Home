﻿/******************************************************************************/
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

# Load @ story_farewell_meal   # Ambience : play_ambience_crowd

-> Start

=== Start ===
{
	-GetValue("SavedTimothy"):
		->GoodMeal
	-else:
		->BadMeal
}

=== GoodMeal ===
I'm called to the kitchen for my farewell meal.   # Play : play_music_happy
I walk in seeing everyone chatting and having a good time.
Trissa is chatting up the other two ruclusive first-floor residents at the center table. {I|Still} can't remember their names...
{
	-GetValue("CompletedTeatime"):
		I see Charlotte, over in her usual chair, relaxing of all things.
	-else:
		I see Charlotte assisting in the preparations of the farewell meal.
}
{
	-GetValue("Convinced Eduardo"):
		Eduardo and Isaac are in their usual corner, giggling.
	-else:
		Eduardo and Isaac are surprisingly not sitting nearby each other, but on near opposite ends of the room.
}
Timothy is sitting eagerly by the side of the center table, just one seat over from where he was earlier this week.
[Max] "We've got your throne all ready, sire." # Max = Happy
[Dyed>Timothy] "H-hi {player_name}!" #Dyed = happy
Timothy has been very giddy all day today. Its nice.
[{player_name}] "Hi Timothy."
[Max] "You get all cozy now. I'll grab you some food! Be right back!" #Max = Exit
[Dyed>Timothy] "Oh! I-I'll help you Max!" #Dyed = Exit
->EduardoUpdate->CharlotteUpdate->GoodEnd
->END

=== GoodEnd ===
[Dyed>Timothy] "w-we're back!" #Dyed = happy #Max =Calm
[Max] "Sorry that took so long! Timothy was just asking if he-" #Skip #Dyed=Angry
"You know what, it's not important."  #Dyed = happy
+[Say Something]
	[{player_name}] "Oh? now I'm curious?" #expression+ #expression ^ good
	[Max] "Sorry, {player_name}. No Spoilers."
+[Let it slide]
	I let the slip up Max made slide. Obviously Timothy wants it to be a secret. #grace+ #grace ^ good
-[{player_name}] "Okay then."
"Thanks, for getting my food, by the way." #Dyed = stage_left
[Max] "Oh c'mon {player_name}. It's your special occasion. We should be the one thanking you." #Max = surprised
"Speaking of that." # Max Calm
Max stands fully straight up, letting out the full volume of his voice over the surrounding shatter. #Max = happy
"Ladies, gentlemen, and everyone in between, may I have your attention?"
The cafeteria's ambient babble slowly subsides into silence. Max looks over an index card before shoving it in their pocket.
[Max] "today we say farewell to one of our more established residents, {player_name}, who is finally ready to go back out into the real world!"
"And while normally I or one of the other RAs gives these toasts, a certain new resident has asked if he can speak, and well, How can I say no to that."
With his introductions out of the way, Max hands over the metaphorical stage to Timothy of all people. What does Timothy have to say? #Max = Stage_right #Dyed = Stage_Center
[Dyed>Timothy] "Uh, H-Hi everyone. I'm, um, Timothy Miyuri."
"<jitter>You, uh, all p-probably remember me fr-from the meal last m-m-monday. hehe...</jitter>"
Timothy is clearly super nervous with everyone's attention up on him. So why did he want to get up and speak?
"<jitter>N-now I w-was r-really n-nervous c-coming here.</jitter>"
"<jitter>A-afraid o-of having to deal with anything. Afraid of p-people expectations of m-me a-an-and stuff.</jitter>" #Dyed = Afraid
"<jitter>B-but I've m-met people h-here w-who helped me b-be a little l-less afraid of e-everything.</jitter>"
"<jitter> A-and the person w-who I think has helped me the m-most was my r-roommate,</jitter><delay=0.25> {player_name}"
Timothy turns directly to me. #Dyed = happy
"Thank You {player_name}. Thank you so much for being my friend."
"This toast is to you. May you're future be as warm as you've made our present."
Timothy raises his cup. Max follows suit, then 2 more people. then 2 more people. Until the entire room is singing my praise. //a bit too cheesy
I just start laughing in shock of everything around me.
The Dinner goes down without incident, and made me feel amazing. #stress -= 50 #depression -=50
I leave once the major festivities have died down. #All = Exit
->END

=== BadMeal ===
I'm called to the kitchen for my farewell meal. # Play : play_music_unfinished_business
I walk in seeing everyone chatting and having a good time.
Acting like everything is okay.
Trissa is chatting up Jesse and John at the center table.
{
	-GetValue("CompletedTeatime"):
		I see Charlotte, over in her usual chair, relaxing of all things.
	-else:
		I see Charlotte assisting in the preparations of the farewell meal.
}
{
	-GetValue("Convinced Eduardo"):
		Eduardo and Isaac are in their usual corner, giggling.
	-else:
		Eduardo and Isaac are surprisingly not sitting nearby each other, but on near opposite ends of the room.
}
[Max] "We've got your throne all ready, sire." # Max = Happy
They're doing a good job of disguising it, but you can tell Max is only pretending to be happy.
I take my seat at the center table, remembering how, less than a week ago, Timothy had done the same.
[Max] "I'll grab you some food!"
->EduardoUpdate->CharlotteUpdate->BadEnd

=== BadEnd===
[Max] "Sorry that took so long! Turns out the cook wasn't really ready yet." # Max = Calm
[{player_name}] "I don't mind at all."
[Max] "Thanks for being so cool about... well, everything." # Max = Happy
 "Timothy might not have had the chance to thank you today, but I know he appreciated what you did for him." # Max = Sad
"Anyway, time for the toast." # Max Calm
"Ladies, gentlemen, and everyone in between, may I have your attention?"
The cafeteria's ambient babble slowly subsides into silence. Max looks over an index card before shoving it in their pocket.
"It's been a rough week for many of us, I know."
"Today we said goodbye to our newest resident, Timothy Miyuri, who I have received word is safe and comfortable at Blackwell. It wasn't what we all hoped for, but I'm sure we'll see him again when he's ready." # Max = Sad
"But today we also say farewell to one of our more established residents, {player_name}, who is finally ready to go back out into the real world!" # Max = Happy
"This toast is to both of you. May you find happiness and good fortune as you face the challenges ahead."
Max raises their cup and the room follows suit. A small applause follows the toast and the murmur of conversation returns.
A few more residents come up to say goodbye, but they're not really people I knew that well.
Today has really tired me out. I end up leaving the cafeteria early. #All = Exit
-> END


=== EduardoUpdate ===
{
	-GetValue("SavedTimothy"):
	{
		-GetValue("Convinced Eduardo"):
			->BrokeUpBad
		-else:
			->UnitBad
	}
	-else:
	{
		-GetValue("Convinced Eduardo"):
			->BrokeUpGood
		-else:
			->UnitGood
	}
	
}

=BrokeUpBad
Eduardo is the first to come up. #Eduardo = Sad
[Eduardo] "Hey, {player_name}. It was great hangin'."
"With your chill, I'm sure you'll do great out there."
<speed=10%>...<speed=100%>
Eduardo just sort of shuffles around, not sure what else to say.
+[Ask if he's okay.]
	[{player_name}] "You seem pretty down, man..."
	[Eduardo] "{player_name}, amigo. Just at the bottom today. Usually I've got Isaac to help around this time, but..."
	"I messed up, {player_name}. I said some things I really shouldn't have and now I don't know where we even stand any more." # Eduardo = Angry
	"And ole' Timmy I..."
	"I kind of blew up at him the other day." # Eduardo = Sad
	"I can't really do anythin' right."
	"But this isn't your problem, eh? You've got the whole world waiting for you. Come back and visit, okay?." 
+[Let him be.]
	I decide he's probably just in his depressive state and needs some time to himself.
-Eduardo shrugs, then turns to leave. The week didn't seem to go well for him... # Eduardo = Exit 
->->

=BrokeUpGood
Eduardo is the first to come up. #Eduardo = Sad
[Eduardo] "Hey, {player_name}. It was great hangin'."
"With your chill, I'm sure you'll do great out there."
<speed=10%>...<speed=100%>
Eduardo just sort of shuffles around, not sure what else to say.
+[Ask if he's okay.]
	[{player_name}] "You seem pretty down, man..."
	[Eduardo] "{player_name}, amigo. Just at the bottom today. Usually I've got Isaac to help around this time, but..."
	"I messed up, {player_name}. I said some things I really shouldn't have and now I don't know where we even stand any more." # Eduardo = Angry
	"I'm glad I could help Timothy out with the dye job, I guess..."
	"I kind of blew up at him the other day." # Eduardo = Sad
	"I can't really do anythin' right."
	"Glad he didn't take what I said to heart or nothing."
	"<size=50%>Man. I really am a screw up.<size=100%>"
	"But this isn't your problem, eh? You've got the whole world waiting for you. Come back and visit, okay?." 
+[Let him be.]
	I decide he's probably just in his depressive state and needs some time to himself.
-Eduardo shrugs, then turns to leave. The week didn't seem to go well for him... # Eduardo = Exit 
->->

=UnitBad
Eduardo is the first to come up. #Eduardo = Sad
[Eduardo] "Hey, {player_name}. It was great hangin'."
"With your chill, I'm sure you'll do great out there."
"Oh yeah, and uh, Thanks?" #Eduardo =afraid
+[Thanks for what?]
	[{player_name}] "Thanks for what?"
	[Eduardo] "{player_gender=="M":Man|{player_gender=="F":Girl|{player_name}}}, Don't make me say it out loud." #Eduardo = Angry
	"I am just done with today, alright? complete emotional exhaust, you know?"
	"And what with Timothy going all..."
	"You know, I kind of blew up at him the other day." # Eduardo = Sad
	"I was so stuck up in my own feeling, I didn't think how it'd affect 'em, and well..."
	"Well whatever. This ain't your problem." #Eduardo = Calm
+[No problem.]
	[{player_name}] "No problem man."
	I give Eduardo a nice firm handshake, and he perks up, just a little. #Eduardo = Calm
-[{player_name}] "Hey, you going to be okay?"
[Eduardo] "Eh. prolly." #Eduardo = Sad
"I don't feel to hot now though. Was up, now I'm down, and all that."
Eduardo shrugs, then turns to leave. The week sounds like it was a roller-coaster for him. Hopefully it'll all end up good. # Eduardo = Exit 
->->

=UnitGood
Eduardo is the first to come up. #Eduardo = Sad
[Eduardo] "Hey, {player_name}. It was great hangin'."
"With your chill, I'm sure you'll do great out there."
"Oh yeah, and uh, Thanks?" #Eduardo =afraid
+[Thanks for what?]
	[{player_name}] "Thanks for what?"
	[Eduardo] "{player_gender=="M":Man|{player_gender=="F":Girl|{player_name}}}, Don't make me say it out loud." #Eduardo = Angry
	"I am just done with today, alright? complete emotional exhaust, you know?"
	"Timothy's dye job is nice, by the way."
	"I, uh, kind of blew up at him the other day." # Eduardo = Sad
	"I was so stuck up in my own feeling, I didn't think how it'd affect 'em, and well..."
	"I'm glad he didn't take what I said to heart." #Eduardo = Calm
	"<size=50%>I should prolly apologize to him or whatever too. ugh...<size=100%>."
+[No problem.]
	[{player_name}] "No problem man."
	I give Eduardo a nice firm handshake, and he perks up, just a little. #Eduardo = Calm
-[{player_name}] "Hey, you going to be okay?"
[Eduardo] "Eh. prolly." #Eduardo = Sad
"I don't feel to hot now though. Was up, now I'm down, and all that."
Eduardo shrugs, then turns to leave. The week sounds like it was a roller-coaster for him. Hopefully it'll all end up good. # Eduardo = Exit 
->->

=== CharlotteUpdate ===
{
	-GetValue("SavedTimothy"):
	{
		-GetValue("CompletedTeatime"):
			->LeavingHappy
		-else:
			->StayingHappy
	}
	-else:
	{
		-GetValue("CompletedTeatime"):
			->LeavingSad
		-else:
			->StayingSad
	}
	
}

=StayingSad
Next to approach is Charlotte, who seems to have taken a break from her duties.
{
- GetValue("MissedTea")==true:
	[Charlotte] "Greetings, {player_name}." # Charlotte = Angry, left
	Uh oh. She looks pissed. What did I- # Skip
	<Speed=200%>Oh god, I forgot to meet her for tea!
	"It is quite rude to accept an invitation and then fail to keep the appointment, you know."
	"And to think I was so impressed with your progress..."
	[{player_name}] "I'm so sorry, Charlotte! It just slipped my mind... I..."
	"I made a grave mistake. I didn't mean to hurt you..." # grace ^ good
	"Would you accept an honest apology from a forgetful idiot?" # grace ^ good
	[Charlotte] "Perhaps you are not as entirely bereft of courtesy as I had surmised." # Charlotte = Calm
	"I suppose I can forgive you, {player_name}..."
	"I would extend a new invitation, only... it seems you wouldn't be around to take advantage of it." # Charlotte = Happy
	"Speaking of which..."
-GetValue("MissedLesson")==true:
	[Charlotte] "Ah. {player_name}." # Charlotte = Angry, left
	Uh oh. She looks pissed. What did I- # Skip
	<Speed=200%>Oh god, I forgot to meet her in the library!
	"It is quite rude to accept an invitation and then fail to keep the appointment, you know."
	[{player_name}] "I'm so sorry... I messed up."
	Charlotte sighs and regains her composure.
	[Charlotte] "But I recognize you are no expert at these sorts of things." # Charlotte = Calm
	"Ironically, had you not been absent for our meeting I would have passed that information along to you."
	"I suppose you will have to do your best to learn `on the fly`, then."
	"Speaking of which..."
}
[Charlotte] "How are you finding your last day? Pleasant, I hope!" # Charlotte = Happy
+[Yeah.]
	[{player_name}] "It's fine, I guess."
	To be honest, I'm not really okay after earlier. But I figure it'd be kinder to put on a good face.
	[Charlotte] "Well, that's not quite the enthusiasm I have come to expect from outgoing residents." # Charlotte = Calm
	"Perhaps I am simply too used to Trissa's unwavering excitement. She's leaving two weeks from now and it seems to be her favorite subject of conversation."
	Charlotte seems to be carrying on without any concern for Timothy's absence.
+[Not really.]
	[{player_name}] "You're kidding, right? Did you not hear about Timothy?"
	[Charlotte] "Ah, you refer to the events of earlier today involving Mr. Miyuri." # Charlotte = Calm
	"These things happen from time to time. The hospital is the best place for him, I'm sure."
- How is she so unfazed?
[Charlotte] "I should return to my post, now. The line is starting to grow to an uncomfortable size. We'll speak later, I'm sure."
I watch Charlotte stride gracefully toward the serving line as Max emerges with a steaming plate of food. # Charlotte = Exit
->->

=StayingHappy
Next to approach is Charlotte, who seems to have taken a break from her duties.
{
- GetValue("MissedTea")==true:
	[Charlotte] "Greetings, {player_name}." # Charlotte = Angry, left
	Uh oh. She looks pissed. What did I- # Skip
	<Speed=200%>Oh god, I forgot to meet her for tea!
	"It is quite rude to accept an invitation and then fail to keep the appointment, you know."
	"And to think I was so impressed with your progress..."
	[{player_name}] "I'm so sorry, Charlotte! It just slipped my mind... I..."
	"I made a grave mistake. I didn't mean to hurt you..." # grace ^ good
	"Would you accept an honest apology from a forgetful idiot?" # grace ^ good
	[Charlotte] "Perhaps you are not as entirely bereft of courtesy as I had surmised." # Charlotte = Calm
	"I suppose I can forgive you, {player_name}..."
	"I would extend a new invitation, only... it seems you wouldn't be around to take advantage of it." # Charlotte = Happy
	"Speaking of which..."
-GetValue("MissedLesson")==true:
	[Charlotte] "Ah. {player_name}." # Charlotte = Angry, left
	Uh oh. She looks pissed. What did I- # Skip
	<Speed=200%>Oh god, I forgot to meet her in the library!
	"It is quite rude to accept an invitation and then fail to keep the appointment, you know."
	[{player_name}] "I'm so sorry... I messed up."
	Charlotte sighs and regains her composure.
	[Charlotte] "But I recognize you are no expert at these sorts of things." # Charlotte = Calm
	"Ironically, had you not been absent for our meeting I would have passed that information along to you."
	"I suppose you will have to do your best to learn `on the fly`, then."
	"Speaking of which..."
}
[Charlotte] "Congratulations on completing your tenure at Sunflower House, {player_name}." # Charlotte = Calm
[{player_name}] "Well, Thank you Charlotte."
[Charlotte] "<size=50%>It's always a joy to see others move on to bigger and better things...<size=100%>" #Charlotte = Sad
+[Ask about Timothy]
	[{player_name}] "So, Charlotte, what do you think of Timothy's new look?"
	[Charlotte] "Hm? I believe I said earlier that I feel it suits him nicely." #Charlotte = Calm
	"Although, that is less to do with the change in hair color and more to do with the change in attitude that has accompanied it."
	Charlotte gives me a knowing look.
	"I trust he has you to thank for both." # Charlotte = Happy
	"I do wonder... with you leaving, who will assist him with the reapplication of the dye once his hair grows out?"
	"Oh, dear! What if he asks me?!" # Charlotte = Surprised
	"I know precious little about exotic hair care solutions..." # Charlotte = Afraid
	"I had best prepare with rigorous study!" # Charlotte = Angry
	"So many things to be done before I can rejoin society..." # Charlotte = Sad
+[Ask about Charlotte]
	[{player_name}] "Charlotte, how are you doing?"
	[Charlotte] "Me? I am well, as usual." # Charlotte = Calm
	"My studies continue to be fruitful. Perhaps soon I should be able to..."
	"<size=50%>No, no. I'm not quite ready yet. Another year, at least.<size=100%>"
- [{player_name}] "Charlotte..."
[Charlotte] "I should return to my post, now. The line is starting to grow to an uncomfortable size. We'll speak later, I'm sure."
I watch Charlotte stride gracefully toward the serving line as Max and Timothy emerge with steaming plates of food. # Charlotte = Exit
->->

=LeavingHappy
Next to approach is Charlotte
[Charlotte] "Congratulations on completing your tenure at the Sunflower House, {player_name}." # Charlotte = Calm
[{player_name}] "Well, thank you Charlotte."
[Charlotte] "I have put a great deal of thought into what you said earlier."
+[Did you decide?]
	[{player_name}] "And...?"
	[Charlotte] "And... I have not quite reached a conclusion just yet."
	"I am realizing a lot of things I thought I knew are wrong. It's difficult for someone like me, but..."
+[Thats good.]
	[{player_name}] "That's good."
	[Charlotte] "Yes. I've even mentioned a few things to Trissa to get a second opinion."
-"I wanted to bring it up in case I come to a decision before you leave." # Charlotte = Calm
"I feel you should know how I am doing, after demonstrating so much concern for me."
"You've done great good here for the residents... And for me. You've done far more than was expected of you."
"You have my deepest and sincerest gratitude for that." #Charlotte = happy 
[{player_name}] "Charlotte, I-" #Skip
[Charlotte] "Please, there is no need for modesty. This evening is dedicated in your honor, after all."
"It would be rude to keep you from your meal any longer. We will speak later, I'm sure."
Charlotte bows out of the conversation, before heading to her seat. Max and Timothy emerge just behind her, holding steaming plates of food. # Charlotte = Exit
->->

=LeavingSad
Next to approach is Charlotte
[Charlotte] "How are you finding your last day? Pleasant, I hope!" # Charlotte = Happy
+[Yeah.]
	[{player_name}] "It's fine, I guess."
	To be honest, I'm not really okay after earlier. But I figure it'd be kinder to put on a good face.
+[Not really.]
	[{player_name}] "Not really at all. My mind's just on Timothy."
-[Charlotte] "It's not your fault, you know." # Charlotte = Calm
"Please don't let the unfortunate event with Mr. Miyuri dampen your spirits."
"You've done great good here for the residents... And for me. You've done far more than was expected of you."
"You don't have to be perfect." #Charlotte = happy 
[{player_name}] "Charlotte, I-" #Skip
[Charlotte] "Please, try to enjoy yourself. This evening is dedicated in your honor, after all."
"It would be rude to keep you from your meal any longer. We will speak later, I'm sure."
Charlotte bows out of the conversation, before heading to her seat. Max and Timothy emerge just behind her, holding steaming plates of food. # Charlotte = Exit
->->