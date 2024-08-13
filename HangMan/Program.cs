using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlTypes;
using System.Runtime.Intrinsics.X86;
using System.IO;
namespace HangMan
{
	internal class Program
	{
		static void Main(string[] args)
		{
			const int MAX = 7; // Constant for the maximal number of guesses! DO NOT CHANGE <- otherwise you'll have...
							   // ...to add or take away hanged man in MAN.cs (no fun!)
			bool goAgain = true;

			do{
				// Solution for hangman picked from a txt file
				string solution = TextyThings.ReturnAWordFromTXT("C:\\Users\\SchneidewindJulia\\VSCode\\HangMan\\hangmanwords.txt");
				solution = solution.ToLower();

				// To show solution before starting the game (for presentation purpose) = copy of solution; solution remains unchanged
				Console.WriteLine(solution);
				Console.ReadKey();

				char[] copy = Hang.CreateStarCopyOfSol(solution); // Creates a word the length of solution and prints it in stars '*'

				int strikes = 0; // Number of wrong guesses

				bool gameOn = true; // While not solved or strikes left
				bool solved = false; // Self explanatory
				bool starsLeft = true; // Are there still stars '*' left in the copy 

				List<char> guessesOverall = new(); // All previously guessed letters (whether part of solution or not)
				List<char> guessesFalse = new();  // All letters which were guessed wrong so far 

				do{
					Console.Clear();

					Man.Hang(strikes); // Prints "Hangman" and the hanged man 
					TextyThings.UnderLine(copy, strikes, MAX, guessesFalse); // the line below the headline

					string usrInp = Hang.GetUserInput(); // returns whether you want to guess the word or a single letter 

					switch (usrInp.Length)
					{
						case > 1:
							if (solution == usrInp) solved = true; // the user input is the same as the solution 
							else {
								strikes++;
								Console.WriteLine("\n\tWrong!");
								Console.ReadKey();
								Console.Clear();
							}
							break;
						case 1: // Check if a letter is part of the solution
							char guess = Convert.ToChar(usrInp); // gets a 'letter'
							bool isLetter = Hang.CharChecks(guess); //checks if returned char is a letter
							if (isLetter){
								bool notBefore = Hang.NotGuessedBefore(guessesOverall, guess); // true if not been guessed 
								if (notBefore){
									bool inSolution = Hang.PartOfSol(guessesFalse, solution, guess); //true if letter part of solution
									if (inSolution){
										Console.WriteLine("\n\tPart of solution :)");
										Console.ReadKey();
										copy = Hang.AddToCopy(copy, solution, guess); // replaces the stars in copy with the corresponding letter(s)
										starsLeft = Hang.StarsLeftInCopy(copy, solution);
										if (!starsLeft){ // All stars of the copy have been replaced by correctly guessed letters
											solved = true;
											usrInp = new String(copy); // copy, which is a char array / char [], is cast to string 
										}
									}
									else{ // letter is not part of solution
										strikes++;
										Console.WriteLine("\n\tThat is not part of the solution.");
										Console.ReadKey();
									}
								}
								else{ // when the input letter has been guessed before
									strikes++;
									Console.WriteLine("\n\tYou tried that before. (one strike more)");
									Console.ReadKey();
								}
							}
							else { // when the input char is not a letter 
							strikes++;
							Console.WriteLine("\n\tIf only you knew what a letter is!\n\t(That's one more strike btw)");
							Console.ReadKey();
							}
							break;
						default:
							strikes++;
							Console.WriteLine("\n\tNow that is some invalid input!\n\t(That's ohonnnnne mooooore strieieieieikkkkke)");
							Console.ReadKey();
							break;
					}

					if (solved) gameOn = TextyThings.UserWins(usrInp);

					if (strikes == MAX) gameOn = TextyThings.MachineWins(solution);

					if (solved || strikes == MAX) goAgain = TextyThings.PlayAnotherRound();

				} while (gameOn);
			} while (goAgain);
		}
	}
}