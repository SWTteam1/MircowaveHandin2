using System;
using System.Threading;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using NUnit.Framework;
using NSubstitute;
using Timer = MicrowaveOvenClasses.Boundary.Timer;


namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT3_DPT
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private IDisplay _display;
        private ICookController _cookController;
        private IOutput _output;
        private ILight _light;
        private IUserInterface _userInterface;
        private ITimer _timer;
        private IPowerTube _powerTube;



        [SetUp]
        public void SetUp()
        {

            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _output = Substitute.For<IOutput>();
            _userInterface = Substitute.For<IUserInterface>();
            _light = new Light(_output);
            _timer = new Timer();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _cookController = new CookController(_timer, _display, _powerTube, _userInterface);



        }

        [TestCase(12, 30)]
        public void DisplayShowTime(int time, int power)
        {
            _cookController.StartCooking(power, time);
            Thread.Sleep(1100);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Display shows: 00:11")));
        }


        [TestCase(1, 30)]
        public void TimerExpired(int time, int power)
        {
            _cookController.StartCooking(power, time);
            Thread.Sleep(1000);
            _output.Received().OutputLine(Arg.Is<string>(str=> str.Contains("PowerTube turned off")));
        }



        [TestCase(10,30)]
        public void PowerTubeIsOn(int time, int power)
        {
           _cookController.StartCooking(power, time);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"PowerTube works with {power} %")));

        }
    }
}