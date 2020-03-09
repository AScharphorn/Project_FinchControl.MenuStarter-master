﻿using System;
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

            string lightSensorsToMonitor = "";
            string lightRangeType = "";
            string tempRangeType = "";
            int minMaxLightThresholdValue = 0;
            int minMaxTempThresholdValue = 0;
            int timeToMonitorLight = 0;
            int timeToMonitorTemp = 0;
            int timeToMonitorLightAndTemp = 0;

            bool tempSensor = false;
            bool quitMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Alarm System Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Activate Temperature Sensor.");
                Console.WriteLine("\tb) Sensors to Monitor");
                Console.WriteLine("\tc) Set Temperature Range Type");
                Console.WriteLine("\td) Set Light Range Type");
                Console.WriteLine("\te) Set Minimum/Maximum Temperature Threshold Value");
                Console.WriteLine("\tf) Set Minimum/Maximum Light Threshold Value");
                Console.WriteLine("\tg) Set Maximum Time to Monitor Temperature");
                Console.WriteLine("\th) Set Maximum Time to Monitor Light");
                Console.WriteLine("\ti) Set Maximum Time to Monitor Light and Temperature");
                Console.WriteLine("\tj) Set Temperature Alarm");
                Console.WriteLine("\tk) Set Light Alarm");
                Console.WriteLine("\tl) Set Light and Temperature Alarm");
                Console.WriteLine("\tq) Return to Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        tempSensor = AlarmSystemDisplaySetTemperatureSensor(tempSensor);
                        break;

                    case "b":
                        lightSensorsToMonitor = AlarmSystemDisplaySetSensorsToMonitor(finchrobot);
                        break;

                    case "c":
                        tempRangeType = AlarmSystemDisplaySetTemperatureRangeType();
                        break;

                    case "d":
                        lightRangeType = AlarmSystemDisplaySetLightRangeType();
                        break;

                    case "e":
                        minMaxTempThresholdValue = AlarmSystemDisplaySetMinMaxTemperatureThresholdValue(tempRangeType, finchrobot);
                        break;

                    case "f":
                        minMaxLightThresholdValue = AlarmSystemDisplaySetMinMaxLightThresholdValue(lightRangeType, finchrobot);
                        break;

                    case "g":
                        timeToMonitorTemp = AlarmSystemDisplaySetMaximumTimeToMonitorTemperature();
                        break;

                    case "h":
                        timeToMonitorLight = AlarmSystemDisplaySetMaximumTimeToMonitorLight();
                        break;

                    case "i":
                        timeToMonitorLightAndTemp = AlarmSystemDisplaySetMaximumTimeToMonitorLightAndTemperature();
                        break;

                    case "j":
                        AlarmSystemDisplaySetTemperatureAlarm(finchrobot, tempSensor, tempRangeType, minMaxTempThresholdValue, timeToMonitorTemp);
                        break;

                    case "k":
                        AlarmSystemDisplaySetLightAlarm(finchrobot, lightSensorsToMonitor, lightRangeType, minMaxLightThresholdValue, timeToMonitorLight);
                        break;

                    case "l":
                        AlarmSystemDisplaySetLightAndTemperatureAlarm(finchrobot, 
                                                                      tempSensor, 
                                                                      tempRangeType,
                                                                      minMaxTempThresholdValue,
                                                                      lightSensorsToMonitor, 
                                                                      lightRangeType, 
                                                                      minMaxLightThresholdValue, 
                                                                      timeToMonitorLightAndTemp);
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

        static int AlarmSystemDisplaySetMaximumTimeToMonitorLightAndTemperature()
        {
            string userResponse = "";
            int timeToMonitorLightAndTemp;
            bool validResponse;

            do
            {
                DisplayScreenHeader("Time to Monitor Light and Temperature");

                Console.Write("Time to monitor light and temperature[seconds]:");
                validResponse = int.TryParse(Console.ReadLine(), out timeToMonitorLightAndTemp);

                if (!validResponse)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a integer.");
                    DisplayContinuePrompt();
                }
                Console.Clear();

                if (validResponse)
                {
                    do
                    {
                        DisplayScreenHeader("Time to Monitor Light");
                        Console.WriteLine();
                        Console.WriteLine($"The current time to monitor light and temperature is set to {timeToMonitorLightAndTemp}, is this correct?");
                        userResponse = Console.ReadLine();

                        if (userResponse != "no" && userResponse != "yes")
                        {
                            Console.WriteLine("Please enter 'yes' or 'no'.");
                        }
                        DisplayContinuePrompt();
                    } while (userResponse != "yes" && userResponse != "no");
                }
            } while (!validResponse || userResponse != "yes");

            return timeToMonitorLightAndTemp;
        }

        static void AlarmSystemDisplaySetLightAndTemperatureAlarm(Finch finchrobot, 
                                                                          bool tempSensor,
                                                                          string tempRangeType,
                                                                          int minMaxTempThresholdValue, 
                                                                          string lightSensorsToMonitor, 
                                                                          string lightRangeType,
                                                                          int minMaxLightThresholdValue, 
                                                                          int timeToMonitorLightAndTemp)
        {
            DisplayScreenHeader("Set Light and Temperature Alarm");

                Console.WriteLine($"\tSensor(s) to monitor: {lightSensorsToMonitor}");
                Console.WriteLine($"\tTemperature range type: {tempRangeType}");
                Console.WriteLine($"\tLight range type: {lightRangeType}");
                Console.WriteLine($"\t{tempRangeType} temperature threshold value: {minMaxTempThresholdValue}");
                Console.WriteLine($"\t{lightRangeType} light threshold value: {minMaxLightThresholdValue}");
                Console.WriteLine($"\tTime to monitor light and temperature: {timeToMonitorLightAndTemp}");
                Console.WriteLine();

                Console.WriteLine("The application is ready to begin monitoring.");

                DisplayContinuePrompt();

                AlarmSystemMonitorLightAndTempSensors(finchrobot,
                                                      tempSensor,
                                                      tempRangeType,
                                                      minMaxTempThresholdValue,
                                                      timeToMonitorLightAndTemp,
                                                      lightSensorsToMonitor,
                                                      lightRangeType,
                                                      minMaxLightThresholdValue);
            DisplayContinuePrompt();

            DisplayMenuPrompt("Alarm System");
        }

        static void AlarmSystemMonitorLightAndTempSensors(Finch finchrobot, 
                                                          bool tempSensor, 
                                                          string tempRangeType, 
                                                          int minMaxTempThresholdValue, 
                                                          int timeToMonitorLightAndTemp, 
                                                          string lightSensorsToMonitor, 
                                                          string lightRangeType, 
                                                          int minMaxLightThresholdValue)
        {
            bool lightThresholdExceeded = false;
            bool tempThresholdExceeded = false;
            int elapsedTime = 0;
            double currentLightSensorValue = 0;
            double currentTempSensorValue = 0;

            if (tempSensor)
            {
                while (!lightThresholdExceeded && !tempThresholdExceeded && elapsedTime < timeToMonitorLightAndTemp)
                {
                    currentLightSensorValue = AlarmSystemGetCurrentLightSensorValue(finchrobot, lightSensorsToMonitor);
                    currentTempSensorValue = AlarmSystemGetCurrentTempSensorValue(finchrobot, tempSensor);

                    switch (lightRangeType)
                    {
                        case "minimum":
                            if (currentLightSensorValue < minMaxLightThresholdValue)
                            {
                                lightThresholdExceeded = true;
                            }
                            break;

                        case "maximum":
                            if (currentLightSensorValue > minMaxLightThresholdValue)
                            {
                                lightThresholdExceeded = true;
                            }
                            break;
                    }

                    switch (tempRangeType)
                    {
                        case "minimum":
                            if (currentTempSensorValue < minMaxTempThresholdValue)
                            {
                                tempThresholdExceeded = true;
                            }
                            break;

                        case "maximum":
                            if (currentTempSensorValue > minMaxTempThresholdValue)
                            {
                                tempThresholdExceeded = true;
                            }
                            break;
                    }
                    AlarmSystemDisplayElapsedTime(elapsedTime);
                    finchrobot.wait(1000);
                    elapsedTime++;
                }
                if (lightThresholdExceeded && !tempThresholdExceeded)
                {
                    Console.WriteLine();
                    Console.WriteLine("***************************************************************");
                    Console.WriteLine($"The {lightRangeType} light threshold of {minMaxLightThresholdValue} has been exceeded by a value of {currentLightSensorValue}.");
                    Console.WriteLine("***************************************************************");
                    Console.WriteLine();
                    Console.Beep();
                    Console.Beep();
                    Console.Beep();
                }
                else if (!lightThresholdExceeded && tempThresholdExceeded)
                {
                    Console.WriteLine();
                    Console.WriteLine("***************************************************************");
                    Console.WriteLine($"The {tempRangeType} temperature threshold of {minMaxTempThresholdValue} has been exceeded by a value of {currentTempSensorValue}.");
                    Console.WriteLine("***************************************************************");
                    Console.WriteLine();
                    Console.Beep();
                    Console.Beep();
                    Console.Beep();
                }
                else if (lightThresholdExceeded && tempThresholdExceeded)
                {
                    Console.WriteLine();
                    Console.WriteLine("***************************************************************");
                    Console.WriteLine($"The {lightRangeType} light threshold of {minMaxLightThresholdValue} has been exceeded by a value of {currentLightSensorValue}.");
                    Console.WriteLine($"The {tempRangeType} temperature threshold of {minMaxTempThresholdValue} has been exceeded by a value of {currentTempSensorValue}.");
                    Console.WriteLine("***************************************************************");
                    Console.WriteLine();
                    Console.Beep();
                    Console.Beep();
                    Console.Beep();
                }
            }
            else
            {
                Console.WriteLine("You have not turned on the temperature sensor in the Alarm System Menu.");
            }
        }

        static void AlarmSystemDisplaySetTemperatureAlarm(Finch finchrobot, 
                                                          bool tempSensor, 
                                                          string tempRangeType, 
                                                          int minMaxTempThresholdValue, 
                                                          int timeToMonitorTemp)
        {
            DisplayScreenHeader("Set Temperature Alarm");

            if (tempSensor)
            {
                Console.WriteLine($"\tTemperature range type: {tempRangeType}");
                Console.WriteLine($"\t{tempRangeType} temperature threshold value: {minMaxTempThresholdValue}");
                Console.WriteLine($"\tTime to monitor temperature: {timeToMonitorTemp}");
                Console.WriteLine();

                Console.WriteLine("The application is ready to begin monitoring.");

                DisplayContinuePrompt();

                AlarmSystemMonitorTempSensors(finchrobot, tempSensor, tempRangeType, minMaxTempThresholdValue, timeToMonitorTemp);
            }
            else if (!tempSensor)
            {
                Console.WriteLine("You have not turned on the temperature sensor in the Alarm System Menu.");
            }
            DisplayContinuePrompt();

            DisplayMenuPrompt("Alarm System");
        }

        static bool AlarmSystemMonitorTempSensors(Finch finchrobot, 
                                                  bool tempSensor, 
                                                  string tempRangeType, 
                                                  int minMaxTempThresholdValue, 
                                                  int timeToMonitorTemp)
        {
            bool thresholdExceeded = false;
            int elapsedTime = 0;
            double currentTempSensorValue = 0;

            while (!thresholdExceeded && elapsedTime < timeToMonitorTemp)
            {
                currentTempSensorValue = AlarmSystemGetCurrentTempSensorValue(finchrobot, tempSensor);

                switch (tempRangeType)
                {
                    case "minimum":
                        if (currentTempSensorValue < minMaxTempThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;

                    case "maximum":
                        if (currentTempSensorValue > minMaxTempThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                }
                AlarmSystemDisplayElapsedTime(elapsedTime);
                finchrobot.wait(1000);
                elapsedTime++;

                
            }
                if (thresholdExceeded)
                {
                    Console.WriteLine();
                    Console.WriteLine("**************************************************************************");
                    Console.WriteLine($"The {tempRangeType} temperature threshold of {minMaxTempThresholdValue} has been exceeded by a value of {currentTempSensorValue}.");
                    Console.WriteLine("**************************************************************************");
                    Console.WriteLine();
                    Console.Beep();
                    Console.Beep();
                    Console.Beep();
                }
            return thresholdExceeded;
        }

        static double AlarmSystemGetCurrentTempSensorValue(Finch finchrobot, bool tempSensor)
        {
            double currentTempSensorValue = 0;

            Console.SetCursorPosition(5, 2);
            if (tempSensor)
            {
                Console.WriteLine();
                Console.WriteLine();
                currentTempSensorValue = finchrobot.getTemperature();
                Console.WriteLine($"Temperature: {currentTempSensorValue}");
            }
            else if (!tempSensor)
            {
                Console.WriteLine("The temperature sensor has not been turned on from the Alarm System Menu.");
            }

            return currentTempSensorValue;
        }

        static int AlarmSystemDisplaySetMaximumTimeToMonitorTemperature()
        {
            string userResponse = "";
            int timeToMonitorTemp;
            bool validResponse;

            do
            {
                DisplayScreenHeader("Time to Monitor Temperature");

                Console.Write("Time to monitor temperature [seconds]:");
                validResponse = int.TryParse(Console.ReadLine(), out timeToMonitorTemp);

                if (!validResponse)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a integer.");
                    DisplayContinuePrompt();
                }
                Console.Clear();

                if (validResponse)
                {
                    do
                    {
                        DisplayScreenHeader("Time to Monitor Temperature");
                        Console.WriteLine();
                        Console.WriteLine($"The current time to monitor temperature is set to {timeToMonitorTemp}, is this correct?");
                        userResponse = Console.ReadLine();

                        if (userResponse != "no" && userResponse != "yes")
                        {
                            Console.WriteLine("Please enter 'yes' or 'no'.");
                        }
                        DisplayContinuePrompt();
                    } while (userResponse != "yes" && userResponse != "no");
                }
            } while (!validResponse || userResponse != "yes");

            return timeToMonitorTemp;
        }

        static int AlarmSystemDisplaySetMinMaxTemperatureThresholdValue(string tempRangeType, Finch finchrobot)
        {
            string userResponse = "";
            int minMaxTempThresholdValue;
            bool validResponse;

            do
            {
                DisplayScreenHeader("Min/Max Temperature Threshold Value:");

                AlarmSystemDisplayCurrentReadings(finchrobot);

                Console.Write($"{tempRangeType} temperature sensor value: ");
                validResponse = int.TryParse(Console.ReadLine(), out minMaxTempThresholdValue);

                if (!validResponse)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a integer.");
                    DisplayContinuePrompt();
                }
                Console.Clear();

                if (validResponse)
                {
                    do
                    {
                        DisplayScreenHeader("Min/Max Temperature Threshold Value:");
                        Console.WriteLine();
                        Console.WriteLine($"The {tempRangeType} temperature threshold value is set to {minMaxTempThresholdValue}, is this correct?");
                        userResponse = Console.ReadLine();

                        if (userResponse != "no" && userResponse != "yes")
                        {
                            Console.WriteLine("Please enter 'yes' or 'no'.");
                        }
                        DisplayContinuePrompt();
                    } while (userResponse != "yes" && userResponse != "no");
                }
            } while (!validResponse || userResponse != "yes");

            return minMaxTempThresholdValue;
        }

        static string AlarmSystemDisplaySetTemperatureRangeType()
        {
            string tempRangeType;
            string userResponse;

            do
            {
                do
                {
                    DisplayScreenHeader("Set Temperature Range Type");

                    Console.WriteLine("Do you want to set the temperature threshold to minimum or maximum?");
                    tempRangeType = Console.ReadLine().ToLower();

                    if (tempRangeType != "minimum" && tempRangeType != "maximum")
                    {
                        Console.WriteLine("Please enter 'minimum' or 'maximum'.");
                        DisplayContinuePrompt();
                    }
                } while (tempRangeType != "minimum" && tempRangeType != "maximum");

                do
                {
                    DisplayScreenHeader("Set Temperature Range Type");

                    Console.WriteLine($"Temperature range type is set to {tempRangeType}, is this correct?");
                    userResponse = Console.ReadLine().ToLower();

                    if (userResponse != "yes" && userResponse != "no")
                    {
                        Console.WriteLine("Please enter 'yes' or 'no'.");
                        DisplayContinuePrompt();
                    }
                } while (userResponse != "yes" && userResponse != "no");

                DisplayContinuePrompt();
            } while (userResponse != "yes");

            return tempRangeType;
        }

        static bool AlarmSystemDisplaySetTemperatureSensor(bool tempSensor)
        {
            string userResponse;

            do
            {
                DisplayScreenHeader("Activate Temperature Sensor");

                Console.WriteLine("Would you like to activate the temperature sensor?");
                userResponse = Console.ReadLine().ToLower();

                if (userResponse == "yes")
                {
                    tempSensor = true;
                }
                else if (userResponse == "no")
                {
                    tempSensor = false;
                }
                else
                {
                    Console.WriteLine("Please enter 'yes' or 'no'.");
                }

                DisplayContinuePrompt();
            } while (userResponse != "yes" && userResponse != "no");

            return tempSensor;
        }

        static void AlarmSystemDisplaySetLightAlarm(Finch finchrobot, 
                                       string lightSensorsToMonitor, 
                                       string lightRangeType, 
                                       int minMaxLightThresholdValue, 
                                       int timeToMonitorLight)
        {

            DisplayScreenHeader("Set Light Alarm");

            Console.WriteLine($"\tSensor(s) to monitor: {lightSensorsToMonitor}");
            Console.WriteLine($"\tLight range type: {lightRangeType}");
            Console.WriteLine($"\t{lightRangeType} light threshold value: {minMaxLightThresholdValue}");
            Console.WriteLine($"\tTime to monitor light: {timeToMonitorLight}");
            Console.WriteLine();

            Console.WriteLine("The application is ready to begin monitoring.");

            DisplayContinuePrompt();

            AlarmSystemMonitorLightSensors(finchrobot, lightSensorsToMonitor, lightRangeType, minMaxLightThresholdValue, timeToMonitorLight);

            DisplayContinuePrompt();

            DisplayMenuPrompt("Alarm System");
        }

        static void AlarmSystemDisplayElapsedTime(int elapsedTime)
        {
            Console.SetCursorPosition(15, 5);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Elapsed time: {elapsedTime}");
        }

        static bool AlarmSystemMonitorLightSensors(Finch finchrobot, 
                                                   string lightSensorsToMonitor, 
                                                   string lightRangeType, 
                                                   int minMaxLightThresholdValue, 
                                                   int timeToMonitorLight)
        {
            bool thresholdExceeded = false;
            int elapsedTime = 0;
            int currentLightSensorValue = 0;

            while (!thresholdExceeded && elapsedTime < timeToMonitorLight)
            {
                currentLightSensorValue = AlarmSystemGetCurrentLightSensorValue(finchrobot, lightSensorsToMonitor);

                switch (lightRangeType)
                {
                    case "minimum":
                        if (currentLightSensorValue < minMaxLightThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;

                    case "maximum":
                        if (currentLightSensorValue > minMaxLightThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                }
                AlarmSystemDisplayElapsedTime(elapsedTime);
                finchrobot.wait(1000);
                elapsedTime++;
            }

            if (thresholdExceeded)
            {
                Console.WriteLine();
                Console.WriteLine("********************************************************************");
                Console.WriteLine($"The {lightRangeType} light threshold of {minMaxLightThresholdValue} has been exceeded by a value of {currentLightSensorValue}.");
                Console.WriteLine("********************************************************************");
                Console.WriteLine();
                Console.Beep();
                Console.Beep();
                Console.Beep();
            }
            return thresholdExceeded;
        }

        static int AlarmSystemGetCurrentLightSensorValue(Finch finchrobot, string lightSensorsToMonitor)
        {
            int currentLightSensorValue = 0;

            Console.SetCursorPosition(0, 2);
            if (lightSensorsToMonitor == "both")
            {
                currentLightSensorValue = (int)finchrobot.getLightSensors().Average();
                Console.WriteLine($"Light Sensor Average: {currentLightSensorValue}");
            }
            else if (lightSensorsToMonitor == "left")
            {
                currentLightSensorValue = finchrobot.getLeftLightSensor();
                Console.WriteLine($"Left Light Sensor: {currentLightSensorValue}");
            }
            else if (lightSensorsToMonitor == "right")
            {
                currentLightSensorValue = finchrobot.getRightLightSensor();
                Console.WriteLine($"Right Light Sensor: {currentLightSensorValue}");
            }
            
                return currentLightSensorValue;
        }

        static string AlarmSystemDisplaySetSensorsToMonitor(Finch finchrobot)
        {
            string lightSensorsToMonitor;
            string userResponse;

            do
            {
                DisplayScreenHeader("Sensors to Monitor");

                AlarmSystemDisplayCurrentReadings(finchrobot);

                Console.WriteLine("Which sensors would you like to monitor?");
                Console.Write("(left, right, or both): ");
                userResponse = Console.ReadLine().ToLower();
                lightSensorsToMonitor = userResponse;

                if (userResponse != "left" && userResponse != "right" && userResponse != "both")
                {
                    Console.WriteLine("Please enter 'left', 'right', or 'both'.");
                    DisplayContinuePrompt();
                }
                Console.Clear();

                if (userResponse == "left" || userResponse == "right" || userResponse == "both")
                {
                    do
                    {
                        DisplayScreenHeader("Sensors to Monitor");
                        Console.WriteLine();
                        Console.WriteLine($"Current settings: using {lightSensorsToMonitor} sensor(s).");
                        Console.WriteLine("is this correct?");
                        userResponse = Console.ReadLine();

                        if (userResponse != "yes" && userResponse != "no")
                        {
                            Console.WriteLine();
                            Console.WriteLine("Please enter 'yes' or 'no'");
                        }

                        DisplayContinuePrompt();
                    } while (userResponse != "yes" && userResponse != "no");
                }
            } while (lightSensorsToMonitor != "left" && lightSensorsToMonitor != "right" && lightSensorsToMonitor != "both" || userResponse == "no");

            return lightSensorsToMonitor;
        }

        static string AlarmSystemDisplaySetLightRangeType()
        {
            string lightRangeType;
            string userResponse;

            do
            {
                do
                {
                    DisplayScreenHeader("Set Light Range Type");

                    Console.WriteLine("Do you want to set the light threshold to minimum or maximum?");
                    lightRangeType = Console.ReadLine().ToLower();

                    if (lightRangeType != "minimum" && lightRangeType != "maximum")
                    {
                        Console.WriteLine("Please enter 'minimum' or 'maximum'.");
                        DisplayContinuePrompt();
                    }
                } while (lightRangeType != "minimum" && lightRangeType != "maximum");

                do
                {
                    DisplayScreenHeader("Set Range Type");

                    Console.WriteLine($"Light range type is set to {lightRangeType}, is this correct?");
                    userResponse = Console.ReadLine().ToLower();

                    if (userResponse != "yes" && userResponse != "no")
                    {
                        Console.WriteLine("Please enter 'yes' or 'no'.");
                        DisplayContinuePrompt();
                    }
                } while (userResponse != "yes" && userResponse != "no");

                DisplayContinuePrompt();
            } while (userResponse != "yes");

            return lightRangeType;
        }

        static int AlarmSystemDisplaySetMinMaxLightThresholdValue(string lightRangeType, Finch finchrobot)
        {
            string userResponse = "";
            int minMaxLightThresholdValue;
            bool validResponse;

            do
            {
                DisplayScreenHeader("Min/Max Light Threshold Value:");

                AlarmSystemDisplayCurrentReadings(finchrobot);

                Console.Write($"{lightRangeType} light sensor value: ");
                validResponse = int.TryParse(Console.ReadLine(), out minMaxLightThresholdValue);

                if (!validResponse)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a integer.");
                    DisplayContinuePrompt();
                }
                Console.Clear();

                if (validResponse)
                {
                    do
                    {
                        DisplayScreenHeader("Min/Max Light Threshold Value:");
                        Console.WriteLine();
                        Console.WriteLine($"The {lightRangeType} light threshold value is set to {minMaxLightThresholdValue}, is this correct?");
                        userResponse = Console.ReadLine();

                        if (userResponse != "no" && userResponse != "yes")
                        {
                            Console.WriteLine("Please enter 'yes' or 'no'.");
                        }
                        DisplayContinuePrompt();
                    } while (userResponse != "yes" && userResponse != "no");
                }
            } while (!validResponse || userResponse != "yes");

            return minMaxLightThresholdValue;
        }

        static int AlarmSystemDisplaySetMaximumTimeToMonitorLight()
        {
            string userResponse = "";
            int timeToMonitorLight;
            bool validResponse;

            do
            {
                DisplayScreenHeader("Time to Monitor Light");

                Console.Write("Time to monitor light [seconds]:");
                validResponse = int.TryParse(Console.ReadLine(), out timeToMonitorLight);

                if (!validResponse)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a integer.");
                    DisplayContinuePrompt();
                }
                Console.Clear();

                if (validResponse)
                {
                    do
                    {
                        DisplayScreenHeader("Time to Monitor Light");
                        Console.WriteLine();
                        Console.WriteLine($"The current time to monitor light is set to {timeToMonitorLight}, is this correct?");
                        userResponse = Console.ReadLine();

                        if (userResponse != "no" && userResponse != "yes")
                        {
                            Console.WriteLine("Please enter 'yes' or 'no'.");
                        }
                        DisplayContinuePrompt();
                    } while (userResponse != "yes" && userResponse != "no");
                }
            } while (!validResponse || userResponse != "yes");

            return timeToMonitorLight;
        }

        static void AlarmSystemDisplayCurrentReadings(Finch finchrobot)
        {
            Console.WriteLine($"Current left light sensor value: {finchrobot.getLeftLightSensor()}");
            Console.WriteLine($"Current right light sensor value: {finchrobot.getRightLightSensor()}");
            Console.WriteLine();
        }

        #endregion

        #region DATA RECORDER

        static void DataRecorderDisplayMenuScreen(Finch finchrobot)
        {
            Console.CursorVisible = true;

            int numberOfDataPoints = 0;
            double dataPointFrequency = 0;
            double[] fahrenheitTemp = null;
            double[] averageLightLevel = null;

            bool quitDataRecorderMenu = false;
            bool leftLightSensor = false;
            bool rightLightSensor = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Data Recorder Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Number of Data Points");
                Console.WriteLine("\tb) Frequency of Data Points");
                Console.WriteLine("\tc) Activate Lights Sensors");
                Console.WriteLine("\td) Get Data");
                Console.WriteLine("\te) Show Data");
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
                        leftLightSensor = DataRecorderGetLeftLightSensor(leftLightSensor);
                        rightLightSensor = DataRecorderGetRightLightSensor(rightLightSensor);
                        break;

                    case "d":
                        fahrenheitTemp = new double[numberOfDataPoints];
                        averageLightLevel = new double[numberOfDataPoints];
                        fahrenheitTemp = DataRecorderDisplayGetData(numberOfDataPoints, 
                                                                    dataPointFrequency, 
                                                                    fahrenheitTemp,
                                                                    averageLightLevel,
                                                                    leftLightSensor, 
                                                                    rightLightSensor, 
                                                                    finchrobot);
                        break;

                    case "e":
                        DataRecorderDisplayData(numberOfDataPoints, fahrenheitTemp, averageLightLevel);
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

        static bool DataRecorderGetLeftLightSensor(bool leftLightSensor)
        {
            string userResponse;

            DisplayScreenHeader("Activate Left Light Sensor");

            do
            {
                Console.WriteLine();
                Console.WriteLine("Would you like to activate the left light sensor?");
                userResponse = Console.ReadLine().ToLower();

                if (userResponse == "yes")
                {
                    leftLightSensor = true;
                    Console.WriteLine("The left light sensor has been activated.");
                }
                else if (userResponse == "no")
                {
                    Console.WriteLine("The left light sensor has not been activated.");
                }
                else
                {
                    Console.WriteLine("Please enter 'yes' or 'no'.");
                }

                DisplayContinuePrompt();
            } while (userResponse != "yes" && userResponse != "no");

            return leftLightSensor;
        }

        static bool DataRecorderGetRightLightSensor(bool rightLightSensor)
        {
            string userResponse;

            DisplayScreenHeader("Activate Right Light Sensor");

            do
            {
                Console.WriteLine();
                Console.WriteLine("Would you like to activate the right light sensor?");
                userResponse = Console.ReadLine().ToLower();

                if (userResponse == "yes")
                {
                    rightLightSensor = true;
                    Console.WriteLine("The right light sensor has been activated.");
                }
                else if (userResponse == "no")
                {
                    Console.WriteLine("The right light sensor has not been activated.");
                }
                else
                {
                    Console.WriteLine("Please enter 'yes' or 'no'.");
                }

                DisplayContinuePrompt();
            } while (userResponse != "yes" && userResponse != "no");

            return rightLightSensor;
        }

        static void DataRecorderDisplayData(int numberOfDataPoints, double[] fahrenheitTemp, double[] averageLightLevel)
        {
            DisplayScreenHeader("Data");

            DataRecorderDisplayDataTable(numberOfDataPoints, fahrenheitTemp, averageLightLevel);

            DisplayContinuePrompt();
        }

        static void DataRecorderDisplayDataTable(int numberOfDataPoints, double[] fahrenheitTemp, double[] averageLightLevel)
        {
            //
            // Table Headers
            //
            Console.WriteLine(
                "Data Point".PadLeft(12) +
                "Tempature".PadLeft(12) +
                "Light Level".PadLeft(12)
                );
            Console.WriteLine(
                "__________".PadLeft(12) +
                "_________".PadLeft(12) +
                "___________".PadLeft(12)
                );

            //
            // Table Data
            //
            for (int index = 0; index < numberOfDataPoints; index++)
            {
                Console.WriteLine(
                    (index + 1).ToString().PadLeft(12) +
                    fahrenheitTemp[index].ToString("n2").PadLeft(12) +
                    averageLightLevel[index].ToString("n2").PadLeft(12)
                    );
            }
            DisplayContinuePrompt();
        }

        static double[] DataRecorderDisplayGetData(int numberOfDataPoints,
                                                   double dataPointFrequency,
                                                   double[] fahrenheitTemp,
                                                   double[] averageLightLevel,
                                                   bool leftLightSensor,
                                                   bool rightLightSensor,
                                                   Finch finchrobot)
        {
            double leftLightLevel = 0;
            double rightLightLevel = 0;
            double celsiusTemp;
            int frequencyInSeconds;

            DisplayScreenHeader("Get Data");

            Console.WriteLine($"Data point frequency: {dataPointFrequency}");
            Console.WriteLine($"Number of data points: {numberOfDataPoints}");

            Console.WriteLine("The finch robot is ready to record temperatures and light levels.");
            DisplayContinuePrompt();

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                celsiusTemp = finchrobot.getTemperature();
                fahrenheitTemp[index] = DataRecorderConvertCelsiusToFahrenheit(celsiusTemp);
                Console.WriteLine($"Temperature #{index + 1}: {fahrenheitTemp[index]} fahrenheit");
                frequencyInSeconds = (int)(dataPointFrequency * 1000);

                if (leftLightSensor)
                {
                    leftLightLevel = finchrobot.getLeftLightSensor();
                }

                if (rightLightSensor)
                {
                    rightLightLevel = finchrobot.getRightLightSensor();
                }

                averageLightLevel[index] = (leftLightLevel + rightLightLevel) / 2;
                Console.WriteLine($"Light Level #{index + 1}: {averageLightLevel[index]}");
                finchrobot.wait(frequencyInSeconds);
            }

            Console.WriteLine();
            Console.WriteLine("The data recording has been completed.");

            DisplayContinuePrompt();

            Console.WriteLine();
            Console.WriteLine("Current data");
            DataRecorderDisplayDataTable(numberOfDataPoints, fahrenheitTemp, averageLightLevel);

            Console.WriteLine();
            Console.WriteLine($"Average Temperature: {fahrenheitTemp.Average()}");
            Console.WriteLine($"Average Light Level: {averageLightLevel.Average()}");

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
