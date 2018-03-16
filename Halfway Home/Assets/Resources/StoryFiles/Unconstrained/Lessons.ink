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
[{player_name}] "Shit, right. Thanks for helping me out."
[Charlotte] "You are very welcome. It's nice to have the chance to share my knowledge." # Charlotte = Happy
"Now, let's begin."
"I have distilled the many lessons I have learned about etiquette over the years into three core ideas: Principles, Protocol, and Behavior." # Charlotte = Calm
"I tried thinking of another word that starts with `P`, but, alas, not everything fits into the creative molds that we would prefer." # Charlotte = Sad
"I would like you to decide which to begin with." # Charlotte = Happy
-> Topic

=== Behavior ===
~topicsDiscussed += 1
Charlotte talks about expressions and micro-expressions and how she uses them to determine how someone is feeling.
Humans are predictable, or at least predictably unpredictable.
Learning more about people's tendencies will help you anticipate their motivations. Psychology and anthropology are a must for someone like her, but she is confident that you can find them useful as well.
{
	-topicsDiscussed>1:->Trissa
	-else:->Topic
}

=== Principles ===
~topicsDiscussed += 1
Charlotte discusses etiquette and how, at its heart, it is based on empathy. For someone like you, that part should be easy.
You must act with virtue to be treated with virtue, etc.
The books all say to consider things from another's point of view. It's a tremendously difficult exercise for Charlotte.
{
	-topicsDiscussed>1:->Trissa
	-else:->Topic
}

=== Protocol ===
~topicsDiscussed += 1
Charlotte teaches you about social rules. She says the rules themselves are important, but what's more important is that you understand that the rules change.
Different groups of people have different social protocols. She can teach you to recognize and adapt to social rules quickly.
{
	-topicsDiscussed>1:->Trissa
	-else:->Topic
}

=== Topic ===
*[Principles] -> Principles
*[Protocol] -> Protocol
*[Behavior] -> Behavior
*{topicsDiscussed>0}[Ask Why She Cares] -> Motivation
*{topicsDiscussed == 3}[Thank Her For Her Time] -> Farewell

=== Trissa ===
Trissa comes in looking for Charlotte. She was gonna go to the store for some things and wanted to see if she needed anything.
Charlotte remarks that this is very thoughtful of her, gets into a politeness contest with Trissa (who is being genuine).
->Topic

=== Motivation ===
You can ask why Charlotte cares so much.
She will discuss her cruelty as a child and how she hurt those around her. 
She was arrogant, abusive, and impulsive. Being a wealthy child only exacerbated the issues.
She talks about how she read her Great Aunt Viola's diary and how it affected her.
She learned that the best way to get what she wanted was to help others.
She still feels nothing for those she helps, but she knows that every positive action makes her life better. The golden rule was not common sense to her, but she still holds it sacrosanct.
-> Farewell

=== Farewell ===
You thank her for her time. If you've progressed to 3 stars, she is proud of you and invites you to tea to test your skills.
-> END