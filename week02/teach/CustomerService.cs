/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService
{
    public static void Run()
    {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: specifying the maximum queue size of 10, 
        // Expected Result: result should be 10
        Console.WriteLine("Test 1");
        var cs = new CustomerService(10);
        Console.WriteLine(cs._maxSize);

        // Defect(s) Found: none

        Console.WriteLine("=================");

        // Test 2
        // Scenario: specifying the maximum queue size of -1 
        // Expected Result: result should be 10
        Console.WriteLine("Test 2");
        var cs2 = new CustomerService(-1);
        Console.WriteLine(cs._maxSize);

        // Defect(s) Found: none

        Console.WriteLine("=================");

        // Test 3
        // Scenario: specifying the maximum queue size of 1 
        // Expected Result: result should be 1
        Console.WriteLine("Test 3");
        var cs3 = new CustomerService(1);
        Console.WriteLine(cs3._maxSize);

        // Defect(s) Found: none

        Console.WriteLine("=================");

        // Test 4
        // Scenario: add new customer to the queue and then serve the customer, checking the queue count before and after each operation
        // Expected Result: customer is added, queue goes to 1, customer is served, queue goes back to 0
        Console.WriteLine("Test 4");
        var cs4 = new CustomerService(1);
        Console.WriteLine($"queue count: {cs4._queue.Count}");
        cs4.AddNewCustomer();
        Console.WriteLine($"queue count: {cs4._queue.Count}");
        cs4.ServeCustomer();
        Console.WriteLine($"queue count: {cs4._queue.Count}");

        // Defect(s) Found: in ServeCustomer, need to save the data before removing the customer from the queue

        Console.WriteLine("=================");

        // Test 5
        // Scenario: add new customer to the queue when the queue is already at maximum size, checking the current queue count before and after each operation
        // Expected Result: customer is added, queue goes to 1, attempting to add another customer should not increase the queue count, error "Maximum Number of Customers in Queue." is displayed
        Console.WriteLine("Test 5");
        var cs5 = new CustomerService(1);
        Console.WriteLine($"queue count: {cs5._queue.Count}");
        Console.WriteLine($"max size: {cs5._maxSize}");
        cs5.AddNewCustomer();
        Console.WriteLine($"queue count: {cs5._queue.Count}");
        cs5.AddNewCustomer();
        Console.WriteLine($"queue count: {cs5._queue.Count}");

        // Defect(s) Found: in AddNewCustomer, need to check for >= instead of > to prevent adding a customer when the queue is full

        Console.WriteLine("=================");

        // Test 6
        // Scenario: try to serve a customer when the queue is empty, checking the current queue count before and after each operation
        // Expected Result: error message is displayed "No Customers in Queue", queue count remains at 0
        Console.WriteLine("Test 6");
        var cs6 = new CustomerService(1);
        cs6.AddNewCustomer();
        Console.WriteLine($"queue count: {cs6._queue.Count}");
        cs6.ServeCustomer();
        Console.WriteLine($"queue count: {cs6._queue.Count}");
        cs6.ServeCustomer();

        // Defect(s) Found: in ServeCustomer, need to check the queue count before trying to serve a customer to avoid an exception

        Console.WriteLine("=================");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize)
    {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer
    {
        public Customer(string name, string accountId, string problem)
        {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString()
        {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer()
    {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) // defect 2 - needs to be >= instead of > to prevent adding a customer when the queue is full
        {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }
        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer()
    {
        if (_queue.Count == 0)              // defect 3 - need to check the queue count before trying to serve a customer to avoid an exception
        {
            Console.WriteLine("No Customers in Queue.");
            return;
        }

        var customer = _queue[0]; // defect 1 - need to save the data before removing the customer from the queue
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString()
    {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}