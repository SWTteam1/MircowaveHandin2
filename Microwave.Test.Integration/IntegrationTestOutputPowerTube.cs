using System;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IntegrationTestOutputPowerTube
    {
        private IOutput _output;
        private IPowerTube _powerTube;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _powerTube = new PowerTube(_output);

        }

        [Test]
        public void PowerTubeTurnOn()
        {
            _powerTube.TurnOn(50);
            _output.Received().OutputLine(Arg.Is<string>(str=>str.Contains("PowerTube works with 50 %")));
        }

        [Test]
        public void PowerTubeTurnOff()
        {
            _powerTube.TurnOn(50);
            _powerTube.TurnOff();
            _output.Received().OutputLine(Arg.Is<string>(str=>str.Contains("PowerTube turned off")));
        }

    }
}