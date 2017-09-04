-> This_Is_A_Knot
=== This_Is_A_Knot ===
This is a knot. Knots are the basic building blocks of an ink narrative. They operate like co-routine driven functions and allow for easy separation of story segments.
Writing in a knot requires no special formatting to appear before the player and can be easily read by the ink compiler.
If you want to introduce choice, record decisions, trigger game events, or other interactive options formatting is required.
*This is a choice -> DescribeChoicesA
*This is another choice -> DescribeChoicesA
= gathertime
- This is a gather. It pulls the flow to a single point, regardless of previous choices.
New Knots can be entered at any time.
* Try this one -> ThisOneA
* Or this one -> ThisOneA

=== ThisOneA ===
Knots can even flow -> ThisOneB

=== ThisOneB ===
mid-sentence.
-> END

=== DescribeChoicesA ===
Choices lead to different lines. It's best to indent choice lines to show how they flow!
Any line opened with an asterisk becomes a choice. There are several ways to format choices based on what choice text you want to show the player.
This text is the same as if you had picked the other choice, but it doeesn't have to be.
* [This is a sub-choice]
* [This is also a sub-choice]
- -> DescribeChoices

=== DescribeChoicesB ===
Subchoices are supported infinitely, but each time a subchoice is given it requires an additional asterisk.
Notice how this time, you can't see the text for the option you picked? That's because I used BRACKETS [] to signify that the choice was read only during the choice prompt.
Any text that appears BEFORE these brackets will show up in both the choice and the printed line. Text INSIDE  the brackets appears on on the choice itself.
Any text that appears AFTER these brackets will show up in the printed line only.
-> This_Is_A_Knot.gathertime