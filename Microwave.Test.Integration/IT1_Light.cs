using System;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT1_Light
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private IDisplay _display;
        private ICookController _cookController;
        private IOutput _output;
        private ILight _uutLight;
        private IUserInterface _userInterface;

        [SetUp]
        public void SetUp()
        {
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _display = Substitute.For<IDisplay>();
            _cookController = Substitute.For<ICookController>();
            _output = Substitute.For<IOutput>();
            _uutLight = new Light(_output);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _uutLight, _cookController);
        }

        [Test]
        public void DoorOpen_LightOn()
        {
            _userInterface.OnDoorOpened(_door, EventArgs.Empty);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        [Test]
        public void DoorClosed_LightOff()
        {
            _userInterface.OnDoorOpened(_door, EventArgs.Empty);
            _userInterface.OnDoorClosed(_door, EventArgs.Empty);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }
    }
}