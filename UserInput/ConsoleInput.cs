using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JeremyOne.UserInput {
    static class ConsoleInput {

        /// <summary>
        /// Prompts a user through the console
        /// </summary>
        /// <typeparam name="T">The desired type</typeparam>
        /// <param name="Name">Name of the item</param>
        /// <param name="Default">Default value if the user types nothing</param>
        /// <param name="PromptText">Longer description text, optional</param>
        /// <returns>The users input in the desired type, currently implemented types:
        /// String, TimeSpan, Uri (absolute), Int, IPAddress, List\<IPAddress\>
        /// </returns>
        public static T PromptUser<T>(string Name, T Default, string PromptText) {
            bool valid = false;
            string userInput;

            while (valid == false) {
                Console.WriteLine("Enter a value for " + Name);

                if (PromptText != null) {
                    Console.WriteLine(" " + PromptText);
                }

                Console.WriteLine(" (or press ENTER to keep the default: " + Default.ToString() + ")");
                userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput)) {
                    //return or nothing entered
                    return Default;
                } else {

                    //Manually convert some types
                    if (typeof(T) == typeof(string)) {
                        return (T)(object)userInput;

                    } else if (typeof(T) == typeof(TimeSpan)) {
                        var ts = new TimeSpan();
                        if (TimeSpan.TryParse(userInput, out ts)) {
                            return (T)(object)ts;
                        }

                    } else if (typeof(T) == typeof(Uri)) {
                        Uri i;
                        if (Uri.TryCreate(userInput, UriKind.Absolute, out i)) {
                            return (T)(object)i;
                        }

                    } else if (typeof(T) == typeof(int)) {
                        int i;
                        if (int.TryParse(userInput, out i)) {
                            return (T)(object)i;
                        }

                    } else if (typeof(T) == typeof(List<IPAddress>)) {
                        string[] split = userInput.Split(',');
                        List<IPAddress> addresses = new List<IPAddress>();
                        valid = true;

                        foreach (string addressString in split) {
                            IPAddress address;
                            if (IPAddress.TryParse(addressString, out address)) {
                                addresses.Add(address);
                            } else {
                                //parse error
                                valid = false;
                            }
                        }

                        if (valid) {
                            return (T)(object)addresses;
                        }

                    } else if (typeof(T) == typeof(IPAddress)) {
                        
                        IPAddress address;
                        if (IPAddress.TryParse(userInput, out address)) {
                            return (T)(object)address;
                        }

                    } else {
                        throw new NotImplementedException("Casting this type is not implemented...");
                    }

                }

                if (valid == false) {
                    Console.WriteLine("Invalid entry...");
                }
            }

            return Default;
        }

        /// <summary>
        /// Prompts user to choose Yes or No and returns a boolean
        /// </summary>
        /// <param name="MessageTitle">Main message/question to display</param>
        /// <param name="TrueActionMessage">Description of what a YES answer means (optional)</param>
        /// <param name="FalseActionMessage">Description of what a NO answer means (optional)</param>
        /// <returns>A bool</returns>
        public static bool DecisionPrompt(string MessageTitle, string TrueActionMessage, string FalseActionMessage) {
            Console.WriteLine(MessageTitle + ":");

            if (string.IsNullOrWhiteSpace(TrueActionMessage) == false) {
                Console.WriteLine(" Yes: " + TrueActionMessage);
            }

            if (string.IsNullOrWhiteSpace(FalseActionMessage) == false) {
                Console.WriteLine(" No: " + FalseActionMessage);
            }

            Console.WriteLine("Enter y/n:");

            while (true) {

                char response = Console.ReadKey().KeyChar;
                response = char.ToLower(response);

                if (response == 'y') {
                    return true;
                } else if (response == 'n') {
                    return false;
                } else {
                    Console.Write("Unknown input: enter 'y' for yes or 'n' for no.");
                }
            }

        }

    }
}
