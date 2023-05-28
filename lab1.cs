//Create class MyCopter (helicopter), which is characterized by the number of screws, speed (current and
//maximum), flight altitude (current and maximum), load capacity(maximum), load(current).Copies of
//the helicopter must have methods for changing the speed, altitude and current load (on the ground), as
//well as the method of cargo discharge in flight.

using System;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;

public class MyCopter
{
    int Screws;
    int MaximumSpeed; // 644
    int MaximumFlightAltitude; // 6
    int MaximumLoadCapacity; // 20

    private int currentSpeed = 0;
    private int currentFlightAltitude = 0;
    private int currentLoadCapacity = 0;
    private bool dischargeCheck = false;



    public MyCopter(int screws, int maximumSpeed, int maximumFlightAltitude, int maximumLoadCapacity)
    {
        Screws = screws;
        MaximumSpeed = maximumSpeed;
        MaximumFlightAltitude = maximumFlightAltitude;
        MaximumLoadCapacity = maximumLoadCapacity;
    }

    public void speedChange(int additionalSpeed) // it returns nothing and accepts additionalSpeed
    {
        currentSpeed += additionalSpeed;
        if (additionalSpeed < 0)
        {
            throw new ArgumentException("The speed of the helicopter cannot be less than zero");
        }

        if (currentSpeed > MaximumSpeed)
        {
            throw new InvalidOperationException("The speed of the helicopter cannot be more than the maximum speed");
        }
    }

    public void altitudeChange(int additionalAltitude)
    {
        currentFlightAltitude += additionalAltitude;
        if (additionalAltitude < 0)
        {
            throw new ArgumentException("The altitude of the helicopter cannot be less than zero");
        }

        if (currentFlightAltitude > MaximumFlightAltitude)
        {
            throw new InvalidOperationException("The altitude of the helicopter cannot be more than the maximum altitude");
        }
    }

    public void loadChange(int additionalLoad) // current load (on the ground)
    {
        currentLoadCapacity += additionalLoad;
        if (additionalLoad < 0)
        {
            throw new ArgumentException("The load of the helicopter cannot be less than zero");
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
            throw new ArgumentException("The cargo discharge load cannot be less than zero");
        }

        if (currentFlightAltitude < 3)
        {
            throw new ArgumentException("The helicopter cannot make a cargo discharge!");
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
}
class Program
{
    static void Main(string[] args)
    {
        MyCopter helicopter_1 = new MyCopter(2, 644, 6, 20);
        MyCopter helicopter_2 = new MyCopter(4, 500, 3, 10);

        bool hasError = false;
        try
        {
            helicopter_1.speedChange(50);
            helicopter_1.speedChange(200);

            helicopter_1.altitudeChange(5);
            helicopter_1.loadChange(15);

            helicopter_2.speedChange(300);

            helicopter_2.altitudeChange(3);
            helicopter_2.loadChange(8);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            hasError = true;
        }
        catch (ArgumentException ex)
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
        }

        try
        {
            helicopter_1.cargoDischarge(5);

            helicopter_2.cargoDischarge(2);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            hasError = true;
        }
        if (!hasError)
        {
            Console.WriteLine("After changes:");
            Console.WriteLine(helicopter_1);
            Console.WriteLine();
            Console.WriteLine(helicopter_2);

        }

    }
}

