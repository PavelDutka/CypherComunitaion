using (Aes myAes = Aes.Create())
{
    foreach (var x in myAes.Key)
    {
        Console.Write(String.Format("{0:x2} ", x));
    }
    foreach (var x in myAes.IV)
    {
        Console.Write(String.Format("{0:x2} ", x));
    }
}