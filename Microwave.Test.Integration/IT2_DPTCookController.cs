using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT2_DPTCookController
    {
        private IOutput _output;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private ICookController _cookController;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _timer = new Timer();
            _cookController = new CookController(_timer, _display, _powerTube);
        }

        [TestCase(12,30)]
        public void StartCooking_StartTimer_PowertubeOn(int min, int power)
        {
            _cookController.StartCooking(power, min);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("PowerTube works with 30 %")));
        }

        [TestCase(12,30)]
        public void OnTimerTick_(int min, int power)
        {
            _cookController.StartCooking(power, min);
            Thread.Sleep(1000);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Display shows: 00:59")));
        }

        //[TestCase(12, 30)]
        //public void Display_ShowTime(int min, int sec)
        //{
        //    _display.ShowTime(min, sec);
        //    _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Display shows: 12:30")));
        //}

        //[Test]
        //[TestCase(50)]
        //public void Display_ShowPower(int power)
        //{
        //    _display.ShowPower(power);
        //    _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Display shows: 50")));
        //}
    }
}