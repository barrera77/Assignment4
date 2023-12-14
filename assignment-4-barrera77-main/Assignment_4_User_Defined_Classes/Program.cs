using System;
using System.Linq;

namespace Assignment_4_User_Defined_Classes
{
    /// <summary>
    /// Purpose: The purpose of this program is to simulate a menu-driven application that simulates a pet
    /// clinic that offers medical services to cats & dogs, allows record management with the use of a user
    /// interface. And it demonstrates the use of User Defined Classes, File I/O, Lists and methods.
    ///
    /// Main Features:
    /// 1. Adding a new pet with user-input information (name, age, weight, type).
    /// 2. Displaying all pet records with detailed information.
    /// 3. Accessing services menu to calculate dosage for painkiller, sedative, or both.
    /// 4. Saving pet records to a file (PetsDb.txt) and loading existing records on program start.
    /// 5. Searching for pets by name and deleting pet records.
    /// 6. Save the current pet records upon exiting the program.
    ///  
    /// Author: Manuel Alva
    /// Last modified: [Date of Last Modification]
    /// 
    /// References:
    /// https://www.c-sharpcorner.com/
    /// https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tutorials/
    /// https://stackoverflow.com/
    /// https://www.geeksforgeeks.org/
    /// </summary>
    /// 
    internal class Program
    {
        static List<Pet> newPetsList = new List<Pet>();
        static List<Pet> petsList = new List<Pet>();        
        static bool isInMainMenu = true;

        static void Main(string[] args)
        {            
            LoadPets(petsList);

            while (isInMainMenu)
            {
                HandleMainMenu();
            }
        }
        #region Part A
        static string RequestPetName()
        {
            bool isValidInput = false;
            string petName = "";

            while (!isValidInput)
            {
                Console.Write("Please enter your pet's name: ");
                petName = Console.ReadLine();

                if (String.IsNullOrEmpty(petName))
                {
                    Console.WriteLine("The name of the pet cannot be blank.\n");
                }
                else
                {
                    isValidInput = true;
                }
            }
            return petName;
        }
        static int RequestPetAge()
        {
            int petAge = 0;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.Write("Please enter the age in years of your pet: ");
                if (int.TryParse(Console.ReadLine(), out petAge))
                {
                    if (petAge < 1)
                    {
                        Console.WriteLine("The age of the pet cannot be less than one year. \n");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.\n");
                }
            }
            return petAge;
        }
        static double RequestPetWeight()
        {
            double petWeight = 0;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.Write("Please enter the weight of your pet in pounds: ");
                if (double.TryParse(Console.ReadLine(), out petWeight))
                {
                    if (petWeight < 5)
                    {
                        Console.WriteLine("The weight of the pet must be 5 pounds or greater.\n");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number. \n");
                }
            }
            return petWeight;
        }

        static string RequestPetType()
        {
            bool isValidInput = false;
            string petType = "";

            while (!isValidInput)
            {
                Console.Write("Please enter 'D' for a dog and 'C' for a cat: ");

                petType = Console.ReadLine();

                if (String.IsNullOrEmpty(petType))
                {
                    Console.WriteLine("The name of the pet cannot be blank");
                }
                else if (petType.ToLower() != "c" && petType.ToLower() != "d")
                {
                    Console.WriteLine("Invalid input. Pet type can only be 'C' or 'D'.");
                }
                else
                {
                    if (petType.ToLower() == "c")
                    {
                        petType = "Cat";
                    }
                    if (petType.ToLower() == "d")
                    {
                        petType = "Dog";
                    }
                    isValidInput = true;
                }
            }
            return petType;
        }

        static Pet CreateNewPet()
        {
            string name = RequestPetName();
            int age = RequestPetAge();
            double weight = RequestPetWeight();
            string type = RequestPetType();

            return new Pet(name, age, weight, type);
        }
        static void PrintPetInfo(Pet newPet)
        {
            Console.Write("Name: ");
            PrintColoredText(ConsoleColor.Green, $"{newPet.Name}" + "\n");
            Console.Write("Age: ");
            PrintColoredText(ConsoleColor.Green, $"{newPet.Age}" + " years" + "\n");
            Console.Write("Weight: ");
            PrintColoredText(ConsoleColor.Green, $"{newPet.Weight}" + " lb" + "\n");
            Console.Write("Type: ");
            PrintColoredText(ConsoleColor.Green, $"{newPet.Type}" + "\n\n");
        }

