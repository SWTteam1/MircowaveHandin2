using System;
using System.Threading;
using NUnit.Framework;
using NSubstitute; 
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT4_UserInterface_CookController
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private IDisplay _display;
        private CookController _uutCookController;
        private IOutput _output;
        private ILight _light;
        private IUserInterface _uutUserInterface;
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
         
            _timer = new Timer();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _light = new Light(_output);
            _uutCookController = new CookController(_timer, _display, _powerTube);
            _uutUserInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, 
                _door, _display, _light, _uutCookController);
            _uutCookController.UI = _uutUserInterface;
        }


        [Test]
        public void StartCooking()
        {
            _uutUserInterface.OnPowerPressed(_powerButton, EventArgs.Empty);
            _uutUserInterface.OnTimePressed(_timeButton, EventArgs.Empty);
            _uutUserInterface.OnStartCancelPressed(_startCancelButton, EventArgs.Empty);
            
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains((50 / 7).ToString())));
        }


        [Test]
        public void CookingIsDone()
        {
            _uutUserInterface.OnPowerPressed(_powerButton, EventArgs.Empty);
            _uutUserInterface.OnTimePressed(_timeButton, EventArgs.Empty);
            _uutUserInterface.OnStartCancelPressed(_startCancelButton, EventArgs.Empty);
            _uutCookController.Stop();
            
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains(("Display cleared"))));
        }
    }
}