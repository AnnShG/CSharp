/*
 “Classes and Encapsulation
1. create and include in the program another similar class, which is characterized 
        by attributes that partially coincide with the attributes of the already 
        created class, but partially differ (or extend them).
2. For each instance of this new class, methods must be created with the names that
        coincide with similar methods from an already existing class. You must also 
        add at least one new polymorphic method. A new class must have two constructors, with 
        a different number of parameters.
3. The program should demonstrate the performance of all methods of the new class
        (output data to the console)
 */

using System;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;

public class Transport

{
    protected int MaximumSpeed;
    protected int MaximumFlightAltitude;
    protected int MaximumLoadCapacity;

    public int currentSpeed = 0;
    public int currentFlightAltitude = 0;
    public int currentLoadCapacity = 0;

    public virtual void speedChange(int additionalSpeed) // we need a virtual keyword so derived classes could inheit it
    {
        
    }

    public virtual void altitudeChange(int additionalAltitude)
    {

    }

    public virtual void loadChange(int additionalLoad) // current load (on the ground)
    {

    }

    public virtual void cargoDischarge(int cargoDischargeLoad)
    {

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

    public override void cargoDischarge(int cargoDischargeLoad)
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

        public override string ToString()
        {
            return $"MyAirplane(Model={Model}, Number Of Engines={NumberOfEngines}, Maximum Speed={MaximumSpeed}, Maximum Flight Altitude={MaximumFlightAltitude}, " +
                $"Maximum Load Capacity={MaximumLoadCapacity},Current Speed={currentSpeed}, Current Flight Altitude={currentFlightAltitude}, Current Load Capacity={currentLoadCapacity})";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Transport helicopter_1 = new MyCopter(2, 644, 6, 20);
            Transport helicopter_2 = new MyCopter(4, 500, 3, 10);

            Transport airplane_1 = new MyAirplane("PST-65", 4);
            Transport airplane_2 = new MyAirplane("SSSPy-65", 2, 125, 6000, 40000);

            bool hasError = false;
            try
            {
                helicopter_1.speedChange(50);
                helicopter_1.speedChange(200);
                airplane_1.speedChange(125);

                helicopter_1.altitudeChange(5);
                airplane_1.altitudeChange(6000);

                helicopter_1.loadChange(15);
                airplane_1.loadChange(40000);

                helicopter_2.speedChange(300);
                airplane_2.speedChange(100);

                helicopter_2.altitudeChange(3);
                airplane_2.altitudeChange(3000);

                helicopter_2.loadChange(8);
                airplane_2.loadChange(20000);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                hasError = true;
            }
            if (!hasError)
            {
                Console.WriteLine(helicopter_1);
                Console.WriteLine();
                Console.WriteLine(helicopter_2);
                Console.WriteLine();

                Console.WriteLine(airplane_1);
                Console.WriteLine();
                Console.WriteLine(airplane_2);
                Console.WriteLine();
            }

            try
            {
                helicopter_1.cargoDischarge(5);

                helicopter_2.cargoDischarge(2);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                hasError = true;
            }
            if (!hasError)
            {
                Console.WriteLine("MyCopter after changes:");
                Console.WriteLine(helicopter_1);
                Console.WriteLine();
                Console.WriteLine(helicopter_2);

            }
        }
    }
}
