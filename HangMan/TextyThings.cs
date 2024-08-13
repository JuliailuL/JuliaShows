using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMan
{
	internal class TextyThings
	{
		// Function takes a path in form of a string as input.
		// This function reads each line of a txt-file and adds them to a list of strings
		// then by means of random number generation picks a word from the list and returns it
		public static string ReturnAWordFromTXT(string path)
		{
			string data;
			StreamReader reader = null;
			List<string> words = new();

			try
			{
				reader = new StreamReader(path);
				data = reader.ReadLine(); // gives the first line of the file

				while (data != null)
				{ // keep reading and printing the file into the console
					words.Add(data);
					data = reader.ReadLine(); // read the next line
				}
				reader.Close();
			}
			catch (Exception e)
			{ //e is the instance of the exception
				Console.WriteLine(e.Message); // this will give inforamtion about what went wrong in the code
			}


			int length = words.Count; // (check how many words are in the file)
			Random random = new Random();
			int rando = random.Next(0, length - 1); // generate a random number in the scope of the list of words
			return words[rando]; // return the word on the index of that number
		}

		// Prints the line under the headline
		public static void UnderLine(char[] cpy, int strks, int MAX, List<char> gsesFls){
			Console.Write("\n\n\tGuess:  ");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write(cpy);
			Console.ResetColor();
			Console.Write($"\t  Strikes: {strks} (of {MAX}) \t  Previously on hangman: ");
			Console.ForegroundColor = ConsoleColor.Magenta;
			if (gsesFls.Count > 0) foreach (char guess in gsesFls) Console.Write((guess) + " ");
			Console.ResetColor();
		}

		public static bool UserWins(string slvWrd){
			Console.Clear();
			Console.Write($"\n\n\tIt is ");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write(slvWrd);
			Console.ResetColor();
			Console.WriteLine(".   You Win! I Lose!");
			Console.ReadKey();
			return false;
		}
	
		public static bool MachineWins(string sltn){
			Console.Clear();
			Console.WriteLine();
			Man.Lost();
			Console.Write("\n\t\t Your man hangs!\n\n\tMy word was ");
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write(sltn);
			Console.ResetColor();
			Console.WriteLine(".   I Win! You Lose!");
			Console.ReadKey();
			return false;
		}

		public static bool PlayAnotherRound(){
			Console.Clear();
			Console.Write("\n\tWant to play again? \n\n\tIf so, enter \"y\" otherwise just press enter.\n\n\t");
			string again = Console.ReadLine().Trim().ToLower();
			Console.Clear();
			if (again == "y") return true;
			else return false;
		}
	}
}