        static void DisplayPets(List<Pet> petsList)
        {
            PrintHeader();

            if (!petsList.Any())
            {
                DisplayContinueMessage("No pet records to show. ");
            }
            else
            {
                foreach (Pet pet in petsList)
                {
                    PrintPetInfo(pet);
                    Console.WriteLine("-------------------------------");                    
                }                
            }
            DisplayContinueMessage("");
        }
        static void HandlePetInfo()
        {
            PrintHeader();
            Console.WriteLine("Please provide your pet's information and press 'ENTER' when done to continue.\n");

            //Create new Pet
            Pet newPet = CreateNewPet();

            //Add the new pet to the list
            newPetsList.Add(newPet);

            List<Pet> lastPet = new List<Pet>();
            lastPet.Add(newPetsList.Last());
           
            DisplayPets(lastPet);

            string correctInfo = PromptContinue("Is the information about your pet correct? Enter 'y' or 'n': ");

            if (correctInfo.ToLower() == "y")
            {

                string seeServices = PromptContinue("\nDo you wish to proceed to our services? Enter 'y' or 'n': ");
                if (seeServices.ToLower() == "y")
                {
                    HandleServiceMenu(newPet);
                }
            }
        }

        static void HandleServiceMenu(Pet newPet)
        {
            string serviceMenuOption;
            string continueService;
            double aDosage;
            double cDosage;
            bool quitServiceMenu = false;

            do
            {
                DisplayServiceMenu();
                Console.Write("Please select the service required for your pet: ");
                serviceMenuOption = Console.ReadLine();

                switch (serviceMenuOption)
                {
                    case "1":
                        aDosage = newPet.Acepromazine();
                        DisplayServiceMenu();
                        Console.WriteLine($"Your {newPet.Type.ToLower()} {newPet.Name}, requires {aDosage}ml of Acepromizine");
                        break;
                    case "2":
                        cDosage = newPet.Carprofen();
                        DisplayServiceMenu();
                        Console.WriteLine($"Your {newPet.Type.ToLower()} {newPet.Name}, requires {cDosage}ml of Carprofen");
                        break;
                    case "3":
                        DisplayServiceMenu();
                        aDosage = newPet.Acepromazine();
                        cDosage = newPet.Carprofen();
                        Console.WriteLine($"Your {newPet.Type.ToLower()} {newPet.Name}, requires {aDosage}ml of Acepromizine and {cDosage}ml of Carprofen");
                        break;
                    default:
                        DisplayContinueMessage("Invalid choice, try again. ");                        
                        continue;
                }
                continueService = PromptContinue("\nDo you have another pet that requires service? Enter 'y' or 'n': ");

                if (continueService.ToLower() == "y")
                {
                    quitServiceMenu = true;
                    isInMainMenu = true;
                }
                if (continueService.ToLower() == "n")  
                {
                    //SavePetToDb();
                    List<Pet> displayAllPetRecords = GetAllPetRecords(petsList, newPetsList);
                    quitServiceMenu = true;
                    isInMainMenu = false;
                    SavePetToDb(displayAllPetRecords);
                    PrintColoredText(ConsoleColor.Red, "Thank you for visiting CPSC1012 Pet Clinic!\n");
                }

            } while (!quitServiceMenu);
        }


        #endregion

        #region Part B
        static void HandleMainMenu()
        {
            List<Pet> displayAllPetRecords = GetAllPetRecords(petsList, newPetsList);
            DisplayMainMenu();
            Console.Write("\nPlease select an option from the menu: ");
            string mainMenuOption = Console.ReadLine();

            switch (mainMenuOption)
            {               
                case "1":
                    HandlePetInfo();
                    break;
                case "2":
                    PetFinder("Please enter the name of the pet you wish to find");
                    break;
                case "3":                    
                    DisplayPets(displayAllPetRecords);
                    break;
                case "4":
                    DeletePetRecords();
                    break;
                case "5":
                    ServicesMenuDirectAccess();                   
                    break;               
                case "6":
                    isInMainMenu = false;
                    SavePetToDb(displayAllPetRecords);
                    PrintColoredText(ConsoleColor.Red, "\nThank you for visiting CPSC1012 Pet Clinic!");
                    break;
                default:
                    DisplayContinueMessage("\nInvalid choice, try again. ");
                    break;
            }
        }

