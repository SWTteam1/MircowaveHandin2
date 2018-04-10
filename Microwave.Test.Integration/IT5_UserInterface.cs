using NUnit.Framework;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using NSubstitute;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT5_UserInterface
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private IDisplay _display;
        private CookController _cookController;
        private IOutput _output;
        private ILight _light;
        private IUserInterface _userInterface;
        private ITimer _timer;
        private IPowerTube _powerTube;

        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _door = new Door();
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
           
            _light = new Light(_output);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _cookController.UI = _userInterface;
        }


        [Test]
        public void DoorOpened()
        {
            _door.Open();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }


        [Test]
        public void DoorClosed()
        {
            _door.Open();
            _door.Close();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }

        [Test]
        public void PowerButton_Pressed()
        {
            _powerButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains((50 / 7).ToString())));
        }

        [Test]
        public void TimeButton_Pressed()
        {
            _powerButton.Press();
            _timeButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Display shows: 01:00")));
        }

        [Test]
        public void StartCancelButton_Pressed()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("PowerTube works with 50 W")));
        }
    }
}