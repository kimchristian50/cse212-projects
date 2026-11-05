using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add three items to the queue with different priorities and 
    // make sure they are in the correct order in the queue.
    // Expected Result: [low (Pri:1), medium (Pri:2), high (Pri:3)] should match the priorityQueue.ToString() result
    // Defect(s) Found: none
    public void TestPriorityQueue_1()
    {
        var high = new PriorityItem("high", 3);
        var medium = new PriorityItem("medium", 2);
        var low = new PriorityItem("low", 1);

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue(low.Value, low.Priority);
        priorityQueue.Enqueue(medium.Value, medium.Priority);
        priorityQueue.Enqueue(high.Value, high.Priority);

        Assert.AreEqual("[low (Pri:1), medium (Pri:2), high (Pri:3)]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Add three items to the queue with different priorities and dequeue 
    // the item with the highest priority.
    // Expected Result: "high" should be returned from the Dequeue() method
    // Defect(s) Found: The for loop in Dequeue was not iterating through the whole object - I changed it to 
    // for (int index = 1; index < _queue.Count; index++) and this works.
    public void TestPriorityQueue_2()
    {
        var high = new PriorityItem("high", 3);
        var medium = new PriorityItem("medium", 2);
        var low = new PriorityItem("low", 1);

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue(low.Value, low.Priority);
        priorityQueue.Enqueue(medium.Value, medium.Priority);
        priorityQueue.Enqueue(high.Value, high.Priority);

        Assert.AreEqual("high", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add four items to the queue with three different priorities and dequeue 
    // the item with the highest priority that's first in the queue.
    // Expected Result: "high" (the first high priority item) should be returned from the Dequeue() method
    // Defect(s) Found: the if statement in Dequeue was using >= instead of > so that an 
    // equal priority would supercede the previous equal priority.  
    // I changed it to > and this works.
    public void TestPriorityQueue_3()
    {
        var high = new PriorityItem("high", 3);
        var medium = new PriorityItem("medium", 2);
        var low = new PriorityItem("low", 1);
        var high2 = new PriorityItem("high2", 3);

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue(low.Value, low.Priority);
        priorityQueue.Enqueue(medium.Value, medium.Priority);
        priorityQueue.Enqueue(high.Value, high.Priority);
        priorityQueue.Enqueue(high2.Value, high2.Priority);

        var value = priorityQueue.Dequeue();
        Debug.WriteLine($"Dequeue returned: {value}");

        Assert.AreEqual("high", value);
    }

    [TestMethod]
    // Scenario: Try to dequeue the first item from an empty queue
    // Expected Result: InvalidOperationException should be thrown with the message
    // "The queue is empty."
    // Defect(s) Found: none
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                  string.Format("Unexpected exception of type {0} caught: {1}",
                                 e.GetType(), e.Message)
             );
        }
    }

    [TestMethod]
    // Scenario: Added 6 items to the queue with three different priorities.
    // Dequeued all 6 items and verified that they were returned in the correct order and
    // that the queue was empty at the end.
    // Expected Result: high, high2, medium, medium2, low, low2 should be returned in that order.
    // The queue should be empty at the end.
    // Defect(s) Found: in Dequeue, the highPriorityIndex item wasn't being removed from the queue.
    // added RemoveAt so that the item is actually removed from the queue.
    public void TestPriorityQueue_5()
    {
        var high = new PriorityItem("high", 3);
        var medium = new PriorityItem("medium", 2);
        var low = new PriorityItem("low", 1);
        var high2 = new PriorityItem("high2", 3);
        var medium2 = new PriorityItem("medium2", 2);
        var low2 = new PriorityItem("low2", 1);

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue(low.Value, low.Priority);
        priorityQueue.Enqueue(medium.Value, medium.Priority);
        priorityQueue.Enqueue(high.Value, high.Priority);
        priorityQueue.Enqueue(high2.Value, high2.Priority);
        priorityQueue.Enqueue(medium2.Value, medium2.Priority);
        priorityQueue.Enqueue(low2.Value, low2.Priority);

        PriorityItem[] expectedResult = [high, high2, medium, medium2, low, low2];

        for (int i = 0; i < 6; i++)
        {
            var value = priorityQueue.Dequeue();
            Debug.WriteLine($"Dequeue returned: {value}");
            Assert.AreEqual(expectedResult[i].Value, value);
        }

        Debug.WriteLine($"Queue after all dequeues: {priorityQueue.ToString()}");
        Assert.AreEqual("[]", priorityQueue.ToString());
    }
}