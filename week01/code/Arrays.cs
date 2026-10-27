public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Make an empty array to fill with the multiples
        // A loop needs to run length times
        // Each time it runs, the number should be multiplied by i and added to the array
        // 

        var newList = new double[length];

        for (int i = 1; i <= length; i++)
        {
            newList[i - 1] = number * i;
        }

        return newList;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // Make a new list to hold the range being removed, which is starts at index Count - amount and is amount items long
        // using GetRange, copy the last amount items, starting at index Count - amount
        // using RemoveRange, remove that range from the end of data
        // using InsertRange, add that range from newData to the beginning of data

        List<int> newData = data.GetRange(data.Count - amount, amount);
        data.RemoveRange(data.Count - amount, amount);
        data.InsertRange(0, newData);
    }
}

