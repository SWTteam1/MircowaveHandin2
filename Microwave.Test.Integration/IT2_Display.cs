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
        private Display _display;
        private ICookController _cookController;
        private IOutput _output;
        private ILight _light;
        private UserInterface _uutUserInterface;

        [SetUp]
        public void SetUp()
        {
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _output = Substitute.For<IOutput>();
            _cookController = Substitute.For<ICookController>();
            _light = new Light(_output);
            _display = new Display(_output);
            _uutUserInterface = new UserInterface(_powerButton, _timeButton, 
                _startCancelButton, _door, _display, _light, _cookController);
        }

        
        [TestCase(12,30)]
        public void CookingDone_ClearDisplay(int min, int power)
        {
            //Sets state to "COOKING"
            _uutUserInterface.OnPowerPressed(_powerButton, EventArgs.Empty);
            _uutUserInterface.OnTimePressed(_timeButton, EventArgs.Empty);
            _uutUserInterface.OnStartCancelPressed(_startCancelButton, EventArgs.Empty);
            
            _uutUserInterface.CookingIsDone();

            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Display cleared")));
        }
    }
}