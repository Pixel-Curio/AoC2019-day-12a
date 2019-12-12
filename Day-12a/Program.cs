using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_12a
{
    class Program
    {
        static void Main(string[] args)
        {
            const int steps = 1000;

            var lines = File.ReadAllLines(@"day12a-input.txt");

            //lines = new[]
            //{
            //    "<x=-1, y=0, z=2>",
            //    "<x=2, y=-10, z=-7>",
            //    "<x=4, y=-8, z=8>",
            //    "<x=3, y=5, z=-1>"
            //};

            int bodyCount = lines.Length;

            Regex reg = new Regex(@"<x=([\-0-9]*), y=([\-0-9]*), z=([\-0-9]*)>");

            var bodies = new Body[bodyCount];
            for (int i = 0; i < bodyCount; i++)
            {
                var result = reg.Match(lines[i]);
                var position = new Vector3
                {
                    X = int.Parse(result.Groups[1].Value),
                    Y = int.Parse(result.Groups[2].Value),
                    Z = int.Parse(result.Groups[3].Value)
                };
                bodies[i] = new Body { Position = position, Velocity = Vector3.Zero, Gravity = Vector3.Zero };
            }

            //for every step
            for (int i = 0; i < steps; i++)
            {
                //add gravity for every body in its current position
                //for each primary body
                for (int x = 0; x < bodyCount; x++)
                {
                    //for each secondary body
                    for (int y = x + 1; y < bodyCount; y++)
                    {
                        if (bodies[x].Position.X > bodies[y].Position.X)
                        {
                            bodies[x].Velocity.X += -1;
                            bodies[y].Velocity.X += 1;
                        }
                        else if (bodies[x].Position.X < bodies[y].Position.X)
                        {
                            bodies[x].Velocity.X += 1;
                            bodies[y].Velocity.X += -1;
                        }

                        if (bodies[x].Position.Y > bodies[y].Position.Y)
                        {
                            bodies[x].Velocity.Y += -1;
                            bodies[y].Velocity.Y += 1;
                        }
                        else if (bodies[x].Position.Y < bodies[y].Position.Y)
                        {
                            bodies[x].Velocity.Y += 1;
                            bodies[y].Velocity.Y += -1;
                        }

                        if (bodies[x].Position.Z > bodies[y].Position.Z)
                        {
                            bodies[x].Velocity.Z += -1;
                            bodies[y].Velocity.Z += 1;
                        }
                        else if (bodies[x].Position.Z < bodies[y].Position.Z)
                        {
                            bodies[x].Velocity.Z += 1;
                            bodies[y].Velocity.Z += -1;
                        }

                        //bodies[x].gravity.x = bodies[x].position.x > bodies[y].position.x ? -1 : 1;
                        //bodies[y].gravity.x = bodies[x].position.x < bodies[y].position.x ? -1 : 1;

                        //bodies[x].gravity.y = bodies[x].position.y > bodies[y].position.y ? -1 : 1;
                        //bodies[y].gravity.y = bodies[x].position.y < bodies[y].position.y ? -1 : 1;

                        //bodies[x].gravity.z = bodies[x].position.z > bodies[y].position.z ? -1 : 1;
                        //bodies[y].gravity.z = bodies[x].position.z < bodies[y].position.z ? -1 : 1;
                    }
                }

                //apply gravity
                //for (int x = 0; x < bodyCount; x++) bodies[x].velocity += bodies[x].gravity;

                //apply velocity
                for (int x = 0; x < bodyCount; x++) bodies[x].Position += bodies[x].Velocity;

                //print positions
                Console.WriteLine($"After {i} steps:");
                for (int x = 0; x < bodyCount; x++)
                {
                    Console.WriteLine($"pos=<x={bodies[x].Position.X}, y={bodies[x].Position.Y}, z={bodies[x].Position.Z}>, vel=<x={bodies[x].Velocity.X}, y={bodies[x].Velocity.Y}, z={bodies[x].Velocity.Z}>");
                }
            }
            Console.WriteLine($"Total energy: {bodies.Sum(x => x.TotalEnergy)}");
        }
    }

    class Body
    {
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 Gravity { get; set; }

        public int PotentialEnergy => Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z);
        public int KineticEnergy => Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z);
        public int TotalEnergy => PotentialEnergy * KineticEnergy;
    }

    class Vector3
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public static Vector3 Zero => new Vector3 { X = 0, Y = 0, Z = 0 };

        public static Vector3 operator +(Vector3 v1, Vector3 v2) =>
            new Vector3
            {
                X = v1.X + v2.X,
                Y = v1.Y + v2.Y,
                Z = v1.Z + v2.Z
            };
    }
}
