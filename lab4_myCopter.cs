/*
1. Create a new class Dispatcher to manage the already created functionality of the
program. Functionality management should be carried out by sending messages using
events.
2. In the refined program it is necessary to use the delegate mechanism for signing methods
on events. The program is proposed to publish several events and make sure that all the
necessary methods are called.
3. The program should demonstrate the performance of all new methods (output data to the
console).
*/

using System;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;
using static MyCopter;

public delegate void MessageDelegate(string? message); // ? = shows that message parameter can be nullable
public class Dispatcher
{
    public event MessageDelegate? SendMessage; // ? - indicated that the event is nullable

    public void RunningAllTheMethods()
    {
        MyCopter helicopter_1 = new MyCopter(2, 644, 6, 20);
        MyAirplane airplane_1 = new MyAirplane("PST-65", 4);
        Transport airplane_2 = new MyAirplane("SPy-183", 2, 125, 6000, 40000);

        bool hasError = false;

       // SendMessage += Console.WriteLine; // publisher-subscriber

        if (SendMessage != null)
        {
            try
            {
                helicopter_1.speedChange(50);
                helicopter_1.speedChange(200);
                helicopter_1.altitudeChange(5);
                helicopter_1.loadChange(15);

                airplane_1.speedChange(125);
                airplane_1.altitudeChange(6000);
                airplane_1.loadChange(40000);

                airplane_2.speedChange(100);
                airplane_2.altitudeChange(3000);
                airplane_2.loadChange(20000);
            }
            catch (InvalidOperationException ex)
            {
                SendMessage.Invoke("Error: " + ex.Message);
                hasError = true;
            }

            if (!hasError)
            {
                helicopter_1.Info();
                SendMessage.Invoke(helicopter_1.ToString());
                SendMessage.Invoke(" ");

                airplane_1.Info();
                SendMessage.Invoke(airplane_1.ToString());
                SendMessage.Invoke(" ");

                airplane_2.Info();
                SendMessage.Invoke(airplane_2.ToString());
                SendMessage.Invoke(" ");
            }

            try
            {
                helicopter_1.cargoDischarge(5);
            }
            catch (InvalidOperationException ex)
            {
                SendMessage.Invoke("Error: " + ex.Message);
                hasError = true;
            }

            if (!hasError)
            {
                SendMessage.Invoke("MyCopter after changes:");
                SendMessage.Invoke(helicopter_1.ToString());
            }
        }
    }
}

public abstract class Transport

{
    protected int MaximumSpeed;
    protected int MaximumFlightAltitude;
    protected int MaximumLoadCapacity;

    public int currentSpeed = 0;
    public int currentFlightAltitude = 0;
    public int currentLoadCapacity = 0;

    public abstract void speedChange(int additionalSpeed);

    public abstract void altitudeChange(int additionalAltitude);

    public abstract void loadChange(int additionalLoad); // current load (on the ground)

    public virtual void Info()
    {
        Console.WriteLine("Info");
    }
}

public class MyCopter : Transport
{
    private int Screws;

    private bool dischargeCheck = false;

    public MyCopter(int screws, int maximumSpeed, int maximumFlightAltitude, int maximumLoadCapacity)
    {
        Screws = screws;
        MaximumSpeed = maximumSpeed;
        MaximumFlightAltitude = maximumFlightAltitude;
        MaximumLoadCapacity = maximumLoadCapacity;
    }

    public override void speedChange(int additionalSpeed) // it returns nothing and accepts additionalSpeed
    {
        currentSpeed += additionalSpeed;
        if (additionalSpeed < 0)
        {
            throw new InvalidOperationException("The speed of the helicopter cannot be less than zero");
        }

        if (currentSpeed > MaximumSpeed)
        {
            throw new InvalidOperationException("The speed of the helicopter cannot be more than the maximum speed");
        }
    }

    public override void altitudeChange(int additionalAltitude)
    {
        currentFlightAltitude += additionalAltitude;
        if (additionalAltitude < 0)
        {
            throw new InvalidOperationException("The altitude of the helicopter cannot be less than zero");
        }

        if (currentFlightAltitude > MaximumFlightAltitude)
        {
            throw new InvalidOperationException("The altitude of the helicopter cannot be more than the maximum altitude");
        }
    }

