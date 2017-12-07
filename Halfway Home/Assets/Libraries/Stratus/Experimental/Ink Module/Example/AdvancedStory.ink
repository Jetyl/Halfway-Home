/******************************************************************************/
/*
@file   AdvancedStory.ink
@author Christian Sagel
@par    email: c.sagel@digipen.edu
All content © 2017 DigiPen (USA) Corporation, all rights reserved.
*/
/******************************************************************************/
// Variables
VAR cats = 0

// External functions
EXTERNAL SetIntegerVariable(name)

// Start
-> Knot1
=== Knot1 ===
= Stitch1
// Brackets denote a speaker. Any line with quotation marks is applied to the last speaker.
[John] "Make me an Ink file that shows what you need." # work++ 
// The # denotes an ink tag. It can be parsed separately from the dialog. It is used for
// custom scripting events
-> Knot1.Stitch2
= Stitch2
[Christian] "Okay. This saddens me." # christian = sad 
[John] "Go! This makes me happy." # john = happy
[Christian] "Your sadistic reaction increases my stress." # stress += 10
// Now let's retrieving values from outside
A little while later...
[John] "How many cats does thou have?"
[Jesse] "I have {cats} cats? I am not sure. Let's retrieve the value from a more reliable source..."
// This will ask the reader to set the variable cats from outside
{SetIntegerVariable("cats")}
[Jesse] "I just called my top people. It seems I actually have {cats} cats"

-> END