using System;
using System.Threading;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT2_Display
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private IDisplay _uutDisplay;
        private ICookController _cookController;
        private IOutput _output;
        private ILight _light;
        private IUserInterface _userInterface;
        private ITimer _timer;

        [SetUp]
        public void SetUp()
        {
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _output = Substitute.For<IOutput>();
            _light = Substitute.For<ILight>();
            _timer = Substitute.For<ITimer>();
            _cookController = ;
            _uutDisplay = new Display(_output);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _uutDisplay, _light, _cookController);
        }

        //Frank vil du hjælpe?
        [TestCase(12,30)]
        public void CookingDone_ClearDisplay(int min, int power)
        {
            _cookController.StartCooking(power, min);
            _timer.TimeRemaining.Returns(0);
            
            _userInterface.CookingIsDone();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Display cleared")));
        }

        [TestCase(12,30)]
        public void Cooking_ShowTime(int min, int power)
        {
            _cookController.StartCooking(power, min);
            _timer.TimerTick += Raise.EventWith(_timer, EventArgs.Empty);
            Thread.Sleep()

        }
    }
}