    public override void loadChange(int additionalLoad) // current load (on the ground)
    {
        currentLoadCapacity += additionalLoad;
        if (additionalLoad < 0)
        {
            throw new InvalidOperationException("The load of the helicopter cannot be less than zero");
        }

        if (currentLoadCapacity > MaximumLoadCapacity)
        {
            throw new InvalidOperationException("The load of the helicopter cannot be more than the maximum load");
        }
    }

    public void cargoDischarge(int cargoDischargeLoad)
    {
        dischargeCheck = false;

        if (cargoDischargeLoad < 0)
        {
            throw new InvalidOperationException("The cargo discharge load cannot be less than zero");
        }

        if (currentFlightAltitude < 3)
        {
            throw new InvalidOperationException("The helicopter cannot make a cargo discharge!");
        }
        else if (currentLoadCapacity - cargoDischargeLoad < 0)
        {
            throw new InvalidOperationException("The helicopter cannot discharge more cargo than it is currently carrying");
        }
        else
        {
            currentLoadCapacity -= cargoDischargeLoad;
            dischargeCheck = true;
        }
    }

    public override void Info()
    {
        Console.WriteLine("Info of the MyCopter");
    }

    public override string ToString()
    {
        return $"MyCopter(Screws={Screws},Maximum Speed={MaximumSpeed},Maximum Flight Altitude={MaximumFlightAltitude},Maximum Load Capacity={MaximumLoadCapacity},Current Speed={currentSpeed}," +
            $"Current Flight Altitude={currentFlightAltitude},Current Load Capacity={currentLoadCapacity},Cargo Discharge={dischargeCheck})";
    }

    public class MyAirplane : Transport
    {
        private string Model;
        private int NumberOfEngines; // 1 - 4

        public MyAirplane(string model, int numberOfEngines, int maximumSpeed, int maximumFlightAltitude, int maximumLoadCapacity)
        {
            Model = model;
            NumberOfEngines = numberOfEngines;
            MaximumSpeed = maximumSpeed;
            MaximumFlightAltitude = maximumFlightAltitude;
            MaximumLoadCapacity = maximumLoadCapacity;
        }

        public MyAirplane(string model, int numberOfEngines)
        {
            Model = model;
            NumberOfEngines = numberOfEngines;
            MaximumSpeed = 125;
            MaximumFlightAltitude = 12000;
            MaximumLoadCapacity = 80000;
        }

        public override void speedChange(int additionalSpeed)
        {
            currentSpeed += additionalSpeed;
            if (additionalSpeed < 0)
            {
                throw new InvalidOperationException("The speed of the airplane cannot be less than zero");
            }

            if (currentSpeed > MaximumSpeed)
            {
                throw new InvalidOperationException("The speed of the airplane cannot be more than the maximum speed");
            }
        }

        public override void altitudeChange(int additionalAltitude)
        {
            currentFlightAltitude += additionalAltitude;
            if (additionalAltitude < 0)
            {
                throw new InvalidOperationException("The altitude of the airplane cannot be less than zero");
            }

            if (currentFlightAltitude > MaximumFlightAltitude)
            {
                throw new InvalidOperationException("The altitude of the airplane cannot be more than the maximum altitude");
            }
        }

        public override void loadChange(int additionalLoad) // current load (on the ground)
        {
            currentLoadCapacity += additionalLoad;
            if (additionalLoad < 0)
            {
                throw new InvalidOperationException("The load of the airplane cannot be less than zero");
            }

            if (currentLoadCapacity > MaximumLoadCapacity)
            {
                throw new InvalidOperationException("The load of the airplane cannot be more than the maximum load");
            }
        }

        public override void Info()
        {
            Console.WriteLine("Info of the MyAirplane");
        }

        public override string ToString()
        {
            return $"MyAirplane(Model={Model}, Number Of Engines={NumberOfEngines}, Maximum Speed={MaximumSpeed}, Maximum Flight Altitude={MaximumFlightAltitude}, " +
                $"Maximum Load Capacity={MaximumLoadCapacity},Current Speed={currentSpeed}, Current Flight Altitude={currentFlightAltitude}, Current Load Capacity={currentLoadCapacity})";
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Dispatcher dispatcher = new Dispatcher();
        dispatcher.SendMessage += Console.WriteLine;

        dispatcher.RunningAllTheMethods();
    }
}
