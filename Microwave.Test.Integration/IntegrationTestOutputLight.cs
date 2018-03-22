﻿using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IntegrationTestOutputLight
    {
        private IOutput _output;
        private ILight _light;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _light = new Light(_output);
        }

        [Test]
        public void LightOn()
        {
            _light.TurnOn();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        public void LightOff()
        {
            _light.TurnOn();
            _light.TurnOff();
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }
    }
}