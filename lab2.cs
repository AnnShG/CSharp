using System;

public class Money : IComparable<Money>
{
    protected long euros; // are accesible to devide classes
    protected byte cents;

    public Money(long euros, byte cents)
    {
        this.euros = euros;
        this.cents = cents;
    }
    //    public string byComma()
    //    {
    //        return euro + "," + cents;
    //    }

    public override string ToString()
    {
        return $"{euros},{cents:D2}";
    }

    // Method to add two Money objects
    public Money Add(Money other)
    {
        long totalCents = (this.euros * 100 + this.cents) + (other.euros * 100 + other.cents);
        return new Money(totalCents / 100, (byte)(totalCents % 100));
    }

    // Method to subtract another Money object from the current Money object
    public Money Subtract(Money other)
    {
        long totalCents = (this.euros * 100 + this.cents) - (other.euros * 100 + other.cents);
        return new Money(totalCents / 100, (byte)(totalCents % 100));
    }

    // Method to divide the current Money object by another Money object
    public double Divide(Money other)
    {
        double thisTotalCents = this.euros * 100 + this.cents;
        double otherTotalCents = other.euros * 100 + other.cents;
        return thisTotalCents / otherTotalCents;
    }

    // Method to divide the current Money object by a fractional number
    public Money Divide(double divisor)
    {
        long totalCents = (long)((this.euros * 100 + this.cents) / divisor);
        return new Money(totalCents / 100, (byte)(totalCents % 100));
    }

    // Method to multiply the current Money object by a fractional number
    public Money Multiply(double multiplier)
    {
        long totalCents = (long)((this.euros * 100 + this.cents) * multiplier);
        return new Money(totalCents / 100, (byte)(totalCents % 100));
    }

    // Method to compare the current Money object with another Money object
    public int CompareTo(Money other)
    {
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

public class Currency : IComparable<Currency>
{
    private string name;
    private string symbol;
    private double exchangeRate;

    public Currency(string name, string symbol, double exchangeRate)
    {
        this.name = name;
        this.symbol = symbol;
        this.exchangeRate = exchangeRate;
    }

    public Currency(string symbol, double exchangeRate) : this("", symbol, exchangeRate)
    {
        this.name = $"{symbol} Currency";
    }

    public override string ToString()
    {
        return $"{symbol}{exchangeRate:F2} ({name})";
    }

    // Method to add two Currency objects
    public Currency Add(Currency other)
    {
        double totalExchangeRate = this.exchangeRate + other.exchangeRate;
        return new Currency($"{this.name} + {other.name}", $"{this.symbol}+{other.symbol}", totalExchangeRate);
    }

    // Method to subtract another Currency object from the current Currency object
    public Currency Subtract(Currency other)
    {
        double totalExchangeRate = this.exchangeRate - other.exchangeRate;
        return new Currency($"{this.name} - {other.name}", $"{this.symbol}-{other.symbol}", totalExchangeRate);
    }

    // Method to divide the current Currency object by another Currency object
    public double Divide(Currency other)
    {
        return this.exchangeRate / other.exchangeRate;
    }

    // Method to divide the current Currency object by a fractional number
    public Currency Divide(double divisor)
    {
        double totalExchangeRate = this.exchangeRate / divisor;
        return new Currency($"{this.name} / {divisor:F2}", $"{this.symbol}/{divisor:F2}", totalExchangeRate);
    }

    // Method to multiply the current Currency object by a fractional number
    public Currency Multiply(double multiplier)
    {
        double totalExchangeRate = this.exchangeRate * multiplier;
        return new Currency($"{this.name} * {multiplier:F2}", $"{this.symbol}*{multiplier:F2}", totalExchangeRate);
    }

    // Method to compare the current Currency object with another Currency object
    public int CompareTo(Currency other)
    {
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

    // Polymorphic method to display currency information
    public void DisplayInfo()
    {
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Symbol: {symbol}");
        Console.WriteLine($"Exchange Rate: {exchangeRate:F2}");
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
        Currency eur = new Currency("Euro", "€", 0.83);

        // Display information about the currencies
        Console.WriteLine("USD:");
        usd.DisplayInfo();
        Console.WriteLine();

        Console.WriteLine("EUR:");
        eur.DisplayInfo();
        Console.WriteLine();

        // Perform arithmetic operations on the currencies
        Console.WriteLine("USD + EUR:");
        Currency sum2 = usd.Add(eur);
        sum2.DisplayInfo();
        Console.WriteLine();

        Console.WriteLine("USD - EUR:");
        Currency difference2 = usd.Subtract(eur);
        difference2.DisplayInfo();
        Console.WriteLine();

        Console.WriteLine("USD / EUR:");
        double division2 = usd.Divide(eur);
        Console.WriteLine($"Result: {division2:F2}");
        Console.WriteLine();

        Console.WriteLine("USD / 2:");
        Currency dividedByNumber2 = usd.Divide(2);
        dividedByNumber2.DisplayInfo();
        Console.WriteLine();

        Console.WriteLine("USD * 2:");
        Currency multiply2 = usd.Multiply(2);
        multiply2.DisplayInfo();
        Console.WriteLine();

        // Compare the currencies
        Console.WriteLine("USD vs. EUR:");
        int comparison2 = usd.CompareTo(eur);
        Console.WriteLine($"Comparison: {comparison2}");
    }
}

