using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace HangMan
{
	internal class Hang
	{

		//Create a copy of the solution that prints in stars
		public static char[] CreateStarCopyOfSol(string sosolution){
			int j = sosolution.Length;
			char[] coppy = new char[j];
			for (int i = 0; i < j; i++){
				coppy[i] = '*';
			}
			return coppy;
		}

		public static string GetUserInput(){
			Console.WriteLine("\n\n\n\n\tTo solve: enter your word\n\n\tTo pick a letter: enter your chosen letter (english alphabet a-z)\n");
			Console.Write("\t"); //???
			return Console.ReadLine().Trim().ToLower();
		}

		// Error checking!!! Returns true when the char is indeed a letter
		public static bool CharChecks(char gesChar){
			int i = Convert.ToInt32(gesChar);
			if (96 < i && i < 123) return true;
			else return false;
		}

		// Checks if the char has been guessed before. Returns false if not preveiously checked
		// Side effect: if not guessed before: adds char to list in functions'...
		// ...parameters (here: of ALL previously guessed letters)
		public static bool NotGuessedBefore(List<char> gsesOvAl, char gesChar){
			for (int i = 0; i < gsesOvAl.Count; i++)
				if (gesChar == gsesOvAl[i]) return false; 
			// The letter has not been guessesd before: add to list of all guesses
			gsesOvAl.Add(gesChar); 
			return true;
		}

		// Checks of the char is part of the solurion, if so returns true
		// Side effect: if char not part of solution-word: adds char to list in...
		// ...functions' parameters (here: of previously incorrectly guessed letters)
		public static bool PartOfSol( List<char> gsesFls, string solutio, char gesChar){
			for (int i = 0; i < solutio.Length; i++)
				if (gesChar == solutio[i]) return true;
			gsesFls.Add(gesChar);
			return false;
		}

		// Exchanges a star for a letter (in the respective position in the copy of the solution)
		public static char[] AddToCopy(char[] cpy, string solutio, char gesChar){
			for (int i = 0; i < solutio.Length; i++)
				if(gesChar == solutio[i]) cpy[i] = gesChar;
			return cpy;
		}

		// Returns false if no more stars ('*') are left in the copy 
		public static bool StarsLeftInCopy(char[] cpy, string solutio){
			for (int i = 0; i < solutio.Length; i++)
				if (cpy[i] == '*') return true;
			return false;
		}
	}
}

