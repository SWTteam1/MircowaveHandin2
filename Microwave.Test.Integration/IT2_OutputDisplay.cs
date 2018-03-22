using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT2_OutputDisplay
    {
        private IOutput _output;
        private IDisplay _display;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
        }

        [Test]
        [TestCase(12, 30)]
        public void Display_ShowTime(int min, int sec)
        {
            _display.ShowTime(min, sec);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Display shows: 12:30")));
        }

        [Test]
        [TestCase(50)]
        public void Display_ShowPower(int power)
        {
            _display.ShowPower(power);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Display shows: 50")));
        }
    }
}