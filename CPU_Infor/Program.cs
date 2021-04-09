using System;
using System.Linq;
using System.Management;
using OpenHardwareMonitor.Collections;
using OpenHardwareMonitor.Hardware;
using OxyPlot;
using OxyPlot.Series;
class Program
{
    static void Main(string[] args)
    {



        Computer c = new Computer { CPUEnabled = true };


        c.Open();
        foreach (var hardwareItem in c.Hardware)
        {
            hardwareItem.Update();
            hardwareItem.GetReport();

            Console.WriteLine(hardwareItem.GetReport());

            var series = new LineSeries();

            foreach (var sensor in hardwareItem.Sensors)
            {
                if (sensor.SensorType == SensorType.Temperature)
                {
                    Console.WriteLine("{0} {1} {2} = {3}", sensor.Name, sensor.Hardware, sensor.SensorType, sensor.Value);

                }

            }

            foreach (var hardware in c.Hardware)
            {
                if (hardware.HardwareType == HardwareType.CPU)
                {
                    hardware.Update();
                    foreach (var sensors in hardware.Sensors)
                    {
                        if (sensors.SensorType == SensorType.Load)
                            Console.WriteLine(sensors.Name + ":" + sensors.Value.GetValueOrDefault());
                    }
                }
            }


            Console.ReadLine();

        }
    }
}
