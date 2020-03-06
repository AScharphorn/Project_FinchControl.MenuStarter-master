using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;
using System.Linq;

namespace Project_FinchControl
{

    // ******************************************************
    //
    // Title: Finch Control - Menu Starter
    // Description: Starter solution with the helper methods,
    //              opening and closing screens, and the menu
    // Application Type: Console
    // Author: Scharphorn, Austin
    // Dated Created: 2/20/2020
    // Last Modified: 3/3/2020
    //
    // ******************************************************

    class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMainMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMainMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch finchRobot = new Finch();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        TalentShowDisplayMenuScreen(finchRobot);
                        break;

                    case "c":
                        DataRecorderDisplayMenuScreen(finchRobot);
                        break;

                    case "d":
                        AlarmSystemDisplayMenuScreen(finchRobot);
                        break;

                    case "e":
                        UserProgrammingDisplayMenuScreen(finchRobot);
                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        #region USER PROGRAMMING

        private static void UserProgrammingDisplayMenuScreen(Finch finchRobot)
        {
            DisplayScreenHeader("Programming Menu");

            Console.WriteLine("This Module is under development.");

            DisplayContinuePrompt();
        }

        #endregion

        #region ALARM SYSTEM

        static void AlarmSystemDisplayMenuScreen(Finch finchrobot)
        {
            Console.CursorVisible = true;

            string sensorsToMonitor = "";
            string rangeType = "";
            int minMaxThresholdValue = 0;
            int timeToMonitor = 0;

            bool quitMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Alarm System Menu");

                Console.WriteLine();
                Console.WriteLine("This module is under development.");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Sensors to Monitor");
                Console.WriteLine("\tb) Set Range Type");
                Console.WriteLine("\tc) Set Minimum/Maximum Threshold Value");
                Console.WriteLine("\td) Set Time to Monitor");
                Console.WriteLine("\te) Set Alarm");
                Console.WriteLine("\tq) Return to Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        sensorsToMonitor = AlarmSystemDisplaySetSensorsToMonitor();
                        break;

                    case "b":
                        rangeType = AlarmSystemDisplaySetRangeType();
                        break;

                    case "c":
                        minMaxThresholdValue = AlarmSystemDisplaySetMinMaxThresholdValue(rangeType, finchrobot);
                        break;

                    case "d":
                        timeToMonitor = AlarmSystemDisplaySetTimeToMonitor();
                        break;

                    case "e":
                        AlarmSystemSetAlarm(finchrobot, sensorsToMonitor, rangeType, minMaxThresholdValue, timeToMonitor);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMenu);
        }

        static void AlarmSystemSetAlarm(Finch finchrobot, 
                                       string sensorsToMonitor, 
                                       string rangeType, 
                                       int minMaxThresholdValue, 
                                       int timeToMonitor)
        {
            bool thresholdExceeded = false;

            DisplayScreenHeader("Set Alarm");

            Console.WriteLine($"\tSensor(s) to monitor: {sensorsToMonitor}");
            Console.WriteLine($"\tRange type: {rangeType}");
            Console.WriteLine($"\t{rangeType} threshold value: {minMaxThresholdValue}");
            Console.WriteLine($"\tTime to monitor: {timeToMonitor}");
            Console.WriteLine();

            Console.WriteLine("Presss any key to begin monitoring.");
            Console.ReadKey();

            if (thresholdExceeded)
            {
                Console.WriteLine($"The {rangeType} threshold value of {minMaxThresholdValue} was exceeded.");
            }
            else
            {
                Console.WriteLine("The threshold value was not exceeded.");
            }

            DisplayMenuPrompt("Light Alarm");
        }

        static void AlarmSystemDisplayElapsedTime(int elapsedTime)
        {
            Console.SetCursorPosition(15, 10);
            Console.WriteLine($"Elapsed time: ");
            Console.WriteLine($"Elapsed time: {elapsedTime}");
        }

