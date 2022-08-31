using System;
using System.IO;

namespace Task1_2.ResourceManagement
{
	class Program
	{
		static void Main(string[] args)
		{
			string filePath;
			using (var managed = new ManagedToFileWriter())
			{
				managed.WriteToFile();
				filePath = managed.FilePath;
			}

			ReadFile(filePath);

			using (var unmanaged = new UnmanagedToFileWriter())
			{
				unmanaged.WriteToFile();
				filePath = unmanaged.FilePath;
			}

			ReadFile(filePath);

			Console.ReadKey();
		}

		public static void ReadFile(string path)
		{
			Console.WriteLine(path + ": " + File.ReadAllText(path));
		}
	}
}