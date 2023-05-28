/*
 It is necessary to create a new program providing for exception handling. Describe the new
class - user exception. In the existing classes, in some methods, to provide for the
occurrence of this exception, which signals an abnormal program behavior. Finalize the
Main() method of the main class in order to intercept and handle a custom exception. In
the main class, implement the All-Catch Exceptions architecture. To test the program,
add code that causes one of the standard exceptions.
 */

using System;

public delegate void MessageDelegate(string? message); // ? - shows that message parameter can be nullable

public class Dispatcher
{
    public event MessageDelegate? SendMessage; // ? - indicates that the event is nullable

    public void RunAllTheOperations()
    {
        bool hasError = false;

        Money first = new Money(30, 25);
        Money second = new Money(40, 50);
        Money money = new Money(150, 15);
        Currency currency = new Currency("US Dollar", "$", 1.0);

        if (SendMessage != null)
        {
            SendMessage.Invoke($"First: {first}");
            SendMessage.Invoke($"Second: {second}");
            SendMessage.Invoke("");


            first.Info();
            second.Info();

            SendMessage.Invoke("");

            Money sum = first.Add(second);
            SendMessage.Invoke($"Sum: {sum}");

            Money difference = first.Subtract(second);
            SendMessage.Invoke($"Difference: {difference}");

            double division = first.Divide(second);
            SendMessage.Invoke($"Division: {division}");

            Money? dividedByNumber = null; // it can be nullable
            Money? multiply = null;

            try
            {
                dividedByNumber = first.Divide(0);
                multiply = first.Multiply(0.5);
            }
            catch (InvalidOperationException ex)
            {
                SendMessage.Invoke("Error: " + ex.Message);
                hasError = true;
            }

            if (!hasError)
            {
                SendMessage.Invoke($"First divided by a given number: {dividedByNumber}");
                SendMessage.Invoke($"First multiplied by a given number: {multiply}");
            }

            int comparison = first.CompareTo(second);
            SendMessage.Invoke($"Comparison: {comparison}");

            SendMessage.Invoke(" ");

            Currency usd = new Currency("US Dollar", "$", 1.0);
            Currency eur = new Currency("Euro", "euro", 0.83);

            SendMessage.Invoke("USD:");
            usd.Info();
            SendMessage.Invoke(" ");

            SendMessage.Invoke("EUR:");
            eur.Info();
            SendMessage.Invoke(" ");

            SendMessage.Invoke("USD + EUR:");
            Currency sum2 = usd.Add(eur);
            sum2.Info();
            SendMessage.Invoke(" ");

            SendMessage.Invoke("USD - EUR:");
            Currency difference2 = usd.Subtract(eur);
            difference2.Info();
            SendMessage.Invoke(" ");

            SendMessage.Invoke("USD / EUR:");
            double division2 = usd.Divide(eur);
            SendMessage.Invoke($"Result: {division2:F2}");
            SendMessage.Invoke(" ");

            Currency? dividedByNumber2 = null;
            Currency? multiply2 = null;

                dividedByNumber2 = usd.Divide(2);
                multiply2 = usd.Multiply(2);

                SendMessage.Invoke("USD / 2:");

                dividedByNumber2.Info();
                SendMessage.Invoke(" ");

                SendMessage.Invoke("USD * 2:");
                multiply2.Info();
                SendMessage.Invoke("");

            SendMessage.Invoke("USD vs. EUR:");
            int comparison2 = usd.CompareTo(eur);
            SendMessage.Invoke($"Comparison: {comparison2}");
        }
    }
}

public class InvalidOperationException : Exception // custom exception class that inherits from the base class Exception
{
    public InvalidOperationException() // constructor, but without parameters
    {
    }

    public InvalidOperationException(string message) : base(message) // message containss the info/description of the error
    {
    }

    // constructor with two parameters
    public InvalidOperationException(string message, Exception inner) : base(message, inner) // inner is a property of the exception base class
    {
    }
}

public abstract class FinancialEntity
{
    protected string name;
    protected double exchangeRate;

    public FinancialEntity(string name, double exchangeRate)
    {
        this.name = name;
        this.exchangeRate = exchangeRate;
    }

    public virtual void Info()
    {
        Console.WriteLine("Info");
    }
}

public class Money : FinancialEntity, IComparable<Money>
    
{
    private const int centsInEuro = 100;
    protected long euros;
    protected byte cents;

    public Money(long euros, byte cents) : base("Money", 1)
    {
        this.euros = euros;
        this.cents = cents;
    }