        static void SavePetToDb(List<Pet> updatedPetsList)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("PetsDb.txt"))
                {         
                    foreach (Pet pet in updatedPetsList)
                    {
                        writer.WriteLine($"{pet.Name},{pet.Age},{pet.Weight},{pet.Type}");
                    }
                }
                Console.WriteLine($"\nPet records added succesfully. ");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error ocurred while saving the file{ex.Message}");
            }
        }

        static void LoadPets(List<Pet> petsList)
        {
            string fileName = "PetsDb.txt";
            string binFolderPath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(binFolderPath, fileName);
            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        string[] data = line.Split(',');

                        if (data.Length == 4)
                        {
                            string name = data[0].Trim();
                            int age = int.Parse(data[1].Trim());
                            double weight = double.Parse(data[2].Trim());
                            string type = data[3].Trim();

                            Pet loadedPet = new Pet(name, age, weight, type);
                            petsList.Add(loadedPet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error ocurred while loading the pets info: {ex.Message}");
            }

            //File.Delete(filePath);
            //File.WriteAllText(filePath, String.Empty);
        }

        static (Pet findPet, List<Pet> petSource) PetFinder(string promptMessage)
        {
            bool isValidInput = false;
            Pet findPet = null;
            List<Pet> petSource = null;

            while (!isValidInput)
            {
                PrintHeader();
                Console.WriteLine(promptMessage + ". Or enter 'c' to cancel: \n");
                DrawTextBox("Pet Name:", 25);                                
                Console.SetCursorPosition(1 + "Pet Name:".Length, 7);

                string petName = Console.ReadLine();

                if (petName.ToLower() == "c")
                {
                    isValidInput = true;
                }
                else
                {
                    if (String.IsNullOrEmpty(petName))
                    {
                        DisplayContinueMessage("\nName of the pet cannot be null. ");
                    }
                    else
                    {
                        //look for the requested pet in the lists
                        findPet = newPetsList.Find(pet => pet.Name.Equals(petName, StringComparison.OrdinalIgnoreCase));
                        petSource = newPetsList;

                        if (findPet == null)
                        {
                            findPet = petsList.Find(pet => pet.Name.Equals(petName, StringComparison.OrdinalIgnoreCase));
                            petSource = petsList;
                        }
                        if (findPet == null)
                        {
                            DisplayContinueMessage($"\nThere is not pet with the name '{petName}' in our records. ");
                            isValidInput = false;
                        }
                        else
                        {
                            PrintHeader();
                            PrintPetInfo(findPet);
                            DisplayContinueMessage("");

                            isValidInput = true;
                        }
                    }
                }
            }
            return (findPet, petSource);
        }

        static void DeletePetRecords()
        {
            bool isValidInput = false;

            while (!isValidInput)
            {
                (Pet deletedPet, List<Pet> petSource) = PetFinder("Please enter the name of the pet you wish to delete");
                
                if (deletedPet == null && petSource == null)
                {
                    isValidInput = true;
                }
                else
                {
                    string decision = PromptContinue($"Are you sure you want to delete the records for {deletedPet.Name}. Enter 'y' or 'n': ");

                    if (decision == "y")
                    {
                        try
                        {
                            petSource.Remove(deletedPet);

                            DisplayContinueMessage("\nRecords deleted succesfully. ");
                            isValidInput = true;
                        }
                        catch (NullReferenceException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        isValidInput = false;
                    }
                }
            }
        }

        static List<Pet> GetAllPetRecords(List<Pet> firstList, List<Pet> SecondList)
        {
            List<Pet> result = new List<Pet>();
            result = Enumerable.Concat(firstList, SecondList).ToList();

            return result.OrderBy(pet => pet.Name).ToList();
        }

        static void ServicesMenuDirectAccess()
        {
            (Pet newPet, List<Pet> petSource) = PetFinder("Please enter the name of the pet you which you need service");

            if (newPet == null || petSource == null)
            {
                isInMainMenu = true;
            }
            else
            {
                HandleServiceMenu(newPet);
            }           
        }

        #endregion

        #region Helper methods
        static void PrintColoredText(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        static void PrintHeader()
        {
            Console.Clear();
            string header = " CPSC1012 Pet Clinic ";
            string separator = new('+', header.Length + 10);

            PrintColoredText(ConsoleColor.DarkCyan, separator + "\n");
            PrintColoredText(ConsoleColor.DarkCyan, "+++++");
            Console.Write(header);
            PrintColoredText(ConsoleColor.DarkCyan, "+++++\n" + separator + "\n\n");
        }
        static void DisplayContinueMessage(string message)
        {
            Console.Write(message + "Please press ENTER to continue ");
            Console.ReadLine();
        }

        static void DisplayMainMenu()
        {
            PrintHeader();

            string serviceMenu = "+++++ Main Menu +++++\n\n" +
                "1. Add a new pet\n" +
                "2. Search for pets\n" +
                "3. Display pets\n" +                
                "4. Delete pets\n" +
                "5. Services Menu\n" +
                "6. Save & Exit";

            Console.WriteLine(serviceMenu);
        }

        static void DisplayServiceMenu()
        {
            PrintHeader();

            string serviceMenu = "+++++ Service Menu +++++\n\n" +
                "1. Pain Killer\n" +
                "2. Sedative\n" +
                "3. Both painkiller and sedative\n";

            Console.WriteLine(serviceMenu);
        }
        static string PromptContinue(string promptString)
        {
            string result = "";
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.Write(promptString);
                result = Console.ReadLine();

                if (string.IsNullOrEmpty(result) || (result.ToLower() != "y" && result.ToLower() != "n"))
                {
                    DisplayContinueMessage("\nInvalid input. Please only enter 'y' or 'n'.\n");
                    
                }
                else
                {
                    isValidInput = true;
                }
            }
            return result;
        }
        
        static void DrawTextBox(string prompt, int width)
        {            
            // Draw the top border           
            Console.Write(new string(' ', prompt.Length) + "+" + new string('-', width - 1) + "+ \n");

            // Draw the prompt          
            Console.Write(prompt + "|" + new string(' ', width - 1) + "|\n");

            // Draw the bottom border           
            Console.Write(new string(' ', prompt.Length) + "+" + new string('-', width - 1) + "+");
        }
      
        #endregion
    }
}