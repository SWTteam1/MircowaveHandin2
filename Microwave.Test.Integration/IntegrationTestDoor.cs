using System;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IntegrationTestDoor
    {
        private IDoor _door;
        private IUserInterface _userInterface;
        private ILight _light;
        private IDisplay _display;
        private ICookController _cookController;

        [SetUp]
        public void Setup()
        {
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            _cookController = Substitute.For<ICookController>();
            _userInterface = Substitute.For<IUserInterface>();
            _door = new Door();
        }

        [Test]
        public void DoorOpen()
        {
            _door.Open();
            //_userInterface.Received().OnDoorOpened(this, EventArgs.Empty);
            _light.Received().TurnOn();
        }

        [Test]
        public void DoorClose()
        {
            //_door.Open();
            _door.Close();
            _userInterface.Received().OnDoorClosed(this, EventArgs.Empty);
            //_light.Received().TurnOff();
        }
    }
}