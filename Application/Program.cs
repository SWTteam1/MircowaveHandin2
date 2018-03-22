using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Controllers;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup all the objects, 
            var door = new Door();
            var output = new Output();
            var powerButton = new Button();
            var timeButton = new Button();
            var scButton = new Button();

            var display = new Display(output);
            var light = new Light(output);
            var timer = new Timer();
            var powerTube = new PowerTube(output);
            var cookController = new CookController(timer, display, powerTube);
            var userInterface = new UserInterface(powerButton,timeButton,scButton,door,display,light,cookController);
            cookController.UI = userInterface;

            // Simulate user activities
            door.Open();
            door.Close();
            powerButton.Press();
            timeButton.Press();
            scButton.Press();


            // Wait while the classes, including the timer, do their job
            System.Console.WriteLine("Tast enter når applikationen skal afsluttes");
            System.Console.ReadLine();
        }
    }
}