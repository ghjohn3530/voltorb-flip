# voltorb-flip
A recreation of the Pokemon HGSS minigame "Voltorb Flip" using WPF and C#.

 For Players:
------------------------
	
	The game is made up of a grid of 25 "cards", each with a
	number value (1, 2, or 3) or a voltorb (bomb).

	If you flip over a number value, you will be given your
	current number of coins times the value of the card.
	For example, if I have 12 coins, and I flip over a "2",
	my current coins will now be 24. If you have 0 coins and
	you flip over a number value, you will be given that many
	coins.

	If you flip over a voltorb, the voltorb "explodes", and
	all of your current coins are lost. The cards are all
	flipped over to show you the game's solution.
	
	On the sides of each column and row, there are two values:
	- A black number
	- A red number

	The black number is the total sum of all the values in
	that column/row. The red number is the total number of
	voltorbs in that column/row. 

	With this information, the player must flip over every
	'2' or '3' card to get the highest value possible.
	You can start each match with flipping every card in
	a column/row with 0 voltorbs. After that, it is up
	to you to figure out where each card is.

	If you get to a point where you cannot find anymore
	'2's or '3's without taking a risk, you can cash in your
	current coins to add to your total score.

	Have fun!

 For Programmers:
------------------------

	The folder called "Source" contains the Visual Studio
	Solution file, as well as all of the XAML and CS files
	needed to build the application. Feel free to mess with or
	read any of the code I've written for the game. (It's not
	very neat, FYI.)

	The "Resources" folder contains the saves text file and
	all of the assets used for the game. The app directly
	refers to this folder, so changing the contents of the
	folder, or the location of the folder/app may cause the
	game to crash the next time it's run.

