using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_lab5
{
    class Program
    {
        //GLOBAL VARIABLE: creating global randomizer
        static Random rng = new Random();

        //////////////////////////////////////////////////
        //Input mechanism with poka-yoke (idiot-proofing)
        //////////////////////////////////////////////////
        static int InputInt32(bool mustBePozitive = false) //Flag that defines either the number is restricted to pozitives or not
        {
            int input;
            //Looping until the correct number is entered
            while (true)
            {
                //Attempt to parse the input.
                bool validInput = Int32.TryParse(Console.ReadLine(), out input);
                //First check the "mustBePozitive" flag. If it's present then also check for input being pozitive
                if (mustBePozitive)
                {
                    //Check for validness and being pozitive
                    //If the input has passed both tests then return the input
                    if (validInput && input >= 0)
                    {
                        return input;
                    }
                    //In case of failure, if it's because of invalid input then report to the user and try again.
                    else
                    {
                        if (validInput)
                        {
                            Console.WriteLine("\nThe number has to be pozitive. Please try again.");
                        }
                        //If it's because of parsing faliure then report an error message to the user and try again.
                        else
                        {
                            Console.WriteLine("\nError reading the input. Please try again and enter a number");
                        }
                    }  
                }
                else
                //If the flag isn't present then just check for input being valid
                {
                    if (validInput)
                    {
                        return input;
                    }
                    else
                    {
                        Console.WriteLine("\nError reading the input. Please try again and enter a number");
                    }
                }
            }
        }
        //////////////////////////////////////////////////////////
        //Function that asks the user the preferred input method
        //Outputs:
        //--TRUE if user has chosen to use randomizer
        //--FALSE if user has chosen to fill the array personally
        //////////////////////////////////////////////////////////
        static bool AskInputMethod()
        {
            //Loop until the correct option is entered
            do
            {
                int option;
                Console.Clear(); //Clear the console
                Console.WriteLine("How do you want to fill the array?\n");
                Console.WriteLine("1. Use randomizer");
                Console.WriteLine("2. I'll fill the array myself!\n");
                Console.WriteLine("Your choice: ");

                option = InputInt32(true);
                switch (option)
                {
                    case 1:
                        {
                            return true;
                        }
                    case 2:
                        {
                            return false;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid option. Please, choose an option from the list above.");
                            Console.ReadKey();
                            break;
                        }
                } 
            } while (true);
        }
        ////////////////////////////////////////////
        //Function that prints the recieved array.
        ////////////////////////////////////////////
        static void PrintArray(int[] array)
        {
            Console.WriteLine("Array's contents:");
            Console.Write("[");
            for (int i = 0; i < array.Length; i++)
            {
                if (i != array.Length - 1)
                {
                    Console.Write("{0},", array[i]);
                }
                else
                {
                    Console.Write(array[i]);
                }
            }
            Console.Write("]\n\n");
        }

        ///////////////////////////////////////////
        //Function that prints the recieved matrix.
        ///////////////////////////////////////////
        static void PrintMatrix(int[,] matrix)
        {
            Console.WriteLine("Contents of the matrix:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write("[");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (j != matrix.GetLength(1) - 1)
                    {
                        Console.Write("{0},", matrix[i,j]);
                    }
                    else
                    {
                        Console.Write(matrix[i, j]);
                    }
                }
                Console.Write("]\n"); 
            }
            Console.WriteLine();
        }

        ////////////////////////////////////////////////////////////////////////
        //Function that prints the recieved matrix. Overloaded for ragged array
        ////////////////////////////////////////////////////////////////////////
        static void PrintMatrix(int[][] ragArr)
        {
            Console.WriteLine("Contents of the ragged array:");
            for (int i = 0; i < ragArr.Length; i++)
            {
                Console.Write("[");
                for(int j = 0; j < ragArr[i].Length; j++)
                {
                    if (j != ragArr[i].Length - 1)
                    {
                        Console.Write("{0},", ragArr[i][j]);
                    }
                    else
                    {
                        Console.Write(ragArr[i][j]);
                    }
                }
                Console.Write("]\n");
            }
            Console.WriteLine();
        }
        ////////////////////////////////////////////////////////////////////
        //A function that returns the random int in the range of (min, max)
        ////////////////////////////////////////////////////////////////////
        static int GetRandomInRange (ref int min, ref int max) //Using references to change the values
        {
            //A loop that handles the possible exception
            do
            {
                try
                {
                    return rng.Next(min, max);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Minimal value cannot be bigger than maximal value.\nPlease enter the correct values."); //Output the error message

                    //Re-enter the numbers
                    Console.WriteLine("Enter the minimal number of the range:");
                    min = InputInt32(false);

                    Console.WriteLine("Enter the maximal number of the range:");
                    max = InputInt32(false);

                    continue;
                }
            } while (true);
        }

        ///////////////////////////////////////////////////////////////
        //This function adds additional columns after each even column
        ///////////////////////////////////////////////////////////////
        static int[,] AddEvenColumns(int[,] matrix, bool randomized)
        {
            int newColumns = 0; //New number of columns
            int n = matrix.GetLength(0) //Number of Rows
                , m = matrix.GetLength(1); //Number of Columns

            //Approximating the number of columns
            //If the previous number was even, then...
            if (m % 2 == 0) newColumns = m + (m / 2);
            //If the previous number was odd, then...
            else newColumns = m + ((m + 1) / 2) - 1;

            int[,] newMatrix = new int[n, newColumns]; //Creating the new matrix with additional columns

            //Enter the left border
            Console.WriteLine("Enter the minimal number of the range:");
            int min = InputInt32(false);

            //Enter the right border
            Console.WriteLine("Enter the maximal number of the range:");
            int max = InputInt32(false);

            for (int i = 0; i < n; i++)
            {
                int k = 0; //Separate counter for the new matrix
                for (int j = 0; j < m; j++)
                {
                    newMatrix[i, k] = matrix[i, j]; //Copying untouched elements
                    //If an even column was found...
                    if (j % 2 == 0)
                    {
                        //Then ask the user for the input method
                        if (randomized)
                        {
                            newMatrix[i, k] = GetRandomInRange(ref min ,ref max);
                            k++; 
                        }
                        else
                        {
                            Console.WriteLine("Please enter the element #{0} of ROW #{1}:", k + 2, i + 1);
                            matrix[i, k] = InputInt32(false);
                        }
                    }
                    k++;
                }
            }

            Console.WriteLine("Column addition concluded successfully.\n");
            Console.ReadKey();

            return newMatrix;
        }

        /////////////////////////////////////////////////////////////////////////////////
        //Function that deletes N elements from the array beginning with the number k
        ////////////////////////////////////////////////////////////////////////////////
        static int[] DeleteElements(int[] arr)
        {
            if (arr.Length == 0) //If the array is empty
            {
                Console.WriteLine(@"Don't even try to delete any elements from an empty array.
                It'll probably cause space-time distortions and destroy the entire universe.");
                return null;
            }
            else
            {
                int k = 0; //Variable for k
                int N = 0; //Variable for N
                //Loop that checks for N being less or equal than array length
                do
                {
                    Console.WriteLine("Enter the number of elements you wish to delete (from 1 to n): ");
                    N = InputInt32(true);
                    //If N is less or equal than the length then quit the cycle
                    if (N <= arr.Length)
                    {
                        break;
                    }
                    else //Otherwise, display an error message and try again
                    {
                        Console.WriteLine("Invalid number of element entered (Outside of array).\nPlease, try again.\n");
                    }
                } while (true);

                //Almost the same with k
                do
                {
                    Console.WriteLine("Enter the number of element you want to delete these elements from: ");
                    k = InputInt32(true);
                    if (k > 0 && k + N - 1 <= arr.Length) //If k + N is less or equal than the length then quit the cycle, with correction of one
                    {
                        k--;
                        break;
                    }
                    else //Otherwise, display an error message and try again
                    {
                        Console.WriteLine("Invalid number of element entered (Outside of array).\nPlease, try again.\n");
                    }
                } while (true);

                int newArrayLength = arr.Length - N; //Reducing actual array size
                int[] supp_array = new int[newArrayLength]; //Declare a supplimentory dynamic array


                for (int i = 0; i < newArrayLength; i++) //Cloning array into supp_array
                {
                    if (i < k)
                    {
                        supp_array[i] = arr[i];
                    }
                    else
                    {
                        supp_array[i] = arr[i + N];
                    }
                }

                Console.WriteLine("Element deletion concluded successfully.\n");
                Console.ReadKey();
                return supp_array; //Return the result array
            }
        }

        ////////////////////////////////////////////////////////////////////
        //A function that returns the ragged array with variable row length
        ///////////////////////////////////////////////////////////////////
        static int[][] MakeRaggedArray(bool randomized)
        {
            Console.Clear();
            //Entering the nubmer of rows
            Console.WriteLine("Enter pozitive number of rows:");
            int n = InputInt32(true);

            //Creating the ragged matrix
            int[][] ragArr = new int[n][];

            //Initializing the arrays rows
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Please enter the length of row {0}", i + 1);
                ragArr[i] = new int[InputInt32(true)];
            }

            if (randomized)
            {
                int min, max;

                //Enter the left border
                Console.WriteLine("Enter the minimal number of the range:");
                min = InputInt32(false);

                //Enter the right border
                Console.WriteLine("Enter the maximal number of the range:");
                max = InputInt32(false);
                //Filling the array with random nubmers
                for(int i = 0; i < ragArr.Length; i++)
                    for (int j = 0; j < ragArr[i].Length; j++)
                    {
                        ragArr[i][j] = GetRandomInRange(ref min, ref max);
                    }
                //Display the message below and pause
                Console.WriteLine("The ragged array has been formed successfully.");
                Console.ReadKey();
                //Finally, return the array
                return ragArr;
            }
            else
            {
                //Filling the ragged array manually
                for (int i = 0; i < ragArr.Length; i++)
                    for (int j = 0; j < ragArr[i].Length; j++)
                    {
                        Console.WriteLine("Please enter the element #{0} of ROW #{1}:", j + 1, i + 1);
                        ragArr[i][j] = InputInt32(false);
                    }
                //Display the message below and pause
                Console.WriteLine("The ragged array has been formed successfully.");
                Console.ReadKey();
                //Finally, return the array
                return ragArr;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        //Fuction that makes, initializes and returns a 2D matrix of desired dimensions
        /////////////////////////////////////////////////////////////////////////////////
        static int[,] MakeMatrix(bool randomized)
        {
            Console.Clear();
            //Entering dimensions, columns and rows respectively
            Console.WriteLine("Enter pozitive number of columns:");
            int m = InputInt32(true);
            Console.WriteLine("Enter pozitive number of rows:");
            int n = InputInt32(true);

            //Creating the matrix
            int[,] matrix = new int[n, m];
            if (randomized)
            {
                int min, max;

                //Enter the left border
                Console.WriteLine("Enter the minimal number of the range:");
                min = InputInt32(false);

                //Enter the right border
                Console.WriteLine("Enter the maximal number of the range:");
                max = InputInt32(false);

                //Filling with random numbers
                for (int i = 0; (i < matrix.GetLength(0)); i++)
                {
                    for (int j = 0; (j < matrix.GetLength(1)); j++)
                    {
                        matrix[i, j] = GetRandomInRange(ref min, ref max);
                    }
                }
                //Display the message below and pause
                Console.WriteLine("The matrix has been formed successfully.");
                Console.ReadKey();
                //Finally, return the array
                return matrix;
            }
            else
            {
                //Filling the matrix manually
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        Console.WriteLine("Please enter the element #{0} of ROW #{1}:", j + 1, i + 1);
                        matrix[i, j] = InputInt32(false); 
                    }
                }
                //Display the message below and pause
                Console.WriteLine("The array has been formed successfully.");
                Console.ReadKey();
                //Finally, return the array
                return matrix;
            }
        }
        ///////////////////////////////////////////////////////////////////////////
        //Fuction that makes, initializes and returns a 1D array of desired length
        ///////////////////////////////////////////////////////////////////////////
        static int[] MakeArray(bool randomized) //Flag for array filling method
        {
            Console.Clear();
            Console.WriteLine("Enter pozitive array size:");
            //Initialized the array with input
            int[] array = new int[InputInt32(true)];

            if (randomized)
            {
                int min, max;

                //Enter the left border
                Console.WriteLine("Enter the minimal number of the range:");
                min = InputInt32(false);

                //Enter the right border
                Console.WriteLine("Enter the maximal number of the range:");
                max = InputInt32(false);

                //Put the random numbers into the array
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = GetRandomInRange(ref min, ref max);
                }
                //Display the message below and pause
                Console.WriteLine("The array has been formed successfully.");
                Console.ReadKey();
                //Finally, return the array
                return array;
            }
            else
            {
                //Filling the array manually
                for (int i = 0; i < array.Length; i++)
                {
                    Console.WriteLine("Please enter the element #{0}:", i + 1);
                    array[i] = InputInt32(false);
                }
                //Display the message below and pause
                Console.WriteLine("The array has been formed successfully.");
                Console.ReadKey();
                //Finally, return the array
                return array;
            }
        }

        ////////////////////////////////////////////////
        //This is a function for array interaction menu
        //It accepts a reference to the existing array.
        //It is to be initialized in the function itself.
        /////////////////////////////////////////////////
        static void ArrayInteractionMenu(ref int[] array)
        {
            //Exit code for this menu.
            byte exitCode = 3;
            int option; //Current option for this menu
            do
            {
                option = 0;//Nullify the option
                Console.Clear(); //Clear the console
                //Header for the menu
                Console.WriteLine("*******************************************");
                Console.WriteLine("********  ARRAY INTERACTIONS MENU  ********");
                Console.WriteLine("*******************************************\n");
                //This part of code displays current status of the array
                Console.WriteLine("Current array status:");
                //Flag for interactions 
                bool allowInteractions = false;

                //If the array isn't initialized then it won't allow the user to do anything other than forming an array
                if (array == null)
                {
                    Console.WriteLine("--UNINITIALIZED");
                    Console.WriteLine("The array is not formed yet. Please form it to unlock other interactions.");
                }
                else //In other case it'll unlock all other interactions.
                {
                    allowInteractions = true;
                    Console.WriteLine("--Initialized");
                    Console.WriteLine("--Length: {0}", array.Length);
                    PrintArray(array);
                }

                //Conditional operator for displaying and proccessing the correct options
                //If interactions are allowed then display and proccess all actions
                if (allowInteractions)
                {
                    Console.WriteLine("\n1. Reform the array");
                    Console.WriteLine("2. Delete N elements of an array, beginning from K'th");
                    Console.WriteLine("3. Back to main menu\n");
                    Console.WriteLine("Your choice: ");
                    exitCode = 3; //Changed exit code due to menu shapeshifting

                    //Accept option as input
                    option = InputInt32(true);
                    //Switch-case controller for this path
                    //!!!!!!CHANGE THIS IN CASE OF RESTRUCTURING THE MENU!!!!!
                    switch (option)
                    {
                        case 1:
                            {
                                array = MakeArray(AskInputMethod());
                                break;
                            }
                        case 2:
                            {
                                array = DeleteElements(array);
                                break;
                            }
                        case 3:
                            {
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid option. Please, choose an option from the list above.");
                                Console.ReadKey();
                                break;
                            }
                    }
                }
                //In other case it will display and process just the option to create an array
                else
                {
                    Console.WriteLine("\n1. Form the array");
                    Console.WriteLine("2. Back to main menu\n");
                    Console.WriteLine("Your choice: ");
                    exitCode = 2; //Changed exit code due to restructuring the menu

                    option = InputInt32(true);
                    //Switch-case controller for this path
                    //!!!!!!CHANGE THIS IN CASE OF RESTRUCTURING THE MENU!!!!!
                    switch (option)
                    {
                        case 1:
                            {
                                array = MakeArray(AskInputMethod());
                                break;
                            }
                        case 2:
                            {
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid option. Please, choose an option from the list above.");
                                Console.ReadKey();
                                break;
                            }
                    }
                }
            } while (option != exitCode);
        }

        //////////////////////////////////////////////////
        //This is a function for matrix interaction menu
        //It accepts a reference to the existing matrix.
        //It is to be initialized in the function itself.
        //////////////////////////////////////////////////
        static void MatrixInteractionMenu(ref int[,] matrix)
        {
            //Exit code for this menu.
            byte exitCode = 3;
            int option; //Current option for this menu
            do
            {
                option = 0;//Nullify the option
                Console.Clear(); //Clear the console
                //Header for the menu
                Console.WriteLine("*******************************************");
                Console.WriteLine("********  MATRIX INTERACTIONS MENU  *******");
                Console.WriteLine("*******************************************\n");
                //This part of code displays current status of the matrix
                Console.WriteLine("Current matrix status:");
                //Flag for interactions 
                bool allowInteractions = false;

                //If the matrix isn't initialized then it won't allow the user to do anything other than forming an matrix
                if (matrix == null)
                {
                    Console.WriteLine("--UNINITIALIZED");
                    Console.WriteLine("The matrix is not formed yet. Please form it to unlock other interactions.");
                }
                else //In other case it'll unlock all other interactions.
                {
                    allowInteractions = true;
                    Console.WriteLine("--Initialized");
                    Console.WriteLine("--Dimensions: {0} x {1}", matrix.GetLength(0), matrix.GetLength(1));
                    PrintMatrix(matrix);
                }

                //Conditional operator for displaying and proccessing the correct options
                //If interactions are allowed then display and proccess all actions
                if (allowInteractions)
                {
                    Console.WriteLine("\n1. Reform the matrix");
                    Console.WriteLine("2. Add columns after every even column");
                    Console.WriteLine("3. Back to main menu\n");
                    Console.WriteLine("Your choice: ");
                    exitCode = 3; //Changed exit code due to menu shapeshifting

                    //Accept option as input
                    option = InputInt32(true);
                    //Switch-case controller for this path
                    //!!!!!!CHANGE THIS IN CASE OF RESTRUCTURING THE MENU!!!!!
                    switch (option)
                    {
                        case 1:
                            {
                                matrix = MakeMatrix(AskInputMethod());
                                break;
                            }
                        case 2:
                            {
                                matrix = AddEvenColumns(matrix, AskInputMethod());
                                break;
                            }
                        case 3:
                            {
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid option. Please, choose an option from the list above.");
                                Console.ReadKey();
                                break;
                            }
                    }
                }
                //In other case it will display and process just the option to create a matrix
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\n1. Form the matrix");
                    Console.WriteLine("2. Back to main menu\n");
                    Console.WriteLine("Your choice: ");
                    exitCode = 2; //Changed exit code due to restructuring the menu

                    option = InputInt32(true);
                    //Switch-case controller for this path
                    //!!!!!!CHANGE THIS IN CASE OF RESTRUCTURING THE MENU!!!!!
                    switch (option)
                    {
                        case 1:
                            {
                                matrix = MakeMatrix(AskInputMethod());
                                break;
                            }
                        case 2:
                            {
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid option. Please, choose an option from the list above.");
                                Console.ReadKey();
                                break;
                            }
                    }
                }
            } while (option != exitCode);
        }

        /////////////////////////////////////////////////////////
        //This is a function for ragged array interaction menu
        //It accepts a reference to the existing ragged array.
        //It is to be initialized in the function itself.
        ////////////////////////////////////////////////////////
        static void RaggedArrayInteractionMenu(ref int[][] ragArr)
        {
            //Exit code for this menu.
            byte exitCode = 3;
            int option; //Current option for this menu
            do
            {
                option = 0;//Nullify the option
                Console.Clear(); //Clear the console
                //Header for the menu
                Console.WriteLine("*******************************************");
                Console.WriteLine("****  RAGGED ARRAY INTERACTIONS MENU  *****");
                Console.WriteLine("*******************************************\n");
                //This part of code displays current status of the matrix
                Console.WriteLine("Current ragged array status:");
                //Flag for interactions 
                bool allowInteractions = false;

                //If the matrix isn't initialized then it won't allow the user to do anything other than forming an matrix
                if (ragArr == null)
                {
                    Console.WriteLine("--UNINITIALIZED");
                    Console.WriteLine("The ragged array is not formed yet. Please form it to unlock other interactions.");
                }
                else //In other case it'll unlock all other interactions.
                {
                    allowInteractions = true;
                    Console.WriteLine("--Initialized");
                    PrintMatrix(ragArr);
                }

                //Conditional operator for displaying and proccessing the correct options
                //If interactions are allowed then display and proccess all actions
                if (allowInteractions)
                {
                    Console.WriteLine("\n1. Reform the ragged array");
                    Console.WriteLine("2. Add columns after every even column");
                    Console.WriteLine("3. Back to main menu\n");
                    Console.WriteLine("Your choice: ");
                    exitCode = 3; //Changed exit code due to menu shapeshifting

                    //Accept option as input
                    option = InputInt32(true);
                    //Switch-case controller for this path
                    //!!!!!!CHANGE THIS IN CASE OF RESTRUCTURING THE MENU!!!!!
                    switch (option)
                    {
                        case 1:
                            {
                                ragArr = MakeRaggedArray(AskInputMethod());
                                break;
                            }
                        case 2:
                            {
                                break;
                            }
                        case 3:
                            {
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid option. Please, choose an option from the list above.");
                                Console.ReadKey();
                                break;
                            }
                    }
                }
                //In other case it will display and process just the option to create a matrix
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\n1. Form the ragged array");
                    Console.WriteLine("2. Back to main menu\n");
                    Console.WriteLine("Your choice: ");
                    exitCode = 2; //Changed exit code due to restructuring the menu

                    option = InputInt32(true);
                    //Switch-case controller for this path
                    //!!!!!!CHANGE THIS IN CASE OF RESTRUCTURING THE MENU!!!!!
                    switch (option)
                    {
                        case 1:
                            {
                                ragArr = MakeRaggedArray(AskInputMethod());
                                break;
                            }
                        case 2:
                            {
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid option. Please, choose an option from the list above.");
                                Console.ReadKey();
                                break;
                            }
                    }
                }
            } while (option != exitCode);
        }

        //////////////////////////
        //Function for main menu
        /////////////////////////
        static void MainMenu()
        {
            //Exit code for this menu. !!!!!!CHANGE THIS FIRST IN CASE OF RESTRUCTURING THE MENU!!!!!
            const byte exitCode = 4; 

            //Size variable declarations:
            /*int length = 0, //Length of a 1D array
                n = 0, //Number of columns in 2D array
                m = 0; //Number of rows in 2D array*/

            //Variable declarations for:
            int[] array = null;    //-Linear array
            int[,] matrix = null;  //-Matrix
            int[][] ragMat = null; //-Ragged matrix
            //They all will be intialized when appropriate functions are activated

            int option; //Current option
            do
            {

                option = 0; //Nullify option counter
                Console.Clear(); //Clear console

                //Below is the text for main menu. !!!!!!CHANGE THIS IN CASE OF RESTRUCTURING THE MENU!!!!!
                Console.WriteLine("*******************************************");
                Console.WriteLine("**************  MAIN MENU  ****************");
                Console.WriteLine("*******************************************\n");
                Console.WriteLine("1. Linear array interactions...");
                Console.WriteLine("2. Matrix interactions...");
                Console.WriteLine("3. Ragged matrix interactions...");
                Console.WriteLine("4. Exit.\n");
                Console.WriteLine("Your choice: ");

                //Switch-case controller
                //!!!!!!CHANGE THIS IN CASE OF RESTRUCTURING THE MENU!!!!!
                option = InputInt32(true);
                switch (option)
                {
                    case 1:
                        {
                            ArrayInteractionMenu(ref array);
                            break;
                        }
                    case 2:
                        {
                            MatrixInteractionMenu(ref matrix);
                            break;
                        }
                    case 3:
                        {
                            RaggedArrayInteractionMenu(ref ragMat);
                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid option. Please, choose an option from the list above.");
                            Console.ReadKey();
                            break;
                        }
                }
            } while (option != exitCode);
        }

        static void Main(string[] args)
        {
            MainMenu();
        }
    }
}
