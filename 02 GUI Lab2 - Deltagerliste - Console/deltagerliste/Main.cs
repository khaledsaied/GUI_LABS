using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace deltagerliste
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string[] tokens;
			string str;
			char[] separators = {';'};

			FileStream fs = new FileStream (@"deltagerliste.csv", FileMode.Open);
			StreamReader s = new StreamReader (fs, Encoding.Default);

			if (s != null) {
				while(!s.EndOfStream)
				{
					str = s.ReadLine();
					tokens = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);

						foreach(string word in tokens)
						{
							Console.Write(String.Format("{0,-30} |  \t",word));
							//Console.Write(word+"\t");
						}
					Console.WriteLine();
				}
			}
		}
	}
}