        static bool AlarmSystemMonitorLightSensors(Finch finchrobot, string sensorsToMonitor, string rangeType, int minMaxThresholdValue, int timeToMonitor)
        {
            bool thresholdExceeded = false;
            int elapsedTime = 0;
            int currentLightSensorValue = 0;

            while (!thresholdExceeded && elapsedTime < timeToMonitor)
            {
                currentLightSensorValue = AlarmSystemGetCurrentLightSensorValue(finchrobot, sensorsToMonitor);

                switch (rangeType)
                {
                    case "minimum":
                        if (currentLightSensorValue < minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;

                    case "maximum":
                        if (currentLightSensorValue > minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                }
                finchrobot.wait(1000);
                elapsedTime++;
                AlarmSystemDisplayElapsedTime(elapsedTime);
            }
            return thresholdExceeded;
        }

        static int AlarmSystemGetCurrentLightSensorValue(Finch finchrobot, string sensorsToMonitor)
        {
            int currentLightSensorValue = 0;

            switch (sensorsToMonitor)
            {

                case "left":
                    currentLightSensorValue = finchrobot.getLeftLightSensor();
                    break;

                case "right":
                    currentLightSensorValue = finchrobot.getRightLightSensor();
                    break;

                case "both":
                    currentLightSensorValue = (int)finchrobot.getLightSensors().Average();
                    break;
            }
            return currentLightSensorValue;
        }

        static string AlarmSystemDisplaySetSensorsToMonitor()
        {
            string sensorsToMonitor;

            Console.Write("Sensors to monitor:");
            sensorsToMonitor = Console.ReadLine();
            //
            // Validate this
            //

            DisplayContinuePrompt();

            return sensorsToMonitor;
        }

        static string AlarmSystemDisplaySetRangeType()
        {
            string rangeType = "";
            //
            // Will adjust this later to fit the requirments
            //


            return rangeType;
        }

        static int AlarmSystemDisplaySetMinMaxThresholdValue(string rangeType, Finch finchrobot)
        {
            int minMaxThresholdValue;

            


            //
            // Validation
            //
            bool validResponse;

            do
            {
                DisplayScreenHeader("Min/Max Threshold Value:");

                Console.WriteLine($"Current Left light sensor value: {finchrobot.getLeftLightSensor()}");
                Console.WriteLine($"Current right light sensor value: {finchrobot.getRightLightSensor()}");
                Console.WriteLine();

                Console.Write($"{rangeType} light sensor value: ");
                validResponse = int.TryParse(Console.ReadLine(), out minMaxThresholdValue);

                if (!validResponse)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a integer.");
                    DisplayContinuePrompt();
                }
            } while (!validResponse);

            //
            // Echo back to user
            //

            DisplayContinuePrompt();

            return minMaxThresholdValue;
        }

        static int AlarmSystemDisplaySetTimeToMonitor()
        {
            int timeToMonitor;

            DisplayScreenHeader("Time to monitor");

            Console.Write("Time to monitor [seconds]:");
            int.TryParse(Console.ReadLine(), out timeToMonitor);

            DisplayContinuePrompt();

            return timeToMonitor;
        }

        #endregion

        #region DATA RECORDER

        static void DataRecorderDisplayMenuScreen(Finch finchrobot)
        {
            Console.CursorVisible = true;

            int numberOfDataPoints = 0;
            double dataPointFrequency = 0;
            double[] fahrenheitTemp = null;

            bool quitDataRecorderMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Data Recorder Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Number of Data Points");
                Console.WriteLine("\tb) Frequency of Data Points");
                Console.WriteLine("\tc) Get Data");
                Console.WriteLine("\td) Show Data");
                Console.WriteLine("\tq) Return to Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        dataPointFrequency = DataRecorderDisplayGetDataPointFrequency();
                        break;

                    case "c":
                        fahrenheitTemp = DataRecorderDisplayGetData(numberOfDataPoints, dataPointFrequency, fahrenheitTemp, finchrobot);
                        break;

                    case "d":
                        DataRecorderDisplayData(fahrenheitTemp);
                        break;

                    case "q":
                        quitDataRecorderMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitDataRecorderMenu);
        }

        static void DataRecorderDisplayData(double[] fahrenheitTemp)
        {
            DisplayScreenHeader("Data");

            DataRecorderDisplayData(fahrenheitTemp);
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        static void DataRecorderDisplayDataTable(double[] fahrenheitTemp)
        {
            //
            // Table Headers
            //
            Console.WriteLine(
                "Data Point".PadLeft(12) +
                "Tempature".PadLeft(12)
                );
            Console.WriteLine(
                "__________".PadLeft(12) +
                "_________".PadLeft(12)
                );

            //
            // Table Data
            //
            for (int index = 0; index < fahrenheitTemp.Length; index++)
            {
                Console.WriteLine(
                    (index + 1).ToString().PadLeft(12) +
                    fahrenheitTemp[index].ToString("n2").PadLeft(12)
                    );
            }
            DisplayContinuePrompt();
        }

        static double[] DataRecorderDisplayGetData(int numberOfDataPoints, double dataPointFrequency, double[] fahrenheitTemp, Finch finchrobot)
        {
            double celsiusTemp;
            int frequencyInSeconds;

            DisplayScreenHeader("Get Data");

            Console.WriteLine($"Data point frequency: {dataPointFrequency}");
            Console.WriteLine($"Number of data points: {numberOfDataPoints}");

            Console.WriteLine("The finch robot is ready to record temperatures.");
            DisplayContinuePrompt();

            for (int index = 1; index < numberOfDataPoints; index++)
            {
                celsiusTemp = finchrobot.getTemperature();
                fahrenheitTemp[index] = DataRecorderConvertCelsiusToFahrenheit(celsiusTemp);
                Console.WriteLine($"Temperature #{index}: {fahrenheitTemp[index]} fahrenheit");
                frequencyInSeconds = (int)(dataPointFrequency * 1000);
                finchrobot.wait(frequencyInSeconds);
            }

            Console.WriteLine();
            Console.WriteLine("The data recording has been completed.");

            DisplayContinuePrompt();

            Console.WriteLine();
            Console.WriteLine("Current data");
            DataRecorderDisplayDataTable(fahrenheitTemp);

            Console.WriteLine();
            Console.WriteLine($"Average Temperature: {fahrenheitTemp.Average()}");

            DisplayContinuePrompt();

            return fahrenheitTemp;
        }

        static double DataRecorderDisplayGetDataPointFrequency()
        {
            double dataPointFrequency;
            bool validResponse;

            DisplayScreenHeader("Data Point Frequency");

            do
            {
                Console.Write("Data Point Frequency[Seconds]: ");
                validResponse = double.TryParse(Console.ReadLine(), out dataPointFrequency);

                Console.WriteLine();
                Console.WriteLine($"Data Point Frequency: {dataPointFrequency}");

                if (!validResponse)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter an integer.");
                }
                DisplayContinuePrompt();
            } while (!validResponse);

            return dataPointFrequency;
        }

        static double DataRecorderConvertCelsiusToFahrenheit(double celsiusTemp)
        {
            double fahrenheitTemp;
            double tempNumber = ((celsiusTemp / 5) * 9 + 32);

            fahrenheitTemp = tempNumber;

            return fahrenheitTemp;
        }

        static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;
            bool validResponse;

            DisplayScreenHeader("Number of Data Points");

            do
            {
                Console.Write("Number of data points: ");
                validResponse = int.TryParse(Console.ReadLine(), out numberOfDataPoints);

                Console.WriteLine();
                Console.WriteLine($"Number of Data Points: {numberOfDataPoints}");
                if (!validResponse)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter an integer.");
                }
                DisplayContinuePrompt();
            } while (!validResponse);

