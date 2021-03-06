﻿using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Interfaced;
using HelloWorld.Interface;

namespace HelloWorld.Program
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var system = ActorSystem.Create("MySystem", "akka.suppress-json-serializer-warning=on");
            DeadRequestProcessingActor.Install(system);
            TestAsync(system).Wait();
        }

        private static async Task TestAsync(ActorSystem system)
        {
            // Create GreetingActor and make a reference pointing to an actor.
            var actor = system.ActorOf<GreetingActor>();
            var greeter = actor.Cast<GreeterRef>();

            // Make some noise
            Console.WriteLine(await greeter.Greet("World"));
            Console.WriteLine(await greeter.Greet("Actor"));
            Console.WriteLine(await greeter.GetCount());
        }
    }
}
