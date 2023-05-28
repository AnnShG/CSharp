/*
one user class is used, which was created in one of the previous laboratory works. 
The class must contain at least three properties,  the values of which will be displayed 
in the visual controls.
The program is created as a Windows Forms Application. The program enters object property values 
(at least one text and one integer) using visual controls that are associated with a custom class object. 
Then, after pressing the button, this object is recorded
with a list of objects associated with the visual control of the DataGridView, where it is displayed.
Initially, two initialized objects of a custom class should be programmatically entered into the list of
objects.
*/

using System.Collections;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Security;

namespace lab6_oop
{
    public partial class Form1 : Form
    {
        public HelicopterList helicopters = new HelicopterList();
        // Data class
        public class Helicopter
        {
            public string Model { get; set; }
            public string UsageOfHelicopter { get; set; }
            public int NumberOfPassengers { get; set; }
            public Helicopter (string model, string usageOfHelicopter, int numberOfPassengers)
            {
                Model = model;
                UsageOfHelicopter = usageOfHelicopter;
                NumberOfPassengers = numberOfPassengers;
            }
        }

        // Collection (try using the CollectionBase class instead of ArrayList)
        public class HelicopterList : ArrayList, ITypedList
        {
            // We announce that Helicopter properties will be used
            public PropertyDescriptorCollection
                GetItemProperties(PropertyDescriptor[] listAccessors)
            {
                return TypeDescriptor.GetProperties(typeof(Helicopter));
            }
            // Use the same name (this method is required for ITypedList)
            public string GetListName(PropertyDescriptor[] listAccessors)
            {
                return "HelicopterList";
            }
        }
        public Form1()
        {
            InitializeComponent();

            helicopters.Add(new Helicopter("Mil Mi-24", "Military", 8));
            helicopters.Add(new Helicopter("Bell LongRanger 206", "Private", 5));

            button1.Click += new EventHandler(addButton_Click);

            dataGridView1.DataSource = helicopters;

        }

        private void addButton_Click(object? sender, EventArgs e)
        {
            Helicopter copter = new Helicopter(textBox1.Text, textBox2.Text, int.Parse(textBox3.Text));
            helicopters.Add(copter);
            // When programmatically adding items to a collection,
            // update the components associated with this collection.
            ((CurrencyManager)dataGridView1.BindingContext[helicopters]).Refresh();
        }

    }
}