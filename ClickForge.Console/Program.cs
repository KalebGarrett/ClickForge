using ClickForge.Models;
using ClickForge.Profiles;
using Action = ClickForge.Models.Action;

var profileRepository = new ProfileRepository();

while (true)
{
    Console.WriteLine("Select an option:\n" +
                      "1. Create\n" +
                      "2. Load\n" +
                      "3. Delete\n" +
                      "4. Quit");

    Console.Write("\nEnter Here: ");
    var option = Console.ReadLine();

    if (option == "1")
    {
        Console.Clear();
        
        string name;
        while (true)
        {
            Console.Write("Enter the name you want to call your profile: ");
            name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Please enter a valid name for your profile.");
                continue;
            }

            break;
        }

        Console.Clear();

        bool loop;
        while (true)
        {
            Console.Write("Do you want this to loop? (Yes or No): ");
            var loopInput = Console.ReadLine();

            if (loopInput!.Equals("yes", StringComparison.CurrentCultureIgnoreCase))
            {
                loop = true;
                break;
            }

            if (loopInput!.Equals("no", StringComparison.CurrentCultureIgnoreCase))
            {
                loop = false;
                break;
            }

            Console.WriteLine("Please enter (Yes or No): ");
        }

        Console.Clear();

        string windowName;
        while (true)
        {
            Console.Write("Enter the name of the window you want to target: ");
            windowName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(windowName))
            {
                Console.WriteLine("Please enter a valid window name.");
                continue;
            }

            break;
        }

        Console.Clear();

        string inputType;
        while (true)
        {
            Console.Write("Enter the input type: (Mouse or Key): ");
            inputType = Console.ReadLine();

            if (inputType!.ToLower() == "mouse" || inputType.ToLower() == "key")
            {
                break;
            }

            Console.WriteLine("Please enter (Mouse or Key): ");
        }

        Console.Clear();

        string inputData;
        while (true)
        {
            Console.Write("Enter the input data: ");
            inputData = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(inputData))
            {
                Console.WriteLine("Please enter a valid (Mouse click or a Key).");
                continue;
            }

            break;
        }

        Console.Clear();

        int delay;
        while (true)
        {
            try
            {
                Console.Write("Enter the time delay: ");
                delay = Convert.ToInt32(Console.ReadLine());
                break;
            }
            catch (Exception)
            {
                Console.WriteLine("You must enter an integer.");
            }
        }

        var action = new Action()
        {
            InputType = inputType,
            InputData = inputData,
            Delay = delay
        };

        var profile = new Profile()
        {
            Name = name,
            Loop = loop,
            WindowName = windowName,
            Action = action
        };

        profileRepository.Create(profile);
        
        Console.Write("Profile created successfully! Press any key to continue...");
        Console.ReadKey();

        Console.Clear();

        continue;
    }

    if (option == "2")
    {
        Console.Clear();
        
        var fileNames = profileRepository.GetFileNames();
        
        if (fileNames.Length == 0)
        {
            Console.WriteLine("There are no profiles to load. Please create a new profile.");   
            
            continue;
        }
        
        Console.WriteLine("List Of Profiles:");

        var i = 1;
        
        foreach (var fileName in fileNames)
        {
            Console.WriteLine($"{i++}. {fileName}");
        }

        Console.WriteLine();

        string profileName;
        while (true)
        {
            Console.Write("Which profile do you want to load?: ");
            profileName = Console.ReadLine();

            var isExistingProfile = fileNames!.Contains(profileName);

            if (!isExistingProfile)
            {
                Console.WriteLine("Profile does not exist. Please select one from the list.");
                continue;
            }

            break;
        }

        // Add user input for stopping loop or if automation only loops once, go back to menu
        profileRepository.Load(profileName!);
        
        continue;
    }

    if (option == "3")
    {
        Console.Clear();

        var fileNames = profileRepository.GetFileNames();
        
        if (fileNames.Length == 0)
        {
            Console.WriteLine("There are no profiles to delete. Please create a new profile.");   
            
            continue;
        }
        
        Console.WriteLine("List Of Profiles:");

        var i = 1;
        
        foreach (var fileName in fileNames)
        {
            Console.WriteLine($"{i++}. {fileName}");
        }

        Console.WriteLine();

        string profileName;
        while (true)
        {
            Console.Write("Which profile do you want to delete?: ");
            profileName = Console.ReadLine();

            var isExistingProfile = fileNames!.Contains(profileName);

            if (!isExistingProfile)
            {
                Console.WriteLine("Profile does not exist. Please select one from the list.");
                continue;
            }

            break;
        }

        profileRepository.Delete(profileName!);
        
        Console.Write("Profile deleted successfully! Press any key to continue...");
        Console.ReadKey();

        Console.Clear();
        
        continue;
    }

    if (option == "4")
    {
        Environment.Exit(1);
    }

    Console.WriteLine("Incorrect option.\n");
}