            return numberOfDataPoints;
        }

        #endregion

        #region TALENT SHOW

        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show Menu                          *
        /// *****************************************************************
        /// </summary>
        static void TalentShowDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb) Dance");
                Console.WriteLine("\tc) Mixing It Up");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        TalentShowDisplayLightAndSound(finchRobot);
                        break;

                    case "b":
                        TalentShowDisplayDance(finchRobot);
                        break;

                    case "c":
                        TalentShowDisplayMixingItUp(finchRobot);
                        break;

                    case "q":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitTalentShowMenu);
        }

        static void TalentShowDisplayMixingItUp(Finch finchRobot)
        {
            DisplayScreenHeader("Mixing It Up");

            Console.WriteLine("\tThe Finch robot is about to show off all its talents!");
            DisplayContinuePrompt();

            for (int lightSoundLevel = 0; lightSoundLevel < 10; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(988);
                finchRobot.setMotors(255, 255);
                finchRobot.wait(250);
                finchRobot.noteOn(587);
                finchRobot.setMotors(-255, -255);
                finchRobot.wait(250);
            }
            for (int lightSoundLevel = 10; lightSoundLevel > 0; lightSoundLevel--)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(698);
                finchRobot.setMotors(255, 0);
                finchRobot.wait(250);
                finchRobot.noteOn(523);
                finchRobot.setMotors(-255, 0);
                finchRobot.wait(250);
                finchRobot.noteOn(880);
                finchRobot.wait(250);
            }
            finchRobot.setMotors(0, 0);
            finchRobot.noteOff();
            DisplayContinuePrompt();
        }

        static void TalentShowDisplayDance(Finch finchRobot)
        {
            DisplayScreenHeader("Dance");

            Console.WriteLine("\tThe Finch robot is about to show off its crazy moves!");
            DisplayContinuePrompt();

            finchRobot.setMotors(255, 255);
            finchRobot.wait(500);
            finchRobot.setMotors(255, 100);
            finchRobot.wait(500);
            finchRobot.setMotors(-255, 50);
            finchRobot.wait(500);
            finchRobot.setMotors(255, -255);
            finchRobot.wait(2000);
            finchRobot.setMotors(-255, -255);
            finchRobot.wait(500);
            finchRobot.setMotors(50, 150);
            finchRobot.wait(500);
            finchRobot.setMotors(-255, 255);
            finchRobot.wait(2000);
            finchRobot.setMotors(0, 0);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light and Sound                   *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void TalentShowDisplayLightAndSound(Finch finchRobot)
        {
            int startingNote;
            bool validResponse;

            Console.CursorVisible = false;

            DisplayScreenHeader("Light and Sound");

            Console.WriteLine("\tThe Finch robot is about to show off its glowing talent!");
            DisplayContinuePrompt();

            do
            {
                Console.WriteLine("Please enter a number from 523 to 1047 to represent a note to be played at the start of the song.");
                validResponse = int.TryParse(Console.ReadLine(), out startingNote);

                if (!validResponse)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter an intager.");
                }
                else if (startingNote < 523 || startingNote > 1047)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter an intager ranging from 523 to 1047.");
                }
                DisplayContinuePrompt();
            } while (!validResponse || startingNote < 523 || startingNote > 1047);

            for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
            }
            for (int lightSoundLevel = 255; lightSoundLevel > 0; lightSoundLevel--)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
            }


            //
            // Play a song
            //
            finchRobot.noteOn(startingNote);
            finchRobot.wait(1000);
            finchRobot.noteOn(698);
            finchRobot.wait(250);
            finchRobot.noteOn(659);
            finchRobot.wait(250);
            finchRobot.noteOn(587);
            finchRobot.wait(500);
            finchRobot.noteOn(698);
            finchRobot.wait(250);
            finchRobot.noteOn(659);
            finchRobot.wait(250);
            finchRobot.noteOn(587);
            finchRobot.wait(500);
            finchRobot.noteOn(587);
            finchRobot.wait(63);
            finchRobot.noteOff();
            finchRobot.wait(62);
            finchRobot.noteOn(587);
            finchRobot.wait(63);
            finchRobot.noteOff();
            finchRobot.wait(62);
            finchRobot.noteOn(587);
            finchRobot.wait(63);
            finchRobot.noteOff();
            finchRobot.wait(62);
            finchRobot.noteOn(587);
            finchRobot.wait(63);
            finchRobot.noteOff();
            finchRobot.wait(62);
            finchRobot.noteOn(659);
            finchRobot.wait(63);
            finchRobot.noteOff();
            finchRobot.wait(62);
            finchRobot.noteOn(659);
            finchRobot.wait(63);
            finchRobot.noteOff();
            finchRobot.wait(62);
            finchRobot.noteOn(659);
            finchRobot.wait(63);
            finchRobot.noteOff();
            finchRobot.wait(62);
            finchRobot.noteOn(659);
            finchRobot.wait(63);
            finchRobot.noteOff();
            finchRobot.wait(62);
            finchRobot.noteOn(698);
            finchRobot.wait(250);
            finchRobot.noteOn(659);
            finchRobot.wait(250);
            finchRobot.noteOn(587);
            finchRobot.wait(500);
            finchRobot.noteOff();

            DisplayContinuePrompt();

            DisplayMenuPrompt("Talent Show Menu");
        }

        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// *****************************************************************
        /// *               Disconnect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnect.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Connect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            do
            {
                DisplayScreenHeader("Connect Finch Robot");

                Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
                DisplayContinuePrompt();

                robotConnected = finchRobot.connect();

                //
                // TODO test connection and provide user feedback - text, lights, sounds
                //
                DisplayMenuPrompt("Main Menu");

                //
                // reset finch robot
                //
                finchRobot.setLED(0, 0, 0);
                finchRobot.noteOff();

                if (!robotConnected)
                {
                    Console.WriteLine("The finch robot has failed to connect.");
                    Console.WriteLine("Please try again after making sure that the USB cable is properly connected.");
                    DisplayContinuePrompt();
                }
            } while (!robotConnected);

            return robotConnected;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();
            Console.WriteLine("This application is to allow you to use a finch robot for a number of uses.");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