    public override void Info()
    {
        Console.WriteLine($"Money Info:");
        Console.WriteLine($"Euros: {euros}");
        Console.WriteLine($"Cents: {cents}");
        Console.WriteLine();
    }
    public override string ToString()
    {
        return $"{euros},{cents:D2}";
    }

    public Money Add(Money other)
    {
        long totalCents = (this.euros * centsInEuro + this.cents) + (other.euros * centsInEuro + other.cents);
        return new Money(totalCents / centsInEuro, (byte)(totalCents % centsInEuro));
    }

    public Money Subtract(Money other)
    {
        long totalCents = (this.euros * centsInEuro + this.cents) - (other.euros * centsInEuro + other.cents);
        return new Money(totalCents / centsInEuro, (byte)(totalCents % centsInEuro));
    }

    public double Divide(Money other)
    {
        double thisTotalCents = this.euros * centsInEuro + this.cents;
        double otherTotalCents = other.euros * centsInEuro + other.cents;
        return thisTotalCents / otherTotalCents;
    }

    public Money Divide(double divisor)
    {
        if (divisor == 0)
        {
            throw new InvalidOperationException("Division by zero is impossible");
        }

        long totalCents = (long)((this.euros * centsInEuro + this.cents) / divisor);
        return new Money(totalCents / centsInEuro, (byte)(totalCents % centsInEuro));
    }

    public Money Multiply(double multiplier)
    {
        if (multiplier == 0)
        {
            throw new InvalidOperationException("Multiplying by zero is impossible");
        }

        long totalCents = (long)((this.euros * centsInEuro + this.cents) * multiplier);
        return new Money(totalCents / centsInEuro, (byte)(totalCents % centsInEuro));
    }

    public int CompareTo(Money? other) // other doesn't match the nullable reference type
    {
        if (other == null)
        {
            return 1;
        }

        if (this.euros > other.euros)
        {
            return 1;
        }
        else if (this.euros < other.euros)
        {
            return -1;
        }
        return 0;
    }
}

public class Currency : FinancialEntity, IComparable<Currency>
{
    protected string symbol;

    public Currency(string name, string symbol, double exchangeRate) : base(name, exchangeRate)
    {
        this.symbol = symbol;
    }

    public override void Info()
    {
        Console.WriteLine($"Currency Info:");
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Symbol: {symbol}");
        Console.WriteLine($"Exchange Rate: {exchangeRate:F2}");
        Console.WriteLine();
    }

    public override string ToString()
    {
        return $"{symbol}{exchangeRate:F2} ({name})";
    }

    public Currency Add(Currency other)
    {
        double totalExchangeRate = this.exchangeRate + other.exchangeRate;
        return new Currency($"{this.name} + {other.name}", $"{this.symbol}+{other.symbol}", totalExchangeRate);
    }

    public Currency Subtract(Currency other)
    {
        double totalExchangeRate = this.exchangeRate - other.exchangeRate;
        return new Currency($"{this.name} - {other.name}", $"{this.symbol}-{other.symbol}", totalExchangeRate);
    }

    public double Divide(Currency other)
    {
        return this.exchangeRate / other.exchangeRate;
    }

    public Currency Divide(double divisor)
    {

        if (divisor == 0)
        {
            throw new InvalidOperationException("Division by zero is impossible");
        }

        double totalExchangeRate = this.exchangeRate / divisor;
        return new Currency($"{this.name} / {divisor:F2}", $"{this.symbol}/{divisor:F2}", totalExchangeRate);
    }

    public Currency Multiply(double multiplier)
    {

        if (multiplier == 0)
        {
            throw new InvalidOperationException("Multiplying by zero is impossible");
        }

        double totalExchangeRate = this.exchangeRate * multiplier;
        return new Currency($"{this.name} * {multiplier:F2}", $"{this.symbol}*{multiplier:F2}", totalExchangeRate);
    }

    public int CompareTo(Currency? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (this.exchangeRate > other.exchangeRate)
        {
            return 1;
        }
        else if (this.exchangeRate < other.exchangeRate)
        {
            return -1;
        }
        return 0;
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        string author = "Anna Shkaeva";
        DateTime startTime = DateTime.Now;

        Console.WriteLine($"Author: {author}");
        Console.WriteLine($"Program Start Time: {startTime}");

        try
        {
            Dispatcher dispatcher = new Dispatcher();
            dispatcher.SendMessage += Console.WriteLine;
            dispatcher.RunAllTheOperations();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Caught an InvalidOperationAmountException: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Caught an unknown exception: " + ex.Message);
        }
    }
}
