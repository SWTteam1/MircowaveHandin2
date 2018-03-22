using System;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT5_Door
    {
        private IDoor _door;
        private IUserInterface _userInterface;
        private ILight _light;
        private IDisplay _display;
        private ICookController _cookController;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        [SetUp]
        public void Setup()
        {
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            _cookController = Substitute.For<ICookController>();
            _powerTube = Substitute.For<IPowerTube>();
            _timer = Substitute.For<ITimer>();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _door = new Door();
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);

        }

        [Test]
        public void DoorOpen()
        {
            _door.Open();
            _light.Received().TurnOn();
        }

        [Test]
        public void DoorClose()
        {
            _door.Open();
            _door.Close();
            _light.Received().TurnOff();
        }
    }
}