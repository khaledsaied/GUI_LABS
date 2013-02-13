using System;

namespace hellloworldC
{
	public class helloworld
	{
		public static void Main (string[] args)		//public helloworld ()
		{
			/************************************ | Delopgave 3 | ************************************/
/*			int number1 = 1;
			int number2 = 2;
			int sum = number1+number2;

			Console.WriteLine("Hello, World!");			// http://msdn.microsoft.com/en-us/library/aa288463.aspx
			Console.WriteLine("Enter 1st number: 1st \nEnter 2nd number: 2");
			//Console.WriteLine("Enter 2nd number: 2");
			Console.ReadLine();
			Console.WriteLine("The sum of " + number1 + " and " + number2 + " is " + sum );
*/
			
			/************************************ | Delopgave 4 | ************************************/
			
			int guess, randomNumber;
			Random rand = new Random ();
			randomNumber = rand.Next (minValue: 1, maxValue: 100);
			
			Console.WriteLine ("\n*** Welcome to the Hi-Lo game ***" +
			                   "\nThe computer choose a number between 1 and 100, you guess it");
			Console.WriteLine ("-- The magic number IS: " + randomNumber + " --");		// Test - Not part of the program
			
			while (true) {
				try
				{	
					Console.WriteLine ("Enter your guess: ");
					string str = Console.ReadLine ();
					guess = int.Parse(str);
					
					if (guess < randomNumber && guess > 0 && guess < 101)
						Console.WriteLine ("Your guess i to low.");
					else if (guess > randomNumber && guess > 0 && guess < 101)
						Console.WriteLine ("Your guess i to high.");
					else if (guess > 100 || guess < 1)
						Console.WriteLine ("!!! Your guess is not within the range [1:100] !!!");
					else if (guess == randomNumber)
					{
						Console.WriteLine ("*** YOU GOT IT! ***");
						break;
					}
				}
				catch (Exception)
				{
					Console.WriteLine("Wrong format! Use only numbers!");
					break;
				}
			}
		}
	}
}
