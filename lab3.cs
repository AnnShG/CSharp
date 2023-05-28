/*
 Inheritance and abstraction
1. create a class that would be their (MyCopter and MyAirplane classes) ancestor and contain members
related to both classes.
2. In the new class, create at least one virtual method Info(), which would be rewritten in
descendant classes. In connection with the advent of the Info() method, you can rewrite
the main Main() method in a more strict OO style (remove loops, etc.). At the end of the
program, it is proposed to ensure that both implementations of the Info() method for the
new base class and at least one old derived class are available in the program (get the
output of the results of each implementation).
◼ The program should demonstrate the performance of all new methods (output data to the console).
 */

using System;

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
        long totalCents = (this.euros * 100 + this.cents) + (other.euros * 100 + other.cents);
        return new Money(totalCents / 100, (byte)(totalCents % 100));
    }

    public Money Subtract(Money other)
    {
        long totalCents = (this.euros * 100 + this.cents) - (other.euros * 100 + other.cents);
        return new Money(totalCents / 100, (byte)(totalCents % 100));
    }

    public double Divide(Money other)
    {
        double thisTotalCents = this.euros * 100 + this.cents;
        double otherTotalCents = other.euros * 100 + other.cents;
        return thisTotalCents / otherTotalCents;
    }

    public Money Divide(double divisor)
    {
        long totalCents = (long)((this.euros * 100 + this.cents) / divisor);
        return new Money(totalCents / 100, (byte)(totalCents % 100));
    }

    public Money Multiply(double multiplier)
    {
        long totalCents = (long)((this.euros * 100 + this.cents) * multiplier);
        return new Money(totalCents / 100, (byte)(totalCents % 100));
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
        double totalExchangeRate = this.exchangeRate / divisor;
        return new Currency($"{this.name} / {divisor:F2}", $"{this.symbol}/{divisor:F2}", totalExchangeRate);
    }

    public Currency Multiply(double multiplier)
    {
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

        Money first = new Money(30, 25);
        Money second = new Money(40, 50);

        Console.WriteLine($"First: {first}");
        Console.WriteLine($"Second: {second}");
        Console.WriteLine();

        Money money = new Money(30, 25);
        Currency currency = new Currency("US Dollar", "$", 1.0);

        first.Info();
        second.Info();

        Money sum = first.Add(second);
        Console.WriteLine($"Sum: {sum}");

        Money difference = first.Subtract(second);
        Console.WriteLine($"Difference: {difference}");

        double division = first.Divide(second);
        Console.WriteLine($"Division: {division}");

        Money dividedByNumber = first.Divide(0.5);
        Console.WriteLine($"First divided by 0.5: {dividedByNumber}");

        Money multiply = first.Multiply(0.5);
        Console.WriteLine($"First multiplied by 0.5: {multiply}");

        int comparison = first.CompareTo(second);
        Console.WriteLine($"Comparison: {comparison}");

        Console.WriteLine();
        Console.WriteLine();

        // Create two Currency objects
        Currency usd = new Currency("US Dollar", "$", 1.0);
        Currency eur = new Currency("Euro", "euro", 0.83);

        // Display information about the currencies

        Console.WriteLine("USD:");
        usd.Info();
        Console.WriteLine();

        Console.WriteLine("EUR:");
        eur.Info();
        Console.WriteLine();

        // Perform arithmetic operations on the currencies
        Console.WriteLine("USD + EUR:");
        Currency sum2 = usd.Add(eur);
        sum2.Info();
        Console.WriteLine();

        Console.WriteLine("USD - EUR:");
        Currency difference2 = usd.Subtract(eur);
        difference2.Info();
        Console.WriteLine();

        Console.WriteLine("USD / EUR:");
        double division2 = usd.Divide(eur);
        Console.WriteLine($"Result: {division2:F2}");
        Console.WriteLine();

        Console.WriteLine("USD / 2:");
        Currency dividedByNumber2 = usd.Divide(2);
        dividedByNumber2.Info();
        Console.WriteLine();

        Console.WriteLine("USD * 2:");
        Currency multiply2 = usd.Multiply(2);
        multiply2.Info();
        Console.WriteLine();

        // Compare the currencies
        Console.WriteLine("USD vs. EUR:");
        int comparison2 = usd.CompareTo(eur);
        Console.WriteLine($"Comparison: {comparison2}");
    }
}

